using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Models.Home
{
    public class SuiteDetalleViewModel
    {
        public Guid SuiteId { get; set; }
        public string SuiteNombre { get; set; }
        public int NroUso { get; set; }
        public string Estado { get; set; }
        public string Tiempo { get; set; }
        public DateTime HoraIngreso { get; set; }
        public string TipoIngreso { get; set; }
        public decimal CostoSuite { get; set; }
        public decimal CostoTV { get; set; }
        public decimal CostoTotal { get; set; }
        public bool ExistePagoACuenta { get; set; }
    }
}