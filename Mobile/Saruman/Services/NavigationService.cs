using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DryIoc;
using Saruman.Interfaces.Services;
using Saruman.ViewModels;
using Xamarin.Forms;

namespace Saruman.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IContainer _container;
        private readonly IDictionary<Type, Type> _mappings;

        private INavigation Navigation => Application.Current.MainPage.Navigation;

        public BaseViewModel CurrentPageModel => GetCurrentPage();

        public NavigationService(IContainer container)
        {
            _container = container;

            _mappings = new Dictionary<Type, Type>();
        }

        public async Task NavigateToAsync<T>(object parameter = null, bool modal = false) where T : BaseViewModel
        {
            Page page = CreatePage<T>(parameter);

            if (modal)
                await Navigation.PushModalAsync(page).ConfigureAwait(false);
            else
                await Navigation.PushAsync(page).ConfigureAwait(false);
        }

        public void SetRootPage<T>(object parameter = null) where T : BaseViewModel
        {
            Page page = CreatePage<T>(parameter);

            Application.Current.MainPage = new NavigationPage(page)
            {
                BarTextColor = Color.White
            };
        }

        public async Task NavigateFromAsync(bool modal = false)
        {
            if (modal)
                await Navigation.PopModalAsync().ConfigureAwait(false);
            else
                await Navigation.PopAsync().ConfigureAwait(false);
        }

        public void MapPageViewModel<TPage, TViewModel>()
            where TPage : Page
            where TViewModel : BaseViewModel
        {
            if (!_mappings.ContainsKey(typeof(TPage)))
                _mappings.Add(typeof(TViewModel), typeof(TPage));
            else
                _mappings[typeof(TViewModel)] = typeof(TPage);
        }

        private Page CreatePage<T>(object parameter) where T : BaseViewModel
        {
            var vmType = typeof(T);

            if (!_mappings.ContainsKey(vmType))
                throw new ArgumentException($"Tipo {vmType.Name} não registrado para navegação");

            var pageType = _mappings[vmType];

            var page = _container.Resolve(pageType) as Page;
            var vm = _container.Resolve(vmType) as BaseViewModel;
            vm.Init(parameter);
            page.BindingContext = vm;
            return page;
        }

        private BaseViewModel GetCurrentPage()
        {
            return Navigation.ModalStack.Any()
                ? Navigation.ModalStack.Last().BindingContext as BaseViewModel
                : Navigation.NavigationStack.Last().BindingContext as BaseViewModel;
        }
    }
}
