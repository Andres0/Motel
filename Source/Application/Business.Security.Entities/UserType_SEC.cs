using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Business.Security.Entities
{
    public class UserType_SEC
    {
        #region Fields & Properties

        public Guid UserTypeId { get; set; }
        public bool Archived { get; set; }
        public string Name { get; set; }

        #endregion






        #region Constructors

        public UserType_SEC()
        {
            UserTypeId = Guid.NewGuid();
        }

        #endregion
    }
}
