using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Unity;
using System.Web.Mvc;
using DS.Motel.Data.AddressBook;
using DS.Motel.Clients.Web.Models.Home;
using DS.Motel.Data.Entities;
using DS.Motel.Clients.Web.Models;
using System.Web.Security;

namespace DS.Motel.Clients.Web.Controllers
{
    [Authorize]
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



        #region Validacion

        private List<Tuple<string, string>> GetErroresLogin(LoginViewModel model)
        {
            PersonalRepository personalRepository = container.Resolve<PersonalRepository>();
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            if (string.IsNullOrEmpty(model.Username))
            {
                toReturn.Add(new Tuple<string, string>("Username", "Por favor ingrese un usuario"));
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                toReturn.Add(new Tuple<string, string>("Password", "Por favor ingrese un password"));
            }
            if (model.Saldo == null)
            {
                toReturn.Add(new Tuple<string, string>("Saldo", "Por favor ingrese un saldo"));
            }
            if (personalRepository.ObtenerPorUsuarioYContraseña(model.Username, model.Password) == null)
            {
                toReturn.Add(new Tuple<string, string>("Password", "Datos incorrectos"));
            }
            return toReturn;
        }
            
        private List<Tuple<string, string>> GetErroresIngresar(IngresarViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            CargoRepository cargoRepository = container.Resolve<CargoRepository>();

            if (model.TipoIngreso == 0)
            {
                toReturn.Add(new Tuple<string, string>("TipoIngreso", "Por favor seleccione un tipo de ingreso"));
            }
            return toReturn;
        }

        private List<Tuple<string, string>> GetErroresAnular(AnularViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();

            if (string.IsNullOrEmpty(model.Descripcion))
            {
                toReturn.Add(new Tuple<string, string>("Descripcion", "Por favor ingrese el motivo de la anulación"));
            }
            return toReturn;
        }

        #endregion






        #region Events

