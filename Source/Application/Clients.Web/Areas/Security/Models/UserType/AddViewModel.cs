using DS.Motel.Clients.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Security.Models.UserType
{
    public class AddViewModel
    {
        public Guid? UserTypeIdSummary { get; set; }
        public string Name { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string TipoUser { get; set; }
        public Guid? CargoId { get; set; }
        public string Estado { get; set; }
        public string CodUserCreator { get; set; }
        public string Obervaciones { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public EnumActionResult Result { get; set; }
        public List<KeyValuePair<Guid,String>>Cargos { get; set; }
    }
}