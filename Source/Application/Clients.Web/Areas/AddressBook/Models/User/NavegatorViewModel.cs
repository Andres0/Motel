using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.AddressBook.Models.User
{
    public class NavegatorViewModel
    {
        public List<NavegatorGridViewModel> Personas { get; set; }
    }
    public class NavegatorGridViewModel
    {
        public Guid CargoId { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }

    }
}