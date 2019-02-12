﻿using System;
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
                FormsAuthentication.SetAuthCookie(model.Username, false);

                try
                {
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
            FormsAuthentication.SignOut();
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
                }
            }

            return toReturn;
        }

        #endregion
    }
}