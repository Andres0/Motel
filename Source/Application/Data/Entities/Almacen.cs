using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class Almacen
    {
        #region Fields & Properties

        public Guid AlmacenId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        #endregion



        #region MyRegion

        public Almacen()
        {
            AlmacenId = Guid.NewGuid();
        }

        #endregion
    }
}

