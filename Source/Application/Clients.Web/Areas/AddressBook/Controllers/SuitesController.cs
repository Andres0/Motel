﻿using DS.Motel.Clients.Web.Areas.AddressBook.Models.Suites;
using DS.Motel.Clients.Web.Models;
using DS.Motel.Data.AddressBook;
using DS.Motel.Data.Entities;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace DS.Motel.Clients.Web.Areas.AddressBook.Controllers
{
    public class SuitesController : Controller
    {
        #region Fields & properties 

        private IUnityContainer container;

        #endregion






        #region Constructor
        public SuitesController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion






        #region Validacion

        private List<Tuple<string, string>> GetErroresAdd(AddViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            SuitesRepository suiteRepository = container.Resolve<SuitesRepository>();

            if (model.ParametroId==Guid.Empty)
            {
                toReturn.Add(new Tuple<string, string>("ParametroId", "Por favor seleccione un Parametro"));
            }
            if (model.EstadoId == 0)
            {
                toReturn.Add(new Tuple<string, string>("EstadoId", "Por favor seleccione un Estado"));
            }
            if (model.TipoId == 0)
            {
                toReturn.Add(new Tuple<string, string>("TipoId", "Por favor seleccione un Tipo"));
            }
            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un Nombre"));
            }
            else
            {
                if (suiteRepository.ObtenerTodo().Count(c => c.Nombre == model.Nombre) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Nombre", "Ya existe una suite con el mismo nombre"));
                }
            }
            return toReturn;
        }

        private List<Tuple<string, string>> GetErroresEdit(EditViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            SuitesRepository suitesRepository = container.Resolve<SuitesRepository>();

            if (model.ParametroId == Guid.Empty)
            {
                toReturn.Add(new Tuple<string, string>("ParametroId", "Por favor seleccione un Parametro"));
            }
            if (model.EstadoId == 0)
            {
                toReturn.Add(new Tuple<string, string>("EstadoId", "Por favor seleccione un Estado"));
            }
            if (model.TipoId == 0)
            {
                toReturn.Add(new Tuple<string, string>("TipoId", "Por favor seleccione un Tipo"));
            }
            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un nombre"));
            }
            else
            {
                if (suitesRepository.ObtenerTodo().Count(c => c.Nombre == model.Nombre && c.SuiteId != model.SuiteId) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Nombre", "Ya existe una suite con el mismo nombre"));
                }
            }
            Suite suite = suitesRepository.ObtenerPorId(model.SuiteId);
            if (suite.Estado != SuiteEstado.Habilitado && suite.Estado != SuiteEstado.Deshabilitado && suite.Estado != SuiteEstado.Mantenimiento)
            {
                toReturn.Add(new Tuple<string, string>("EstadoId", "No se puede editar porque la suite se encuentra: " + suite.Estado.ToString() ));
            }
            return toReturn;
        }

        private List<Tuple<string, string>> GetErroresDelete(DeleteViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            UsoHabitacionRepository usoHabitacionRepository = container.Resolve<UsoHabitacionRepository>();

            if (usoHabitacionRepository.ObtenerTodo().Where(w => w.SuiteId == model.SuiteId).Count() > 0)
            {
                toReturn.Add(new Tuple<string, string>("ErrorMessage", "No se pudo borrar la suite porque tiene registros asociados de uso"));
            }
            return toReturn;
        }

        #endregion






        #region Eventos

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            AddViewModel addViewModel = new AddViewModel();
            addViewModel.ListaParametros = ObtenerParametros();
            addViewModel.ListaEstado = ObtenerEstados();
            addViewModel.ListaTipo = ObtenerTipo();
            
            return PartialView(addViewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            SuitesRepository suitesRepository = container.Resolve<SuitesRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresAdd(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                Suite suite = new Suite();
                suite.Nombre = model.Nombre;
                suite.Estado = (SuiteEstado)model.EstadoId;
                suite.Tipo = (SuiteTipo)model.TipoId;
                suite.ParametroId = model.ParametroId;

                try
                {
                    suitesRepository.Agregar(suite);

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
            model.ListaParametros = ObtenerParametros();
            model.ListaEstado = ObtenerEstados();
            model.ListaTipo = ObtenerTipo();
            return PartialView(model);
        }

        public ActionResult Edit(Guid suiteId)
        {
            SuitesRepository suiteRepository = container.Resolve<SuitesRepository>();
            Suite suite = suiteRepository.ObtenerTodo().SingleOrDefault(s => s.SuiteId == suiteId);

            EditViewModel editViewModel = new EditViewModel();
            editViewModel.SuiteId = suite.SuiteId;
            editViewModel.Nombre = suite.Nombre;
            editViewModel.EstadoId = suite.Estado == SuiteEstado.Habilitado || suite.Estado == SuiteEstado.Deshabilitado || suite.Estado == SuiteEstado.Mantenimiento ? (int)suite.Estado : (int)SuiteEstado.Habilitado;
            editViewModel.TipoId = (int)suite.Tipo;
            editViewModel.ParametroId = suite.ParametroId;

            editViewModel.ListaParametros = ObtenerParametros();
            editViewModel.ListaEstado = ObtenerEstados();
            editViewModel.ListaTipo = ObtenerTipo();

            return PartialView(editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            SuitesRepository suitesRepository = container.Resolve<SuitesRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresEdit(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                Suite suite = new Suite();
                suite.SuiteId = model.SuiteId;
                suite.Nombre = model.Nombre;
                suite.Estado = (SuiteEstado)model.EstadoId;
                suite.Tipo = (SuiteTipo)model.TipoId;
                suite.ParametroId = model.ParametroId;

                try
                {
                    suitesRepository.Editar(suite);

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
            model.ListaParametros = ObtenerParametros();
            model.ListaEstado = ObtenerEstados();
            model.ListaTipo = ObtenerTipo();
            return PartialView(model);
        }

        public ActionResult Delete(Guid suiteId)
        {
            DeleteViewModel deleteViewModel = new DeleteViewModel();
            deleteViewModel.SuiteId = suiteId;

            return PartialView(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel model)
        {
            SuitesRepository suiteRepository = container.Resolve<SuitesRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresDelete(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    suiteRepository.Eliminar(model.SuiteId);

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

        public ActionResult LoadGrid([DataSourceRequest]DataSourceRequest request)
        {
            SuitesRepository suitesRepository = container.Resolve<SuitesRepository>();
            List<NavegadorViewModel> toReturn = suitesRepository.ObtenerTodo().Select(t => new NavegadorViewModel()
            {
                SuiteId = t.SuiteId,
                Nombre = t.Nombre,
                Estado = t.Estado.ToString(),
                Tipo = t.Tipo.ToString(),
                ParametroNombre = t.Parametros.Nombre
            }).OrderBy(y => y.Nombre).ToList();

            return Json(toReturn.ToDataSourceResult(request));
        }
        public List<DropdownListViewModel> ObtenerParametros() {

            List<DropdownListViewModel> ToReturn = new List<DropdownListViewModel>();
            ParametrosRepository parametrosRepository = container.Resolve<ParametrosRepository>();
            ToReturn = parametrosRepository.ObtenerTodo().Where(w => w.Activado == true).Select(x => new DropdownListViewModel()
            {
                Id = x.ParametroId,
                Nombre = x.Nombre
            }).OrderBy(y => y.Nombre).ToList();
            if (ToReturn.Count() > 0)
            {
                ToReturn.Insert(0, new DropdownListViewModel()
                {
                    Id = Guid.Empty,
                    Nombre = "Seleccione un parametro"
                });
            }
            else
            {
                ToReturn.Add(new DropdownListViewModel()
                {
                    Id = Guid.Empty,
                    Nombre = "No hay datos"
                });
            }
            return ToReturn;
        }
        public List<DropdownListEnumViewModel> ObtenerEstados()
        {
            List<DropdownListEnumViewModel> ToReturn = new List<DropdownListEnumViewModel>();
            ToReturn.Add(new DropdownListEnumViewModel() { Id = (int)SuiteEstado.Habilitado, Nombre= SuiteEstado.Habilitado.ToString()  });
            ToReturn.Add(new DropdownListEnumViewModel() { Id = (int)SuiteEstado.Deshabilitado, Nombre = SuiteEstado.Deshabilitado.ToString() });
            ToReturn.Add(new DropdownListEnumViewModel() { Id = (int)SuiteEstado.Mantenimiento, Nombre = SuiteEstado.Mantenimiento.ToString() });

            if (ToReturn.Count() > 0)
            {
                ToReturn.Insert(0, new DropdownListEnumViewModel()
                {
                    Id = 0,
                    Nombre = "Seleccione un estado"
                });
            }
          
            return ToReturn;
        }

        public List<DropdownListEnumViewModel> ObtenerTipo()
        {
            List<DropdownListEnumViewModel> ToReturn = new List<DropdownListEnumViewModel>();
            ToReturn.Add(new DropdownListEnumViewModel() { Id = (int)SuiteTipo.Especial, Nombre = SuiteTipo.Especial.ToString() });
            ToReturn.Add(new DropdownListEnumViewModel() { Id = (int)SuiteTipo.Platinum, Nombre = SuiteTipo.Platinum.ToString() });
            ToReturn.Add(new DropdownListEnumViewModel() { Id = (int)SuiteTipo.Gold, Nombre = SuiteTipo.Gold.ToString() });
            ToReturn.Add(new DropdownListEnumViewModel() { Id = (int)SuiteTipo.Silver, Nombre = SuiteTipo.Silver.ToString() });

            if (ToReturn.Count() > 0)
            {
                ToReturn.Insert(0, new DropdownListEnumViewModel()
                {
                    Id = 0,
                    Nombre = "Seleccione un tipo"
                });
            }

            return ToReturn;
        }
        #endregion
    }
}