using DS.Motel.Clients.Web.Areas.Inventario.Models.InventarioProducto;
using DS.Motel.Clients.Web.Models;
using DS.Motel.Data.Entities;
using DS.Motel.Data.Inventarios;
using DS.Motel.Data.Items;
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
    public class InventarioProductoController : Controller
    {
        #region Fields & properties 

        private IUnityContainer container;

        #endregion



        #region Constructor

        public InventarioProductoController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion



        #region Validacion

        private List<Tuple<string, string>> GetErroresAdd(AddViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            ProductoRepository productoRepository = container.Resolve<ProductoRepository>();

            if (model.Fecha == new DateTime())
            {
                toReturn.Add(new Tuple<string, string>("Fecha", "Por favor ingrese una fecha"));
            }
            if (model.Tipo == InventarioTipo.Ingreso && model.AId == Guid.Empty)
            {
                toReturn.Add(new Tuple<string, string>("AId", "Por favor seleccione un almacen"));
            }
            if (model.Tipo == InventarioTipo.Egreso && model.DeId == Guid.Empty)
            {
                toReturn.Add(new Tuple<string, string>("DeId", "Por favor seleccione un almacen"));
            }
            if (model.Tipo == InventarioTipo.Transferencia)
            {
                if (model.DeId == null)
                    toReturn.Add(new Tuple<string, string>("DeId", "Por favor seleccione un almacen"));
                if (model.AId == null)
                    toReturn.Add(new Tuple<string, string>("AId", "Por favor seleccione un almacen"));
            }

            if (model.Detalles != null && model.Detalles.Count() > 0)
            {
                for (int i = 0; i < model.Detalles.Count() - 1; i++)
                {
                    if (model.Detalles[i].ProductoId == Guid.Empty)
                    {
                        model.Detalles[i].ProductoIdError = "Por favor seleccione un producto";
                        toReturn.Add(new Tuple<string, string>("Detalles[" + i + "].ProductoId", "Por favor seleccione un producto"));
                    }
                    if (model.Detalles[i].Cantidad == 0)
                    {
                        model.Detalles[i].CantidadError = "Por favor ingrese una cantidad";
                        toReturn.Add(new Tuple<string, string>("Detalles[" + i + "].Cantidad", "Por favor ingrese una cantidad"));
                    }
                    if (model.Tipo == InventarioTipo.Ingreso && model.Detalles[i].Costo == 0)
                    {
                        model.Detalles[i].CostoError = "Por favor ingrese un costo";
                        toReturn.Add(new Tuple<string, string>("Detalles[" + i + "].Costo", "Por favor ingrese un costo"));
                    }
                    if (model.Detalles.Count(c => c.ProductoId == model.Detalles[i].ProductoId) > 1)
                    {
                        model.Detalles[i].ProductoIdError = "Este producto ya fue agregado a la lista";
                        toReturn.Add(new Tuple<string, string>("Detalles[" + i + "].ProductoId", "Este producto ya fue agregado a la lista"));
                    }
                    else if (model.Detalles[i].ProductoId != null && (model.Tipo == InventarioTipo.Egreso || model.Tipo == InventarioTipo.Transferencia))
                    {
                        Producto producto = productoRepository.ObtenerPorId(model.Detalles[i].ProductoId);
                        if (producto != null)
                        {
                            if ((producto.Cantidad_Stock - model.Detalles[i].Cantidad) < 0)
                            {
                                model.Detalles[i].CantidadError = "El stock es insuficiente, se tiene en stock: " + producto.Cantidad_Stock;
                                toReturn.Add(new Tuple<string, string>("Detalles[" + i + "].Cantidad", "El stock es insuficiente, se tiene en stock: " + producto.Cantidad_Stock));
                            }
                        }
                    }
                    else if (model.Detalles[i].ProductoId != null && model.Tipo == InventarioTipo.Ingreso)
                    {
                        Producto producto = productoRepository.ObtenerPorId(model.Detalles[i].ProductoId);
                        foreach (var productoDetalle in producto.Detalle)
                        {
                            if ((productoDetalle.Item.Cantidad_Stock - (productoDetalle.Cantidad * model.Detalles[i].Cantidad)) < 0)
                            {
                                model.Detalles[i].ProductoIdError += "No existe stock suficiente del item " + productoDetalle.Item.Nombre + " </br>";
                            }
                        }
                    }
                }
            }
            return toReturn;
        }

        private List<Tuple<string, string>> GetErroresEdit(EditViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            InventarioProductoRepository inventarioProductoRepository = container.Resolve<InventarioProductoRepository>();
            ProductoRepository productoRepository = container.Resolve<ProductoRepository>();

            if (model.Fecha == new DateTime())
            {
                toReturn.Add(new Tuple<string, string>("Fecha", "Por favor ingrese una fecha"));
            }
            if (model.Tipo == InventarioTipo.Ingreso && model.AId == Guid.Empty)
            {
                toReturn.Add(new Tuple<string, string>("AId", "Por favor seleccione un almacen"));
            }
            if (model.Tipo == InventarioTipo.Egreso && model.DeId == Guid.Empty)
            {
                toReturn.Add(new Tuple<string, string>("DeId", "Por favor seleccione un almacen"));
            }
            if (model.Tipo == InventarioTipo.Transferencia)
            {
                if (model.DeId == null)
                    toReturn.Add(new Tuple<string, string>("DeId", "Por favor seleccione un almacen"));
                if (model.AId == null)
                    toReturn.Add(new Tuple<string, string>("AId", "Por favor seleccione un almacen"));
            }

            if (model.Detalles != null && model.Detalles.Count() > 0)
            {
                for (int i = 0; i < model.Detalles.Count() - 1; i++)
                {
                    if (model.Detalles[i].ProductoId == Guid.Empty)
                    {
                        model.Detalles[i].ProductoIdError = "Por favor seleccione un producto";
                        toReturn.Add(new Tuple<string, string>("Detalles[" + i + "].ProductoId", "Por favor seleccione un producto"));
                    }
                    if (model.Detalles[i].Cantidad == 0)
                    {
                        model.Detalles[i].CantidadError = "Por favor ingrese una cantidad";
                        toReturn.Add(new Tuple<string, string>("Detalles[" + i + "].Cantidad", "Por favor ingrese una cantidad"));
                    }
                    if (model.Tipo == InventarioTipo.Ingreso && model.Detalles[i].Costo == 0)
                    {
                        model.Detalles[i].CostoError = "Por favor ingrese un costo";
                        toReturn.Add(new Tuple<string, string>("Detalles[" + i + "].Costo", "Por favor ingrese un costo"));
                    }
                    if (model.Detalles.Count(c => c.ProductoId == model.Detalles[i].ProductoId) > 1)
                    {
                        model.Detalles[i].ProductoIdError = "Este producto ya fue agregado a la lista";
                        toReturn.Add(new Tuple<string, string>("Detalles[" + i + "].ItemId", "Este producto ya fue agregado a la lista"));
                    }
                    else if (model.Detalles[i].ProductoId != null && (model.Tipo == InventarioTipo.Egreso || model.Tipo == InventarioTipo.Transferencia))
                    {
                        Producto producto = productoRepository.ObtenerPorId(model.Detalles[i].ProductoId);
                        InventarioProductoDetalle inventarioProductoDetalle = inventarioProductoRepository.ObtenerPorId(model.InventarioProductoId).Detalle.SingleOrDefault(s => s.ProductoId == model.Detalles[i].ProductoId);
                        decimal count = inventarioProductoDetalle != null ? inventarioProductoDetalle.Cantidad : Convert.ToDecimal("0");
                        if (producto != null)
                        {
                            if ((producto.Cantidad_Stock - model.Detalles[i].Cantidad + count) < 0)
                            {
                                model.Detalles[i].CantidadError = "El stock es insuficiente, se tiene en stock: " + producto.Cantidad_Stock;
                                toReturn.Add(new Tuple<string, string>("Detalles[" + i + "].Cantidad", "El stock es insuficiente, se tiene en stock: " + producto.Cantidad_Stock));
                            }
                        }
                    }
                }
            }
            return toReturn;
        }

        #endregion



        #region Eventos

        public ActionResult Index(bool? masterload = true)
        {
            ViewData["MasterLoad"] = masterload;
            return View();
        }

        public ActionResult Add(InventarioTipo tipo)
        {
            AddViewModel addViewModel = new AddViewModel();
            addViewModel.Fecha = DateTime.Now;
            addViewModel.Tipo = tipo;
            addViewModel.Detalles = new List<AddDetailViewModel>() { new AddDetailViewModel() { Indice = 0, ProductoName = string.Empty, Borrar = false } };
            addViewModel.Almacenes = ObtenerAlmacenes();

            return PartialView(addViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            InventarioProductoRepository inventarioProductoRepository = container.Resolve<InventarioProductoRepository>();

            //LIMPIA EL MODEO DE ERRORES
            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresAdd(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }


            if (ModelState.IsValid)
            {
                model.Detalles.RemoveAt(model.Detalles.Count() - 1);
                DS.Motel.Data.Entities.InventarioProducto inventarioProducto = new DS.Motel.Data.Entities.InventarioProducto();
                inventarioProducto.Fecha = model.Fecha;
                inventarioProducto.Descripcion = model.Descripcion;
                inventarioProducto.Total = model.Detalles.Sum(s => (s.Cantidad * s.Costo));
                inventarioProducto.Tipo = model.Tipo;
                inventarioProducto.DeAlmacenId = model.DeId == Guid.Empty ? null : (Guid?)model.DeId;
                inventarioProducto.AAlmacenId = model.AId == Guid.Empty ? null : (Guid?)model.AId;
                foreach (var detalle in model.Detalles)
                {
                    inventarioProducto.Detalle.Add(new InventarioProductoDetalle() { InventarioProductoId = inventarioProducto.InventarioProductoId, Indice = detalle.Indice, ProductoId = detalle.ProductoId, Cantidad = detalle.Cantidad, Costo = detalle.Costo });
                }

                try
                {
                    inventarioProductoRepository.Agregar(inventarioProducto);

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

            foreach (var item in model.Detalles)
            {
                item.ProductoName = string.IsNullOrEmpty(item.ProductoName) ? string.Empty : item.ProductoName;
                item.ProductoIdError = string.IsNullOrEmpty(item.ProductoIdError) ? string.Empty : item.ProductoIdError;
                item.CantidadError = string.IsNullOrEmpty(item.CantidadError) ? string.Empty : item.CantidadError;
                item.CostoError = string.IsNullOrEmpty(item.CostoError) ? string.Empty : item.CostoError;
            }
            model.Almacenes = ObtenerAlmacenes();
            return PartialView(model);
        }

        public ActionResult Edit(Guid inventarioProductoId)
        {
            InventarioProductoRepository inventarioProductoRepository = container.Resolve<InventarioProductoRepository>();
            DS.Motel.Data.Entities.InventarioProducto inventarioProducto = inventarioProductoRepository.ObtenerPorId(inventarioProductoId);

            EditViewModel editViewModel = new EditViewModel();
            editViewModel.InventarioProductoId = inventarioProducto.InventarioProductoId;
            editViewModel.Tipo = inventarioProducto.Tipo;
            editViewModel.Fecha = inventarioProducto.Fecha;
            editViewModel.Descripcion = inventarioProducto.Descripcion;
            editViewModel.Total = inventarioProducto.Total;
            editViewModel.DeId = inventarioProducto.DeAlmacenId == null ? Guid.Empty : inventarioProducto.DeAlmacenId.Value;
            editViewModel.AId = inventarioProducto.AAlmacenId == null ? Guid.Empty : inventarioProducto.AAlmacenId.Value;

            editViewModel.Almacenes = ObtenerAlmacenes();
            foreach (var item in inventarioProducto.Detalle.OrderBy(o => o.Indice))
            {
                editViewModel.Detalles.Add(new EditDetailViewModel()
                {
                    ProductoId = item.ProductoId,
                    ProductoName = item.Producto.Nombre,
                    Cantidad = item.Cantidad,
                    Costo = item.Costo,
                    Total = item.Cantidad * item.Costo,
                    Indice = item.Indice,
                    Borrar = true,
                });
            }
            editViewModel.Detalles.Add(new EditDetailViewModel() { Indice = inventarioProducto.Detalle.Count(), ProductoName = string.Empty, Borrar = false });

            return PartialView(editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            InventarioProductoRepository inventarioProductoRepository = container.Resolve<InventarioProductoRepository>();

            //LIMPIA EL MODEO DE ERRORES
            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresEdit(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }


            if (ModelState.IsValid)
            {
                model.Detalles.RemoveAt(model.Detalles.Count() - 1);
                DS.Motel.Data.Entities.InventarioProducto inventarioProducto = new DS.Motel.Data.Entities.InventarioProducto();
                inventarioProducto.InventarioProductoId = model.InventarioProductoId;
                inventarioProducto.Fecha = model.Fecha;
                inventarioProducto.Descripcion = model.Descripcion;
                inventarioProducto.Total = model.Detalles.Sum(s => (s.Cantidad * s.Costo));
                inventarioProducto.Tipo = model.Tipo;
                inventarioProducto.DeAlmacenId = model.DeId == Guid.Empty ? null : (Guid?)model.DeId;
                inventarioProducto.AAlmacenId = model.AId == Guid.Empty ? null : (Guid?)model.AId;
                foreach (var detalle in model.Detalles)
                {
                    inventarioProducto.Detalle.Add(new InventarioProductoDetalle() { InventarioProductoDetalleId = inventarioProducto.InventarioProductoId, Indice = detalle.Indice, ProductoId = detalle.ProductoId, Cantidad = detalle.Cantidad, Costo = detalle.Costo });
                }

                try
                {
                    inventarioProductoRepository.Editar(inventarioProducto);

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

            foreach (var item in model.Detalles)
            {
                item.ProductoName = string.IsNullOrEmpty(item.ProductoName) ? string.Empty : item.ProductoName;
                item.ProductoIdError = string.IsNullOrEmpty(item.ProductoIdError) ? string.Empty : item.ProductoIdError;
                item.CantidadError = string.IsNullOrEmpty(item.CantidadError) ? string.Empty : item.CantidadError;
                item.CostoError = string.IsNullOrEmpty(item.CostoError) ? string.Empty : item.CostoError;
            }
            model.Almacenes = ObtenerAlmacenes();
            return PartialView(model);
        }

        public ActionResult Delete(Guid inventarioProductoId)
        {
            DeleteViewModel deleteViewModel = new DeleteViewModel();
            deleteViewModel.InventarioProductoId = inventarioProductoId;

            return PartialView(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel model)
        {
            InventarioProductoRepository inventarioProductoRepository = container.Resolve<InventarioProductoRepository>();

            ModelState.Clear();
            //Se debe validar que no tenga relaciones con otras entidades caso contrario se mostrara un mensaje

            if (ModelState.IsValid)
            {
                try
                {
                    inventarioProductoRepository.Eliminar(model.InventarioProductoId);

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



        #region Otros

        public ActionResult LoadGrid([DataSourceRequest]DataSourceRequest request)
        {
            InventarioProductoRepository inventarioProductoRepository = container.Resolve<InventarioProductoRepository>();
            List<NavegadorViewModel> toReturn = inventarioProductoRepository.ObtenerTodo().ToList().Select(t => new NavegadorViewModel()
            {
                MovimientoProductoId = t.InventarioProductoId,
                Fecha = t.Fecha,
                Numero = t.Numero.ToString(),
                Almacen = t.Tipo == InventarioTipo.Ingreso ? t.A.Nombre : t.Tipo == InventarioTipo.Egreso ? t.De.Nombre : "De " + t.De.Nombre + " A " + t.A.Nombre,
                Tipo = t.Tipo,
            }).OrderByDescending(y => y.Fecha).ToList();

            return Json(toReturn.ToDataSourceResult(request));
        }

        public List<DropdownListViewModel> ObtenerAlmacenes()
        {
            AlmacenRepository almacenRepository = container.Resolve<AlmacenRepository>();
            List<DropdownListViewModel> toReturn = almacenRepository.ObtenerTodo().Select(x => new DropdownListViewModel()
            {
                Id = x.AlmacenId,
                Nombre = x.Nombre
            }).OrderBy(y => y.Nombre).ToList();

            if (toReturn.Count() > 0)
            {
                toReturn.Insert(0, new DropdownListViewModel() { Id = Guid.Empty, Nombre = "Seleccione un almacen" });
            }
            else
            {
                toReturn.Add(new DropdownListViewModel() { Id = Guid.Empty, Nombre = "No hay datos" });
            }
            return toReturn;
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