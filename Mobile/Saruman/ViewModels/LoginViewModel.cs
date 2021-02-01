using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AsyncAwaitBestPractices.MVVM;
using Saruman.Helpers;
using Saruman.Interfaces.Services;
using Saruman.Models;
using Xamarin.Forms;

namespace Saruman.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IAuthService _loginService;

        private Incident _incident = null;

        public bool IsDeepLink { get; set; }

        public IAsyncCommand LoginCommand => new AsyncCommand(Login);

        public LoginViewModel(INavigationService navigationService, IAuthService loginService)
        {
            _navigationService = navigationService;
            _loginService = loginService;
        }

        public override void Init(object parameter)
        {
            base.Init(parameter);

            if (parameter is Incident incident)
            {
                _incident = incident;
                IsDeepLink = true;
            }
        }

        private async Task Login()
        {
            using (new Busy(this))
            {
                if (await _loginService.LoginAsync("username", "password"))
                {
                    await _navigationService.NavigateToAsync<DashboardViewModel>(_incident).ConfigureAwait(false);
                }
            }
        }
    }
}
