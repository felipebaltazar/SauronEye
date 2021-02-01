using SauronEye.AllDomain.Domain.Dtos;
using SauronEye.AllDomain.Domain.Entities;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SauronEye.AllDomain.Domain.Contracts.Services
{
    public interface IServiceProblem
    {
        void GetAndSaveProblems(ConcurrentDictionary<string, LoggedUser> loggedUsers);
        Task<List<Problem>> GetProblems(string token);
    }
}