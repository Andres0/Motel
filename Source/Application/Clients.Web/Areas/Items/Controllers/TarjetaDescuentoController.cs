using DS.Motel.Clients.Web.Areas.Items.Models.TarjetaDescuento;
using DS.Motel.Data.Entities;
using DS.Motel.Data.Items;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace DS.Motel.Clients.Web.Areas.Items.Controllers
{
    public class TarjetaDescuentoController : Controller
    {
        #region Fields & properties 

        private IUnityContainer container;

        #endregion






        #region Constructor
        public TarjetaDescuentoController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion






        #region Validacion

        private List<Tuple<string, string>> GetErroresAdd(AddViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            TarjetaDescuentoRepository tarjetaDescuentoRepository = container.Resolve<TarjetaDescuentoRepository>();

            if (string.IsNullOrEmpty(model.Codigo))
            {
                toReturn.Add(new Tuple<string, string>("Codigo", "Por favor ingrese un Codigo"));
            }
            else
            {
                if (tarjetaDescuentoRepository.ObtenerTodo().Count(c => c.Codigo == model.Codigo) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Codigo", "Ya existe una tarjeta con el mismo Codigo"));
                }
            }
            return toReturn;
        }

        private List<Tuple<string, string>> GetErroresEdit(EditViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            TarjetaDescuentoRepository tarjetaDescuentoRepository = container.Resolve<TarjetaDescuentoRepository>();

            if (string.IsNullOrEmpty(model.Codigo))
            {
                toReturn.Add(new Tuple<string, string>("Codigo", "Por favor ingrese un codigo"));
            }
            else
            {
                if (tarjetaDescuentoRepository.ObtenerTodo().Count(c => c.Codigo == model.Codigo && c.Codigo != model.Codigo) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Codigo", "Ya existe un Codigo con el mismo nombre"));
                }
            }
            return toReturn;
        }

        private List<Tuple<string, string>> GetErroresDelete(DeleteViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            TarjetaDescuentoRepository tarjetaDescuentoRepository = container.Resolve<TarjetaDescuentoRepository>();

            if (tarjetaDescuentoRepository.ObtenerPorId(model.tarjetaDescuentoId).TransaccionId != null)
            {
                toReturn.Add(new Tuple<string, string>("ErrorMessage", "No se pudo borrar la tarjeta de descuento porque se encuentra vendida"));
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
            TarjetaDescuentoRepository tarjetaDescuentoRepository = container.Resolve<TarjetaDescuentoRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresAdd(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                TarjetaDescuento tarjetaDescuento = new TarjetaDescuento();
                tarjetaDescuento.Activado = model.Activado;
                tarjetaDescuento.Codigo = model.Codigo;
                tarjetaDescuento.Costo = model.Costo;
                tarjetaDescuento.Estados = TarjetaEstados.Creada;
                tarjetaDescuento.FechaCreacion = DateTime.Now;
                tarjetaDescuento.FechaUltimoUso = null;
                tarjetaDescuento.NroUsadas = 0;
                tarjetaDescuento.NroUsos = model.NroUsos;
                tarjetaDescuento.Porcentaje = model.Porcentaje;
                
                try
                {
                    tarjetaDescuentoRepository.Agregar(tarjetaDescuento);

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



        public ActionResult Edit(Guid TarjetaDescuentoId)
        {
            TarjetaDescuentoRepository tarjetadescuentoRepository = container.Resolve<TarjetaDescuentoRepository>();
            TarjetaDescuento tarjetaDescuento = tarjetadescuentoRepository.ObtenerTodo().SingleOrDefault(s => s.TarjetaDescuentoId == TarjetaDescuentoId);

            EditViewModel editViewModel = new EditViewModel();
            editViewModel.TarjetaDescuentoId = tarjetaDescuento.TarjetaDescuentoId;
            editViewModel.Codigo = tarjetaDescuento.Codigo;
            editViewModel.NroUsos = tarjetaDescuento.NroUsos;
            editViewModel.Porcentaje = tarjetaDescuento.Porcentaje;
            editViewModel.Costo = tarjetaDescuento.Costo;
            editViewModel.Activado = tarjetaDescuento.Activado;

            return PartialView(editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            TarjetaDescuentoRepository tarjetaDescuentoRepository = container.Resolve<TarjetaDescuentoRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresEdit(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                TarjetaDescuento tarjeta = new TarjetaDescuento();
                tarjeta.TarjetaDescuentoId = model.TarjetaDescuentoId;
                tarjeta.Codigo = model.Codigo;
                tarjeta.NroUsos = model.NroUsos;
                tarjeta.Porcentaje = model.Porcentaje;
                tarjeta.Costo = model.Costo;
                tarjeta.Activado = model.Activado;

                try
                {
                    tarjetaDescuentoRepository.Editar(tarjeta);

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

        public ActionResult Delete(Guid tarjetaDescuentoId)
        {
            DeleteViewModel deleteViewModel = new DeleteViewModel();
            deleteViewModel.tarjetaDescuentoId = tarjetaDescuentoId;

            return PartialView(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel model)
        {
            TarjetaDescuentoRepository tarjetaDescuentoRepository = container.Resolve<TarjetaDescuentoRepository>();

            ModelState.Clear();
            //Se debe validar que no tenga relaciones con otras entidades caso contrario se mostrara un mensaje

            if (ModelState.IsValid)
            {
                try
                {
                    tarjetaDescuentoRepository.Eliminar(model.tarjetaDescuentoId);

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

        public ActionResult Sale(Guid tarjetaDescuentoId)
        {
            SaleViewModel saleViewModel = new SaleViewModel();
            saleViewModel.tarjetaDescuentoId = tarjetaDescuentoId;

            return PartialView(saleViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sale(SaleViewModel model)
        {
            TarjetaDescuentoRepository tarjetaDescuentoRepository = container.Resolve<TarjetaDescuentoRepository>();

            ModelState.Clear();
            //Se debe validar que no tenga relaciones con otras entidades caso contrario se mostrara un mensaje

            if (ModelState.IsValid)
            {
                try
                {
                    tarjetaDescuentoRepository.Vender(model.tarjetaDescuentoId);

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
            TarjetaDescuentoRepository tarjetaDescuentoRepository = container.Resolve<TarjetaDescuentoRepository>();
            List<NavegadorViewModel> toReturn = tarjetaDescuentoRepository.ObtenerTodo().ToList().Select(t => new NavegadorViewModel()
            {
                TarjetaDescuentoId = t.TarjetaDescuentoId,
                Porcentaje = t.Porcentaje,
                Codigo = t.Codigo,
                Costo = t.Costo,
                Activado = t.Activado ? "Si" : "No",
                FechaCreacion = t.FechaCreacion.ToString("dd/MM/yyyy"),
                FechaUltimoUso = t.FechaUltimoUso != null ? t.FechaUltimoUso.Value.ToString("dd/MM/yyyy") : string.Empty,
                NroUsadas = t.NroUsadas,
                EsVendido = t.TransaccionId == null ? "No" : "Si",
            }).OrderBy(y => y.Codigo).ToList();

            return Json(toReturn.ToDataSourceResult(request));
        }

        #endregion
    }
}