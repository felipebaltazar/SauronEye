using DryIoc;
using Saruman.Interfaces.Services;
using Saruman.ViewModels;
using Xamarin.Forms;

namespace Saruman.Helpers.Extensions
{
    public static class ContainerExtensions
    {
        public static void RegisterSingleton<T, TImp>(this IContainer container)
            where TImp : T
            => container.Register<T, TImp>(Reuse.Singleton);

        public static void RegisterSingleton<T>(this IContainer container)
            => container.Register<T>(Reuse.Singleton);

        public static void RegisterNavigation<TPage, TViewModel>(this IContainer container)
            where TPage : Page
            where TViewModel : BaseViewModel
        {
            var navigationService = container.Resolve<INavigationService>();
            navigationService.MapPageViewModel<TPage, TViewModel>();
            container.Register<TPage>();
            container.Register<TViewModel>();
        }
    }
}
