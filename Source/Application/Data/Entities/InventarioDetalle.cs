using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class InventarioDetalle
    {
        #region Fields & Properties

        public Guid InventarioDetalleId { get; set; }
        public Guid InventarioId { get; set; }
        public int Indice { get; set; }
        public Guid ItemId { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Costo { get; set; }
        public virtual Item Item { get; set; }
        public virtual Inventario Inventario { get; set; }

        #endregion



        #region Constructors

        public InventarioDetalle()
        {
            InventarioDetalleId = Guid.NewGuid();
        }

        #endregion
    }
}
