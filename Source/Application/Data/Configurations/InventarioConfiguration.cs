using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations
{
    public class InventarioConfiguration : EntityTypeConfiguration<Inventario>
    {
        #region Others

        public InventarioConfiguration()
        {
            HasKey(k => new { k.InventarioId });
            Property(p => p.Numero).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            ToTable("Inventario");
        }

        #endregion
    }
}
