using DS.Motel.Clients.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Security.Models.UserType
{
    public class EditViewModel
    {
        public Guid? UserTypeIdSummary { get; set; }
        public string Name { get; set; }
        public EnumActionResult Result { get; set; }
    }
}