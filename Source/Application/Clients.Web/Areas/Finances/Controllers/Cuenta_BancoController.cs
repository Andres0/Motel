using DS.Motel.Clients.Web.Areas.Finances.Models.Cuenta_Banco;
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
    public class Cuenta_BancoController : Controller
    {
        #region Fields & properties 

        private IUnityContainer container;

        #endregion






        #region Constructor
        public Cuenta_BancoController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion






        #region Validacion

        private List<Tuple<string, string>> GetErroresAdd(AddViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            Cuenta_BancoRepository cuenta_BancoRepository = container.Resolve<Cuenta_BancoRepository>();

            if (string.IsNullOrEmpty(model.Numero))
            {
                toReturn.Add(new Tuple<string, string>("Numero", "Por favor ingrese un número"));
            }
            else
            {
                if (cuenta_BancoRepository.ObtenerTodo().Count(c => c.Numero == model.Numero) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Numero", "Ya existe una cuenta con el mismo número"));
                }
            }
            if (string.IsNullOrEmpty(model.Titular))
            {
                toReturn.Add(new Tuple<string, string>("Titular", "Por favor ingrese un titular"));
            }
            if (string.IsNullOrEmpty(model.Banco))
            {
                toReturn.Add(new Tuple<string, string>("Banco", "Por favor ingrese un nombre de banco"));
            }
            return toReturn;
        }

        private List<Tuple<string, string>> GetErroresEdit(EditViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            Cuenta_BancoRepository cuenta_BancoRepository = container.Resolve<Cuenta_BancoRepository>();

            if (string.IsNullOrEmpty(model.Numero))
            {
                toReturn.Add(new Tuple<string, string>("Numero", "Por favor ingrese un número"));
            }
            else
            {
                if (cuenta_BancoRepository.ObtenerTodo().Count(c => c.Numero == model.Numero && c.CuentaId != model.CuentaId) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Numero", "Ya existe una cuenta con el mismo número"));
                }
            }
            if (string.IsNullOrEmpty(model.Titular))
            {
                toReturn.Add(new Tuple<string, string>("Titular", "Por favor ingrese un titular"));
            }
            if (string.IsNullOrEmpty(model.Banco))
            {
                toReturn.Add(new Tuple<string, string>("Banco", "Por favor ingrese un nombre de banco"));
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
            Cuenta_BancoRepository cuenta_BancoRepository = container.Resolve<Cuenta_BancoRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresAdd(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }



            if (ModelState.IsValid)
            {
                Cuenta_Banco cuenta_Banco = new Cuenta_Banco();
                cuenta_Banco.Numero = model.Numero;
                cuenta_Banco.Titular = model.Titular;
                cuenta_Banco.Banco = model.Banco;

                try
                {
                    cuenta_BancoRepository.Agregar(cuenta_Banco);

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

        public ActionResult Edit(Guid cuentaId)
        {
            Cuenta_BancoRepository cuenta_BancoRepository = container.Resolve<Cuenta_BancoRepository>();
            Cuenta_Banco cuenta_Banco = cuenta_BancoRepository.ObtenerPorId(cuentaId);

            EditViewModel editViewModel = new EditViewModel();
            editViewModel.CuentaId = cuenta_Banco.CuentaId;
            editViewModel.Numero = cuenta_Banco.Numero;
            editViewModel.Titular = cuenta_Banco.Titular;
            editViewModel.Banco = cuenta_Banco.Banco;

            return PartialView(editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            Cuenta_BancoRepository cuenta_BancoRepository = container.Resolve<Cuenta_BancoRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresEdit(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                Cuenta_Banco cuenta_Banco = new Cuenta_Banco();
                cuenta_Banco.CuentaId = model.CuentaId;
                cuenta_Banco.Numero = model.Numero;
                cuenta_Banco.Titular = model.Titular;
                cuenta_Banco.Banco = model.Banco;

                try
                {
                    cuenta_BancoRepository.Editar(cuenta_Banco);

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

        public ActionResult Delete(Guid cuentaId)
        {
            DeleteViewModel deleteViewModel = new DeleteViewModel();
            deleteViewModel.CuentaId = cuentaId;

            return PartialView(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel model)
        {
            Cuenta_BancoRepository cuenta_BancoRepository = container.Resolve<Cuenta_BancoRepository>();

            ModelState.Clear();
            //Se debe validar que no tenga relaciones con otras entidades caso contrario se mostrara un mensaje

            if (ModelState.IsValid)
            {
                try
                {
                    cuenta_BancoRepository.Eliminar(model.CuentaId);

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
            Cuenta_BancoRepository cuenta_BancoRepository = container.Resolve<Cuenta_BancoRepository>();
            List<NavegadorViewModel> toReturn = cuenta_BancoRepository.ObtenerTodo().Select(t => new NavegadorViewModel()
            {
                CuentaId = t.CuentaId,
                Numero = t.Numero,
                Titular = t.Titular,
                Banco = t.Banco,
            }).OrderBy(y => y.Titular).ToList();

            return Json(toReturn.ToDataSourceResult(request));
        }

        #endregion
    }
}