using DS.Motel.Business.Security;
using DS.Motel.Business.Security.Entities;
using DS.Motel.Business.Security.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace DS.Motel.Clients.Web.Areas.Security.Controllers
{
    public class UserTypeController : Controller
    {
        #region Fields & Properties

        private IUnityContainer container;

        #endregion






        #region Constructors

        public UserTypeController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion






        #region Events

        public ActionResult Index()
        {
            //Cargamos el primer elemento para cargar en un panel izquierdo el navegador y en el derecho
            //UserTypeManager userManager = container.Resolve<UserTypeManager>();
            //IQueryable<UserType_SEC> userTypes = userManager.GetAll().OrderBy(o => o.Name);

            //ViewData["UserTypeId"] View userTypes.Count() > 0 ? userTypes.FirstOrDefault() : null;
            return View();
        }

        #endregion
    }
}