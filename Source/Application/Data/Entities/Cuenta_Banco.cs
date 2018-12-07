using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class Cuenta_Banco
    {
        #region Fields & Properties

        public Guid CuentaId { get; set; }
        public string Numero { get; set; }
        public string Titular { get; set; }
        public string Banco { get; set; }

        #endregion



        #region Constructors

        public Cuenta_Banco()
        {
            CuentaId = Guid.NewGuid();
        }

        #endregion
    }
}
