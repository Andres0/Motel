using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Models.Home
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Saldo { get; set; }
    }
}