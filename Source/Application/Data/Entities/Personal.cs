using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class Personal
    {
        #region Fields & Properties

        public Guid PersonalId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CI { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Observacion { get; set; }
        public virtual PersonalEstado Estado { get; set; }
        public Guid UserTypeId { get; set; }
        public Guid CargoId { get; set; }
        public virtual UserType TipoUsuario { get; set; }
        public virtual Cargo Cargo { get; set; }

        #endregion






        #region Constructors

        public Personal()
        {
            PersonalId = Guid.NewGuid();
        }

        #endregion
    }
}
