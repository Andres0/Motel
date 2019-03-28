using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations
{
    public class InventarioProductoDetalleConfiguration : EntityTypeConfiguration<InventarioProductoDetalle>
    {
        #region Others

        public InventarioProductoDetalleConfiguration()
        {
            HasKey(k => new { k.InventarioProductoDetalleId });
            ToTable("InventarioProductoDetalle");
        }

        #endregion
    }
}
