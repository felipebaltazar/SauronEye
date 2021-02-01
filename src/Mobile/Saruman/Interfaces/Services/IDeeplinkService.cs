using System.Threading.Tasks;
using Saruman.Models;

namespace Saruman.Interfaces.Services
{
    public interface IDeeplinkService
    {
        Task HandleIncidentAsync(Incident incident);
    }
}