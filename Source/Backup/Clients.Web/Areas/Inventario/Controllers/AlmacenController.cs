using DS.Motel.Clients.Web.Areas.Inventario.Models.Almacen;
using DS.Motel.Data.Entities;
using DS.Motel.Data.Inventarios;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace DS.Motel.Clients.Web.Areas.Inventario.Controllers
{
    public class AlmacenController : Controller
    {
        #region Fields & properties 

        private IUnityContainer container;

        #endregion






        #region Constructor
        public AlmacenController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion






        #region Validacion

        private List<Tuple<string, string>> GetErroresAdd(AddViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            AlmacenRepository almacenRepository = container.Resolve<AlmacenRepository>();

            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un nombre"));
            }
            else
            {
                if (almacenRepository.ObtenerTodo().Count(c => c.Nombre == model.Nombre) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Nombre", "Ya existe un almacen con el mismo nombre"));
                }
            }
            return toReturn;
        }

        private List<Tuple<string, string>> GetErroresEdit(EditViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            AlmacenRepository almacenRepository = container.Resolve<AlmacenRepository>();

            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un nombre"));
            }
            else
            {
                if (almacenRepository.ObtenerTodo().Count(c => c.Nombre == model.Nombre && c.AlmacenId != model.AlmacenId) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Nombre", "Ya existe un almacen con el mismo nombre"));
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
            return PartialView(addViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            AlmacenRepository almacenRepository = container.Resolve<AlmacenRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresAdd(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }



            if (ModelState.IsValid)
            {
                Almacen almacen = new Almacen();
                almacen.Nombre = model.Nombre;
                almacen.Descripcion = model.Descripcion;

                try
                {
                    almacenRepository.Agregar(almacen);

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

        public ActionResult Edit(Guid almacenId)
        {
            AlmacenRepository almacenRepository = container.Resolve<AlmacenRepository>();
            Almacen almacen = almacenRepository.ObtenerTodo().SingleOrDefault(s => s.AlmacenId == almacenId);

            EditViewModel editViewModel = new EditViewModel();
            editViewModel.AlmacenId = almacen.AlmacenId;
            editViewModel.Nombre = almacen.Nombre;
            editViewModel.Descripcion = almacen.Descripcion;

            return PartialView(editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            AlmacenRepository almacenRepository = container.Resolve<AlmacenRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresEdit(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                Almacen almacen = new Almacen();
                almacen.AlmacenId = model.AlmacenId;
                almacen.Nombre = model.Nombre;
                almacen.Descripcion = model.Descripcion;

                try
                {
                    almacenRepository.Editar(almacen);

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

        public ActionResult Delete(Guid almacenId)
        {
            DeleteViewModel deleteViewModel = new DeleteViewModel();
            deleteViewModel.AlmacenId = almacenId;

            return PartialView(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel model)
        {
            AlmacenRepository almacenRepository = container.Resolve<AlmacenRepository>();

            ModelState.Clear();
            //Se debe validar que no tenga relaciones con otras entidades caso contrario se mostrara un mensaje

            if (ModelState.IsValid)
            {
                try
                {
                    almacenRepository.Eliminar(model.AlmacenId);

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
            AlmacenRepository almacenRepository = container.Resolve<AlmacenRepository>();
            List<NavegadorViewModel> toReturn = almacenRepository.ObtenerTodo().Select(t => new NavegadorViewModel()
            {
                AlmacenId = t.AlmacenId,
                Nombre = t.Nombre,
                Descripcion = t.Descripcion
            }).OrderBy(y => y.Nombre).ToList();

            return Json(toReturn.ToDataSourceResult(request));
        }

        #endregion
    }
}