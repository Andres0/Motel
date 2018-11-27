using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Entities
{
    public class Person
    {
        #region Fields & Properties

        public Guid PersonId { get; set; }
        public bool Archived { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CI { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }

        #endregion






        #region Constructors

        public Person()
        {
            PersonId = Guid.NewGuid();
        }

        #endregion
    }
}
