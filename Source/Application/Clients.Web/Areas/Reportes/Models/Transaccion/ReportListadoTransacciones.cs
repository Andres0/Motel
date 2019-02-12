using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Reportes.Models.Transaccion
{
    public class ReportListadoTransacciones
    {
        public string Fecha { get; set; }
        public string Concepto { get; set; }
        public decimal Deposito { get; set; }
        public decimal Retiro { get; set; }
        public decimal Saldo { get; set; }
    }
}