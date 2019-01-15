using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Models.Home
{
    public class IndexViewModel
    {
        public Guid SuiteId { get; set; }
        public string Nombre { get; set; }
        public int EstadoId { get; set; }
        public string EstadoNombre { get; set; }
    }
}