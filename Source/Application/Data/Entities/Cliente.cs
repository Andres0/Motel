using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class Cliente
    {
        #region Fields & Properties

        public Guid ClienteId { get; set; }
        public string Placa { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
        public string TipoVehiculo { get; set; }
        public string Descripcion { get; set; }

        #endregion



        #region Constructors

        public Cliente()
        {
            ClienteId = Guid.NewGuid();
        }

        #endregion
    }
}
