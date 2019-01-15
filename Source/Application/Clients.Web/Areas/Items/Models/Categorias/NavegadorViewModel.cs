using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Items.Models.Categorias
{
    public class NavegadorViewModel
    {
        public Guid CategoriaId { get; set; }
        public string Nombre { get; set; }
        public List<NavegadorViewModel> SubCategorias { get; set; }
    }
}