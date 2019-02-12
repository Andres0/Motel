using DS.Motel.Clients.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Inventario.Models.Almacen
{
    public class EditViewModel
    {
        public EnumActionResult Result { get; set; }

        public Guid AlmacenId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}