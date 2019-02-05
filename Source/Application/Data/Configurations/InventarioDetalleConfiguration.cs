using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations
{
    public class InventarioDetalleConfiguration : EntityTypeConfiguration<InventarioDetalle>
    {
        #region Others

        public InventarioDetalleConfiguration()
        {
            HasKey(k => new { k.InventarioDetalleId });
            ToTable("InventarioDetalle");
        }

        #endregion
    }
}
