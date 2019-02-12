using DS.Motel.Clients.Web.Models;
using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Finances.Models.CajaChica
{
    public class EditViewModel
    {
        public EnumActionResult Result { get; set; }


        public Guid TransaccionId { get; set; }
        public Guid CuentaId { get; set; }
        public TransaccionTipo Tipo { get; set; }
        public string Concepto { get; set; }
        public decimal Monto { get; set; }
    }
}