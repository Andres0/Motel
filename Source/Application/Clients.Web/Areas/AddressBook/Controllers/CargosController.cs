using DS.Motel.Business.AddressBook;
using DS.Motel.Clients.Web.Areas.AddressBook.Models.Cargos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace DS.Motel.Clients.Web.Areas.AddressBook.Controllers
{
    public class CargosController : Controller
    {
        #region Fill & properties 
        private IUnityContainer container;

        #endregion

        #region Constructor
        public CargosController(IUnityContainer _container)
        {
            container = _container;
        }


        #endregion
        #region Eventos
        // GET: AddressBook/Cargos
        public ActionResult Index()
        {
            CargoManager cargoManager = container.Resolve<CargoManager>();
            NavegadorViewModel navegadorViewModel = new NavegadorViewModel();
            navegadorViewModel.Cargos = cargoManager.ObtenerTodos().Select(t => new NavegadorGridViewModel()
            {
                CargoId = t.CargoId,
                Nombre = t.Nombre,
                Descripcion = t.Descripcion
            }).OrderBy(y => y.Nombre).ToList();

            return View(navegadorViewModel);
        }
        public ActionResult Add()
        {
            AddViewModel addViewModel = new AddViewModel();
            return View(addViewModel);
        }

        #endregion

    }
}