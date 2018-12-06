using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations
{
    public class PersonalConfiguration : EntityTypeConfiguration<Personal>
    {
        #region Others

        public PersonalConfiguration()
        {
            HasKey(k => new { k.PersonalId });
            ToTable("Personal");
        }

        #endregion
    }
}
