using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations
{
    public class AlmacenConfiguration : EntityTypeConfiguration<Almacen>
    {
        #region Others

        public AlmacenConfiguration()
        {
            HasKey(k => new { k.AlmacenId });
            ToTable("Almacen");
        }

        #endregion
    }
}
