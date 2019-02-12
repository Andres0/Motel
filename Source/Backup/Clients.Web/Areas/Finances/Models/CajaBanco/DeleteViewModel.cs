using DS.Motel.Clients.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Finances.Models.CajaBanco
{
    public class DeleteViewModel
    {
        public EnumActionResult Result { get; set; }

        public Guid CajaBancoId { get; set; }
        public string ErrorMessage { get; set; }
    }
}