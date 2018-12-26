using DS.Motel.Clients.Web.Areas.Finances.Models.Transaccion;
using DS.Motel.Data.Finances;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Kendo.Mvc.Extensions;
using DS.Motel.Clients.Web.Models;

namespace DS.Motel.Clients.Web.Areas.Finances.Controllers
{
    public class TransaccionController : Controller
    {
        #region Fields & properties 

        private IUnityContainer container;

        #endregion






        #region Constructor
        public TransaccionController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion






        #region Validacion

        #endregion






        #region Interface

        public ActionResult Index(bool? masterload = true)
        {
            ViewData["MasterLoad"] = masterload;
            return View();
        }

        //public ActionResult Add()
        //{
        //    AddViewModel addViewModel = new AddViewModel();
        //    addViewModel.Cuentas = ObtenerCuentas();
        //    return PartialView(addViewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Add(AddViewModel model)
        //{
        //    PersonalRepository personalRepository = container.Resolve<PersonalRepository>();

        //    //LIMPIA EL MODEO DE ERRORES
        //    ModelState.Clear();

        //    //OBTENGO LOS POSIBLES ERRORES
        //    List<Tuple<string, string>> errores = GetErroresAdd(model);

        //    //CARGO LOS ERRORES AL MODELSTATE
        //    foreach (Tuple<string, string> item in errores)
        //    {
        //        ModelState.AddModelError(item.Item1, item.Item2);
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        Personal personal = new Personal();
        //        personal.Nombre = model.Nombre;
        //        personal.Apellido = model.Apellido;
        //        personal.CI = model.CI;
        //        personal.Direccion = model.Direccion;
        //        personal.Telefono = model.Telefono;
        //        personal.Email = model.Email;
        //        personal.Login = model.Login;
        //        personal.Password = model.Password;
        //        personal.Observacion = model.Observacion;
        //        personal.Estado = PersonalEstado.Activado;
        //        personal.UserTypeId = model.UserTypeId;
        //        personal.CargoId = model.CargoId;
        //        personal.Creado_Por = null;

        //        try
        //        {
        //            personalRepository.Agregar(personal);

        //            model.Result = EnumActionResult.Saved;
        //        }
        //        catch (Exception)
        //        {
        //            model.Result = Web.Models.EnumActionResult.Error;
        //        }
        //    }
        //    else
        //    {
        //        model.Result = Web.Models.EnumActionResult.Validation;
        //    }

        //    model.Cargos = Obtenercargos();
        //    model.TipoUsuario = ObtenerTipodeUser();
        //    return PartialView(model);
        //}

        //public ActionResult Edit(Guid personalId)
        //{
        //    PersonalRepository personalRepository = container.Resolve<PersonalRepository>();
        //    Personal personal = personalRepository.ObtenerPorId(personalId);

        //    EditViewModel editViewModel = new EditViewModel();
        //    editViewModel.PersonalId = personal.PersonalId;
        //    editViewModel.Nombre = personal.Nombre;
        //    editViewModel.Apellido = personal.Apellido;
        //    editViewModel.CI = personal.CI;
        //    editViewModel.Direccion = personal.Direccion;
        //    editViewModel.Telefono = personal.Telefono;
        //    editViewModel.Email = personal.Email;
        //    editViewModel.Login = personal.Login;
        //    editViewModel.Password = personal.Password;
        //    editViewModel.Observacion = personal.Observacion;
        //    editViewModel.Estado = personal.Estado;
        //    editViewModel.CargoId = personal.CargoId;
        //    editViewModel.UserTypeId = personal.UserTypeId;

        //    editViewModel.Cargos = Obtenercargos();
        //    editViewModel.TipoUsuario = ObtenerTipodeUser();

        //    return PartialView(editViewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(EditViewModel model)
        //{
        //    PersonalRepository personalRepository = container.Resolve<PersonalRepository>();

        //    ModelState.Clear();

        //    List<Tuple<string, string>> errores = GeterrorEdit(model);

