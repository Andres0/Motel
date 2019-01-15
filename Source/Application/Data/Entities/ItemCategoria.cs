using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class ItemCategoria
    {
        #region Fields & Properties

        public Guid ItemCategoriaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public ItemCategoriaTipo Tipo { get; set; }
        public Guid? PadreItemCategoriaId { get; set; }
        public ItemCategoria Padre { get; set; }
        public virtual List<ItemCategoria> SubCategorias { get; set; }

        #endregion



        #region MyRegion

        public ItemCategoria()
        {
            ItemCategoriaId = Guid.NewGuid();
        }

        #endregion
    }


    public enum ItemCategoriaTipo
    {
        Item = 1,
        Producto = 2
    }
}
