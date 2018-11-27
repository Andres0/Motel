using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class Cargo
    {
        #region Fields & Properties

        public Guid CargoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        #endregion



        #region Constructors

        public Cargo()
        {
            CargoId = Guid.NewGuid();
        }

        #endregion
    }
}
