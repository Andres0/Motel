using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Models
{
    public class SessionViewModel
    {
        public Guid PersonalId { get; set; }
        public string Nombre { get; set; }

        public Guid CajaChicaId { get; set; }
    }
}