using DS.Motel.Clients.Web.Areas.Reportes.Models.Transaccion;
using DS.Motel.Data.Finances;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Unity;

namespace DS.Motel.Clients.Web.Areas.Reportes.Diseños.Transaccion
{
    [DataObject]
    public class RptListadoTransacciones_Q
    {
        #region Fields & Properties

        private IUnityContainer container = new UnityContainer();

        #endregion






        #region Constructors

        public RptListadoTransacciones_Q()
        {
        }

        #endregion






        #region Events

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<ReportListadoTransacciones> ListadoTransaccionesDatos(string cuentaId)
        {
            List<ReportListadoTransacciones> toReturn = new List<ReportListadoTransacciones>();
            TransaccionRepository transaccionRepository = container.Resolve<TransaccionRepository>();
            foreach (var item in transaccionRepository.ObtenerTodo().Where(w => w.CajaBancoId == new Guid(cuentaId)))
            {
                toReturn.Add(new ReportListadoTransacciones()
                {
                    Fecha = item.Fecha_Transaccion.ToString("dd/MM/yyyy"),
                    Concepto = item.Concepto,
                    Deposito = item.Deposito,
                    Retiro = item.Retiro,
                    Saldo = item.Saldo,
                });
            }
            return toReturn;
        }

        #endregion
    }
}