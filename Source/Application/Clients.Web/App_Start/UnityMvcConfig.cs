using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace DS.Motel.Clients.Web
{
    public static class UnityMvcConfig
    {
        public static IUnityContainer InitializeContainerForMVC()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;

        }

        public static void RegisterTypes(IUnityContainer container)
        {
            //Managers
            container.RegisterType<DS.Motel.Business.Security.Repositories.IUserRepository, DS.Motel.Business.Security.UserManager>();

            //Data
            container.RegisterType<DS.Motel.Business.Security.Repositories.IUserRepository, DS.Motel.Data.Security.UserRepository>();
        }
    }
}