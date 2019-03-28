using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations
{
    public class ConsumoConfiguration : EntityTypeConfiguration<Consumo>
    {
        #region Others

        public ConsumoConfiguration()
        {
            HasKey(k => new { k.ConsumoId });
            ToTable("Consumo");
        }

        #endregion
    }
}
