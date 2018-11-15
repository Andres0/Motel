using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data
{
    [DbConfigurationType(typeof(DsConfiguration))]
    public class DsContext : DbContext
    {
        #region Fields & Properties

        public DbSet<DS.Motel.Business.Security.Entities.UserType_SEC> Security_UserType { get; set; }
        public DbSet<DS.Motel.Business.Security.Entities.User_SEC> Security_User { get; set; }

        #endregion






        #region Constructors

        public DsContext() : base("MotelDB")
        {
        }

        #endregion






        #region Events

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Configurations.Security.UserTypeConfiguration());
            modelBuilder.Configurations.Add(new Configurations.Security.UserConfiguration());
        }

        #endregion
    }
}
