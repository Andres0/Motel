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
using DS.Motel.Data.Entities;

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

        private List<Tuple<string, string>> GetErroresAdd(AddViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();

            if (model.CuentaId == Guid.Empty)
            {
                toReturn.Add(new Tuple<string, string>("CuentaId", "Por favor seleccione una cuenta"));
            }
            if (model.Tipo == TransaccionTipo.Deposito)
            {
                if (model.Fecha_Inicio == null)
                {
                    toReturn.Add(new Tuple<string, string>("Fecha_Inicio", "Por favor ingrese una fecha valida"));
                }
                if (model.Fecha_Fin == null)
                {
                    toReturn.Add(new Tuple<string, string>("Fecha_Fin", "Por favor ingrese una fecha valida"));
                }
                if (model.Fecha_Inicio != null && model.Fecha_Fin != null && model.Fecha_Fin < model.Fecha_Inicio)
                {
                    toReturn.Add(new Tuple<string, string>("Fecha_Inicio", "Por favor ingrese un intervalo de fechas valido"));
                }
            }
            if (string.IsNullOrEmpty(model.Concepto))
            {
                toReturn.Add(new Tuple<string, string>("Concepto", "Por favor ingrese una descripción"));
            }
            if (model.Monto == 0)
            {
                toReturn.Add(new Tuple<string, string>("Monto", "Por favor ingrese un monto"));
            }
            return toReturn;
        }

        private List<Tuple<string, string>> GetErrorEdit(EditViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();

            if (model.CuentaId == Guid.Empty)
            {
                toReturn.Add(new Tuple<string, string>("CuentaId", "Por favor seleccione una cuenta"));
            }
            if (model.Tipo == TransaccionTipo.Deposito)
            {
                if (model.Fecha_Inicio == null)
                {
                    toReturn.Add(new Tuple<string, string>("Fecha_Inicio", "Por favor ingrese una fecha valida"));
                }
                if (model.Fecha_Fin == null)
                {
                    toReturn.Add(new Tuple<string, string>("Fecha_Fin", "Por favor ingrese una fecha valida"));
                }
                if (model.Fecha_Inicio != null && model.Fecha_Fin != null && model.Fecha_Fin < model.Fecha_Inicio)
                {
                    toReturn.Add(new Tuple<string, string>("Fecha_Inicio", "Por favor ingrese un intervalo de fechas valido"));
                }
            }
            if (string.IsNullOrEmpty(model.Concepto))
            {
                toReturn.Add(new Tuple<string, string>("Concepto", "Por favor ingrese una descripción"));
            }
            if (model.Monto == 0)
            {
                toReturn.Add(new Tuple<string, string>("Monto", "Por favor ingrese un monto"));
            }
            return toReturn;
        }
        
        #endregion






        #region Interface

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(TransaccionTipo tipo)
        {
            AddViewModel addViewModel = new AddViewModel();
            addViewModel.Tipo = tipo;
            addViewModel.Fecha_Inicio = DateTime.Now;
            addViewModel.Fecha_Fin = DateTime.Now;
            addViewModel.Cuentas = ObtenerCuentas();
            return PartialView(addViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            TransaccionRepository transaccionRepository = container.Resolve<TransaccionRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresAdd(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                Transaccion transaccion = new Transaccion();
                transaccion.CajaBancoId = model.CuentaId;
                transaccion.Tipo = model.Tipo;
                transaccion.Fecha_Ini = model.Fecha_Inicio;
                transaccion.Fecha_Fin = model.Fecha_Fin;
                transaccion.Fecha_Transaccion = DateTime.Now;
                transaccion.Concepto = model.Concepto;
                transaccion.Deposito = model.Tipo == TransaccionTipo.Deposito ? model.Monto : 0;
                transaccion.Retiro = model.Tipo == TransaccionTipo.Retiro ? model.Monto : 0;
                transaccion.Saldo = 0;
                
                try
                {
                    transaccionRepository.Agregar(transaccion);

                    model.Result = EnumActionResult.Saved;
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

            model.Cuentas = ObtenerCuentas();
            return PartialView(model);
        }

        public ActionResult Edit(Guid transaccionId)
        {
            TransaccionRepository transaccionRepository = container.Resolve<TransaccionRepository>();
            Transaccion transaccion = transaccionRepository.ObtenerPorId(transaccionId);

            EditViewModel editViewModel = new EditViewModel();
            editViewModel.TransaccionId = transaccion.TransaccionId;
            editViewModel.CuentaId = transaccion.CajaBancoId;
            editViewModel.Tipo = transaccion.Tipo;
            editViewModel.Fecha_Inicio = transaccion.Fecha_Ini;
            editViewModel.Fecha_Fin = transaccion.Fecha_Fin;
            editViewModel.Concepto = transaccion.Concepto;
            editViewModel.Monto = transaccion.Tipo == TransaccionTipo.Retiro ? transaccion.Retiro : transaccion.Deposito;

            editViewModel.Cuentas = ObtenerCuentas();

            return PartialView(editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            TransaccionRepository transaccionRepository = container.Resolve<TransaccionRepository>();

            ModelState.Clear();

            List<Tuple<string, string>> errores = GetErrorEdit(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                Transaccion transaccion = new Transaccion();
                transaccion.TransaccionId = model.TransaccionId;
                transaccion.CajaBancoId = model.CuentaId;
                transaccion.Tipo = model.Tipo;
                transaccion.Fecha_Ini = model.Fecha_Inicio;
                transaccion.Fecha_Fin = model.Fecha_Fin;
                transaccion.Fecha_Transaccion = DateTime.Now;
                transaccion.Concepto = model.Concepto;
                transaccion.Deposito = model.Tipo == TransaccionTipo.Deposito ? model.Monto : 0;
                transaccion.Retiro = model.Tipo == TransaccionTipo.Retiro ? model.Monto : 0;
                transaccion.Saldo = 0;

                try
                {
                    transaccionRepository.Editar(transaccion);

                    model.Result = EnumActionResult.Saved;
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

            model.Cuentas = ObtenerCuentas();
            return PartialView(model);
        }

        public ActionResult Delete(Guid transaccionId)
        {
            DeleteViewModel deleteViewModel = new DeleteViewModel();
            deleteViewModel.TransaccionId = transaccionId;

            return PartialView(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel model)
        {
            TransaccionRepository transaccionRepository = container.Resolve<TransaccionRepository>();

            ModelState.Clear();
            //Se debe validar que no tenga relaciones con otras entidades caso contrario se mostrara un mensaje

            if (ModelState.IsValid)
            {
                try
                {
                    transaccionRepository.Eliminar(model.TransaccionId);

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
            TransaccionRepository transaccionRepository = container.Resolve<TransaccionRepository>();
            List<NavegadorViewModel> toReturn = transaccionRepository.ObtenerTodo().Where(w => w.Cuenta.Tipo == CajaBancoTipo.Banco).Select(t => new NavegadorViewModel()
            {
                TransaccionId = t.TransaccionId,
                Nro_Cuenta = t.Cuenta.Nombre,
                Tipo = t.Tipo.ToString(),
                Fecha_Ini = t.Fecha_Ini != null ? t.Fecha_Ini : null,
                Fecha_Fin = t.Fecha_Fin != null ? t.Fecha_Fin : null,
                Fecha_Transaccion = t.Fecha_Transaccion,
                Concepto = t.Concepto,
                Saldo = t.Tipo == TransaccionTipo.Retiro ? t.Retiro.ToString() : t.Deposito.ToString(),
            }).OrderBy(y => y.Fecha_Transaccion).ToList();

            return Json(toReturn.ToDataSourceResult(request));
        }

        public List<DropdownListViewModel> ObtenerCuentas()
        {
            CajaBancoRepository cuentaRepository = container.Resolve<CajaBancoRepository>();
            List<DropdownListViewModel> toReturn = cuentaRepository.ObtenerTodo().Where(w => w.Tipo == CajaBancoTipo.Banco).Select(x => new DropdownListViewModel()
            {
                Id = x.CajaBancoId,
                Nombre = x.Nombre
            }).OrderBy(y => y.Nombre).ToList();
            if (toReturn.Count() > 0)
            {
                toReturn.Insert(0, new DropdownListViewModel() { Id = Guid.Empty, Nombre = "Seleccione una cuenta" });
            }
            else
            {
                toReturn.Add(new DropdownListViewModel() { Id = Guid.Empty, Nombre = "No hay datos" });
            }
            return toReturn;
        }
        
        #endregion
    }
}