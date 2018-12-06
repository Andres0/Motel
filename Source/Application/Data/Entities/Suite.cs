using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class Suite
    {
        #region Fields & Properties

        public Guid SuiteId { get; set; }
        public string Nombre { get; set; }
        public SuiteEstado Estado { get; set; }
        public Guid ParametroId { get; set; }
        public virtual Parametros Parametros { get; set; }

        #endregion



        #region Constructors

        public Suite()
        {
            SuiteId = Guid.NewGuid();
        }

        #endregion
    }
}
