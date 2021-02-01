using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using Saruman.Interfaces.Services;
using Saruman.Models;
using Xamarin.Forms;
using static Saruman.Helpers.Enums.Priority;

namespace Saruman.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private readonly INavigationService _navivationService;

        public IEnumerable<Incident> Incidents => new ObservableCollection<Incident>
        {
            new Incident
            {
                Priority = P1,
                IncidentId = "INC1278923",
                RegisteredAt = DateTime.Now.AddMinutes(-2),
                Description = "Disco cheio de servidor srv-1234.",
                Title = "Disco srv-1234",
                Squad = "Rico",
                Ack = true
            },
            new Incident
            {
                Priority = P3,
                IncidentId = "INC8798923",
                RegisteredAt = DateTime.Now.AddMinutes(-3),
                Description = "Servidor não responde srv-345",
                Title = "Servidor srv-345",
                Squad = "Clear",
                Ack = true
            },
            new Incident
            {
                Priority = P2,
                IncidentId = "INC2348923",
                RegisteredAt = DateTime.Now.AddMinutes(-10),
                Description = "Node offline",
                Title = "Node",
                Squad = "Bull"
            },
            new Incident
            {
                Priority = P2,
                IncidentId = "INC1274523",
                RegisteredAt = DateTime.Now.AddHours(-1),
                Description = "Datacenter sem luz.",
                Title = "DC energia",
                Squad = "Rico",
                Ack = true
            },
            new Incident
            {
                Priority = P4,
                IncidentId = "INC1245923",
                RegisteredAt = DateTime.Now.AddDays(-3),
                Description = "Database lento.",
                Title = "Db lento",
                Squad = "Bull"
            },
            new Incident
            {
                Priority = Info,
                IncidentId = "INC1963923",
                RegisteredAt = DateTime.Now.AddMinutes(-2),
                Description = "Database lento.",
                Title = "Db lento",
                Squad = "Clear",
                Ack = true
            },
            new Incident
            {
                Priority = P4,
                IncidentId = "INC4268923",
                RegisteredAt = DateTime.Now.AddMinutes(-2),
                Description = "Disco cheio de servidor srv-678.",
                Title = "Disco srv-678",
                Squad = "Rico",
                Ack = true
            },
            new Incident
            {
                Priority = P1,
                IncidentId = "INC1278923",
                RegisteredAt = DateTime.Now.AddMinutes(-2),
                Description = "Disco cheio de servidor srv-1234.",
                Title = "Disco srv-1234",
                Squad = "Rico"
            },
            new Incident
            {
                Priority = P3,
                IncidentId = "INC8798923",
                RegisteredAt = DateTime.Now.AddMinutes(-3),
                Description = "Servidor não responde srv-345",
                Title = "Servidor srv-345",
                Squad = "Clear"
            },
            new Incident
            {
                Priority = P2,
                IncidentId = "INC2348923",
                RegisteredAt = DateTime.Now.AddMinutes(-10),
                Description = "Node offline",
                Title = "Node",
                Squad = "Bull"
            },
            new Incident
            {
                Priority = P2,
                IncidentId = "INC1274523",
                RegisteredAt = DateTime.Now.AddHours(-1),
                Description = "Datacenter sem luz.",
                Title = "DC energia",
                Squad = "Rico"
            },
            new Incident
            {
                Priority = P4,
                IncidentId = "INC1245923",
                RegisteredAt = DateTime.Now.AddDays(-3),
                Description = "Database lento.",
                Title = "Db lento",
                Squad = "Bull",
                Ack = true
            },
            new Incident
            {
                Priority = Info,
                IncidentId = "INC1963923",
                RegisteredAt = DateTime.Now.AddMinutes(-2),
                Description = "Database lento.",
                Title = "Db lento",
                Squad = "Clear"
            },
            new Incident
            {
                Priority = P4,
                IncidentId = "INC4268923",
                RegisteredAt = DateTime.Now.AddMinutes(-2),
                Description = "Disco cheio de servidor srv-678.",
                Title = "Disco srv-678",
                Squad = "Rico"
            },
            new Incident
            {
                Priority = P1,
                IncidentId = "INC1278923",
                RegisteredAt = DateTime.Now.AddMinutes(-2),
                Description = "Disco cheio de servidor srv-1234.",
                Title = "Disco srv-1234",
                Squad = "Rico",
                Ack = true
            },
            new Incident
            {
                Priority = P3,
                IncidentId = "INC8798923",
                RegisteredAt = DateTime.Now.AddMinutes(-3),
                Description = "Servidor não responde srv-345",
                Title = "Servidor srv-345",
                Squad = "Clear"
            },
            new Incident
            {
                Priority = P2,
                IncidentId = "INC2348923",
                RegisteredAt = DateTime.Now.AddMinutes(-10),
                Description = "Node offline",
                Title = "Node",
                Squad = "Bull",
                Ack = true
            },
            new Incident
            {
                Priority = P2,
                IncidentId = "INC1274523",
                RegisteredAt = DateTime.Now.AddHours(-1),
                Description = "Datacenter sem luz.",
                Title = "DC energia",
                Squad = "Rico"
            },
            new Incident
            {
                Priority = P4,
                IncidentId = "INC1245923",
                RegisteredAt = DateTime.Now.AddDays(-3),
                Description = "Database lento.",
                Title = "Db lento",
                Squad = "Bull"
            },new Incident
            {
                Priority = Info,
                IncidentId = "INC1963923",
                RegisteredAt = DateTime.Now.AddMinutes(-2),
                Description = "Database lento.",
                Title = "Db lento",
                Squad = "Clear",
                Ack = true
            },
            new Incident
            {
                Priority = P4,
                IncidentId = "INC4268923",
                RegisteredAt = DateTime.Now.AddMinutes(-2),
                Description = "Disco cheio de servidor srv-678.",
                Title = "Disco srv-678",
                Squad = "Rico"
            },
        };

        public IEnumerable<PendingAction> Actions => new ObservableCollection<PendingAction>
        {
            new PendingAction
            {
                Title = "WAR ROOM",
                Description = "Abre war room com o squad",
                Done = false,
                Priority = P1
            },
            new PendingAction
            {
                Title = "WAR ROOM",
                Description = "Abre war room com o squad Rico",
                Done = false,
                Priority = P3
            },
            new PendingAction
            {
                Title = "REINICIA SERVIDOR",
                Description = "Reinicia servidor src-1234",
                Done = false,
                Priority = Info
            },
            new PendingAction
            {
                Title = "RODA DB SCRIPT",
                Description = "Roda script X no db-4567",
                Done = false,
                Priority = P4
            },
            new PendingAction
            {
                Title = "AVISA STAKEHOLDERS",
                Description = "Avisar stakeholders do problema",
                Done = false,
                Priority = P2
            },
            new PendingAction
            {
                Title = "WAR ROOM",
                Description = "Abre war room com o squad",
                Done = false,
                Priority = P1
            },
            new PendingAction
            {
                Title = "MAPEAR GMUD",
                Description = "Mapeia GMUD que pode ter causado incidente",
                Done = false,
                Priority = P1
            },
            new PendingAction
            {
                Title = "WAR ROOM",
                Description = "Abre war room com o squad",
                Done = false,
                Priority = P1
            },
            new PendingAction
            {
                Title = "WAR ROOM",
                Description = "Abre war room com o squad Rico",
                Done = false,
                Priority = P3
            },
            new PendingAction
            {
                Title = "REINICIA SERVIDOR",
                Description = "Reinicia servidor src-1234",
                Done = false,
                Priority = Info
            },
            new PendingAction
            {
                Title = "RODA DB SCRIPT",
                Description = "Roda script X no db-4567",
                Done = false,
                Priority = P4
            },
            new PendingAction
            {
                Title = "AVISA STAKEHOLDERS",
                Description = "Avisar stakeholders do problema",
                Done = false,
                Priority = P2
            },
            new PendingAction
            {
                Title = "WAR ROOM",
                Description = "Abre war room com o squad",
                Done = false,
                Priority = P1
            },
            new PendingAction
            {
                Title = "MAPEAR GMUD",
                Description = "Mapeia GMUD que pode ter causado incidente",
                Done = false,
                Priority = P1
            },new PendingAction
            {
                Title = "WAR ROOM",
                Description = "Abre war room com o squad",
                Done = false,
                Priority = P1
            },
            new PendingAction
            {
                Title = "WAR ROOM",
                Description = "Abre war room com o squad Rico",
                Done = false,
                Priority = P3
            },
            new PendingAction
            {
                Title = "REINICIA SERVIDOR",
                Description = "Reinicia servidor src-1234",
                Done = false,
                Priority = Info
            },
            new PendingAction
            {
                Title = "RODA DB SCRIPT",
                Description = "Roda script X no db-4567",
                Done = false,
                Priority = P4
            },
            new PendingAction
            {
                Title = "AVISA STAKEHOLDERS",
                Description = "Avisar stakeholders do problema",
                Done = false,
                Priority = P2
            },
            new PendingAction
            {
                Title = "WAR ROOM",
                Description = "Abre war room com o squad",
                Done = false,
                Priority = P1
            },
            new PendingAction
            {
                Title = "MAPEAR GMUD",
                Description = "Mapeia GMUD que pode ter causado incidente",
                Done = false,
                Priority = P1
            },
        };

        public ICommand SwitchViewCommand => new Command(SwitchView);

        public IAsyncCommand<Incident> IncidentClickedCommand => new AsyncCommand<Incident>(OpenIncident);

        public bool ShowIncidents { get; set; } = true;

        public string ListTitle { get; set; } = "INCIDENTES ABERTOS";

        public DashboardViewModel(INavigationService navivationService)
        {
            _navivationService = navivationService;
        }

        public override void Init(object parameter)
        {
            base.Init(parameter);

            if (parameter is Incident incident)
                OpenIncident(incident).SafeFireAndForget();
        }

        private void SwitchView(object parameter)
        {
            ShowIncidents = !ShowIncidents;
            ListTitle = ShowIncidents ? "INCIDENTES ABERTOS" : "AÇÕES ABERTOS";
            OnPropertyChanged(nameof(ShowIncidents));
            OnPropertyChanged(nameof(ListTitle));
        }

        private async Task OpenIncident(Incident incident)
            => await _navivationService.NavigateToAsync<IncidentViewModel>(incident, true).ConfigureAwait(false);
    }
}
