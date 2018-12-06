using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Unity;
using System.Web.Mvc;
using DS.Motel.Data.AddressBook;

namespace DS.Motel.Clients.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Fields & Properties

        //private IUserRepository userRepository;
        private IUnityContainer container;

        #endregion






        #region Constructors

        public HomeController(IUnityContainer _container)//, IUserRepository _userRepository
        {
            container = _container;
            //userRepository = _userRepository;
        }

        #endregion






        #region Events

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            CargoRepository cargoRepository = container.Resolve<CargoRepository>();
            var toReturn = cargoRepository.ObtenerTodo().ToList();

            return View();
        }

        #endregion
    }
}