using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Models.Home
{
    public class IndexViewModel
    {
        public Guid SuiteId { get; set; }
        public string Nombre { get; set; }
        public SuiteEstado Estado { get; set; }
        public decimal Tiempo_Anular { get; set; }
        public DateTime Ingreso { get; set; }
        public TipoIngreso TipoIngreso { get; set; }
        public decimal Costo_Total { get; set; }
    }
}