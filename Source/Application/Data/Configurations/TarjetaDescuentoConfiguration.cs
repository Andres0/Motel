using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations
{
    public class TarjetaDescuentoConfiguration: EntityTypeConfiguration<TarjetaDescuento>
    {

        #region Others

        public TarjetaDescuentoConfiguration()
        {
            HasKey(k => new { k.TarjetaDescuentoId });
            ToTable("TarjetaDescuento");
        }

        #endregion

    }
}
