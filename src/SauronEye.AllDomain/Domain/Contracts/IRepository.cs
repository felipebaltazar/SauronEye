namespace SauronEye.AllDomain.Domain.Contracts
{
    public interface IRepository<TClass> where TClass: class
    {
        public void Save(TClass item);
        public void Get(string id);
    }
}
