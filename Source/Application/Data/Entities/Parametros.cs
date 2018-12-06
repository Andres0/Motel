using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class Parametros
    {
        #region Fields & Properties

        public Guid ParametroId { get; set; }
        public string Codigo { get; set; }
        public decimal Costo_Habitacion { get; set; }
        public decimal Costo_Adicional1 { get; set; }
        public decimal Costo_Adicional2 { get; set; }
        public decimal Tiempo_Hora { get; set; }
        public decimal Tiempo_Incremento { get; set; }
        public decimal Costo_Tv { get; set; }
        public decimal Tolerancia { get; set; }
        public decimal Tiempo_Anular { get; set; }
        public decimal Tiempo_Salir { get; set; }
        public decimal Tiempo_Limpieza { get; set; }
        public decimal Tiempo_Revision { get; set; }
        public decimal Numero_Inicio_Boleta { get; set; }
        public decimal Numero_Fin_Boleta { get; set; }
        public DateTime Fecha_Modificado { get; set; }
        public Guid PersonalId { get; set; }
        public bool Activado { get; set; }
        public virtual Personal Usuario { get; set; }

        #endregion



        #region Constructors

        public Parametros()
        {
            ParametroId = Guid.NewGuid();
        }

        #endregion
    }
}
