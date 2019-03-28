using DS.Motel.Clients.Web.Areas.Items.Models.Consumo;
using DS.Motel.Data.AddressBook;
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
    public class ConsumoController : Controller
    {
        #region Fields & Properties

        private IUnityContainer container;

        #endregion






        #region Constructors

        public ConsumoController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion



        #region Validacion

        private List<Tuple<string, string>> GetErroresAdd(AddViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            ItemRepository itemRepository = container.Resolve<ItemRepository>();
            ProductoRepository productoRepository = container.Resolve<ProductoRepository>();

            if (model.Detalles != null && model.Detalles.Count() > 0)
            {
                for (int i = 0; i < model.Detalles.Count() - 1; i++)
                {
                    if (model.Detalles[i].ItemId == Guid.Empty && model.Detalles[i].ProductoId == Guid.Empty)
                    {
                        model.Detalles[i].ItemIdError = "Por favor seleccione un item";
                        toReturn.Add(new Tuple<string, string>("ErrorMessage", "ErrorMessage"));
                    }
                    if (model.Detalles[i].Cantidad == 0)
                    {
                        model.Detalles[i].CantidadError = "Por favor ingrese una cantidad";
                        toReturn.Add(new Tuple<string, string>("ErrorMessage", "ErrorMessage"));
                    }
                    if (model.Detalles.Count(c => c.ItemId == model.Detalles[i].ItemId && model.Detalles[i].ItemId != Guid.Empty) > 1)
                    {
                        model.Detalles[i].ItemIdError = "Este ítem ya fue agregado a la lista";
                        toReturn.Add(new Tuple<string, string>("ErrorMessage", "ErrorMessage"));
                    }
                    else if (model.Detalles[i].ItemId != null)
                    {
                        Item item = itemRepository.ObtenerPorId(model.Detalles[i].ItemId);
                        if (item != null)
                        {
                            if ((item.Cantidad_Stock - model.Detalles[i].Cantidad) < 0)
                            {
                                model.Detalles[i].CantidadError = "El stock es insuficiente, se tiene en stock: " + item.Cantidad_Stock;
                                toReturn.Add(new Tuple<string, string>("ErrorMessage", "ErrorMessage"));
                            }
                        }
                    }

                    if (model.Detalles.Count(c => c.ProductoId == model.Detalles[i].ProductoId && model.Detalles[i].ProductoId != Guid.Empty) > 1)
                    {
                        model.Detalles[i].ItemIdError = "Este producto ya fue agregado a la lista";
                        toReturn.Add(new Tuple<string, string>("ErrorMessage", "ErrorMessage"));
                    }
                    else if (model.Detalles[i].ItemId != null)
                    {
                        Producto producto = productoRepository.ObtenerPorId(model.Detalles[i].ProductoId);
                        if (producto != null)
                        {
                            if ((producto.Cantidad_Stock - model.Detalles[i].Cantidad) < 0)
                            {
                                model.Detalles[i].CantidadError = "El stock es insuficiente, se tiene en stock: " + producto.Cantidad_Stock;
                                toReturn.Add(new Tuple<string, string>("ErrorMessage", "ErrorMessage"));
                            }
                        }
                    }
                }
            }
            return toReturn;
        }

        private List<Tuple<string, string>> GetErroresDelete(DeleteViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            ConsumoRepository consumoRepository = container.Resolve<ConsumoRepository>();

            if (string.IsNullOrEmpty(model.Observacion))
            {
                toReturn.Add(new Tuple<string, string>("Observacion", "Por favor ingrese el motivo"));
            }
            return toReturn;
        }

        #endregion



        #region Events

        public ActionResult Add(Guid suiteId)
        {
            ConsumoRepository consumoRepository = container.Resolve<ConsumoRepository>();
            UsoHabitacionRepository usoHabitacionRepository = container.Resolve<UsoHabitacionRepository>();

            AddViewModel addViewModel = new AddViewModel();
            UsoHabitacion usoHabitacion = usoHabitacionRepository.ObtenerPorSuiteId(suiteId);
            List<Consumo> consumos = consumoRepository.ObtenerTodo().Where(w => w.UsoHabitacionId == usoHabitacion.UsoHabitacionId && w.Estado == ConsumoEstado.Vendido).OrderBy(o => o.Index).ToList();
            addViewModel.UsoHabitacionId = usoHabitacion.UsoHabitacionId;

            if (consumos != null && consumos.Count() > 0)
            {
                foreach (var item in consumos)
                {
                    addViewModel.Detalles.Add(new AddDetailViewModel() {
                        ConsumoId = item.ConsumoId,
                        Indice = item.Index, 
                        ItemId = item.ItemId.HasValue ? item.ItemId.Value : Guid.Empty,
                        ItemName = item.ItemId.HasValue ? item.Item.Nombre : string.Empty,
                        ProductoId = item.ProductoId.HasValue ? item.ProductoId.Value : Guid.Empty,
                        ProductoName = item.ProductoId.HasValue ? item.Producto.Nombre : string.Empty,
                        Cantidad = item.Cantidad,
                        ItemIdError = "", ProductoIdError = "", CantidadError = "",
                        Borrar = false,
                    });
                }
            }
            addViewModel.Detalles.Add(new AddDetailViewModel() { Indice = addViewModel.Detalles.Count(), ItemName = string.Empty, ProductoName = string.Empty, Borrar = true });

            return PartialView(addViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            ConsumoRepository consumoRepository = container.Resolve<ConsumoRepository>();
            ProductoRepository productoRepository = container.Resolve<ProductoRepository>();
            ItemRepository itemRepository = container.Resolve<ItemRepository>();
            UsoHabitacionRepository usoHabitacionRepository = container.Resolve<UsoHabitacionRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresAdd(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                model.Detalles.RemoveAt(model.Detalles.Count() - 1);
                try
                {
                    List<Consumo> consumo = new List<Consumo>();
                    foreach (var item in model.Detalles)
                    {
                        Producto _producto = productoRepository.ObtenerPorId(item.ProductoId);
                        Item _item = itemRepository.ObtenerPorId(item.ItemId);
                        decimal _costo = _item != null ? _item.Precio : _producto.Costo_venta;
                        
                        consumo.Add(new Consumo()
                        {
                            ConsumoId = item.ConsumoId,
                            ItemId = item.ItemId == Guid.Empty ? null : (Guid?)item.ItemId,
                            ProductoId = item.ProductoId == Guid.Empty ? null : (Guid?)item.ProductoId,
                            Cantidad = item.Cantidad,
                            Costo = _costo,
                            CostoTotal = item.Cantidad * _costo,
                            Entregado = true,
                            Estado = ConsumoEstado.Vendido,
                            Index = item.Indice,
                            UsoHabitacionId = model.UsoHabitacionId,
                        });
                    }

                    consumoRepository.Agregar(consumo);


                    UsoHabitacion usoHabitacion = usoHabitacionRepository.ObtenerPorId(model.UsoHabitacionId);
                    usoHabitacion.Costo_Insumos = consumo.Sum(s => s.CostoTotal);
                    usoHabitacion.Costo_Total = usoHabitacion.Costo_Habitacion + usoHabitacion.Costo_Insumos + usoHabitacion.Costo_Insumos_Externo + usoHabitacion.Costo_tv;
                    usoHabitacionRepository.Editar(usoHabitacion);

                    model.Result = Web.Models.EnumActionResult.Saved;
                }
                catch (Exception ex)
                {
                    model.Result = Web.Models.EnumActionResult.Error;
                }
            }
            else
            {
                model.Result = Web.Models.EnumActionResult.Validation;
            }

            foreach (var item in model.Detalles)
            {
                item.ItemName = string.IsNullOrEmpty(item.ItemName) ? string.Empty : item.ItemName;
                item.ProductoName = string.IsNullOrEmpty(item.ProductoName) ? string.Empty : item.ProductoName;
                item.ItemIdError = string.IsNullOrEmpty(item.ItemIdError) ? string.Empty : item.ItemIdError;
                item.ProductoIdError = string.IsNullOrEmpty(item.ProductoIdError) ? string.Empty : item.ProductoIdError;
                item.CantidadError = string.IsNullOrEmpty(item.CantidadError) ? string.Empty : item.CantidadError;
            }

            return PartialView(model);
        }

        public ActionResult Delete(Guid consumoId)
        {
            DeleteViewModel deleteViewModel = new DeleteViewModel();
            deleteViewModel.ConsumoId = consumoId;

            return PartialView(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel model)
        {
            ConsumoRepository consumoRepository = container.Resolve<ConsumoRepository>();
            UsoHabitacionRepository usoHabitacionRepository = container.Resolve<UsoHabitacionRepository>();

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
                    Guid usoHabitacionId = consumoRepository.ObtenerPorId(model.ConsumoId).UsoHabitacionId;
                    consumoRepository.Eliminar(model.ConsumoId, model.Observacion);

                    List<Consumo> consumos = consumoRepository.ObtenerPorUsoHabitacionId(usoHabitacionId).Where(w => w.Estado == ConsumoEstado.Vendido).ToList();

                    UsoHabitacion usoHabitacion = usoHabitacionRepository.ObtenerPorId(usoHabitacionId);
                    usoHabitacion.Costo_Insumos = consumos.Count() > 0 ? consumos.Sum(s => s.CostoTotal) : 0;
                    usoHabitacion.Costo_Total = usoHabitacion.Costo_Habitacion + usoHabitacion.Costo_Insumos + usoHabitacion.Costo_Insumos_Externo + usoHabitacion.Costo_tv;
                    usoHabitacionRepository.Editar(usoHabitacion);

                    model.Result = Web.Models.EnumActionResult.Saved;
                }
                catch (Exception ex)
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


        #region Others
        public ActionResult LoadGrid([DataSourceRequest]DataSourceRequest request, Guid suiteId)
        {
            ConsumoRepository consumoRepository = container.Resolve<ConsumoRepository>();
            UsoHabitacionRepository usoHabitacionRepository = container.Resolve<UsoHabitacionRepository>();
            UsoHabitacion usoHabitacion = usoHabitacionRepository.ObtenerPorSuiteId(suiteId);

            List<NavegadorViewModel> toReturn = consumoRepository.ObtenerTodo().Where(w => w.UsoHabitacionId == usoHabitacion.UsoHabitacionId && w.Estado == ConsumoEstado.Vendido).Select(t => new NavegadorViewModel()
            {
                ConsumoId = t.ConsumoId,
                ItemNombre = t.Item != null ? t.Item.Nombre : string.Empty,
                ProductoNombre = t.Producto != null ? t.Producto.Nombre : string.Empty,
                Cantidad = t.Cantidad.ToString(),
                Total = t.CostoTotal.ToString(),
                Index = t.Index,
            }).OrderBy(y => y.Index).ToList();

            return Json(toReturn.ToDataSourceResult(request));
        }

        [HttpGet]
        public JsonResult LoadItem(string term)
        {
            ItemRepository itemRepository = container.Resolve<ItemRepository>();

            var toReturn = itemRepository.ObtenerTodo().Where(w => w.Nombre.Contains(term) && w.EsVendible == true)
                .Select(t => new { value = t.ItemId, label = t.Nombre }).OrderBy(o => o.label).ToList();

            if (toReturn.Count() == 0)
            {
                toReturn.Add(new { value = Guid.Empty, label = "No hay datos" });
            }

            return Json(toReturn, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LoadProducto(string term)
        {
            ProductoRepository productoRepository = container.Resolve<ProductoRepository>();

            var toReturn = productoRepository.ObtenerTodo().Where(w => w.Nombre.Contains(term))
                .Select(t => new { value = t.ProductoId, label = t.Nombre }).OrderBy(o => o.label).ToList();

            if (toReturn.Count() == 0)
            {
                toReturn.Add(new { value = Guid.Empty, label = "No hay datos" });
            }

            return Json(toReturn, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}