        public ActionResult Index(bool? masterload = true)
        {
            ViewData["MasterLoad"] = masterload;
            List<IndexViewModel> Suites = ObtenerSuites();
            return View(Suites);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginViewModel loginViewModel = container.Resolve<LoginViewModel>();
            return View(loginViewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            PersonalRepository personalRepository = container.Resolve<PersonalRepository>();
            TrabajoRepository trabajoRepository = container.Resolve<TrabajoRepository>();

            ModelState.Clear();

            List<Tuple<string, string>> errores = GetErroresLogin(model);
            
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                Personal personal = personalRepository.ObtenerPorUsuarioYContraseña(model.Username, model.Password);
                SessionViewModel sessionViewModel = new SessionViewModel();
                sessionViewModel.PersonalId = personal.PersonalId;
                sessionViewModel.Nombre = personal.Nombre;

                Session["System_Information"] = sessionViewModel;

                Trabajo trabajo = new Trabajo()
                {
                    PersonalId = personal.PersonalId,
                    Ingreso = DateTime.Now,
                };

                FormsAuthentication.SetAuthCookie(model.Username, false);
                try
                {
                    trabajoRepository.Agregar(trabajo);
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                catch (Exception)
                {
                }
            }

            return PartialView(model);
        }

        public ActionResult SignOut()
        {
            SessionViewModel sessionViewModel = (SessionViewModel)Session["System_Information"];
            TrabajoRepository trabajoRepository = container.Resolve<TrabajoRepository>();
            Trabajo trabajo = trabajoRepository.ObtenerTodo().Where(w => w.PersonalId == sessionViewModel.PersonalId).OrderByDescending(o => o.Ingreso).FirstOrDefault();
            trabajo.Egreso = DateTime.Now;
            trabajoRepository.Editar(trabajo);

            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public ActionResult Ingresar(Guid suiteId)
        {
            IngresarViewModel ingresarViewModel = new IngresarViewModel();
            ingresarViewModel.SuiteId = suiteId;
            return View(ingresarViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ingresar(IngresarViewModel model)
        {
            UsoHabitacionRepository usoHabitacionRepository = container.Resolve<UsoHabitacionRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresIngresar(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                UsoHabitacion usoHabitacion = new UsoHabitacion();
                usoHabitacion.SuiteId = model.SuiteId;
                usoHabitacion.TipoIngreso = model.TipoIngreso;
                usoHabitacion.Ingreso = DateTime.Now;
                usoHabitacion.Costo_Habitacion = GetCostoHabitacion(model.SuiteId, 0);
                usoHabitacion.Costo_Total = usoHabitacion.Costo_Habitacion;
                try
                {
                    usoHabitacionRepository.Agregar(usoHabitacion);

                    model.Result = Web.Models.EnumActionResult.Saved;
                }
                catch (Exception)
                {
                    model.Result = Web.Models.EnumActionResult.Error;
                }
            }
            else
            {
                model.Result = Web.Models.EnumActionResult.Validation;
            }
            return PartialView(model);
        }

        public ActionResult SuiteDetalle(Guid suiteId)
        {
            UsoHabitacionRepository usoHabitacionRepository = container.Resolve<UsoHabitacionRepository>();
            SuitesRepository suitesRepository = container.Resolve<SuitesRepository>();

            Suite suite = suitesRepository.ObtenerPorId(suiteId);
            UsoHabitacion usoHabitacion = usoHabitacionRepository.ObtenerPorSuiteId(suiteId);

            SuiteDetalleViewModel suiteDetalleViewModel = new SuiteDetalleViewModel();
            suiteDetalleViewModel.SuiteId = suiteId;
            suiteDetalleViewModel.SuiteNombre = suite.Nombre;
            suiteDetalleViewModel.NroUso = usoHabitacionRepository.ObtenerTodo().Where(w => w.SuiteId == suiteId && w.TipoUso != TipoUso.Interrumpido).Count();
            suiteDetalleViewModel.Estado = suite.Estado.ToString();
            suiteDetalleViewModel.Tiempo = (DateTime.Now - usoHabitacion.Ingreso).ToString(@"hh\:mm") ;
            suiteDetalleViewModel.HoraIngreso = usoHabitacion.Ingreso;
            suiteDetalleViewModel.TipoIngreso = usoHabitacion.TipoIngreso.ToString();
            suiteDetalleViewModel.CostoSuite = suite.Parametros.Costo_Habitacion;
            suiteDetalleViewModel.CostoTV = usoHabitacion.Costo_tv;
            suiteDetalleViewModel.CostoTotal = GetCostoHabitacion(suite.SuiteId, (DateTime.Now - usoHabitacion.Ingreso).Minutes) + usoHabitacion.Costo_Insumos + usoHabitacion.Costo_Insumos_Externo+ usoHabitacion.Costo_tv;
            suiteDetalleViewModel.ExistePagoACuenta = false;

            return View(suiteDetalleViewModel);
        }


        public ActionResult Anular(Guid suiteId)
        {
            AnularViewModel anularViewModel = new AnularViewModel();
            anularViewModel.SuiteId = suiteId;

            return PartialView(anularViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Anular(AnularViewModel model)
        {
            UsoHabitacionRepository usoHabitacionRepository = container.Resolve<UsoHabitacionRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresAnular(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    usoHabitacionRepository.Anular(model.SuiteId, model.Descripcion);

                    model.Result = Web.Models.EnumActionResult.Saved;
                }
                catch (Exception)
                {
                    model.Result = Web.Models.EnumActionResult.Error;
                }
            }
            else
            {
                model.Result = Web.Models.EnumActionResult.Validation;
            }
            return PartialView(model);
        }

        public ActionResult Presentacion()
        {
            SuitesRepository suitesRepository = container.Resolve<SuitesRepository>();
            List<Suite> suites = suitesRepository.ObtenerTodo().ToList();

            Suite especial = suites.FirstOrDefault(f => f.Tipo == SuiteTipo.Especial);
            Suite platinum = suites.FirstOrDefault(f => f.Tipo == SuiteTipo.Platinum);
            Suite gold = suites.FirstOrDefault(f => f.Tipo == SuiteTipo.Gold);
            Suite silver = suites.FirstOrDefault(f => f.Tipo == SuiteTipo.Silver);

            ViewData["especial"] = especial != null ? especial.Nombre : string.Empty;
            ViewData["platinum"] = platinum != null ? platinum.Nombre : string.Empty;
            ViewData["gold"] = gold != null ? gold.Nombre : string.Empty;
            ViewData["silver"] = silver != null ? silver.Nombre : string.Empty;
            return View();
        }

        [HttpGet]
        public JsonResult UsoTv(bool usoTv, Guid suiteId)
        {
            SuitesRepository suitesRepository = container.Resolve<SuitesRepository>();
            UsoHabitacionRepository usoHabitacionRepository = container.Resolve<UsoHabitacionRepository>();

            Suite suite = suitesRepository.ObtenerPorId(suiteId);
            UsoHabitacion usoHabitacion = usoHabitacionRepository.ObtenerPorSuiteId(suiteId);
            usoHabitacion.Costo_tv = usoTv == true ? suite.Parametros.Costo_Tv : 0;
            usoHabitacion.Costo_Total = usoHabitacion.Costo_Habitacion + usoHabitacion.Costo_Insumos + usoHabitacion.Costo_Insumos_Externo + usoHabitacion.Costo_tv;
            usoHabitacionRepository.Editar(usoHabitacion);

            return Json(usoHabitacion.Costo_Total.ToString(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCostTotal(Guid suiteId)
        {
            UsoHabitacionRepository usoHabitacionRepository = container.Resolve<UsoHabitacionRepository>();

            UsoHabitacion usoHabitacion = usoHabitacionRepository.ObtenerPorSuiteId(suiteId);
            decimal costo_Total = usoHabitacion.Costo_Habitacion + usoHabitacion.Costo_Insumos + usoHabitacion.Costo_Insumos_Externo + usoHabitacion.Costo_tv;
            usoHabitacionRepository.Editar(usoHabitacion);

            return Json(usoHabitacion.Costo_Total.ToString(), JsonRequestBehavior.AllowGet);
        }
        
        #endregion



        #region Queries
        public List<IndexViewModel> ObtenerSuites()
        {
            SuitesRepository suitesRepository = container.Resolve<SuitesRepository>();
            UsoHabitacionRepository usoHabitacionRepository = container.Resolve<UsoHabitacionRepository>();
            List<IndexViewModel> toReturn = suitesRepository.ObtenerTodo().Select(s => new IndexViewModel()
            {
                SuiteId = s.SuiteId,
                Nombre = s.Nombre,
                Estado = s.Estado,
                Tiempo_Anular = s.Parametros.Tiempo_Anular,
            }).OrderBy(o => o.Nombre).ToList();
            foreach (IndexViewModel item in toReturn)
            {
                if (item.Estado == SuiteEstado.Ocupado)
                {
                    UsoHabitacion usoHabitacion = usoHabitacionRepository.ObtenerPorSuiteId(item.SuiteId);
                    item.Ingreso = usoHabitacion.Ingreso;
                    item.TipoIngreso = usoHabitacion.TipoIngreso;
                    item.Costo_Total = usoHabitacion.Costo_Total;
                }
            }

            return toReturn;
        }

        public decimal GetCostoHabitacion(Guid suiteId, decimal tiempo_transcurido_minutos)
        {
            decimal toReturn = 0;
            SuitesRepository suitesRepository = container.Resolve<SuitesRepository>();
            Suite suite = suitesRepository.ObtenerPorId(suiteId);

            decimal a = 0;
            decimal b = 0;

            if (tiempo_transcurido_minutos > 0)
            {
                if (tiempo_transcurido_minutos < (suite.Parametros.Tiempo_Hora + suite.Parametros.Tolerancia))
                    a = 0;
                else if (tiempo_transcurido_minutos <= (suite.Parametros.Tiempo_Hora + suite.Parametros.Tolerancia + suite.Parametros.Tiempo_Incremento))
                    a = 1;
                else if (tiempo_transcurido_minutos > (suite.Parametros.Tiempo_Hora + suite.Parametros.Tolerancia + suite.Parametros.Tiempo_Incremento))
                    a = 2;

                b = ((tiempo_transcurido_minutos - suite.Parametros.Tolerancia - suite.Parametros.Tiempo_Hora - 1) / suite.Parametros.Tiempo_Incremento) - 1;
                b = b < 0 ? 0 : b;
            }

            toReturn = suite.Parametros.Costo_Habitacion + (a * suite.Parametros.Costo_Adicional1) + (b * suite.Parametros.Costo_Adicional2);
            return toReturn;
        }

        #endregion
    }
}