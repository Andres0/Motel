using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class InventarioProductoDetalle
    {
        #region Fields & Properties

        public Guid InventarioProductoDetalleId { get; set; }
        public Guid InventarioProductoId { get; set; }
        public int Indice { get; set; }
        public Guid ProductoId { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Costo { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual InventarioProducto InventarioProducto { get; set; }

        #endregion



        #region Constructors

        public InventarioProductoDetalle()
        {
            InventarioProductoDetalleId = Guid.NewGuid();
        }

        #endregion
    }
}
