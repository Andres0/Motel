using DS.Motel.Clients.Web.Areas.Reportes.Models.Viewer;
using DS.Motel.Clients.Web.Models;
using DS.Motel.Data.Entities;
using DS.Motel.Data.Finances;
using DS.Motel.Data.Inventarios;
using DS.Motel.Data.Items;
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



        #region Contabilidad

        public ActionResult ListadoCuentas()
        {
            return View();
        }

        public ActionResult RefreshListadoCuentas()
        {
            SessionViewModel sessionViewModel = (SessionViewModel)Session["System_Information"];
            Telerik.Reporting.TypeReportSource reportSource = new Telerik.Reporting.TypeReportSource();

            reportSource.TypeName = "DS.Motel.Clients.Web.Areas.Reportes.Diseños.Transaccion.RptListadoCuentas, DS.Motel.Clients.Web";
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("ContactName", sessionViewModel.Nombre));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("DateTime", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));

            ReportViewModel reportViewModel = new ReportViewModel();
            reportViewModel.ReportGenerate = reportSource;

            return PartialView("../Viewer/ReporteTemplate", reportViewModel);
        }



        public ActionResult ListadoTransaccionesBanco()
        {
            ViewData["Cuentas"] = ObtenerCuentas();
            return View();
        }

        public ActionResult RefreshListadoTransaccionesBanco(Guid cuentaId, DateTime start, DateTime end)
        {
            SessionViewModel sessionViewModel = (SessionViewModel)Session["System_Information"];
            Telerik.Reporting.TypeReportSource reportSource = new Telerik.Reporting.TypeReportSource();
            CajaBancoRepository cajaBancoRepository = container.Resolve<CajaBancoRepository>();
            TransaccionRepository transaccionRepository = container.Resolve<TransaccionRepository>();
            CajaBanco cajaBanco = cajaBancoRepository.ObtenerPorId(cuentaId);
            Transaccion transaccion = transaccionRepository.ObtenerTodo().Where(w => w.CajaBancoId == cuentaId && w.Fecha_Transaccion < start.Date).OrderByDescending(o => o.Fecha_Transaccion).FirstOrDefault();
            decimal saldoAnterior = transaccion == null ? 0 : transaccion.Saldo;

            reportSource.TypeName = "DS.Motel.Clients.Web.Areas.Reportes.Diseños.Transaccion.RptListadoTransacciones, DS.Motel.Clients.Web";
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("ContactName", sessionViewModel.Nombre));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("DateTime", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("CuentaId", cuentaId.ToString()));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("CuentaNombre", cajaBanco != null ? cajaBanco.Nombre : string.Empty));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("CuentaDetalle", cajaBanco != null ? cajaBanco.Descripcion : string.Empty));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("Start", start.Date));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("End", end.Date.AddDays(1)));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("SaldoAnterior", saldoAnterior));

            
            ReportViewModel reportViewModel = new ReportViewModel();
            reportViewModel.ReportGenerate = reportSource;

            return PartialView("../Viewer/ReporteTemplate", reportViewModel);
        }



        public ActionResult ListadoTransaccionesCaja()
        {
            return View();
        }

        public ActionResult RefreshListadoTransaccionesCaja(DateTime start, DateTime end)
        {
            SessionViewModel sessionViewModel = (SessionViewModel)Session["System_Information"];
            Telerik.Reporting.TypeReportSource reportSource = new Telerik.Reporting.TypeReportSource();

            TransaccionRepository transaccionRepository = container.Resolve<TransaccionRepository>();
            Transaccion transaccion = transaccionRepository.ObtenerTodo().Where(w => w.CajaBancoId == new Guid("11111111-2222-3333-4444-555555555555") && w.Fecha_Transaccion < start.Date).OrderByDescending(o => o.Fecha_Transaccion).FirstOrDefault();
            decimal saldoAnterior = transaccion == null ? 0 : transaccion.Saldo;

            reportSource.TypeName = "DS.Motel.Clients.Web.Areas.Reportes.Diseños.Transaccion.RptListadoTransaccionesCaja, DS.Motel.Clients.Web";
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("ContactName", sessionViewModel.Nombre));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("DateTime", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("Start", start.Date));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("End", end.Date.AddDays(1)));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("SaldoAnterior", saldoAnterior));

            ReportViewModel reportViewModel = new ReportViewModel();
            reportViewModel.ReportGenerate = reportSource;

            return PartialView("../Viewer/ReporteTemplate", reportViewModel);
        }

        #endregion



        #region items

        public ActionResult ItemStock()
        {
            return View();
        }

        public ActionResult RefreshItemStock()
        {
            SessionViewModel sessionViewModel = (SessionViewModel)Session["System_Information"];
            Telerik.Reporting.TypeReportSource reportSource = new Telerik.Reporting.TypeReportSource();

            reportSource.TypeName = "DS.Motel.Clients.Web.Areas.Reportes.Diseños.Items.RptItemStock, DS.Motel.Clients.Web";
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("ContactName", sessionViewModel.Nombre));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("DateTime", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));

            ReportViewModel reportViewModel = new ReportViewModel();
            reportViewModel.ReportGenerate = reportSource;

            return PartialView("../Viewer/ReporteTemplate", reportViewModel);
        }


        public ActionResult ItemKardexFisico()
        {
            ViewData["Almacen"] = ObtenerAlmacenes();
            return View();
        }

        public ActionResult RefreshItemKardexFisico(Guid? itemId, Guid? almacenId, DateTime start, DateTime end)
        {
            SessionViewModel sessionViewModel = (SessionViewModel)Session["System_Information"];
            Telerik.Reporting.TypeReportSource reportSource = new Telerik.Reporting.TypeReportSource();
            AlmacenRepository almacenRepository = container.Resolve<AlmacenRepository>();
            InventarioRepository inventarioRepository = container.Resolve<InventarioRepository>();
            ItemRepository itemRepository = container.Resolve<ItemRepository>();


            Almacen almacen = almacenRepository.ObtenerPorId(almacenId);
            Item item = itemRepository.ObtenerPorId(itemId);
            decimal saldoAnterior = inventarioRepository.ObtenerSaldoAnteriorItemIdAndAlmacenId(itemId ?? Guid.Empty, almacenId ?? Guid.Empty, start);

            reportSource.TypeName = "DS.Motel.Clients.Web.Areas.Reportes.Diseños.Items.RptItemKardexFisico, DS.Motel.Clients.Web";
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("ContactName", sessionViewModel.Nombre));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("DateTime", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("ItemId", item != null ? item.ItemId : Guid.Empty));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("AlmacenId", almacen != null ? almacen.AlmacenId : Guid.Empty));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("Start", start));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("End", end));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("SaldoAnterior", saldoAnterior));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("ItemName", item != null ? item.Nombre : ""));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("ItemCodigo", item != null ? item.Codigo : ""));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("AlmacenNombre", almacen != null ? almacen.Nombre : ""));

            ReportViewModel reportViewModel = new ReportViewModel();
            reportViewModel.ReportGenerate = reportSource;

            return PartialView("../Viewer/ReporteTemplate", reportViewModel);
        }


        public ActionResult ItemsAcabados()
        {
            return View();
        }

        public ActionResult RefreshItemsAcabados()
        {
            SessionViewModel sessionViewModel = (SessionViewModel)Session["System_Information"];
            Telerik.Reporting.TypeReportSource reportSource = new Telerik.Reporting.TypeReportSource();

            reportSource.TypeName = "DS.Motel.Clients.Web.Areas.Reportes.Diseños.Items.RptItemsAcabados, DS.Motel.Clients.Web";
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("ContactName", sessionViewModel.Nombre));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("DateTime", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));

            ReportViewModel reportViewModel = new ReportViewModel();
            reportViewModel.ReportGenerate = reportSource;

            return PartialView("../Viewer/ReporteTemplate", reportViewModel);
        }


        public ActionResult ListadoTarjetas()
        {
            return View();
        }

        public ActionResult RefreshListadoTarjetas()
        {
            SessionViewModel sessionViewModel = (SessionViewModel)Session["System_Information"];
            Telerik.Reporting.TypeReportSource reportSource = new Telerik.Reporting.TypeReportSource();

            reportSource.TypeName = "DS.Motel.Clients.Web.Areas.Reportes.Diseños.Items.RptTarjetaDescuentoListado, DS.Motel.Clients.Web";
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("ContactName", sessionViewModel.Nombre));
            reportSource.Parameters.Add(new Telerik.Reporting.Parameter("DateTime", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));

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



        public List<DropdownListViewModel> ObtenerAlmacenes()
        {
            AlmacenRepository almacenRepository = container.Resolve<AlmacenRepository>();
            List<DropdownListViewModel> toReturn = almacenRepository.ObtenerTodo().Select(x => new DropdownListViewModel()
            {
                Id = x.AlmacenId,
                Nombre = x.Nombre
            }).OrderBy(y => y.Nombre).ToList();
            if (toReturn.Count() > 0)
            {
                toReturn.Insert(0, new DropdownListViewModel() { Id = Guid.Empty, Nombre = "Seleccione un almacen" });
            }
            else
            {
                toReturn.Add(new DropdownListViewModel() { Id = Guid.Empty, Nombre = "No hay datos" });
            }
            return toReturn;
        }



        [HttpGet]
        public JsonResult LoadItem(string term)
        {
            ItemRepository itemRepository = container.Resolve<ItemRepository>();

            var toReturn = itemRepository.ObtenerTodo().Where(w => w.Nombre.Contains(term))
                .Select(t => new { value = t.ItemId, label = t.Nombre }).OrderBy(o => o.label).ToList();

            if (toReturn.Count() == 0)
            {
                toReturn.Add(new { value = Guid.Empty, label = "No hay datos" });
            }

            return Json(toReturn, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}