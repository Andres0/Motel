using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Inventario.Models.InventarioProducto
{
    public class NavegadorViewModel
    {
        public Guid MovimientoProductoId { get; set; }
        public DateTime Fecha { get; set; }
        public string Numero { get; set; }
        public string Almacen { get; set; }
        public InventarioTipo Tipo { get; set; }
    }
}