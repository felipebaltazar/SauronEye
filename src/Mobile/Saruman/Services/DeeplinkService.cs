using System.Threading.Tasks;
using Saruman.Interfaces.Services;
using Saruman.Models;
using Saruman.ViewModels;

namespace Saruman.Services
{
    public class DeeplinkService : IDeeplinkService
    {
        private readonly INavigationService _navigationService;
        private readonly IAuthService _authService;

        public DeeplinkService(INavigationService navigationService, IAuthService authService)
        {
            _navigationService = navigationService;
            _authService = authService;
        }

        public async Task HandleIncidentAsync(Incident incident)
        {
            if (!_authService.IsAuthenticated)
                _navigationService.SetRootPage<LoginViewModel>(incident);
            else
                await _navigationService.NavigateToAsync<IncidentViewModel>(incident, true).ConfigureAwait(false);
        }
    }
}
