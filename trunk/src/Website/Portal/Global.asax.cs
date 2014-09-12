using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Portal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            System.Web.Mvc.ViewEngines.Engines.Add(new Portal.ViewEngines.PortalViewEngine());

            // Code that runs on application startup
            InitializeTracing();
            if (Meanstream.Portal.Core.Messaging.ApplicationMessagingManager.Enabled)
            {
                Meanstream.Portal.Core.Messaging.ApplicationMessagingManager Manage = Meanstream.Portal.Core.Messaging.ApplicationMessagingManager.Current;
            }

            Meanstream.Portal.ComponentModel.ComponentFactory.Container = new Meanstream.Portal.ComponentModel.SimpleContainer();
            Meanstream.Portal.ComponentModel.ComponentFactory.InstallComponents(new Meanstream.Portal.ComponentModel.ProviderInstaller("searchData", typeof(Meanstream.Portal.Core.Services.Search.DataProvider)));
            Meanstream.Portal.ComponentModel.ComponentFactory.InstallComponents(new Meanstream.Portal.ComponentModel.ProviderInstaller("searchIndex", typeof(Meanstream.Portal.Core.Services.Search.IndexingProvider)));
            Meanstream.Portal.ComponentModel.ComponentFactory.InstallComponents(new Meanstream.Portal.ComponentModel.ProviderInstaller("scheduling", typeof(Meanstream.Portal.Core.Services.Scheduling.SchedulingProvider)));
            Meanstream.Portal.ComponentModel.ComponentFactory.InstallComponents(new Meanstream.Portal.ComponentModel.ProviderInstaller("data", typeof(Meanstream.Core.Data.DataProvider)));
            Meanstream.Portal.ComponentModel.ComponentFactory.InstallComponents(new Meanstream.Portal.ComponentModel.ProviderInstaller("repository", typeof(Meanstream.Core.Repository.RepositoryProvider)));

            //start the scheduler on startup
            //Meanstream.Portal.Core.Services.Scheduling.SchedulingService.Current.StartService()
        }

        public void Application_End(object sender, EventArgs e)
        {
            // Code that runs on application shutdown
            if (Meanstream.Portal.Core.Messaging.ApplicationMessagingManager.Enabled)
            {
                Meanstream.Portal.Core.Messaging.ApplicationMessagingManager.Current.Deinitialize();
            }
            Meanstream.Portal.Core.Services.Scheduling.SchedulingService.Current.StopService();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            
        }

        public void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
        }

        public void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
        }

        public void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }

        private void InitializeTracing()
        {
            //Dim application As AppDomain = AppDomain.CurrentDomain
            //AddHandler application.UnhandledException, AddressOf Application_UnhandledException
            //AddHandler application.ReflectionOnlyAssemblyResolve, AddressOf Application_ReflectionOnlyAssemblyResolve
            //AddHandler application.AssemblyResolve, AddressOf Application_AssemblyResolve
            //AddHandler application.TypeResolve, AddressOf Application_TypeResolve
            //AddHandler application.ResourceResolve, AddressOf Application_ResourceResolve
            //AddHandler application.AssemblyLoad, AddressOf Application_AssemblyLoad
        }

        private void Application_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Application_AssemblyLoad: " + args.LoadedAssembly.FullName);
        }

        private void Application_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Application_UnhandledException: " + e.ExceptionObject.ToString());
        }

        private System.Reflection.Assembly Application_TypeResolve(object sender, ResolveEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Application_TypeResolve: " + args.Name);
            return null;
        }

        private System.Reflection.Assembly Application_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Application_ReflectionOnlyAssemblyResolve: " + args.Name);
            return null;
        }

        private System.Reflection.Assembly Application_ResourceResolve(object sender, ResolveEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Application_ResourceResolve: " + args.Name);
            return null;
        }

        private System.Reflection.Assembly Application_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Application_AssemblyResolve: " + args.Name);
            return null;
        }
    }


}
