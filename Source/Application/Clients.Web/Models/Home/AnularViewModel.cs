using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Models.Home
{
    public class AnularViewModel
    {
        public EnumActionResult Result { get; set; }

        public Guid SuiteId { get; set; }
        public string Descripcion { get; set; }
        public string ErrorMessage { get; set; }
    }
}