using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations
{
    public class UserTypeConfiguration : EntityTypeConfiguration<UserType>
    {
        #region Others

        public UserTypeConfiguration()
        {
            HasKey(k => new { k.UserTypeId });
            ToTable("UserType");
        }

        #endregion
    }
}
