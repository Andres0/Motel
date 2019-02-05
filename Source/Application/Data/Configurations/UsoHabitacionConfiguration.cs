using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations
{
    public class UsoHabitacionConfiguration : EntityTypeConfiguration<UsoHabitacion>
    {
        #region Others

        public UsoHabitacionConfiguration()
        {
            HasKey(k => new { k.UsoHabitacionId });
            Property(p => p.Numero).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            ToTable("UsoHabitacion");
        }

        #endregion
    }
}
