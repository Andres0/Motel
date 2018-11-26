using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.AddressBook.Models.Cargos
{
    public class NavegadorViewModel
    {
        #region Fields & Properties

        public List<NavegadorGridViewModel> Cargos { get; set; }

        #endregion



        #region Consutrctor
        public NavegadorViewModel()
        {
            Cargos = new List<NavegadorGridViewModel>();
        }
        #endregion
    }



    public class NavegadorGridViewModel
    {
        #region Fields & Properties

        public Guid CargoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        
        #endregion
    }
}