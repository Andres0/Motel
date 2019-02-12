using DS.Motel.Clients.Web.Areas.Items.Models.Categorias;
using DS.Motel.Clients.Web.Models;
using DS.Motel.Data.Entities;
using DS.Motel.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace DS.Motel.Clients.Web.Areas.Items.Controllers
{
    public class CategoriasController : Controller
    {
        #region Fields & properties 

        private IUnityContainer container;

        #endregion






        #region Constructor
        public CategoriasController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion






        #region Validacion

        private List<Tuple<string, string>> GetErroresAdd(AddViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();

            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un nombre"));
            }
            return toReturn;
        }

        private List<Tuple<string, string>> GetErroresEdit(EditViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();

            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un nombre"));
            }
            return toReturn;
        }
        #endregion






        #region Interfaces

        public ActionResult Index(ItemCategoriaTipo tipo)
        {
            ViewData["Type"] = tipo;
            return View();
        }

        public ActionResult TreeView(ItemCategoriaTipo tipo)
        {
            ViewData["Type"] = tipo;
            List<NavegadorViewModel> navegadorViewModel = LoadTreeView(tipo);
            return PartialView(navegadorViewModel);
        }

        public ActionResult Add(ItemCategoriaTipo tipo)
        {
            AddViewModel addViewModel = new AddViewModel();
            addViewModel.Tipo = tipo;
            addViewModel.Categorias = ObtenerCategorias(tipo);

            return PartialView(addViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            ItemCategoriaRepository itemCategoriaRepository = container.Resolve<ItemCategoriaRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresAdd(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }



            if (ModelState.IsValid)
            {
                ItemCategoria itemCategoria = new ItemCategoria();
                itemCategoria.Nombre = model.Nombre;
                itemCategoria.Descripcion = model.Descripcion;
                itemCategoria.Tipo = model.Tipo;
                itemCategoria.PadreItemCategoriaId = model.PadreId == Guid.Empty ? null : model.PadreId;

                try
                {
                    itemCategoriaRepository.Agregar(itemCategoria);

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
            model.Categorias = ObtenerCategorias(model.Tipo);
            return PartialView(model);
        }
        
        public ActionResult Edit(Guid categoriaId, ItemCategoriaTipo tipo)
        {
            ItemCategoriaRepository itemCategoriaRepository = container.Resolve<ItemCategoriaRepository>();
            ItemCategoria itemCategoria = itemCategoriaRepository.ObtenerTodo().SingleOrDefault(s => s.ItemCategoriaId == categoriaId);

            EditViewModel editViewModel = new EditViewModel();
            editViewModel.CategoriaId = itemCategoria.ItemCategoriaId;
            editViewModel.Nombre = itemCategoria.Nombre;
            editViewModel.Descripcion = itemCategoria.Descripcion;
            editViewModel.PadreId = itemCategoria.PadreItemCategoriaId;
            editViewModel.Tipo = tipo;
            editViewModel.Categorias = ObtenerCategorias(tipo, itemCategoria.ItemCategoriaId);

            return PartialView(editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            ItemCategoriaRepository itemCategoriaRepository = container.Resolve<ItemCategoriaRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresEdit(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                ItemCategoria itemCategoria = new ItemCategoria();
                itemCategoria.ItemCategoriaId = model.CategoriaId;
                itemCategoria.Nombre = model.Nombre;
                itemCategoria.Descripcion = model.Descripcion;
                itemCategoria.Tipo = model.Tipo;
                itemCategoria.PadreItemCategoriaId = model.PadreId == Guid.Empty ? null : model.PadreId;

                try
                {
                    itemCategoriaRepository.Editar(itemCategoria);

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

        public ActionResult Delete(Guid categoriaId, ItemCategoriaTipo tipo)
        {
            DeleteViewModel deleteViewModel = new DeleteViewModel();
            deleteViewModel.CategoriaId = categoriaId;
            deleteViewModel.Tipo = tipo;

            return PartialView(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel model)
        {
            ItemCategoriaRepository itemCategoriaRepository = container.Resolve<ItemCategoriaRepository>();

            ModelState.Clear();
            //Se debe validar que no tenga relaciones con otras entidades caso contrario se mostrara un mensaje

            if (ModelState.IsValid)
            {
                try
                {
                    itemCategoriaRepository.Eliminar(model.CategoriaId);

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






        #region Consultas
        public List<NavegadorViewModel> LoadTreeView(ItemCategoriaTipo tipo)
        {
            ItemCategoriaRepository itemCategoriaRepository = container.Resolve<ItemCategoriaRepository>();
            List<ItemCategoria> categoriasTodo = itemCategoriaRepository.ObtenerTodo().Where(w => w.Tipo == tipo).ToList();
            List<NavegadorViewModel> toReturn = categoriasTodo.Where(w => w.PadreItemCategoriaId == null).OrderBy(o => o.Nombre)
                .Select(s => new NavegadorViewModel
                {
                    CategoriaId = s.ItemCategoriaId,
                    Nombre = s.Nombre,
                    SubCategorias = LoadTreeViewChilds(tipo, s.ItemCategoriaId, categoriasTodo)
                }).ToList();

            if (toReturn.Count() == 0)
            {
                toReturn.Add(new NavegadorViewModel() { Nombre = "No hay items para mostrar" });
            }

            return toReturn;
        }

        private List<NavegadorViewModel> LoadTreeViewChilds(ItemCategoriaTipo tipo, Guid categoriaId, List<ItemCategoria> categoriasTodo)
        {
            return categoriasTodo.Where(w => w.PadreItemCategoriaId == categoriaId).OrderBy(o => o.Nombre)
                .Select(s => new NavegadorViewModel
                {
                    CategoriaId = s.ItemCategoriaId,
                    Nombre = s.Nombre,
                    SubCategorias = s.SubCategorias.Any() ? LoadTreeViewChilds(tipo, s.ItemCategoriaId, categoriasTodo) : null
                }).ToList();
        }

        private List<DropDownListHierarchyViewModel> ObtenerCategorias(ItemCategoriaTipo tipo, Guid? categoriaId=null)
        {
            ItemCategoriaRepository itemCategoriaRepository = container.Resolve<ItemCategoriaRepository>();
            List<ItemCategoria> categorias = categoriaId.HasValue ? 
                itemCategoriaRepository.ObtenerTodo().Where(w => w.Tipo == tipo && w.ItemCategoriaId != categoriaId).ToList() : 
                itemCategoriaRepository.ObtenerTodo().Where(w => w.Tipo == tipo).ToList();
            List<DropDownListHierarchyViewModel> toReturn = new List<DropDownListHierarchyViewModel>();

            foreach (ItemCategoria i in categorias.Where(w => w.PadreItemCategoriaId == null).OrderBy(o => o.Nombre))
            {
                toReturn.Add(new DropDownListHierarchyViewModel() { Id = i.ItemCategoriaId, Nombre = i.Nombre, Index = 0 });
                toReturn.AddRange(ObtenerCategoriasHijos(i, 0, categorias, new List<DropDownListHierarchyViewModel>()));
            }

            if (toReturn.Count() > 0)
            {
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
                    toReturn.Add(new DropDownListHierarchyViewModel() { Id = i.ItemCategoriaId, Nombre = i.Nombre, Index = indice+1 });
                    toReturn.AddRange(ObtenerCategoriasHijos(i, indice + 1, todasCategorias, new List<DropDownListHierarchyViewModel>()));
                }
            }
            return toReturn;
        }

        #endregion
    }
}