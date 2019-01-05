using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations
{
    public class ItemConfiguration : EntityTypeConfiguration<Item>
    {
        #region Others

        public ItemConfiguration()
        {
            HasKey(k => new { k.ItemId });
            ToTable("Item");
        }

        #endregion
    }
}
