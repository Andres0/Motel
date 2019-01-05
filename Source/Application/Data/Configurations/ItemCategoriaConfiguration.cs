using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations
{
    public class ItemCategoriaConfiguration : EntityTypeConfiguration<ItemCategoria>
    {
        #region Others

        public ItemCategoriaConfiguration()
        {
            HasKey(k => new { k.ItemCategoriaId });
            ToTable("ItemCategoria");
        }

        #endregion
    }
}
