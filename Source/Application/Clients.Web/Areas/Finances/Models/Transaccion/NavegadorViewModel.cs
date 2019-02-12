using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Finances.Models.Transaccion
{
    public class NavegadorViewModel
    {
        public Guid TransaccionId { get; set; }
        public string Nro_Cuenta { get; set; }
        public string Tipo { get; set; }
        public string Fecha_Ini { get; set; }
        public string Fecha_Fin { get; set; }
        public string Fecha_Transaccion { get; set; }
        public string Concepto { get; set; }
        public string Saldo { get; set; }
    }
}