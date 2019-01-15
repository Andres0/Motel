using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class ProductoDetalle
    {
        #region Fields & Properties

        public Guid ProductoDetalleId { get; set; }
        public int Cantidad { get; set; }
        public Guid ItemId { get; set; }
        public virtual Item Item { get; set; }
        public virtual Producto Producto { get; set; }

        #endregion






        #region Constructors

        public ProductoDetalle()
        {
            ProductoDetalleId = Guid.NewGuid();
        }

        #endregion
    }
}
