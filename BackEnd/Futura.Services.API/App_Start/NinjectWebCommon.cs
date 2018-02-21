

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Futura.Services.API.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Futura.Services.API.App_Start.NinjectWebCommon), "Stop")]
namespace Futura.Services.API.App_Start
{
    using Futura.BusinessOperations.Implementations;
    using Futura.BusinessOperations.Interfaces;
    using Futura.DataAccess.Common;
    using Futura.DataAccess.EF;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using System;
    using System.Web;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }



        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }


        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel);
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }


        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            ////Data Access
            //container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
            //container.RegisterType<IUnitOfWork, UnitOfWork>();

            ////Business Operations
            //container.RegisterType<IXmlMigrationManager, XmlMigrationManager>();
            //container.RegisterType<ICustomersManager, CustomersManager>();
            //container.RegisterType<IOrdersManager, OrdersManager>();

            //Data Access
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();

            //Business Operations
            kernel.Bind<IXmlMigrationManager>().To<XmlMigrationManager>();
            kernel.Bind<ICustomersManager>().To<CustomersManager>();
            kernel.Bind<IOrdersManager>().To<OrdersManager>();

        }
    }
}