﻿using DS.Motel.Clients.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.AddressBook.Models.Cargos
{
    public class EditViewModel
    {
        public EnumActionResult Result { get; set; }

        public Guid CargoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}