using DS.Motel.Clients.Web.Models;
using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.AddressBook.Models.Personal
{
    public class  EditViewModel
    {
        public EnumActionResult Result { get; set; }

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
        public PersonalEstado Estado { get; set; }
        public Guid UserTypeId { get; set; }
        public Guid CargoId { get; set; }
        public List<DropdownListViewModel> Cargos { get; set; }
        public List<DropdownListViewModel> TipoUsuario { get; set; }
    }
}