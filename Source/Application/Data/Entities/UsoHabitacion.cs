using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class UsoHabitacion
    {
        #region Fields & Properties

        public Guid UsoHabitacionId { get; set; }
        public Guid SuiteId { get; set; }
        public Guid? ClienteId { get; set; }
        public int Numero { get; set; }
        public TipoIngreso TipoIngreso { get; set; }
        public DateTime Ingreso { get; set; }
        public DateTime? Salida { get; set; }
        public DateTime? Cobro { get; set; }
        public int Tiempo_Uso { get; set; }
        public int Tiempo_Total { get; set; }
        public TipoUso TipoUso { get; set; }
        public decimal Costo { get; set; }
        public decimal CostoEspecial { get; set; }
        public string Observacion { get; set; }
        public decimal Costo_Habitacion { get; set; }
        public decimal Costo_Insumos { get; set; }
        public decimal Costo_Insumos_Externo { get; set; }
        public decimal Costo_tv { get; set; }
        public decimal Costo_Total { get; set; }
        public virtual Suite Suite { get; set; }
        public virtual Cliente Cliente { get; set; }

        #endregion






        #region Constructors

        public UsoHabitacion()
        {
            UsoHabitacionId = Guid.NewGuid();
        }

        #endregion
    }
    public enum TipoIngreso
    {
        Pie = 1,
        Particular = 2,
        Taxi = 3,
    }

    public enum TipoUso
    {
        Normal = 1,
        Interrumpido = 2,
        ReActivado = 3,
    }
}