using DS.Motel.Clients.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Finances.Models.Transaccion
{
    public class AddViewModel
    {
        public EnumActionResult Result { get; set; }


        public Guid CuentaId { get; set; }
        public bool IngresoEgreso { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Fin { get; set; }
        public string Concepto { get; set; }
        public decimal Monto { get; set; }
        public List<DropdownListViewModel> Cuentas { get; set; }
    }
}