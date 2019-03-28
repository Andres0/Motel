using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations
{
    public class ClienteConfiguration : EntityTypeConfiguration<Cliente>
    {
        #region Others

        public ClienteConfiguration()
        {
            HasKey(k => new { k.ClienteId });
            ToTable("Cliente");
        }

        #endregion
    }
}
