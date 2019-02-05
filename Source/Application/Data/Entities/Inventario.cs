using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class Inventario
    {
        #region Fields & Properties

        public Guid InventarioId { get; set; }
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Total { get; set; }
        public InventarioTipo Tipo { get; set; }
        public Guid? DeAlmacenId { get; set; }
        public Guid? AAlmacenId { get; set; }
        public virtual Almacen De { get; set; }
        public virtual Almacen A { get; set; }
        public virtual List<InventarioDetalle> Detalle { get; set; }

        #endregion



        #region Constructors

        public Inventario()
        {
            InventarioId = Guid.NewGuid();
            Detalle = new List<InventarioDetalle>();
        }

        #endregion
    }



    public enum InventarioTipo
    {
        Ingreso = 1,
        Egreso = 2,
        Transferencia = 3
    }
}
