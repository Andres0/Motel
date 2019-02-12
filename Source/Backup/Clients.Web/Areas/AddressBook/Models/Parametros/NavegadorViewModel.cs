using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.AddressBook.Models.Parametros
{
    public class NavegadorViewModel
    {
        public Guid ParametroId { get; set; }
        public string Nombre { get; set; }
        public decimal Costo_Habitacion { get; set; }
        public string Activado { get; set; }
    }
}