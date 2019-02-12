using DS.Motel.Clients.Web.Models;
using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Finances.Models.Transaccion
{
    public class EditViewModel
    {
        public EnumActionResult Result { get; set; }


        public Guid TransaccionId { get; set; }
        public Guid CuentaId { get; set; }
        public TransaccionTipo Tipo { get; set; }
        public DateTime? Fecha_Inicio { get; set; }
        public DateTime? Fecha_Fin { get; set; }
        public string Concepto { get; set; }
        public decimal Monto { get; set; }
        public List<DropdownListViewModel> Cuentas { get; set; }
    }
}