using DryIoc;
using Saruman.Helpers.Extensions;

namespace Saruman
{
    public abstract class Initializer
    {
        public abstract void RegisterPlatformDependencies(IContainer container);

        public IContainer GetContainer()
        {
            var container = new Container();
            container.RegisterInstance(container);
            container.RegisterSingleton<App>();
            RegisterPlatformDependencies(container);
            return container;
        }
    }
}
