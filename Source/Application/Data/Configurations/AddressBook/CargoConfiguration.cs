using DS.Motel.Business.AddressBook.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations.AddressBook
{
   public class CargoConfiguration : EntityTypeConfiguration<Cargo_ADB>
    {
        #region Others

        public CargoConfiguration()
        {
            HasKey(k => new { k.CargoId });
            ToTable("AddressBook_Cargo");
        }

        #endregion
    }
}
