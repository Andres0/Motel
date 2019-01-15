using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations
{
    public class ProductoConfiguration : EntityTypeConfiguration<Producto>
    {
        #region Others

        public ProductoConfiguration()
        {
            HasKey(k => new { k.ProductoId });
            ToTable("Producto");
        }

        #endregion
    }
}
