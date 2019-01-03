using DS.Motel.Clients.Web.Areas.AddressBook.Models.Parametros;
using DS.Motel.Clients.Web.Models;
using DS.Motel.Data.AddressBook;
using DS.Motel.Data.Entities;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace DS.Motel.Clients.Web.Areas.AddressBook.Controllers
{
    public class ParametrosController : Controller
    {
        
        #region Fields & properties 

        private IUnityContainer container;

        #endregion

        #region Constructor
        public ParametrosController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion


        #region Validacion

        private List<Tuple<string, string>> GetErroresAdd(AddViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            ParametrosRepository parametrosRepository = container.Resolve<ParametrosRepository>();

            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un nombre"));
            }
            else
            {
                if (parametrosRepository.ObtenerTodo().Count(c => c.Nombre == model.Nombre) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Nombre", "Ya existe un parametro con el mismo Nombre"));
                }
            }
            if (model.Costo_Habitacion == null)
            {
                toReturn.Add(new Tuple<string, string>("Costo_Habitacion", "Por favor ingrese un costo"));

            }
            if (model.Costo_Adicional1 == null)
            {
                toReturn.Add(new Tuple<string, string>("Costo_Adicional1", "Por favor ingrese un costo"));
            }
            if (model.Costo_Adicional2 == null)
            {
                toReturn.Add(new Tuple<string, string>("Costo_Adicional2", "Por favor ingrese un costo"));
            }
            if (model.Tiempo_Hora == null)
            {
                toReturn.Add(new Tuple<string, string>("Tiempo_Hora", "Por favor ingrese minutos de hora"));
            }
            if (model.Tiempo_Incremento == null)
            {
                toReturn.Add(new Tuple<string, string>("Tiempo_Incremento", "Por favor ingrese minutos"));
            }
            if (model.Costo_Tv == null)
            {
                toReturn.Add(new Tuple<string, string>("Costo_Tv", "Por favor ingrese costo"));
            }
            if (model.Tolerancia == null)
            {
                toReturn.Add(new Tuple<string, string>("Tolerancia", "Por favor ingrese minutos"));
            }
            if (model.Tiempo_Anular == null)
            {
                toReturn.Add(new Tuple<string, string>("Tiempo_Anular", "Por favor ingrese minutos"));
            }
            if (model.Tiempo_Salir == null)
            {
                toReturn.Add(new Tuple<string, string>("Tiempo_Salir", "Por favor ingrese minutos"));
            }
            if (model.Tiempo_Limpieza == null)
            {
                toReturn.Add(new Tuple<string, string>("Tiempo_Limpieza", "Por favor ingrese minutos"));
            }
            if (model.Tiempo_Revision == null)
            {
                toReturn.Add(new Tuple<string, string>("Tiempo_Revision", "Por favor ingrese minutos"));
            }
            if (model.Numero_Inicio_Boleta == null)
            {
                toReturn.Add(new Tuple<string, string>("Numero_Inicio_Boleta", "Por favor ingrese minutos"));
            }
            if (model.Numero_Fin_Boleta == null)
            {
                toReturn.Add(new Tuple<string, string>("Numero_Fin_Boleta", "Por favor ingrese minutos"));
            }




            return toReturn;
        }

        private List<Tuple<string, string>> GeterrorEdit(EditViewModel model)
        {
            List<Tuple<string, string>> toReturn = new List<Tuple<string, string>>();
            ParametrosRepository parametrosRepository = container.Resolve<ParametrosRepository>();

            if (string.IsNullOrEmpty(model.Nombre))
            {
                toReturn.Add(new Tuple<string, string>("Nombre", "Por favor ingrese un nombre"));
            }
            else
            {
                if (parametrosRepository.ObtenerTodo().Count(c => c.Nombre == model.Nombre && c.ParametroId!= model.ParametroId) > 0)
                {
                    toReturn.Add(new Tuple<string, string>("Nombre", "Ya existe un parametro con el mismo Nombre"));
                }
            }
            if (model.Costo_Habitacion == null)
            {
                toReturn.Add(new Tuple<string, string>("Costo_Habitacion", "Por favor ingrese un costo"));

            }
            if (model.Costo_Adicional1 == null)
            {
                toReturn.Add(new Tuple<string, string>("Costo_Adicional1", "Por favor ingrese un costo"));
            }
            if (model.Costo_Adicional2 == null)
            {
                toReturn.Add(new Tuple<string, string>("Costo_Adicional2", "Por favor ingrese un costo"));
            }
            if (model.Tiempo_Hora == null)
            {
                toReturn.Add(new Tuple<string, string>("Tiempo_Hora", "Por favor ingrese minutos de hora"));
            }
            if (model.Tiempo_Incremento == null)
            {
                toReturn.Add(new Tuple<string, string>("Tiempo_Incremento", "Por favor ingrese minutos"));
            }
            if (model.Costo_Tv == null)
            {
                toReturn.Add(new Tuple<string, string>("Costo_Tv", "Por favor ingrese costo"));
            }
            if (model.Tolerancia == null)
            {
                toReturn.Add(new Tuple<string, string>("Tolerancia", "Por favor ingrese minutos"));
            }
            if (model.Tiempo_Anular == null)
            {
                toReturn.Add(new Tuple<string, string>("Tiempo_Anular", "Por favor ingrese minutos"));
            }
            if (model.Tiempo_Salir == null)
            {
                toReturn.Add(new Tuple<string, string>("Tiempo_Salir", "Por favor ingrese minutos"));
            }
            if (model.Tiempo_Limpieza == null)
            {
                toReturn.Add(new Tuple<string, string>("Tiempo_Limpieza", "Por favor ingrese minutos"));
            }
            if (model.Tiempo_Revision == null)
            {
                toReturn.Add(new Tuple<string, string>("Tiempo_Revision", "Por favor ingrese minutos"));
            }
            if (model.Numero_Inicio_Boleta == null)
            {
                toReturn.Add(new Tuple<string, string>("Numero_Inicio_Boleta", "Por favor ingrese minutos"));
            }
            if (model.Numero_Fin_Boleta == null)
            {
                toReturn.Add(new Tuple<string, string>("Numero_Fin_Boleta", "Por favor ingrese minutos"));
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

        public ActionResult AddParametros()
        {
            AddViewModel addViewModel = new AddViewModel();
           
            return PartialView(addViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddParametros(AddViewModel model)
        {
            ParametrosRepository parametrosRepository = container.Resolve<ParametrosRepository>();

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
                Parametros parametros = new Parametros();
                parametros.Nombre = model.Nombre;
                parametros.Costo_Habitacion = model.Costo_Habitacion.Value;
                parametros.Costo_Adicional1 = model.Costo_Adicional1.Value;
                parametros.Costo_Adicional2 = model.Costo_Adicional2.Value;
                parametros.Tiempo_Hora = model.Tiempo_Hora.Value;
                parametros.Tiempo_Incremento = model.Tiempo_Incremento.Value;
                parametros.Costo_Tv = model.Costo_Tv.Value;
                parametros.Tolerancia = model.Tolerancia.Value;
                parametros.Tiempo_Anular = model.Tiempo_Anular.Value;
                parametros.Tiempo_Salir = model.Tiempo_Salir.Value;
                parametros.Tiempo_Limpieza = model.Tiempo_Limpieza.Value;
                parametros.Tiempo_Revision = model.Tiempo_Revision.Value;
                parametros.Numero_Inicio_Boleta = model.Numero_Inicio_Boleta.Value;
                parametros.Numero_Fin_Boleta = model.Numero_Fin_Boleta.Value;
                parametros.Fecha_Modificado = DateTime.Now;
                parametros.Activado = true;
                


                try
                {
                    parametrosRepository.Agregar(parametros);

                    model.Result = EnumActionResult.Saved;
                }
                catch (Exception ex)
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

        public ActionResult EditParametros(Guid parametroId)
        {
            ParametrosRepository parametroRepository = container.Resolve<ParametrosRepository>();
            Parametros parametro = parametroRepository.ObtenerPorId(parametroId);

            EditViewModel editViewModel = new EditViewModel();

            editViewModel.ParametroId = parametro.ParametroId;
            editViewModel.Nombre = parametro.Nombre;
            editViewModel.Costo_Habitacion = parametro.Costo_Habitacion;
            editViewModel.Costo_Adicional1 = parametro.Costo_Adicional1;
            editViewModel.Costo_Adicional2 = parametro.Costo_Adicional2;
            editViewModel.Tiempo_Hora = parametro.Tiempo_Hora;
            editViewModel.Tiempo_Incremento = parametro.Tiempo_Incremento;
            editViewModel.Costo_Tv = parametro.Costo_Tv;
            editViewModel.Tolerancia = parametro.Tolerancia;
            editViewModel.Tiempo_Anular = parametro.Tiempo_Anular;
            editViewModel.Tiempo_Salir = parametro.Tiempo_Salir;
            editViewModel.Tiempo_Limpieza = parametro.Tiempo_Limpieza;
            editViewModel.Tiempo_Revision = parametro.Tiempo_Revision;
            editViewModel.Numero_Inicio_Boleta = parametro.Numero_Inicio_Boleta;
            editViewModel.Numero_Fin_Boleta = parametro.Numero_Fin_Boleta;
           

            return PartialView(editViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditParametros(EditViewModel model)
        {
            ParametrosRepository parametrosRepository = container.Resolve<ParametrosRepository>();

            //LIMPIA EL MODEO DE ERRORES
            ModelState.Clear();

            //OBTENGO LOS POSIBLES ERRORES
            List<Tuple<string, string>> errores = GeterrorEdit(model);

            //CARGO LOS ERRORES AL MODELSTATE
            foreach (Tuple<string, string> item in errores)
            {
                ModelState.AddModelError(item.Item1, item.Item2);
            }

            if (ModelState.IsValid)
            {
                Parametros parametros = new Parametros();
                parametros.ParametroId = model.ParametroId;
                parametros.Nombre = model.Nombre;
                parametros.Costo_Habitacion = model.Costo_Habitacion.Value;
                parametros.Costo_Adicional1 = model.Costo_Adicional1.Value;
                parametros.Costo_Adicional2 = model.Costo_Adicional2.Value;
                parametros.Tiempo_Hora = model.Tiempo_Hora.Value;
                parametros.Tiempo_Incremento = model.Tiempo_Incremento.Value;
                parametros.Costo_Tv = model.Costo_Tv.Value;
                parametros.Tolerancia = model.Tolerancia.Value;
                parametros.Tiempo_Anular = model.Tiempo_Anular.Value;
                parametros.Tiempo_Salir = model.Tiempo_Salir.Value;
                parametros.Tiempo_Limpieza = model.Tiempo_Limpieza.Value;
                parametros.Tiempo_Revision = model.Tiempo_Revision.Value;
                parametros.Numero_Inicio_Boleta = model.Numero_Inicio_Boleta.Value;
                parametros.Numero_Fin_Boleta = model.Numero_Fin_Boleta.Value;
                parametros.Fecha_Modificado = DateTime.Now;
                try
                {
                    parametrosRepository.Editar(parametros);

                    model.Result = EnumActionResult.Saved;
                }
                catch (Exception ex)
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

        public ActionResult DeleteParametros(Guid parametroId)
        {
            DeleteViewModel deleteViewModel = new DeleteViewModel();
            deleteViewModel.ParametroId = parametroId;

            return PartialView(deleteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteParametros(DeleteViewModel model)
        {
            ParametrosRepository parametrosRepository = container.Resolve<ParametrosRepository>();

            ModelState.Clear();
            //Se debe validar que no tenga relaciones con otras entidades caso contrario se mostrara un mensaje

            if (ModelState.IsValid)
            {
                try
                {
                    parametrosRepository.Eliminar(model.ParametroId);

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

        public ActionResult EstadoParametros(Guid parametroId)
        {
            ParametrosRepository parametrosRepository = container.Resolve<ParametrosRepository>();
            Parametros parametro = parametrosRepository.ObtenerPorId(parametroId);
            EstadoViewModel estadoViewModel = new EstadoViewModel();
            estadoViewModel.ParametroId = parametroId;
            estadoViewModel.Titulo = parametro.Activado ? "Desactivar Parametro" : "Activar Parametro";
            return PartialView(estadoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EstadoParametros(EstadoViewModel model)
        {
            ParametrosRepository parametrosRepository = container.Resolve<ParametrosRepository>();

            ModelState.Clear();
            //Se debe validar que no tenga relaciones con otras entidades caso contrario se mostrara un mensaje

            if (ModelState.IsValid)
            {
                try
                {
                    parametrosRepository.CambiarEstado(model.ParametroId);

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

        public ActionResult LoadGrid([DataSourceRequest]DataSourceRequest request)
        {
            decimal test = 9.2M;
            decimal a = test;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-BO");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-BO");
            decimal b = test;
            decimal test3 = 9.2M;
            decimal z = test3;

            ParametrosRepository parametrosRepository = container.Resolve<ParametrosRepository>();
            List<NavegadorViewModel> toReturn = parametrosRepository.ObtenerTodo().Select(t => new NavegadorViewModel()
            {
                ParametroId = t.ParametroId,
                Nombre = t.Nombre,
                Activado = t.Activado == true ? "Activado" : "Desactivado",
                Costo_Habitacion = t.Costo_Habitacion
            }).OrderBy(y => y.Nombre).ToList();
            return Json(toReturn.ToDataSourceResult(request));
        }
        #endregion
    }
}