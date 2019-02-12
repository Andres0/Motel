using DS.Motel.Clients.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Items.Models.TarjetaDescuento
{
    public class SaleViewModel
    {
        public EnumActionResult Result { get; set; }

        public Guid tarjetaDescuentoId { get; set; }
        public string ErrorMessage { get; set; }
    }
}