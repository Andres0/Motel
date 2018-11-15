using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Unity;
using System.Web.Mvc;

using DS.Motel.Business.Security;
using DS.Motel.Business.Security.Repositories;

namespace DS.Motel.Clients.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Fields & Properties

        private IUserRepository userRepository;
        private IUnityContainer container;

        #endregion






        #region Constructors

        public HomeController(IUnityContainer _container, IUserRepository _userRepository)
        {
            container = _container;
            userRepository = _userRepository;
        }

        #endregion






        #region Events

        // GET: Home
        public ActionResult Index()
        {
            UserManager userManager = container.Resolve<UserManager>();
            var test = userManager.GetUserByUsernameAndPassword("", "");

            //var test2 = userRepository.GetUserByUsernameAndPassword("", "");

            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        #endregion
    }
}