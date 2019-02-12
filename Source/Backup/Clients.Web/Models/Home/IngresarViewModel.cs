using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Models.Home
{
    public class IngresarViewModel
    {
        public EnumActionResult Result { get; set; }

        public Guid SuiteId { get; set; }
        public TipoIngreso TipoIngreso { get; set; }
    }
}