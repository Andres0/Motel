using DS.Motel.Clients.Web.Areas.Inventario.Models.Inventario;
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
    public class InventarioController : Controller
    {
        #region Fields & properties 

        private IUnityContainer container;

        #endregion



        #region Constructor

        public InventarioController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion



        #region Validacion

        private List<Tuple<string, string>> GetErroresAdd(AddViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            InventarioRepository inventarioRepository = container.Resolve<InventarioRepository>();
            ItemRepository itemRepository = container.Resolve<ItemRepository>();

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
                for (int i = 0; i < model.Detalles.Count()-1; i++)
                {
                    if (model.Detalles[i].ItemId == Guid.Empty)
                    {
                        model.Detalles[i].ItemIdError = "Por favor seleccione un item";
                        toReturn.Add(new Tuple<string, string>("Detalles[" + i + "].ItemId", "Por favor seleccione un item"));
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
                    if (model.Detalles.Count(c => c.ItemId == model.Detalles[i].ItemId) > 1)
                    {
                        model.Detalles[i].ItemIdError = "Este ítem ya fue agregado a la lista";
                        toReturn.Add(new Tuple<string, string>("Detalles[" + i + "].ItemId", "Este ítem ya fue agregado a la lista"));
                    }
                    else if (model.Detalles[i].ItemId != null && (model.Tipo == InventarioTipo.Egreso || model.Tipo == InventarioTipo.Transferencia))
                    {
                        Item item = itemRepository.ObtenerPorId(model.Detalles[i].ItemId);
                        if (item != null)
                        {
                            if ((item.Cantidad_Stock - model.Detalles[i].Cantidad) < 0)
                            {
                                model.Detalles[i].CantidadError = "El stock es insuficiente, se tiene en stock: " + item.Cantidad_Stock;
                                toReturn.Add(new Tuple<string, string>("Detalles[" + i + "].Cantidad", "El stock es insuficiente, se tiene en stock: " + item.Cantidad_Stock));
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
            InventarioRepository inventarioRepository = container.Resolve<InventarioRepository>();
            ItemRepository itemRepository = container.Resolve<ItemRepository>();

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
                    if (model.Detalles[i].ItemId == Guid.Empty)
                    {
                        model.Detalles[i].ItemIdError = "Por favor seleccione un item";
                        toReturn.Add(new Tuple<string, string>("Detalles[" + i + "].ItemId", "Por favor seleccione un item"));
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
                    if (model.Detalles.Count(c => c.ItemId == model.Detalles[i].ItemId) > 1)
                    {
                        model.Detalles[i].ItemIdError = "Este ítem ya fue agregado a la lista";
                        toReturn.Add(new Tuple<string, string>("Detalles[" + i + "].ItemId", "Este ítem ya fue agregado a la lista"));
                    }
                    else if (model.Detalles[i].ItemId != null && (model.Tipo == InventarioTipo.Egreso || model.Tipo == InventarioTipo.Transferencia))
                    {
                        Item item = itemRepository.ObtenerPorId(model.Detalles[i].ItemId);
                        InventarioDetalle inventarioDetalle = inventarioRepository.ObtenerPorId(model.InventarioId).Detalle.SingleOrDefault(s => s.ItemId == model.Detalles[i].ItemId);
                        decimal count = inventarioDetalle != null ? inventarioDetalle.Cantidad : Convert.ToDecimal("0");
                        if (item != null)
                        {
                            if ((item.Cantidad_Stock - model.Detalles[i].Cantidad + count) < 0)
                            {
                                model.Detalles[i].CantidadError = "El stock es insuficiente, se tiene en stock: " + item.Cantidad_Stock;
                                toReturn.Add(new Tuple<string, string>("Detalles[" + i + "].Cantidad", "El stock es insuficiente, se tiene en stock: " + item.Cantidad_Stock));
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
            addViewModel.Detalles = new List<AddDetailViewModel>() { new AddDetailViewModel() { Indice = 0, ItemName = string.Empty, Borrar = false } };
            addViewModel.Almacenes = ObtenerAlmacenes();

            return PartialView(addViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            InventarioRepository inventarioRepository = container.Resolve<InventarioRepository>();

            //LIMPIA EL MODEO DE ERRORES
            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresAdd(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }
            

            if (ModelState.IsValid)
            {
                model.Detalles.RemoveAt(model.Detalles.Count()-1);
                DS.Motel.Data.Entities.Inventario inventario = new DS.Motel.Data.Entities.Inventario();
                inventario.Fecha = model.Fecha;
                inventario.Descripcion = model.Descripcion;
                inventario.Total = model.Detalles.Sum(s => (s.Cantidad * s.Costo));
                inventario.Tipo = model.Tipo;
                inventario.DeAlmacenId = model.DeId == Guid.Empty ? null : (Guid?)model.DeId;
                inventario.AAlmacenId = model.AId == Guid.Empty ? null : (Guid?)model.AId;
                foreach (var detalle in model.Detalles)
                {
                    inventario.Detalle.Add(new InventarioDetalle() { InventarioId = inventario.InventarioId, Indice = detalle.Indice, ItemId = detalle.ItemId, Cantidad = detalle.Cantidad, Costo = detalle.Costo });
                }

                try
                {
                    inventarioRepository.Agregar(inventario);

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

            foreach (var item in model.Detalles )
            {
                item.ItemName = string.IsNullOrEmpty(item.ItemName) ? string.Empty : item.ItemName;
                item.ItemIdError = string.IsNullOrEmpty(item.ItemIdError) ? string.Empty : item.ItemIdError;
                item.CantidadError = string.IsNullOrEmpty(item.CantidadError) ? string.Empty : item.CantidadError;
                item.CostoError = string.IsNullOrEmpty(item.CostoError) ? string.Empty : item.CostoError;
            }
            model.Almacenes = ObtenerAlmacenes();
            return PartialView(model);
        }

        public ActionResult Edit(Guid inventarioId)
        {
            InventarioRepository inventarioRepository = container.Resolve<InventarioRepository>();
            DS.Motel.Data.Entities.Inventario inventario = inventarioRepository.ObtenerPorId(inventarioId);

            EditViewModel editViewModel = new EditViewModel();
            editViewModel.InventarioId = inventario.InventarioId;
            editViewModel.Tipo = inventario.Tipo;
            editViewModel.Fecha = inventario.Fecha;
            editViewModel.Descripcion = inventario.Descripcion;
            editViewModel.Total = inventario.Total;
            editViewModel.DeId = inventario.DeAlmacenId == null ? Guid.Empty : inventario.DeAlmacenId.Value;
            editViewModel.AId = inventario.AAlmacenId == null ? Guid.Empty : inventario.AAlmacenId.Value;

            editViewModel.Almacenes = ObtenerAlmacenes();
            foreach (var item in inventario.Detalle.OrderBy(o => o.Indice))
            {
                editViewModel.Detalles.Add(new EditDetailViewModel()
                {
                    ItemId = item.ItemId,
                    ItemName = item.Item.Nombre,
                    Cantidad = item.Cantidad,
                    Costo = item.Costo,
                    Total = item.Cantidad * item.Costo,
                    Indice = item.Indice,
                    Borrar = true,
                });
            }
            editViewModel.Detalles.Add(new EditDetailViewModel() { Indice = inventario.Detalle.Count(), ItemName = string.Empty, Borrar = false });

            return PartialView(editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            InventarioRepository inventarioRepository = container.Resolve<InventarioRepository>();

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
                DS.Motel.Data.Entities.Inventario inventario = new DS.Motel.Data.Entities.Inventario();
                inventario.InventarioId = model.InventarioId;
                inventario.Fecha = model.Fecha;
                inventario.Descripcion = model.Descripcion;
                inventario.Total = model.Detalles.Sum(s => (s.Cantidad * s.Costo));
                inventario.Tipo = model.Tipo;
                inventario.DeAlmacenId = model.DeId == Guid.Empty ? null : (Guid?)model.DeId;
                inventario.AAlmacenId = model.AId == Guid.Empty ? null : (Guid?)model.AId;
                foreach (var detalle in model.Detalles)
                {
                    inventario.Detalle.Add(new InventarioDetalle() { InventarioId = inventario.InventarioId, Indice = detalle.Indice, ItemId = detalle.ItemId, Cantidad = detalle.Cantidad, Costo = detalle.Costo });
                }

                try
                {
                    inventarioRepository.Editar(inventario);

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
                item.ItemName = string.IsNullOrEmpty(item.ItemName) ? string.Empty : item.ItemName;
                item.ItemIdError = string.IsNullOrEmpty(item.ItemIdError) ? string.Empty : item.ItemIdError;
                item.CantidadError = string.IsNullOrEmpty(item.CantidadError) ? string.Empty : item.CantidadError;
                item.CostoError = string.IsNullOrEmpty(item.CostoError) ? string.Empty : item.CostoError;
            }
            model.Almacenes = ObtenerAlmacenes();
            return PartialView(model);
        }

        public ActionResult Delete(Guid inventarioId)
        {
            DeleteViewModel deleteViewModel = new DeleteViewModel();
            deleteViewModel.InventarioId = inventarioId;

            return PartialView(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel model)
        {
            InventarioRepository inventarioRepository = container.Resolve<InventarioRepository>();

            ModelState.Clear();
            //Se debe validar que no tenga relaciones con otras entidades caso contrario se mostrara un mensaje

            if (ModelState.IsValid)
            {
                try
                {
                    inventarioRepository.Eliminar(model.InventarioId);

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
            InventarioRepository inventarioRepository = container.Resolve<InventarioRepository>();
            List<NavegadorViewModel> toReturn = inventarioRepository.ObtenerTodo().ToList().Select(t => new NavegadorViewModel()
            {
                MovimientoId = t.InventarioId,
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
        public JsonResult LoadItem(string term)
        {
            ItemRepository itemRepository = container.Resolve<ItemRepository>();

            var toReturn = itemRepository.ObtenerTodo().Where(w => w.Nombre.Contains(term))
                .Select(t => new { value = t.ItemId, label = t.Nombre }).OrderBy(o => o.label).ToList();

            if (toReturn.Count() == 0)
            {
                toReturn.Add(new { value = Guid.Empty, label = "No hay datos" });
            }

            return Json(toReturn, JsonRequestBehavior.AllowGet);
        }
        
        #endregion  
    }
}