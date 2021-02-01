using AsyncAwaitBestPractices;
using DryIoc;
using Saruman.Helpers.Extensions;
using Saruman.Interfaces.Services;
using Saruman.Models;
using Saruman.Pages;
using Saruman.Services;
using Saruman.ViewModels;
using Xamarin.Forms;

namespace Saruman
{
    public partial class App : Application
    {
        private IContainer _container;

        public App(IContainer container, Incident incident = null)
        {
            InitializeComponent();

            _container = container;

            RegisterServices();
            RegisterNavigation();

            if (incident is null)
                _container.Resolve<INavigationService>().SetRootPage<LoginViewModel>();
            else
                _container.Resolve<IDeeplinkService>().HandleIncidentAsync(incident).SafeFireAndForget();
        }

        private void RegisterServices()
        {
            _container.RegisterSingleton<INavigationService, NavigationService>();
            _container.RegisterSingleton<IAuthService, AuthService>();
            _container.RegisterSingleton<IDeeplinkService, DeeplinkService>();
        }

        private void RegisterNavigation()
        {
            _container.RegisterNavigation<LoginPage, LoginViewModel>();
            _container.RegisterNavigation<DashboardPage, DashboardViewModel>();
            _container.RegisterNavigation<IncidentPage, IncidentViewModel>();
        }

        public T Resolve<T>()
            => _container.Resolve<T>();

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
