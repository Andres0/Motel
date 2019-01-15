using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class Producto
    {
        #region Fields & Properties

        public Guid ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo_venta { get; set; }
        public Guid ItemCategoriaId { get; set; }
        public virtual ItemCategoria Categoria { get; set; }
        public virtual List<ProductoDetalle> Detalle { get; set; }

        #endregion






        #region Constructors

        public Producto()
        {
            ProductoId = Guid.NewGuid();
            Detalle = new List<ProductoDetalle>();
        }

        #endregion
    }
}
