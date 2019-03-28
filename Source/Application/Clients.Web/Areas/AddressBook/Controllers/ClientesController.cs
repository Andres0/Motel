using DS.Motel.Clients.Web.Areas.AddressBook.Models.Clientes;
using DS.Motel.Data.AddressBook;
using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace DS.Motel.Clients.Web.Areas.AddressBook.Controllers
{
    public class ClientesController : Controller
    {
        #region Fields & properties 

        private IUnityContainer container;

        #endregion






        #region Constructor
        public ClientesController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion






        #region Validacion

        private List<Tuple<string, string>> GetErroresAdd(AddViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();

            if (string.IsNullOrEmpty(model.Placa))
            {
                toReturn.Add(new Tuple<string, string>("Placa", "Por favor ingrese una placa"));
            }
            return toReturn;
        }
        #endregion



        #region Eventos
        public ActionResult Add(Guid suiteId)
        {
            AddViewModel addViewModel = new AddViewModel();
            addViewModel.SuiteId = suiteId;

            UsoHabitacionRepository usoHabitacionRepository = container.Resolve<UsoHabitacionRepository>();
            UsoHabitacion usoHabitacion = usoHabitacionRepository.ObtenerPorSuiteId(suiteId);
            if (usoHabitacion.Cliente != null)
            {
                addViewModel.Placa = usoHabitacion.Cliente.Placa;
                addViewModel.Nombre = usoHabitacion.Cliente.Nombre;
                addViewModel.Color = usoHabitacion.Cliente.Color;
                addViewModel.Tipo = usoHabitacion.Cliente.TipoVehiculo;
                addViewModel.Descripcion = usoHabitacion.Cliente.Descripcion;
            }

            return PartialView(addViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            ClienteRepository clienteRepository = container.Resolve<ClienteRepository>();
            UsoHabitacionRepository usoHabitacionRepository = container.Resolve<UsoHabitacionRepository>();

            ModelState.Clear();
            List<Tuple<string, string>> errores = GetErroresAdd(model);
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                Cliente cliente = new Cliente();
                cliente.Placa = model.Placa;
                cliente.Nombre = model.Nombre;
                cliente.TipoVehiculo = model.Tipo;
                cliente.Color = model.Color;
                cliente.Descripcion = model.Descripcion;

                try
                {
                    Cliente _cliente = clienteRepository.ObtenerTodo().SingleOrDefault(w => w.Placa == model.Placa);
                    if (_cliente == null)
                        clienteRepository.Agregar(cliente);
                    else
                    {
                        cliente.ClienteId = _cliente.ClienteId;
                        clienteRepository.Editar(cliente);
                    }

                    UsoHabitacion usoHabitacion = usoHabitacionRepository.ObtenerPorSuiteId(model.SuiteId);
                    usoHabitacion.ClienteId = cliente.ClienteId;
                    usoHabitacionRepository.Editar(usoHabitacion);

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



        #region Others


        [HttpGet]
        public JsonResult BuscarPlaca(string placa)
        {
            ClienteRepository clienteRepository = container.Resolve<ClienteRepository>();

            Cliente toReturn = clienteRepository.ObtenerTodo().SingleOrDefault(w => w.Placa == placa);

            return Json(toReturn, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}