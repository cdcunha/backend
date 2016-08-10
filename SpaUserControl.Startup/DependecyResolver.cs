using Microsoft.Practices.Unity;
using SpaUserControl.Domain;
using SpaUserControl.Domain.Contracts.Repositories;
using SpaUserControl.Infraestructure.Data;
using SpaUserControl.Infraestructure.Repositories;

namespace SpaUserControl.Startup
{
    public class DependecyResolver
    {
        public static void Resolve(UnityContainer container)
        {
            container.RegisterType<AppDataContext, AppDataContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
            //container.RegisterType<IUserService, IUserService>(new HierarchicalLifetimeManager());
            //container.RegisterType<User, User>(new HierarchicalLifetimeManager());
        }
    }
}
