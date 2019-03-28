using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class Consumo
    {
        #region Fields & Properties

        public Guid ConsumoId { get; set; }
        public Guid UsoHabitacionId { get; set; }
        public Guid? ItemId { get; set; }
        public Guid? ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal Costo { get; set; }
        public decimal CostoTotal { get; set; }
        public ConsumoEstado Estado { get; set; }
        public bool Entregado { get; set; }
        public string Observacion { get; set; }
        public int Index { get; set; }
        public virtual UsoHabitacion UsoHabitacion { get; set; }
        public virtual Item Item { get; set; }
        public virtual Producto Producto { get; set; }

        #endregion



        #region Constructors

        public Consumo()
        {
            ConsumoId = Guid.NewGuid();
        }

        #endregion
    }

    public enum ConsumoEstado
    {
        Vendido = 1,
        Devuelto = 2
    }
}
