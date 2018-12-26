using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Configurations
{
    public class CajaBancoConfiguration : EntityTypeConfiguration<CajaBanco>
    {
        #region Others

        public CajaBancoConfiguration()
        {
            HasKey(k => new { k.CajaBancoId });
            ToTable("CajaBanco");
        }

        #endregion
    }
}
