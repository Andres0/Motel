using DS.Motel.Clients.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Items.Models.Consumo
{
    public class AddViewModel
    {
        public EnumActionResult Result { get; set; }

        public Guid UsoHabitacionId { get; set; }
        public List<AddDetailViewModel> Detalles { get; set; }


        public AddViewModel()
        {
            Detalles = new List<AddDetailViewModel>();
        }
    }


    public class AddDetailViewModel
    {
        public Guid ConsumoId { get; set; }
        public int Indice { get; set; }
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public Guid ProductoId { get; set; }
        public string ProductoName { get; set; }
        public int Cantidad { get; set; }
        public bool Borrar { get; set; }

        public string ItemIdError { get; set; }
        public string ProductoIdError { get; set; }
        public string CantidadError { get; set; }

        public AddDetailViewModel()
        {
            ItemIdError = string.Empty;
            ProductoIdError = string.Empty;
            CantidadError = string.Empty;
        }
    }
}