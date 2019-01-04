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
        public Guid? PadreId { get; set; }
        public virtual ItemCategoria Padre { get; set; }

        #endregion



        #region MyRegion

        public ItemCategoria()
        {
            ItemCategoriaId = new Guid();
        }

        #endregion
    }
}
