using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations
{
   public class CargoConfiguration : EntityTypeConfiguration<Cargo>
    {
        #region Others

        public CargoConfiguration()
        {
            HasKey(k => new { k.CargoId });
            ToTable("Cargo");
        }

        #endregion
    }
}
