using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.ModelConfiguration;

using DS.Motel.Business.Security.Entities;

namespace DS.Motel.Data.Configurations.Security
{
    public class UserConfiguration : EntityTypeConfiguration<User_SEC>
    {
        #region Others

        public UserConfiguration()
        {
            HasKey(k => new { k.UserId });
            Ignore(i => i.ConfirmPassword);
            ToTable("Security_User");
        }

        #endregion
    }
}