        //    //CARGO LOS ERRORES AL MODELSTATE
        //    foreach (Tuple<string, string> item in errores)
        //    {
        //        ModelState.AddModelError(item.Item1, item.Item2);
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        Personal personal = new Personal();
        //        personal.PersonalId = model.PersonalId;
        //        personal.Nombre = model.Nombre;
        //        personal.Apellido = model.Apellido;
        //        personal.CI = model.CI;
        //        personal.Direccion = model.Direccion;
        //        personal.Telefono = model.Telefono;
        //        personal.Email = model.Email;
        //        personal.Login = model.Login;
        //        personal.Password = model.Password;
        //        personal.Observacion = model.Observacion;
        //        personal.Estado = PersonalEstado.Activado;
        //        personal.UserTypeId = model.UserTypeId;
        //        personal.CargoId = model.CargoId;
        //        personal.Creado_Por = null;

        //        try
        //        {
        //            personalRepository.Editar(personal);

        //            model.Result = EnumActionResult.Saved;
        //        }
        //        catch (Exception)
        //        {
        //            model.Result = Web.Models.EnumActionResult.Error;
        //        }
        //    }
        //    else
        //    {
        //        model.Result = Web.Models.EnumActionResult.Validation;
        //    }

        //    model.Cargos = Obtenercargos();
        //    model.TipoUsuario = ObtenerTipodeUser();
        //    return PartialView(model);
        //}

        //public ActionResult Delete(Guid personalId)
        //{
        //    DeleteViewModel deleteViewModel = new DeleteViewModel();
        //    deleteViewModel.PersonalId = personalId;

        //    return PartialView(deleteViewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(DeleteViewModel model)
        //{
        //    PersonalRepository personalRepository = container.Resolve<PersonalRepository>();

        //    ModelState.Clear();
        //    //Se debe validar que no tenga relaciones con otras entidades caso contrario se mostrara un mensaje

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            personalRepository.Eliminar(model.PersonalId);

        //            model.Result = Web.Models.EnumActionResult.Saved;
        //        }
        //        catch (Exception)
        //        {
        //            model.Result = Web.Models.EnumActionResult.Error;
        //        }
        //    }
        //    else
        //    {
        //        model.Result = Web.Models.EnumActionResult.Validation;
        //    }
        //    return PartialView(model);
        //}

        #endregion



        #region Queries

        public ActionResult LoadGrid([DataSourceRequest]DataSourceRequest request)
        {
            TransaccionRepository transaccionRepository = container.Resolve<TransaccionRepository>();
            List<NavegadorViewModel> toReturn = transaccionRepository.ObtenerTodo().Select(t => new NavegadorViewModel()
            {
                TransaccionId = t.TransaccionId,
                Nro_Cuenta = t.Cuenta.Nombre,
                Tipo = t.Tipo.ToString(),
                Fecha_Ini = t.Fecha_Ini.ToString("dd/MM/yyyy"),
                Fecha_Fin = t.Fecha_Fin.ToString("dd/MM/yyyy"),
                Fecha_Transaccion = t.Fecha_Transaccion,
                Concepto = t.Saldo.ToString(),
                Saldo = t.Saldo.ToString(),
            }).OrderBy(y => y.Fecha_Transaccion).ToList();

            return Json(toReturn.ToDataSourceResult(request));
        }

        public List<DropdownListViewModel> ObtenerCuentas()
        {
            List<DropdownListViewModel> ToReturn = new List<DropdownListViewModel>();
            CajaBancoRepository cuentaRepository = container.Resolve<CajaBancoRepository>();
            ToReturn = cuentaRepository.ObtenerTodo().Select(x => new DropdownListViewModel()
            {
                Id = x.CajaBancoId,
                Nombre = x.Nombre
            }).OrderBy(y => y.Nombre).ToList();
            if (ToReturn.Count() > 0)
            {
                ToReturn.Insert(0, new DropdownListViewModel()
                {
                    Id = Guid.Empty,
                    Nombre = "Seleccione una Cuenta"
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
        
        #endregion
    }
}