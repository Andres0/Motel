using DS.Motel.Clients.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Items.Models.Consumo
{
    public class DeleteViewModel
    {
        public EnumActionResult Result { get; set; }

        public Guid ConsumoId { get; set; }
        public string Observacion { get; set; }
        public string ErrorMessage { get; set; }
    }
}