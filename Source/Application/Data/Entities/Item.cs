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
        public Guid CategoriaId { get; set; }
        public virtual ItemCategoria Categoria { get; set; }

        public decimal Saldo_Inicial { get; set; }
        public decimal Cantidad_Stock { get; set; }
        public decimal Stock_Minimo { get; set; }


        #endregion






        #region Constructors

        public Item()
        {
            ItemId = Guid.NewGuid();
        }

        #endregion
    }
}
