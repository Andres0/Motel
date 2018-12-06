using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations
{
    public class ParametrosConfiguration : EntityTypeConfiguration<Parametros>
    {
        #region Others

        public ParametrosConfiguration()
        {
            HasKey(k => new { k.ParametroId });
            ToTable("Parametros");
        }

        #endregion
    }
}
