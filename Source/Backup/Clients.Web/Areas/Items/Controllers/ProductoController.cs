using DS.Motel.Clients.Web.Areas.Items.Models.Producto;
using DS.Motel.Clients.Web.Models;
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
    public class ProductoController : Controller
    {
        #region Fields & properties 

        private IUnityContainer container;

        #endregion






        #region Constructor

        public ProductoController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion






        #region Validacion

        private List<Tuple<string, string>> GetErroresAdd(AddViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            ProductoRepository productoRepository = container.Resolve<ProductoRepository>();

            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un nombre"));
            }
            else
            {
                if (productoRepository.ObtenerTodo().Count(c => c.Nombre == model.Nombre) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Nombre", "Ya existe un producto con el mismo nombre"));
                }
            }
            if (model.CategoriaId == Guid.Empty)
            {
                toReturn.Add(new Tuple<string, string>("CategoriaId", "Por favor seleccione una categoría"));
            }
            if (model.Costo_Venta == 0)
            {
                toReturn.Add(new Tuple<string, string>("Costo_Venta", "Por favor ingrese un precio de venta"));
            }
            if (model.Detalle == null || model.Detalle.Count() == 0)
            {
                toReturn.Add(new Tuple<string, string>("Detalle", "Debe ingresar un item como minimo"));
            }
            return toReturn;
        }

        private List<Tuple<string, string>> GeterrorEdit(EditViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            ProductoRepository productoRepository = container.Resolve<ProductoRepository>();

            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un nombre"));
            }
            else
            {
                if (productoRepository.ObtenerTodo().Count(c => c.Nombre == model.Nombre && c.ProductoId != model.ProductoId) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Nombre", "Ya existe un producto con el mismo nombre"));
                }
            }
            if (model.CategoriaId == Guid.Empty)
            {
                toReturn.Add(new Tuple<string, string>("CategoriaId", "Por favor seleccione una categoría"));
            }
            if (model.Costo_Venta == 0)
            {
                toReturn.Add(new Tuple<string, string>("Costo_Venta", "Por favor ingrese un precio de venta"));
            }
            if (model.Detalle == null || model.Detalle.Count() == 0)
            {
                toReturn.Add(new Tuple<string, string>("Detalle", "Debe ingresar un item como minimo"));
            }
            return toReturn;
        }
        #endregion






        #region Interface

        public ActionResult Index(bool? masterload = true)
        {
            ViewData["MasterLoad"] = masterload;
            return View();
        }

        public ActionResult Add()
        {
            AddViewModel addViewModel = new AddViewModel();
            addViewModel.CategoriasItems = ObtenerCategorias(ItemCategoriaTipo.Item);
            addViewModel.CategoriasProducto = ObtenerCategorias(ItemCategoriaTipo.Producto);

            return PartialView(addViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            ProductoRepository productoRepository = container.Resolve<ProductoRepository>();

            //LIMPIA EL MODEO DE ERRORES
            ModelState.Clear();

            //OBTENGO LOS POSIBLES ERRORES
            List<Tuple<string, string>> errores = GetErroresAdd(model);

            //CARGO LOS ERRORES AL MODELSTATE
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                Producto producto = new Producto();
                producto.Nombre = model.Nombre;
                producto.Descripcion = model.Descripcion;
                producto.ItemCategoriaId = model.CategoriaId;
                producto.Costo_venta = model.Costo_Venta;
                foreach (var detalle in model.Detalle)
                {
                    producto.Detalle.Add(new ProductoDetalle() { ItemId = detalle.ItemId, Cantidad = detalle.Cantidad });
                }

                try
                {
                    productoRepository.Agregar(producto);

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

            model.CategoriasItems = ObtenerCategorias(ItemCategoriaTipo.Item);
            model.CategoriasProducto = ObtenerCategorias(ItemCategoriaTipo.Producto);
            return PartialView(model);
        }

        public ActionResult Edit(Guid productoId)
        {
            ProductoRepository productoRepository = container.Resolve<ProductoRepository>();
            Producto producto = productoRepository.ObtenerPorId(productoId);

            EditViewModel editViewModel = new EditViewModel();
            editViewModel.ProductoId = producto.ProductoId;
            editViewModel.Nombre = producto.Nombre;
            editViewModel.Descripcion = producto.Descripcion;
            editViewModel.CategoriaId = producto.ItemCategoriaId;
            editViewModel.Costo_Venta = producto.Costo_venta;
            int count = 0;
            foreach (var item in producto.Detalle)
            {
                editViewModel.Detalle.Add(new EditDetalleViewModel() { ItemId = item.ItemId, Nombre = item.Item.Nombre, Cantidad = item.Cantidad, ProductoDetalleId = item.ProductoDetalleId, Index = count });
                count++;
            }

            editViewModel.CategoriasItems = ObtenerCategorias(ItemCategoriaTipo.Item);
            editViewModel.CategoriasProducto = ObtenerCategorias(ItemCategoriaTipo.Producto);

            return PartialView(editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            ProductoRepository productoRepository = container.Resolve<ProductoRepository>();

            ModelState.Clear();

            List<Tuple<string, string>> errores = GeterrorEdit(model);

            //CARGO LOS ERRORES AL MODELSTATE
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                Producto producto = new Producto();
                producto.ProductoId = model.ProductoId;
                producto.Nombre = model.Nombre;
                producto.Descripcion = model.Descripcion;
                producto.ItemCategoriaId = model.CategoriaId;
                producto.Costo_venta = model.Costo_Venta;
                foreach (var detalle in model.Detalle)
                {
                    producto.Detalle.Add(new ProductoDetalle() { ProductoDetalleId = detalle.ProductoDetalleId, ItemId = detalle.ItemId, Cantidad = detalle.Cantidad });
                }

                try
                {
                    productoRepository.Editar(producto);

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

            model.CategoriasItems = ObtenerCategorias(ItemCategoriaTipo.Item);
            model.CategoriasProducto = ObtenerCategorias(ItemCategoriaTipo.Producto);
            return PartialView(model);
        }

        public ActionResult Delete(Guid productoId)
        {
            DeleteViewModel deleteViewModel = new DeleteViewModel();
            deleteViewModel.ProductoId = productoId;

            return PartialView(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel model)
        {
            ProductoRepository productoRepository = container.Resolve<ProductoRepository>();

            ModelState.Clear();
            //Se debe validar que no tenga relaciones con otras entidades caso contrario se mostrara un mensaje

            if (ModelState.IsValid)
            {
                try
                {
                    productoRepository.Eliminar(model.ProductoId);

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
            ProductoRepository productoRepository = container.Resolve<ProductoRepository>();
            List<NavegadorViewModel> toReturn = productoRepository.ObtenerTodo().Select(t => new NavegadorViewModel()
            {
                ProductoId = t.ProductoId,
                Nombre = t.Nombre,
                Descripcion = t.Descripcion,
                Categoria = t.Categoria.Nombre,
            }).OrderBy(y => y.Nombre).ToList();

            return Json(toReturn.ToDataSourceResult(request));
        }

        private List<DropDownListHierarchyViewModel> ObtenerCategorias(ItemCategoriaTipo tipo)
        {
            ItemCategoriaRepository itemCategoriaRepository = container.Resolve<ItemCategoriaRepository>();
            List<ItemCategoria> categorias = itemCategoriaRepository.ObtenerTodo().Where(w => w.Tipo == tipo).ToList();
            List<DropDownListHierarchyViewModel> toReturn = new List<DropDownListHierarchyViewModel>();

            foreach (ItemCategoria i in categorias.Where(w => w.PadreItemCategoriaId == null).OrderBy(o => o.Nombre))
            {
                toReturn.Add(new DropDownListHierarchyViewModel() { Id = i.ItemCategoriaId, Nombre = i.Nombre, Index = 0 });
                toReturn.AddRange(ObtenerCategoriasHijos(i, 0, categorias, new List<DropDownListHierarchyViewModel>()));
            }

            if (toReturn.Count() > 0)
            {
                if (tipo == ItemCategoriaTipo.Item)
                    toReturn.Insert(0, new DropDownListHierarchyViewModel() { Nombre = "Todos los Items", Index = 0 });
                else
                    toReturn.Insert(0, new DropDownListHierarchyViewModel() { Nombre = "Seleccione una categoria", Index = 0 });
            }
            else
            {
                toReturn.Add(new DropDownListHierarchyViewModel() { Nombre = "No hay datos", Index = 0 });
            }

            return toReturn;
        }

        public List<DropDownListHierarchyViewModel> ObtenerCategoriasHijos(ItemCategoria categoria, int indice, List<ItemCategoria> todasCategorias, List<DropDownListHierarchyViewModel> toReturn)
        {
            List<ItemCategoria> query = todasCategorias.Where(w => w.PadreItemCategoriaId == categoria.ItemCategoriaId).OrderBy(o => o.Nombre).ToList();
            if (query.Count() > 0)
            {
                foreach (ItemCategoria i in query)
                {
                    toReturn.Add(new DropDownListHierarchyViewModel() { Id = i.ItemCategoriaId, Nombre = i.Nombre, Index = indice + 1 });
                    toReturn.AddRange(ObtenerCategoriasHijos(i, indice + 1, todasCategorias, new List<DropDownListHierarchyViewModel>()));
                }
            }
            return toReturn;
        }

        public JsonResult GetItems(Guid categoriaId)
        {
            ItemRepository itemRepository = container.Resolve<ItemRepository>();
            ItemCategoriaRepository itemCategoriaRepository = container.Resolve<ItemCategoriaRepository>();

            ItemCategoria categoria = itemCategoriaRepository.ObtenerPorId(categoriaId);
            List<Guid> subcategorias = new List<Guid>() { categoriaId };
            if (categoria != null && categoria.SubCategorias.Count() > 0)
                subcategorias.AddRange(ObtenerCategoriasHijos(categoria, new List<Guid>()));
                

            IQueryable<Item> query = Enumerable.Empty<Item>().AsQueryable();
            if (categoriaId == Guid.Empty)
                query = itemRepository.ObtenerTodo();
            else
                query = itemRepository.ObtenerTodo().Where(w => subcategorias.Contains(w.ItemCategoriaId));

            List<DropdownListViewModel> toReturn = query.Select(s => new DropdownListViewModel()
                {
                    Id = s.ItemId,
                    Nombre = s.Nombre,
                }).OrderBy(o => o.Nombre).ToList();

            if (toReturn.Count() > 0)
                toReturn.Insert(0, new DropdownListViewModel() { Nombre = "Seleccione un item" });
            else
                toReturn.Add(new DropdownListViewModel() { Nombre = "No hay datos" });

            return Json(toReturn, JsonRequestBehavior.AllowGet);
        }

        public List<Guid> ObtenerCategoriasHijos(ItemCategoria categoria, List<Guid> toReturn)
        {
            List<ItemCategoria> query = categoria.SubCategorias.ToList();
            if (query.Count() > 0)
            {
                foreach (ItemCategoria i in query)
                {
                    toReturn.Add(i.ItemCategoriaId);
                    toReturn.AddRange(ObtenerCategoriasHijos(i, new List<Guid>()));
                }
            }
            return toReturn;
        }

        #endregion
    }
}