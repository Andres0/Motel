using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace DS.Motel.Clients.Web.Areas.Reportes.Controllers
{
    public class ReporteController : Controller
    {
        #region Fields & Properties

        private IUnityContainer container;
        #endregion



        #region Constructor
        public ReporteController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion



        #region Interfaces
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListadoPersonal()
        {
            return View();
        }



        #endregion

        #region ListadoPersonal
        public ActionResult CargarListadoPersonal()
        {
            return View;   
        }

        #endregion  
    }
}