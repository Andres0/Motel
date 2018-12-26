using DS.Motel.Clients.Web.Areas.Finances.Models.CajaBanco;
using DS.Motel.Data.Entities;
using DS.Motel.Data.Finances;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace DS.Motel.Clients.Web.Areas.Finances.Controllers
{
    public class CajaBancoController : Controller
    {
        #region Fields & properties 

        private IUnityContainer container;

        #endregion






        #region Constructor
        public CajaBancoController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion






        #region Validacion

        private List<Tuple<string, string>> GetErroresAdd(AddViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            CajaBancoRepository cuenta_BancoRepository = container.Resolve<CajaBancoRepository>();

            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un nombre"));
            }
            else
            {
                if (cuenta_BancoRepository.ObtenerTodo().Count(c => c.Nombre == model.Nombre) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Numero", "Ya existe una cuenta con el mismo nombre"));
                }
            }
            return toReturn;
        }

        private List<Tuple<string, string>> GetErroresEdit(EditViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            CajaBancoRepository cuenta_BancoRepository = container.Resolve<CajaBancoRepository>();

            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un nombre"));
            }
            else
            {
                if (cuenta_BancoRepository.ObtenerTodo().Count(c => c.Nombre == model.Nombre && c.CajaBancoId != model.CajaBancoId) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Nombre", "Ya existe una cuenta con el mismo nombre"));
                }
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
            CajaBancoRepository cajaBancoRepository = container.Resolve<CajaBancoRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresAdd(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }



            if (ModelState.IsValid)
            {
                CajaBanco cajaBanco = new CajaBanco();
                cajaBanco.Nombre = model.Nombre;
                cajaBanco.Descripcion = model.Descripcion;
                cajaBanco.Tipo = CajaBancoTipo.Banco;

                try
                {
                    cajaBancoRepository.Agregar(cajaBanco);

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

        public ActionResult Edit(Guid cajaBancoId)
        {
            CajaBancoRepository cajaBancoRepository = container.Resolve<CajaBancoRepository>();
            CajaBanco cajaBanco = cajaBancoRepository.ObtenerPorId(cajaBancoId);

            EditViewModel editViewModel = new EditViewModel();
            editViewModel.CajaBancoId = cajaBanco.CajaBancoId;
            editViewModel.Nombre = cajaBanco.Nombre;
            editViewModel.Descripcion = cajaBanco.Descripcion;

            return PartialView(editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            CajaBancoRepository cajaBancoRepository = container.Resolve<CajaBancoRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresEdit(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                CajaBanco cajaBanco = new CajaBanco();
                cajaBanco.CajaBancoId = model.CajaBancoId;
                cajaBanco.Nombre = model.Nombre;
                cajaBanco.Descripcion = model.Descripcion;

                try
                {
                    cajaBancoRepository.Editar(cajaBanco);

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

        public ActionResult Delete(Guid cajaBancoId)
        {
            DeleteViewModel deleteViewModel = new DeleteViewModel();
            deleteViewModel.CajaBancoId = cajaBancoId;

            return PartialView(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel model)
        {
            CajaBancoRepository CajaBancoRepository = container.Resolve<CajaBancoRepository>();

            ModelState.Clear();
            //Se debe validar que no tenga relaciones con otras entidades caso contrario se mostrara un mensaje

            if (ModelState.IsValid)
            {
                try
                {
                    CajaBancoRepository.Eliminar(model.CajaBancoId);

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
            CajaBancoRepository cuenta_BancoRepository = container.Resolve<CajaBancoRepository>();
            List<NavegadorViewModel> toReturn = cuenta_BancoRepository.ObtenerTodo().Where(w => w.Tipo == CajaBancoTipo.Banco).Select(t => new NavegadorViewModel()
            {
                CajaBancoId = t.CajaBancoId,
                Nombre = t.Nombre,
                Descripcion = t.Descripcion,
            }).OrderBy(y => y.Nombre).ToList();

            return Json(toReturn.ToDataSourceResult(request));
        }

        #endregion
    }
}