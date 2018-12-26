using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations
{
    public class TransaccionConfiguration : EntityTypeConfiguration<Transaccion>
    {
        #region Others

        public TransaccionConfiguration()
        {
            HasKey(k => new { k.TransaccionId });
            ToTable("Transaccion");
        }

        #endregion
    }
}
