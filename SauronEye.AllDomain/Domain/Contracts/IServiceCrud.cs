using System.Threading.Tasks;

namespace SauronEye.AllDomain.Domain.Contracts
{
    public interface IServiceCrud<TClass> where TClass: class
    {
        Task Save(TClass item);
    }
}
