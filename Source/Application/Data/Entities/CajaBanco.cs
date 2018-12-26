using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class CajaBanco
    {
        #region Fields & Properties

        public Guid CajaBancoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public CajaBancoTipo Tipo { get; set; }

        #endregion



        #region Constructors

        public CajaBanco()
        {
            CajaBancoId = Guid.NewGuid();
        }

        #endregion
    }
}
