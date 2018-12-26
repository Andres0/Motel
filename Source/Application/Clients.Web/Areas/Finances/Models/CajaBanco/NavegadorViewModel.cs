using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Finances.Models.CajaBanco
{
    public class NavegadorViewModel
    {
        #region Fields & Properties

        public Guid CajaBancoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        #endregion
    }
}