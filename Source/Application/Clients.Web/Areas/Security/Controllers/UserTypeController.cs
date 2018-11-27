using DS.Motel.Clients.Web.Areas.Security.Models.UserType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

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






        //#region Events

        //public ActionResult Index()
        //{
        //    //Cargamos el primer elemento para cargar en un panel izquierdo el navegador y en el derecho
        //    UserTypeManager userManager = container.Resolve<UserTypeManager>();
        //    UserType_SEC userType = userManager.GetAll().OrderBy(o => o.Name).FirstOrDefault();

        //    ViewData["UserTypeId"] = userType != null ? (Guid?)userType.UserTypeId : null;

        //    return View();
        //}



        //public ActionResult Navigator(Guid? userTypeId)
        //{
        //    UserTypeManager userManager = container.Resolve<UserTypeManager>();

        //    NavigatorViewModel navigatorViewModel = new NavigatorViewModel();
        //    navigatorViewModel.UserTypeId = userTypeId;
        //    navigatorViewModel.GridPageSize = 10; //19 registros por pagina
        //    navigatorViewModel.UserTypes = userManager.GetAll().Select(s => new NavigatorGridViewModel() {
        //        UserTypeId = s.UserTypeId,
        //        Name = s.Name,
        //    }).OrderBy(o => o.Name).ToList();

        //    if (userTypeId != null && navigatorViewModel.UserTypes != null && navigatorViewModel.UserTypes.Count() > 0)
        //    {
        //        int indexOfItem = navigatorViewModel.UserTypes.FindIndex(f => f.UserTypeId == userTypeId.Value);
        //        navigatorViewModel.GridNumberOfPage = (int)(indexOfItem / navigatorViewModel.GridPageSize) + 1;
        //    }
        //    else
        //        navigatorViewModel.GridNumberOfPage = 1;


        //    return PartialView(navigatorViewModel);
        //}

        //public ActionResult Add(Guid? userTypeId)
        //{
        //    AddViewModel addViewModel = new AddViewModel();
        //    addViewModel.UserTypeIdSummary = userTypeId;

        //    return PartialView(addViewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Add(AddViewModel model)
        //{
        //    UserTypeManager userManager = container.Resolve<UserTypeManager>();

        //    ModelState.Clear();
        //    if (string.IsNullOrEmpty(model.Name))
        //    {
        //        ModelState.AddModelError("Name", "Por favor ingrese un nombre");
        //    }


        //    if (ModelState.IsValid)
        //    {
        //        UserType_SEC userType = new UserType_SEC();
        //        userType.Name = model.Name;

        //        try
        //        {
        //            userManager.Add(userType, true);

        //            model.UserTypeIdSummary = userType.UserTypeId;
        //            model.Result = Web.Models.EnumActionResult.Saved;
        //        }
        //        catch (Exception ex)
        //        {
        //            model.Result = Web.Models.EnumActionResult.Error;
        //        }
        //    }
        //    else
        //    {
        //        model.Result = Web.Models.EnumActionResult.Validation;
        //    }
        //    return PartialView("Add", model);
        //}

        //public ActionResult Edit(Guid? userTypeId)
        //{
        //    UserTypeManager userManager = container.Resolve<UserTypeManager>();
        //    UserType_SEC userType = userManager.GetSingle(userTypeId);

        //    EditViewModel editViewModel = new EditViewModel();
        //    editViewModel.UserTypeIdSummary = userType.UserTypeId;
        //    editViewModel.Name = userType.Name;

        //    return PartialView(editViewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(EditViewModel model)
        //{
        //    UserTypeManager userManager = container.Resolve<UserTypeManager>();

        //    ModelState.Clear();
        //    if (string.IsNullOrEmpty(model.Name))
        //    {
        //        ModelState.AddModelError("Name", "Por favor ingrese un nombre");
        //    }


        //    if (ModelState.IsValid)
        //    {
        //        UserType_SEC userType = new UserType_SEC();
        //        userType.UserTypeId = model.UserTypeIdSummary;
        //        userType.Name = model.Name;

        //        try
        //        {
        //            userManager.Edit(userType, true);

        //            model.UserTypeIdSummary = userType.UserTypeId;
        //            model.Result = Web.Models.EnumActionResult.Saved;
        //        }
        //        catch (Exception ex)
        //        {
        //            model.Result = Web.Models.EnumActionResult.Error;
        //        }
        //    }
        //    else
        //    {
        //        model.Result = Web.Models.EnumActionResult.Validation;
        //    }
        //    return PartialView("Edit", model);
        //}

        //public ActionResult Summary(Guid? userTypeId)
        //{
        //    UserTypeManager userManager = container.Resolve<UserTypeManager>();
        //    UserType_SEC userType = userManager.GetSingle(userTypeId);

        //    SummaryViewModel summaryViewModel = new SummaryViewModel();
        //    summaryViewModel.UserTypeId = (userType != null) ? (Guid?)userType.UserTypeId : null;
        //    summaryViewModel.Name = (userType != null) ? userType.Name : string.Empty;

        //    return PartialView(summaryViewModel);
        //}

        //#endregion
    }
}