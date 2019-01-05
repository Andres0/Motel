using DS.Motel.Clients.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Items.Models.Items
{
    public class EditViewModel
    {
        public EnumActionResult Result { get; set; }


        public Guid ItemId { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad_Stock { get; set; }
        public decimal Costo_Total { get; set; }
        public decimal Costo_Unitario { get; set; }
        public decimal Proporcion { get; set; }
        public decimal Stock_Minimo { get; set; }
        public bool EsVendible { get; set; }
        public Guid CategoriaId { get; set; }
        public List<DropdownListViewModel> Categorias { get; set; }
    }
}