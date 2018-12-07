using DS.Motel.Data.Entities;
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

        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<Cuenta_Banco> Cuenta_Banco { get; set; }
        public DbSet<Parametros> Parametros { get; set; }
        public DbSet<Personal> Personal { get; set; }
        public DbSet<Suite> Suite { get; set; }
        public DbSet<UserType> UserType { get; set; }

        #endregion






        #region Constructors

        public DsContext() : base("MotelDB")
        {
        }

        #endregion






        #region Events

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Configurations.CargoConfiguration());
            modelBuilder.Configurations.Add(new Configurations.Cuenta_BancoConfiguration());
            modelBuilder.Configurations.Add(new Configurations.ParametrosConfiguration());
            modelBuilder.Configurations.Add(new Configurations.PersonalConfiguration());
            modelBuilder.Configurations.Add(new Configurations.SuiteConfiguration());
            modelBuilder.Configurations.Add(new Configurations.UserTypeConfiguration());
        }

        #endregion
    }
}
