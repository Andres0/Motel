using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Items.Models.Consumo
{
    public class NavegadorViewModel
    {
        public Guid ConsumoId { get; set; }
        public string ItemNombre { get; set; }
        public string ProductoNombre { get; set; }
        public string Cantidad { get; set; }
        public string Total { get; set; }
        public int Index { get; set; }
    }
}