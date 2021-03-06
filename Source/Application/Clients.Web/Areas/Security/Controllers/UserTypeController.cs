﻿using DS.Motel.Clients.Web.Areas.Security.Models.UserType;
using DS.Motel.Data.Entities;
using DS.Motel.Data.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using DS.Motel.Data.AddressBook;

namespace DS.Motel.Clients.Web.Areas.Security.Controllers
{
    public class UserTypeController : Controller
    {
        #region Fields & Properties

        private IUnityContainer container;

        #endregion






        #region Constructors

        public UserTypeController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion






        #region Validacion

        private List<Tuple<string, string>> GetErroresAdd(AddViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            UserTypeRepository UserTyoeRepository = container.Resolve<UserTypeRepository>();

            if (string.IsNullOrEmpty(model.Name))
            {
                toReturn.Add(new Tuple<string, string>("Name", "Por favor ingrese tipo de cuenta"));
            }
            else
            {
                if (UserTyoeRepository.GetAll().Count(c => c.Name == model.Name) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Name", "Ya existe un tipo de cuenta con el mismo nombre"));
                }
            }
            return toReturn;
        }

        private List<Tuple<string, string>> GetErroresEdit(EditViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            UserTypeRepository UserTyoeRepository = container.Resolve<UserTypeRepository>();

            if (string.IsNullOrEmpty(model.Name))
            {
                toReturn.Add(new Tuple<string, string>("Name", "Por favor ingrese el tipo de cuenta"));
            }
            else
            {
                if (UserTyoeRepository.GetAll().Count(c => c.Name == model.Name && c.UserTypeId != model.UserTypeId) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Name", "Ya existe un tipo de cuenta con el mismo nombre"));
                }
            }
            return toReturn;
        }

        private List<Tuple<string, string>> GetErroresDelete(DeleteViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            PersonalRepository personalRepository = container.Resolve<PersonalRepository>();

            if (personalRepository.ObtenerTodo().Where(w => w.UserTypeId == model.UserTypeId).Count() > 0)
            {
                toReturn.Add(new Tuple<string, string>("ErrorMessage", "No se pudo borrar el tipo de usuario porque se encuentra asociado a un personal"));
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
            UserTypeRepository userTypeRepository = container.Resolve<UserTypeRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresAdd(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }



            if (ModelState.IsValid)
            {
                UserType userType = new UserType();
                userType.Name = model.Name;
                userType.Descripcion = model.Descripcion;

                try
                {
                    userTypeRepository.Add(userType);

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

        public ActionResult Edit(Guid userTypeID)
        {
            UserTypeRepository userTypeRepository = container.Resolve<UserTypeRepository>();
            UserType userType = userTypeRepository.GetSingle(userTypeID);

            EditViewModel editViewModel = new EditViewModel();
            editViewModel.UserTypeId = userType.UserTypeId;
            editViewModel.Name = userType.Name;
            editViewModel.Descripcion = userType.Descripcion;

            return PartialView(editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            UserTypeRepository userTypeRepository = container.Resolve<UserTypeRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresEdit(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                UserType userType = new UserType();
                userType.UserTypeId = model.UserTypeId;
                userType.Name = model.Name;
                userType.Descripcion = model.Descripcion;

                try
                {
                    userTypeRepository.Edit(userType);

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

        public ActionResult Delete(Guid userTypeID)
        {
            DeleteViewModel deleteViewModel = new DeleteViewModel();
            deleteViewModel.UserTypeId = userTypeID;

            return PartialView(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel model)
        {
            UserTypeRepository userTypeRepository = container.Resolve<UserTypeRepository>();

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
                    userTypeRepository.Eliminar(model.UserTypeId);

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
            UserTypeRepository userTypeRepository = container.Resolve<UserTypeRepository>();
            List<NavigatorViewModel> toReturn = userTypeRepository.GetAll().Select(t => new NavigatorViewModel()
            {
                UserTypeId = t.UserTypeId,
                Name = t.Name,
                Descripcion = t.Descripcion
            }).OrderBy(y => y.Name).ToList();

            return Json(toReturn.ToDataSourceResult(request));
        }

        #endregion
    }
}