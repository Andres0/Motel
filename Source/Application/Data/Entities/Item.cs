using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class Item
    {
        #region Fields & Properties

        public Guid ItemId { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad_Stock { get; set; }
        public decimal Costo_Total { get; set; }
        public decimal Costo_Unitario { get; set; }
        public decimal Proporcion { get; set; }
        public decimal Stock_Minimo { get; set; }
        public bool EsVendible { get; set; }
        public Guid ItemCategoriaId { get; set; }
        public virtual ItemCategoria Categoria { get; set; }

        #endregion






        #region Constructors

        public Item()
        {
            ItemId = Guid.NewGuid();
        }

        #endregion
    }
}
