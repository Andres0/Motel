using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Finances.Models.Cuenta_Banco
{
    public class NavegadorViewModel
    {
        #region Fields & Properties

        public Guid CuentaId { get; set; }
        public string Numero { get; set; }
        public string Titular { get; set; }
        public string Banco { get; set; }

        #endregion
    }
}