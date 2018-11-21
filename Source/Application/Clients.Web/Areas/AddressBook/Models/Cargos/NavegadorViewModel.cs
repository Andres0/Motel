using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.AddressBook.Models.Cargos
{
    public class NavegadorViewModel
    {
        public List<NavegadorGridViewModel> Cargos { get; set; }
        public NavegadorViewModel()
        {
            Cargos = new List<NavegadorGridViewModel>();
        }
    }
    public class NavegadorGridViewModel
    {
        public Guid CargoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}