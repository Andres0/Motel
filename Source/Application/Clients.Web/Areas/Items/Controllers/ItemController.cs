using DS.Motel.Clients.Web.Areas.Items.Models.Items;
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
    public class ItemController : Controller
    {
        #region Fields & properties 

        private IUnityContainer container;

        #endregion






        #region Constructor

        public ItemController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion






        #region Validacion

        private List<Tuple<string, string>> GetErroresAdd(AddViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            ItemRepository itemRepository = container.Resolve<ItemRepository>();

            if (string.IsNullOrEmpty(model.Codigo))
            {
                toReturn.Add(new Tuple<string, string>("Codigo", "Por favor ingrese un codigo"));
            }
            else
            {
                if (itemRepository.ObtenerTodo().Count(c => c.Codigo == model.Codigo) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Codigo", "Ya existe un item con el mismo codigo"));
                }
            }
            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un nombre"));
            }
            if (model.CategoriaId == Guid.Empty)
            {
                toReturn.Add(new Tuple<string, string>("CategoriaId", "Por favor seleccione una categoría"));
            }
            return toReturn;
        }

        private List<Tuple<string, string>> GeterrorEdit(EditViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            ItemRepository itemRepository = container.Resolve<ItemRepository>();

            if (string.IsNullOrEmpty(model.Codigo))
            {
                toReturn.Add(new Tuple<string, string>("Codigo", "Por favor ingrese un codigo"));
            }
            else
            {
                if (itemRepository.ObtenerTodo().Count(c => c.Codigo == model.Codigo && c.ItemId != model.ItemId) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Codigo", "Ya existe un item con el mismo codigo"));
                }
            }
            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un nombre"));
            }
            if (model.CategoriaId == Guid.Empty)
            {
                toReturn.Add(new Tuple<string, string>("CategoriaId", "Por favor seleccione una categoría"));
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
            addViewModel.Categorias = ObtenerCategorias();
            
            return PartialView(addViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            ItemRepository itemRepository = container.Resolve<ItemRepository>();

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
                Item item = new Item();
                item.Codigo = model.Codigo;
                item.Nombre = model.Nombre;
                item.Descripcion = model.Descripcion;
                item.Cantidad_Stock = model.Cantidad_Stock;
                item.Costo_Total = model.Costo_Total;
                item.Costo_Unitario = model.Costo_Unitario;
                item.Proporcion = model.Proporcion;
                item.Stock_Minimo = model.Stock_Minimo;
                item.EsVendible = model.EsVendible;
                item.ItemCategoriaId = model.CategoriaId;

                try
                {
                    itemRepository.Agregar(item);

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

            model.Categorias = ObtenerCategorias();
            return PartialView(model);
        }

        public ActionResult Edit(Guid itemId)
        {
            ItemRepository itemRepository = container.Resolve<ItemRepository>();
            Item item = itemRepository.ObtenerPorId(itemId);

            EditViewModel editViewModel = new EditViewModel();
            editViewModel.ItemId = item.ItemId;
            editViewModel.Codigo = item.Codigo;
            editViewModel.Nombre = item.Nombre;
            editViewModel.Descripcion = item.Descripcion;
            editViewModel.Cantidad_Stock = item.Cantidad_Stock;
            editViewModel.Costo_Total = item.Costo_Total;
            editViewModel.Costo_Unitario = item.Costo_Unitario;
            editViewModel.Proporcion = item.Proporcion;
            editViewModel.Stock_Minimo = item.Stock_Minimo;
            editViewModel.EsVendible = item.EsVendible;
            editViewModel.CategoriaId = item.ItemCategoriaId;

            editViewModel.Categorias = ObtenerCategorias();

            return PartialView(editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            ItemRepository itemRepository = container.Resolve<ItemRepository>();

            ModelState.Clear();

            List<Tuple<string, string>> errores = GeterrorEdit(model);

            //CARGO LOS ERRORES AL MODELSTATE
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                Item item = new Item();
                item.ItemId = model.ItemId;
                item.Codigo = model.Codigo;
                item.Nombre = model.Nombre;
                item.Descripcion = model.Descripcion;
                item.Cantidad_Stock = model.Cantidad_Stock;
                item.Costo_Total = model.Costo_Total;
                item.Costo_Unitario = model.Costo_Unitario;
                item.Proporcion = model.Proporcion;
                item.Stock_Minimo = model.Stock_Minimo;
                item.EsVendible = model.EsVendible;
                item.ItemCategoriaId = model.CategoriaId;

                try
                {
                    itemRepository.Editar(item);

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

            model.Categorias = ObtenerCategorias();
            return PartialView(model);
        }

        public ActionResult Delete(Guid itemId)
        {
            DeleteViewModel deleteViewModel = new DeleteViewModel();
            deleteViewModel.ItemId = itemId;

            return PartialView(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel model)
        {
            ItemRepository itemRepository = container.Resolve<ItemRepository>();

            ModelState.Clear();
            //Se debe validar que no tenga relaciones con otras entidades caso contrario se mostrara un mensaje

            if (ModelState.IsValid)
            {
                try
                {
                    itemRepository.Eliminar(model.ItemId);

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
            ItemRepository itemRepository = container.Resolve<ItemRepository>();
            List<NavegadorViewModel> toReturn = itemRepository.ObtenerTodo().Select(t => new NavegadorViewModel()
            {
                ItemId = t.ItemId,
                Codigo = t.Codigo,
                Nombre = t.Nombre,
                Cantidad_Stock = t.Cantidad_Stock,
                Stock_Minimo = t.Stock_Minimo,
                Es_Vendible = t.EsVendible == true ? "Si" : "No",
            }).OrderBy(y => y.Codigo).ToList();

            return Json(toReturn.ToDataSourceResult(request));
        }

        public List<DropdownListViewModel> ObtenerCategorias()
        {
            List<DropdownListViewModel> toReturn = new List<DropdownListViewModel>();
            ItemCategoriaRepository itemCategoriaRepository = container.Resolve<ItemCategoriaRepository>();
            toReturn = itemCategoriaRepository.ObtenerTodo().Select(x => new DropdownListViewModel()
            {
                Id = x.ItemCategoriaId,
                Nombre = x.Nombre
            }).OrderBy(y => y.Nombre).ToList();
            if (toReturn.Count() > 0)
            {
                toReturn.Insert(0, new DropdownListViewModel()
                {
                    Id = Guid.Empty,
                    Nombre = "Seleccione una categoría"
                });
            }
            else
            {
                toReturn.Add(new DropdownListViewModel()
                {
                    Id = Guid.Empty,
                    Nombre = "No hay datos"
                });
            }
            return toReturn;
        }

        #endregion
    }
}