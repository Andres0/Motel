using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Items.Models.Items
{
    public class NavegadorViewModel
    {
        public Guid ItemId { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal Cantidad_Stock { get; set; }
        public decimal Stock_Minimo { get; set; }
        public string Es_Vendible { get; set; }
    }
}