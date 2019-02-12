using DS.Motel.Clients.Web.Models;
using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Items.Models.Categorias
{
    public class DeleteViewModel
    {
        public EnumActionResult Result { get; set; }

        public Guid CategoriaId { get; set; }
        public ItemCategoriaTipo Tipo { get; set; }
        public string ErrorMessage { get; set; }
    }
}