using DS.Motel.Business.AddressBook;
using DS.Motel.Business.AddressBook.Entities;
using DS.Motel.Clients.Web.Areas.AddressBook.Models.Cargos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace DS.Motel.Clients.Web.Areas.AddressBook.Controllers
{
    public class CargosController : Controller
    {
        #region Fields & properties 

        private IUnityContainer container;

        #endregion






        #region Constructor
        public CargosController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion






        #region Validacion

        private List<Tuple<string, string>> GetErroresAdd(AddViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            CargoManager cargoManager = container.Resolve<CargoManager>();

            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un nombre"));
            }
            else
            {
                if (cargoManager.ObtenerTodos().Count(c => c.Nombre == model.Nombre) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Nombre", "Ya existe una categoria con el mismo nombre"));
                }
            }
            return toReturn;
        }

        private List<Tuple<string, string>> GetErroresEdit(EditViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            CargoManager cargoManager = container.Resolve<CargoManager>();

            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un nombre"));
            }
            else
            {
                if (cargoManager.ObtenerTodos().Count(c => c.Nombre == model.Nombre && c.CargoId != model.CargoId) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Nombre", "Ya existe una categoria con el mismo nombre"));
                }
            }
            return toReturn;
        }
        #endregion






        #region Eventos

        public ActionResult Index()
        {
            CargoManager cargoManager = container.Resolve<CargoManager>();
            NavegadorViewModel navegadorViewModel = new NavegadorViewModel();
            navegadorViewModel.Cargos = cargoManager.ObtenerTodos().Select(t => new NavegadorGridViewModel()
            {
                CargoId = t.CargoId,
                Nombre = t.Nombre,
                Descripcion = t.Descripcion
            }).OrderBy(y => y.Nombre).ToList();

            return View(navegadorViewModel);
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
            CargoManager cargoManager = container.Resolve<CargoManager>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresAdd(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }
            


            if (ModelState.IsValid)
            {
                Cargo_ADB cargo = new Cargo_ADB();
                cargo.Nombre = model.Nombre;
                cargo.Descripcion = model.Descripcion;

                try
                {
                    cargoManager.Add(cargo);

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

        public ActionResult Edit(Guid cargoId)
        {
            CargoManager cargoManager = container.Resolve<CargoManager>();
            Cargo_ADB cargo = cargoManager.ObtenerTodos().SingleOrDefault(s => s.CargoId == cargoId);

            EditViewModel editViewModel = new EditViewModel();
            editViewModel.CargoId = cargo.CargoId;
            editViewModel.Nombre = cargo.Nombre;
            editViewModel.Descripcion = cargo.Descripcion;

            return PartialView(editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            CargoManager cargoManager = container.Resolve<CargoManager>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresEdit(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                Cargo_ADB cargo = new Cargo_ADB();
                cargo.CargoId = model.CargoId;
                cargo.Nombre = model.Nombre;
                cargo.Descripcion = model.Descripcion;

                try
                {
                    cargoManager.Edit(cargo);

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

        public ActionResult Delete(Guid cargoId)
        {
            DeleteViewModel deleteViewModel = new DeleteViewModel();
            deleteViewModel.CargoId = cargoId;
            
            return PartialView(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel model)
        {
            CargoManager cargoManager = container.Resolve<CargoManager>();

            ModelState.Clear();
            //Se debe validar que no tenga relaciones con otras entidades caso contrario se mostrara un mensaje

            if (ModelState.IsValid)
            {
                try
                {
                    cargoManager.Delete(model.CargoId);

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

    }
}