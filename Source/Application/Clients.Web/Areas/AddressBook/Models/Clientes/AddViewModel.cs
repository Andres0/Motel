using DS.Motel.Clients.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.AddressBook.Models.Clientes
{
    public class AddViewModel
    {
        public EnumActionResult Result { get; set; }

        public Guid SuiteId { get; set; }
        public string Placa { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
    }
}