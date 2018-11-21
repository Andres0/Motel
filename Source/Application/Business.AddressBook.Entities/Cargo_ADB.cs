using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Business.AddressBook.Entities
{
    public class Cargo_ADB 
    {
        #region fields and properties

        public Guid CargoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        #endregion
    }
}
