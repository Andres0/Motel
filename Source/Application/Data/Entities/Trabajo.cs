using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class Trabajo
    {
        #region Fields & Properties

        public Guid TrabajoId { get; set; }
        public Guid PersonalId { get; set; }
        public DateTime Ingreso { get; set; }
        public DateTime? Egreso { get; set; }
        public virtual Personal Personal { get; set; }

        #endregion



        #region Constructors

        public Trabajo()
        {
            TrabajoId = Guid.NewGuid();
        }

        #endregion
    }
}
