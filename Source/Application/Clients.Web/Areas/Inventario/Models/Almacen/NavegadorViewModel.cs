using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Inventario.Models.Almacen
{
    public class NavegadorViewModel
    {
        #region Fields & Properties

        public Guid AlmacenId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        #endregion
    }
}