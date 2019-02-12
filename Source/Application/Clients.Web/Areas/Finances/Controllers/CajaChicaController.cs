using DS.Motel.Clients.Web.Areas.Finances.Models.CajaChica;
using DS.Motel.Clients.Web.Models;
using DS.Motel.Data.Entities;
using DS.Motel.Data.Finances;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace DS.Motel.Clients.Web.Areas.Finances.Controllers
{
    public class CajaChicaController : Controller
    {
        #region Fields & properties 

        private IUnityContainer container;

        #endregion






        #region Constructor
        public CajaChicaController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion






        #region Validacion

        private List<Tuple<string, string>> GetErroresAdd(AddViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();

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
                transaccion.CajaBancoId = new Guid("11111111-2222-3333-4444-555555555555");
                transaccion.Tipo = model.Tipo;
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

            return PartialView(model);
        }

        public ActionResult Edit(Guid transaccionId)
        {
            TransaccionRepository transaccionRepository = container.Resolve<TransaccionRepository>();
            Transaccion transaccion = transaccionRepository.ObtenerPorId(transaccionId);

            EditViewModel editViewModel = new EditViewModel();
            editViewModel.TransaccionId = transaccion.TransaccionId;
            editViewModel.Tipo = transaccion.Tipo;
            editViewModel.Concepto = transaccion.Concepto;
            editViewModel.Monto = transaccion.Tipo == TransaccionTipo.Retiro ? transaccion.Retiro : transaccion.Deposito;

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
                transaccion.CajaBancoId = new Guid("11111111-2222-3333-4444-555555555555");
                transaccion.Tipo = model.Tipo;
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
            List<NavegadorViewModel> toReturn = transaccionRepository.ObtenerTodo().Where(w => w.Cuenta.Tipo == CajaBancoTipo.Caja)
                .OrderByDescending(o => o.Fecha_Transaccion).ToList().Select(t => new NavegadorViewModel()
            {
                TransaccionId = t.TransaccionId,
                Nro_Cuenta = t.Cuenta.Nombre,
                Tipo = t.Tipo.ToString(),
                Fecha_Transaccion = t.Fecha_Transaccion.ToString("dd/MM/yyyy"),
                Concepto = t.Concepto,
                Saldo = t.Tipo == TransaccionTipo.Retiro ? t.Retiro.ToString() : t.Deposito.ToString(),
            }).ToList();

            return Json(toReturn.ToDataSourceResult(request));
        }

        #endregion
    }
}