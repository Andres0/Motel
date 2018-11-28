using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class UserType
    {
        #region Fields & Properties

        public Guid UserTypeId { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }


        #endregion






        #region Constructors

        public UserType()
        {
            UserTypeId = Guid.NewGuid();
        }

        #endregion
    }
}
