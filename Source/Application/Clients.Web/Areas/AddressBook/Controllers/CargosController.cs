﻿using DS.Motel.Clients.Web.Areas.AddressBook.Models.Cargos;
using DS.Motel.Data.AddressBook;
using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

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
            CargoRepository cargoRepository = container.Resolve<CargoRepository>();

            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un nombre"));
            }
            else
            {
                if (cargoRepository.ObtenerTodo().Count(c => c.Nombre == model.Nombre) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Nombre", "Ya existe una categoria con el mismo nombre"));
                }
            }
            return toReturn;
        }

        private List<Tuple<string, string>> GetErroresEdit(EditViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            CargoRepository cargoRepository = container.Resolve<CargoRepository>();

            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un nombre"));
            }
            else
            {
                if (cargoRepository.ObtenerTodo().Count(c => c.Nombre == model.Nombre && c.CargoId != model.CargoId) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Nombre", "Ya existe una categoria con el mismo nombre"));
                }
            }
            return toReturn;
        }

        private List<Tuple<string, string>> GetErroresDelete(DeleteViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            PersonalRepository personalRepository = container.Resolve<PersonalRepository>();

            if (personalRepository.ObtenerTodo().Where(w => w.CargoId == model.CargoId).Count() > 0)
            {
                toReturn.Add(new Tuple<string, string>("ErrorMessage", "No se pudo borrar el cargo porque se encuentra asociado a un personal"));
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
            CargoRepository cargoRepository = container.Resolve<CargoRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresAdd(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                Cargo cargo = new Cargo();
                cargo.Nombre = model.Nombre;
                cargo.Descripcion = model.Descripcion;

                try
                {
                    cargoRepository.Agregar(cargo);

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
            CargoRepository cargoRepository = container.Resolve<CargoRepository>();
            Cargo cargo = cargoRepository.ObtenerTodo().SingleOrDefault(s => s.CargoId == cargoId);

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
            CargoRepository cargoRepository = container.Resolve<CargoRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresEdit(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                Cargo cargo = new Cargo();
                cargo.CargoId = model.CargoId;
                cargo.Nombre = model.Nombre;
                cargo.Descripcion = model.Descripcion;

                try
                {
                    cargoRepository.Editar(cargo);

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
            CargoRepository cargoRepository = container.Resolve<CargoRepository>();

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
                    cargoRepository.Eliminar(model.CargoId);

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
            CargoRepository cargoRepository = container.Resolve<CargoRepository>();
            List<NavegadorViewModel> toReturn = cargoRepository.ObtenerTodo().Select(t => new NavegadorViewModel()
            {
                CargoId = t.CargoId,
                Nombre = t.Nombre,
                Descripcion = t.Descripcion
            }).OrderBy(y => y.Nombre).ToList();

            return Json(toReturn.ToDataSourceResult(request));
        }

        #endregion
    }
}