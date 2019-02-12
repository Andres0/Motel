using DS.Motel.Clients.Web.Areas.Reportes.Models.Transaccion;
using DS.Motel.Clients.Web.Areas.Reportes.Models.Viewer;
using DS.Motel.Clients.Web.Models;
using DS.Motel.Data.Entities;
using DS.Motel.Data.Finances;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace DS.Motel.Clients.Web.Areas.Reportes.Controllers
{
    public class ReporteController : Controller
    {
        #region Fields & Properties

        private IUnityContainer container;
        #endregion



        #region Constructor
        public ReporteController(IUnityContainer _container)
        {
            container = _container;
        }

        #endregion



        #region Interfaces
        public ActionResult Index()
        {
            return View();
        }

        #endregion



        #region ListadoPersonal
        public ActionResult ListadoPersonal()
        {
            return View();
        }

        public ActionResult CargarListadoPersonal()
        {
            return View();   
        }

        #endregion



        #region ListadoTransaccion

        public ActionResult ListadoTransacciones()
        {
            ViewData["Cuentas"] = ObtenerCuentas();
            return View();
        }

        public ActionResult RefreshListadoTransacciones(Guid cuentaId)
        {
            SessionViewModel sessionViewModel = (SessionViewModel)Session["System_Information"];
            Telerik.Reporting.TypeReportSource reportSource = new Telerik.Reporting.TypeReportSource();
            CajaBancoRepository cajaBancoRepository = container.Resolve<CajaBancoRepository>();
            CajaBanco cajaBanco = cajaBancoRepository.ObtenerPorId(cuentaId);

            reportSource.TypeName = "DS.Motel.Clients.Web.Areas.Reportes.Diseños.Transaccion.RptListadoTransacciones, DS.Motel.Clients.Web";
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("CuentaId", cuentaId.ToString()));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("ContactName", sessionViewModel.Nombre));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("DateTime", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("CuentaNombre", cajaBanco != null ? cajaBanco.Nombre : string.Empty));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("CuentaDetalle", cajaBanco != null ? cajaBanco.Descripcion : string.Empty));

            ReportViewModel reportViewModel = new ReportViewModel();
            reportViewModel.ReportGenerate = reportSource;

            return PartialView("../Viewer/ReporteTemplate", reportViewModel);
        }

        #endregion



            #region Otros

            public List<DropdownListViewModel> ObtenerCuentas()
        {
            CajaBancoRepository cuentaRepository = container.Resolve<CajaBancoRepository>();
            List<DropdownListViewModel> toReturn = cuentaRepository.ObtenerTodo().Where(w => w.Tipo == CajaBancoTipo.Banco).Select(x => new DropdownListViewModel()
            {
                Id = x.CajaBancoId,
                Nombre = x.Nombre
            }).OrderBy(y => y.Nombre).ToList();
            if (toReturn.Count() > 0)
            {
                toReturn.Insert(0, new DropdownListViewModel() { Id = Guid.Empty, Nombre = "Seleccione una cuenta" });
            }
            else
            {
                toReturn.Add(new DropdownListViewModel() { Id = Guid.Empty, Nombre = "No hay datos" });
            }
            return toReturn;
        }

        #endregion
    }
}