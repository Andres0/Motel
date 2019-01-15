using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Unity;
using System.Web.Mvc;
using DS.Motel.Data.AddressBook;
using DS.Motel.Clients.Web.Models.Home;

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
            List<IndexViewModel> Suites = ObtenerSuites();
            return View(Suites);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginViewModel loginViewModel = container.Resolve<LoginViewModel>();
            return View(loginViewModel);
        }

        #endregion



        #region Queries
        public List<IndexViewModel> ObtenerSuites()
        {
            SuitesRepository suitesRepository = container.Resolve<SuitesRepository>();
            List<IndexViewModel> ToReturn = suitesRepository.ObtenerTodo().Select(s => new IndexViewModel()
            {
                SuiteId = s.SuiteId,
                Nombre = s.Nombre,
                EstadoId = (int)s.Estado,
                EstadoNombre = s.Estado.ToString(),
            }).OrderBy(o => o.Nombre).ToList();

            return ToReturn;
        }

        #endregion
    }
}