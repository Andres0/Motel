using DS.Motel.Clients.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Items.Models.TarjetaDescuento
{
    public class EditViewModel
    {
        public EnumActionResult Result { get; set; }

        public Guid TarjetaDescuentoId { get; set; }
        public string Codigo { get; set; }
        public int NroUsos { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Costo { get; set; }
        public bool Activado { get; set; }
    }
}