using DS.Motel.Clients.Web.Models;
using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Inventario.Models.InventarioProducto
{
    public class EditViewModel
    {
        public EnumActionResult Result { get; set; }

        public Guid InventarioProductoId { get; set; }
        public InventarioTipo Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Total { get; set; }
        public Guid DeId { get; set; }
        public Guid AId { get; set; }
        public List<DropdownListViewModel> Almacenes { get; set; }
        public List<EditDetailViewModel> Detalles { get; set; }


        public EditViewModel()
        {
            Detalles = new List<EditDetailViewModel>();
        }
    }



    public class EditDetailViewModel
    {
        public int Indice { get; set; }
        public Guid ProductoId { get; set; }
        public string ProductoName { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Costo { get; set; }
        public decimal Total { get; set; }
        public bool Borrar { get; set; }

        public string ProductoIdError { get; set; }
        public string CantidadError { get; set; }
        public string CostoError { get; set; }

        public EditDetailViewModel()
        {
            ProductoIdError = string.Empty;
            CantidadError = string.Empty;
            CostoError = string.Empty;
        }
    }
}