using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class Transaccion
    {
        #region Fields & Properties

        public Guid TransaccionId { get; set; }
        public Guid CuentaId { get; set; }
        public TransaccionTipo Tipo { get; set; }
        public DateTime Fecha_Ini { get; set; }
        public DateTime Fecha_Fin { get; set; }
        public DateTime Fecha_Transaccion { get; set; }
        public string Concepto { get; set; }
        public decimal Deposito { get; set; }
        public decimal Retiro { get; set; }
        public decimal Saldo { get; set; }
        public virtual CajaBanco Cuenta { get; set; }

        #endregion






        #region Constructors

        public Transaccion()
        {
            TransaccionId = Guid.NewGuid();
        }

        #endregion
    }
}
