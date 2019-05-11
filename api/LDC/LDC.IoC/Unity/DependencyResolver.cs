using Microsoft.Practices.Unity;
using prmToolkit.NotificationPattern;
using System.Data.Entity;
using LDC.Domain.Interfaces.Repositories;
using LDC.Domain.Interfaces.Repositories.Base;
using LDC.Domain.Interfaces.Services;
using LDC.Domain.Services;
using LDC.Infra.Persistence;
using LDC.Infra.Persistence.Repositories;
using LDC.Infra.Persistence.Repositories.Base;
using LDC.Infra.Transactions;

namespace LDC.IoC.Unity
{
    public static class DependencyResolver
    {
        public static void Resolve(UnityContainer container)
        {
            container.RegisterType<DbContext, LDCContext>(new HierarchicalLifetimeManager());
            //UnitOfWork
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<INotifiable, Notifiable>(new HierarchicalLifetimeManager());

            //Serviço de Domain
            //container.RegisterType(typeof(IServiceBase<,>), typeof(ServiceBase<,>));

            container.RegisterType<IServiceCategoria, ServiceCategoria>(new HierarchicalLifetimeManager());
            container.RegisterType<IServiceUsuario, ServiceUsuario>(new HierarchicalLifetimeManager());

            //Repository
            container.RegisterType(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));

            container.RegisterType<IRepositoryCategoria, RepositoryCategoria>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryUsuario, RepositoryUsuario>(new HierarchicalLifetimeManager());
        }
    }
}
