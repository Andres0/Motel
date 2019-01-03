using DS.Motel.Clients.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.AddressBook.Models.Suites
{
    public class AddViewModel
    {
        public EnumActionResult Result { get; set; }
        public string Nombre { get; set; }
        public int EstadoId { get; set; }
        public Guid ParametroId { get; set; }
        public List<DropdownListViewModel> ListaParametros { get; set; }
        public List<DropdownListEnumViewModel> ListaEstado { get; set; }
    }
}