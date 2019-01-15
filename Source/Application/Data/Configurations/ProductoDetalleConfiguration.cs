using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations
{
    public class ProductoDetalleConfiguration : EntityTypeConfiguration<ProductoDetalle>
    {
        #region Others

        public ProductoDetalleConfiguration()
        {
            HasKey(k => new { k.ProductoDetalleId });
            ToTable("ProductoDetalle");
        }

        #endregion
    }
}
