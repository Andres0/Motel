using DS.Motel.Clients.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.AddressBook.Models.Parametros
{
    public class EditViewModel
    {

        public EnumActionResult Result { get; set; }

        public Guid ParametroId { get; set; }
        public string Nombre { get; set; }
        public decimal? Costo_Habitacion { get; set; }
        public decimal? Costo_Adicional1 { get; set; }
        public decimal? Costo_Adicional2 { get; set; }
        public decimal? Tiempo_Hora { get; set; }
        public decimal? Tiempo_Incremento { get; set; }
        public decimal? Costo_Tv { get; set; }
        public decimal? Tolerancia { get; set; }
        public decimal? Tiempo_Anular { get; set; }
        public decimal? Tiempo_Salir { get; set; }
        public decimal? Tiempo_Limpieza { get; set; }
        public decimal? Tiempo_Revision { get; set; }
        public decimal? Numero_Inicio_Boleta { get; set; }
        public decimal? Numero_Fin_Boleta { get; set; }
    }
}