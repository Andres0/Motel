using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Business.Common.Entities
{
    public class Country_CORCOM
    {
        #region Fields & Properties

        public Guid CountryId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        #endregion






        #region Constructors

        public Country_CORCOM()
        {
            CountryId = Guid.NewGuid();
        }

        #endregion
    }
}
