using DS.Motel.Clients.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Finances.Models.Cuenta_Banco
{
    public class EditViewModel
    {
        public EnumActionResult Result { get; set; }

        public Guid CuentaId { get; set; }
        public string Numero { get; set; }
        public string Titular { get; set; }
        public string Banco { get; set; }
    }
}