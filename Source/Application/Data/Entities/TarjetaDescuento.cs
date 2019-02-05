using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class TarjetaDescuento
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
        public TarjetaEstados Estados { get; set; }
        public TarjetaDescuento() { TarjetaDescuentoId = Guid.NewGuid(); }

    }
    public enum TarjetaEstados {

        Creada=1,
        Vendida=2
    }
}
