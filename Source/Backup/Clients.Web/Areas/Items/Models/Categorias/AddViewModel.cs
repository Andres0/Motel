using DS.Motel.Clients.Web.Models;
using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Items.Models.Categorias
{
    public class AddViewModel
    {
        public EnumActionResult Result { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public ItemCategoriaTipo Tipo { get; set; }
        public Guid? PadreId { get; set; }
        public List<DropDownListHierarchyViewModel> Categorias { get; set; }
    }
}