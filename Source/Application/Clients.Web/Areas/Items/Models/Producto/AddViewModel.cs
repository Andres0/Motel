using DS.Motel.Clients.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Items.Models.Producto
{
    public class AddViewModel
    {
        public EnumActionResult Result { get; set; }


        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo_Venta { get; set; }
        public Guid CategoriaId { get; set; }
        public List<DropDownListHierarchyViewModel> CategoriasProducto { get; set; }
        public List<DropDownListHierarchyViewModel> CategoriasItems { get; set; }

        public List<AddDetalleViewModel> Detalle { get; set; }
    }


    public class AddDetalleViewModel
    {
        public Guid ItemId { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public int Index { get; set; }
    }
}