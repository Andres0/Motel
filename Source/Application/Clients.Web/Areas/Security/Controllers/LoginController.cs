using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Unity;

using DS.Motel.Business.Security;
using DS.Motel.Business.Security.Entities;

namespace DS.Motel.Clients.Web.Areas.Security.Controllers
{
    public class LoginController : Controller
    {
        #region Fields & Properties

        private IUnityContainer container;

        #endregion






        #region Constructors

        public LoginController()
        {
            //container = _container;
        }

        #endregion






        #region Events

        [AllowAnonymous]
        // GET: Security/Login
        public ActionResult Index()
        {
            //UserManager userManager = container.Resolve<UserManager>();
            //User_SEC user = userManager.GetUserByUsernameAndPassword("1", "2");

            return View();
        }

        #endregion
    }
}