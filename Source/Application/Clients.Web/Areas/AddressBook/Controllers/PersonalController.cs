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
using DS.Motel.Data.Entities;

namespace DS.Motel.Clients.Web.Areas.AddressBook.Controllers
{
    public class PersonalController : Controller
    {
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
                    toReturn.Add(new Tuple<string, string>("Login", "Ya existe una cuenta con ese Login"));
                }
            }
            if (model.Password != null && model.ConfirmarPsw != null && !model.Password.Equals(model.ConfirmarPsw))
            {
                toReturn.Add(new Tuple<string, string>("ConfirmarPsw", "Confirmar password no coincide con el password"));
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
                if (personalRepository.ObtenerTodo().Count(c => c.CI == model.CI && c.PersonalId != model.PersonalId) > 0)
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
                if (personalRepository.ObtenerTodo().Count(c => c.Login == model.Login && c.PersonalId != model.PersonalId) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Login", "Ya existe una cuenta con ese Login"));
                }
            }
            if (model.Password != null && model.ConfirmarPsw != null && !model.Password.Equals(model.ConfirmarPsw))
            {
                toReturn.Add(new Tuple<string, string>("ConfirmarPsw", "Confirmar password no coincide con el password"));
            }
            return toReturn;
        }
        #endregion






        #region Interface

        public ActionResult Index(bool? masterload=true)
        {
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPersonal(AddViewModel model)
        {
            PersonalRepository personalRepository = container.Resolve<PersonalRepository>();

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
                Personal personal = new Personal();
                personal.Nombre = model.Nombre;
                personal.Apellido = model.Apellido;
                personal.CI = model.CI;
                personal.Direccion = model.Direccion;
                personal.Telefono = model.Telefono;
                personal.Email = model.Email;
                personal.Login = model.Login;
                personal.Password = model.Password;
                personal.Observacion = model.Observacion;
                personal.Estado = PersonalEstado.Activado;
                personal.UserTypeId = model.UserTypeId;
                personal.CargoId = model.CargoId;
                personal.Creado_Por = null;

                try
                {
                    personalRepository.Agregar(personal);

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

            model.Cargos = Obtenercargos();
            model.TipoUsuario = ObtenerTipodeUser();
            return PartialView(model);
        }

        public ActionResult EditPersonal(Guid personalId)
        {
            PersonalRepository personalRepository = container.Resolve<PersonalRepository>();
            Personal personal = personalRepository.ObtenerPorId(personalId);

            EditViewModel editViewModel = new EditViewModel();
            editViewModel.PersonalId = personal.PersonalId;
            editViewModel.Nombre = personal.Nombre;
            editViewModel.Apellido = personal.Apellido;
            editViewModel.CI = personal.CI;
            editViewModel.Direccion = personal.Direccion;
            editViewModel.Telefono = personal.Telefono;
            editViewModel.Email = personal.Email;
            editViewModel.Login = personal.Login;
            editViewModel.Password = personal.Password;
            editViewModel.Observacion = personal.Observacion;
            editViewModel.Estado = personal.Estado;
            editViewModel.CargoId = personal.CargoId;
            editViewModel.UserTypeId = personal.UserTypeId;

            editViewModel.Cargos = Obtenercargos();
            editViewModel.TipoUsuario = ObtenerTipodeUser();
            
            return PartialView(editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPersonal(EditViewModel model)
        {
            PersonalRepository personalRepository = container.Resolve<PersonalRepository>();

            ModelState.Clear();

            List<Tuple<string, string>> errores = GeterrorEdit(model);

            //CARGO LOS ERRORES AL MODELSTATE
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                Personal personal = new Personal();
                personal.PersonalId = model.PersonalId;
                personal.Nombre = model.Nombre;
                personal.Apellido = model.Apellido;
                personal.CI = model.CI;
                personal.Direccion = model.Direccion;
                personal.Telefono = model.Telefono;
                personal.Email = model.Email;
                personal.Login = model.Login;
                personal.Password = model.Password;
                personal.Observacion = model.Observacion;
                personal.Estado = PersonalEstado.Activado;
                personal.UserTypeId = model.UserTypeId;
                personal.CargoId = model.CargoId;
                personal.Creado_Por = null;

                try
                {
                    personalRepository.Editar(personal);

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

            model.Cargos = Obtenercargos();
            model.TipoUsuario = ObtenerTipodeUser();
            return PartialView(model);
        }

        public ActionResult DeletePersonal(Guid personalId)
        {
            DeleteViewModel deleteViewModel = new DeleteViewModel();
            deleteViewModel.PersonalId = personalId;

            return PartialView(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePersonal(DeleteViewModel model)
        {
            PersonalRepository personalRepository = container.Resolve<PersonalRepository>();

            ModelState.Clear();
            //Se debe validar que no tenga relaciones con otras entidades caso contrario se mostrara un mensaje

            if (ModelState.IsValid)
            {
                try
                {
                    personalRepository.Eliminar(model.PersonalId);

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
            PersonalRepository PersonalRepository = container.Resolve<PersonalRepository>();
            List<NavegadorViewModel> toReturn = PersonalRepository.ObtenerTodo().Select(t => new NavegadorViewModel()
            {
                PersonalId = t.PersonalId,
                Nombre = t.Nombre,
                Apellido = t.Apellido,
                CI = t.CI,
                Direccion = t.Direccion,
                Login = t.Login,
                Estado = t.Estado.ToString(),
                Cargo_Name = t.Cargo.Nombre
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

        #endregion
    }
}