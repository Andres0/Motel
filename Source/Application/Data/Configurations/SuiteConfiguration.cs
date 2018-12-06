using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations
{
    public class SuiteConfiguration : EntityTypeConfiguration<Suite>
    {
        #region Others

        public SuiteConfiguration()
        {
            HasKey(k => new { k.SuiteId });
            ToTable("Suite");
        }

        #endregion
    }
}
