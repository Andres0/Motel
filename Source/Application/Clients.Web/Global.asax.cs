using DS.Motel.Clients.Web.Models;
using DS.Motel.Data.AddressBook;
using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DS.Motel.Clients.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Código que se ejecuta al iniciarse la aplicación
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityMvcConfig.InitializeContainerForMVC();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Código que se ejecuta cuando se cierra la aplicación
        }

        protected void Session_Start()
        {
            // Código que se ejecuta cuando se inicia una nueva sesión
            Response.Redirect("/Home/LogIn");
        }

        public void Session_End()
        {
            // Código que se ejecuta cuando finaliza una sesión. solo funcion con InProc
            //SessionViewModel sessionViewModel = (SessionViewModel)Session["System_Information"];
            //TrabajoRepository trabajoRepository = new TrabajoRepository();
            //Trabajo trabajo = trabajoRepository.ObtenerTodo().Where(w => w.PersonalId == sessionViewModel.PersonalId).OrderByDescending(o => o.Ingreso).FirstOrDefault();
            //trabajo.Egreso = DateTime.Now;
            //trabajoRepository.Editar(trabajo);
        }
    }
}
