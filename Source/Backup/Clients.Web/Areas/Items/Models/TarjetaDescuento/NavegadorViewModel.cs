using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Items.Models.TarjetaDescuento
{
    public class NavegadorViewModel
    {
       
        public Guid TarjetaDescuentoId { get; set; }
        public string Codigo { get; set; }
        public int NroUsos { get; set; }
        public int NroUsadas { get; set; }
        public decimal Costo { get; set; }
        public decimal Porcentaje { get; set; }
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaUltimoUso { get; set; }
        public bool Activado { get; set; }
       
    }
}