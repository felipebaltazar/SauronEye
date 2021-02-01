using System;
using System.Threading.Tasks;
using AsyncAwaitBestPractices.MVVM;
using Saruman.Interfaces.Services;
using Saruman.Models;

namespace Saruman.ViewModels
{
    public class IncidentViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public Incident Incident { get; set; }

        public IAsyncCommand CloseCommand => new AsyncCommand(Close);

        public IncidentViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Init(object parameter)
        {
            base.Init(parameter);

            if (parameter is Incident incident)
                Incident = incident;
        }

        private Task Close()
            => _navigationService.NavigateFromAsync(true);
    }
}

