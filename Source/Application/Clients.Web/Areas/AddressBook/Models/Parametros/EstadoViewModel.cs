using DS.Motel.Clients.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.AddressBook.Models.Parametros
{
    public class EstadoViewModel
    {
        public EnumActionResult Result { get; set; }

        public Guid ParametroId { get; set; }
        public string ErrorMessage { get; set; }
        public string Titulo { get; set; }
    }
}