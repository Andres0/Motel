using DS.Motel.Clients.Web.Areas.AddressBook.Models.Suites;
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
        // GET: AddressBook/Suites
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
            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un nombre"));
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
            return PartialView(model);
        }

        public ActionResult Edit(Guid suiteId)
        {
            SuitesRepository suiteRepository = container.Resolve<SuitesRepository>();
            Suite suite = suiteRepository.ObtenerTodo().SingleOrDefault(s => s.SuiteId == suiteId);

            EditViewModel editViewModel = new EditViewModel();
            editViewModel.SuiteId = suite.SuiteId;
            editViewModel.Nombre = suite.Nombre;
            editViewModel.EstadoId = (int)suite.Estado;
            editViewModel.ParametroId = suite.ParametroId;


            editViewModel.ListaParametros = ObtenerParametros();
            editViewModel.ListaEstado = ObtenerEstados();
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
            //Se debe validar que no tenga relaciones con otras entidades caso contrario se mostrara un mensaje

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
                ParametroNombre = t.Parametros.Nombre
            }).OrderBy(y => y.Nombre).ToList();

            return Json(toReturn.ToDataSourceResult(request));
        }
        public List<DropdownListViewModel> ObtenerParametros() {

            List<DropdownListViewModel> ToReturn = new List<DropdownListViewModel>();
            ParametrosRepository parametrosRepository = container.Resolve<ParametrosRepository>();
            ToReturn = parametrosRepository.ObtenerTodo().Select(x => new DropdownListViewModel()
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
            foreach (SuiteEstado suitestado in (SuiteEstado[])Enum.GetValues(typeof(SuiteEstado)))
            {


                ToReturn.Add(new DropdownListEnumViewModel() { Id=(int) suitestado, Nombre= suitestado.ToString()  });
            }
           
            if (ToReturn.Count() > 0)
            {
                ToReturn.Insert(0, new DropdownListEnumViewModel()
                {
                    Id = 0,
                    Nombre = "Seleccione un Estado"
                });
            }
          
            return ToReturn;
        }
        #endregion
    }
}