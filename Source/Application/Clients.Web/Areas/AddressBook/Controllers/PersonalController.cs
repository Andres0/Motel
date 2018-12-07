using DS.Motel.Clients.Web.Areas.AddressBook.Models.Personal;
using DS.Motel.Data.AddressBook;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Kendo.Mvc.Extensions;
using DS.Motel.Clients.Web.Models;
using DS.Motel.Data.Security;

namespace DS.Motel.Clients.Web.Areas.AddressBook.Controllers
{
    public class PersonalController : Controller
    {
        // GET: AddressBook/Personal
        #region Fields & properties 

        private IUnityContainer container;

        #endregion



        #region Constructor
        public PersonalController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion

        #region Validacion

        private List<Tuple<string, string>> GetErroresAdd(AddViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            PersonalRepository personalRepository = container.Resolve<PersonalRepository>();

            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un nombre"));
            }
            if (string.IsNullOrEmpty(model.Apellido))
            {
                toReturn.Add(new Tuple<string, string>("Apellido", "Por favor ingrese un Apellido"));
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                toReturn.Add(new Tuple<string, string>("Password", "Por favor ingrese un Password"));
            }
            if (model.CargoId==Guid.Empty)
            {
                toReturn.Add(new Tuple<string, string>("CargoId", "Por favor seleccione un cargo"));
            }
            if (model.UserTypeId == Guid.Empty)
            {
                toReturn.Add(new Tuple<string, string>("UserTypeId", "Por favor seleccione un tipo de usuario"));
            }
            if (string.IsNullOrEmpty(model.CI))
            {
                toReturn.Add(new Tuple<string, string>("CI", "Por favor ingrese un C.I."));
            }

            else
            {

                if (personalRepository.ObtenerTodo().Count(c => c.CI == model.CI) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("CI", "Numero de carnet ya registrado"));
                }
            }
        
            if (string.IsNullOrEmpty(model.Login))
            {
                toReturn.Add(new Tuple<string, string>("Login", "Por favor ingrese un Login"));
            }
            else
            {
                if (personalRepository.ObtenerTodo().Count(c => c.Login == model.Login) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Login", "Ya existe una cuenta con ese LOGIN"));
                }
            }
            return toReturn;
        }

        private List<Tuple<string, string>> GeterrorEdit(EditViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            PersonalRepository personalRepository = container.Resolve<PersonalRepository>();

            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un nombre"));
            }
            if (string.IsNullOrEmpty(model.Apellido))
            {
                toReturn.Add(new Tuple<string, string>("Apellido", "Por favor ingrese un Apellido"));
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                toReturn.Add(new Tuple<string, string>("Password", "Por favor ingrese un Password"));
            }
            if (model.CargoId == Guid.Empty)
            {
                toReturn.Add(new Tuple<string, string>("CargoId", "Por favor seleccione un cargo"));
            }
            if (model.UserTypeId == Guid.Empty)
            {
                toReturn.Add(new Tuple<string, string>("UserTypeId", "Por favor seleccione un tipo de usuario"));
            }
            if (string.IsNullOrEmpty(model.CI))
            {
                toReturn.Add(new Tuple<string, string>("CI", "Por favor ingrese un C.I."));
            }

            else
            {

                if (personalRepository.ObtenerTodo().Count(c => c.CI == model.CI && c.PersonalId!=model.PersonalId) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("CI", "Numero de carnet ya registrado"));
                }
            }

            if (string.IsNullOrEmpty(model.Login))
            {
                toReturn.Add(new Tuple<string, string>("Login", "Por favor ingrese un Login"));
            }
            else
            {
                if (personalRepository.ObtenerTodo().Count(c => c.Login == model.Login && c.PersonalId!= model.PersonalId) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Login", "Ya existe una cuenta con ese LOGIN"));
                }
            }
            return toReturn;
        }
        #endregion
        #region Interface

        public ActionResult Index(bool? masterload=true) {

            ViewData["MasterLoad"] = masterload;
            return View();
        }
        public ActionResult AddPersonal()
        {
            AddViewModel addViewModel = new AddViewModel();
            addViewModel.Cargos = Obtenercargos();
            addViewModel.TipoUsuario = ObtenerTipodeUser();
            return PartialView(addViewModel);
        }
        #endregion
        #region Queries

        public ActionResult LoadGrid([DataSourceRequest]DataSourceRequest request)
        {
            PersonalRepository PersonalRepository = container.Resolve<PersonalRepository>();
            List<NavegadorViewModel> toReturn = PersonalRepository.ObtenerTodo().Select(t => new NavegadorViewModel()
            {
               
                Nombre = t.Nombre,
                Apellido = t.Apellido,
                CI=t.Apellido,
                Direccion= t.Direccion,
                Login=t.Login,
                Estado=t.Estado.ToString(),
                CargoId=t.CargoId

                
            }).OrderBy(y => y.Nombre).ToList();

            return Json(toReturn.ToDataSourceResult(request));
        }
        public List<DropdownListViewModel> Obtenercargos()
        {
            List<DropdownListViewModel> ToReturn = new List<DropdownListViewModel>();
            CargoRepository VarCargo = container.Resolve<CargoRepository>();
            ToReturn = VarCargo.ObtenerTodo().Select(x => new DropdownListViewModel() {
                Id = x.CargoId,
                Nombre= x.Nombre
            }).OrderBy(y => y.Nombre).ToList() ;
            if (ToReturn.Count() > 0)
            {
                ToReturn.Insert(0, new DropdownListViewModel()
                {
                    Id = Guid.Empty,
                    Nombre = "Seleccione un Cargo"
                });
            }
            else {
                ToReturn.Add(new DropdownListViewModel()
                {
                    Id = Guid.Empty,
                    Nombre = "No hay datos"
                });
            }
            return ToReturn;
        }
        #endregion

        public List<DropdownListViewModel> ObtenerTipodeUser()
        {
            List<DropdownListViewModel> ToReturn = new List<DropdownListViewModel>();
            UserTypeRepository VarUsuario = container.Resolve<UserTypeRepository>();
            ToReturn = VarUsuario.GetAll().Select(x => new DropdownListViewModel()
            {
                Id = x.UserTypeId,
                Nombre = x.Name
            }).OrderBy(y => y.Nombre).ToList();
            if (ToReturn.Count() > 0)
            {
                ToReturn.Insert(0, new DropdownListViewModel()
                {
                    Id = Guid.Empty,
                    Nombre = "Seleccione un Tipo de Usuario"
                });
            }
            else
            {
                ToReturn.Add(new DropdownListViewModel()
                {
                    Id = Guid.Empty,
                    Nombre = "No hay datos"
                });
            }
            return ToReturn;
        }

    }
}