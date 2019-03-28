using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations
{
    public class TrabajoConfiguration : EntityTypeConfiguration<Trabajo>
    {
        #region Others

        public TrabajoConfiguration()
        {
            HasKey(k => new { k.TrabajoId });
            ToTable("Trabajo");
        }

        #endregion
    }
}
