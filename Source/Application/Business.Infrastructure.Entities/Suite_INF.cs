using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Business.Infrastructure.Entities
{
    public class Suite_INF
    {
        #region Fields & Properties

        public Guid SuiteId { get; set; }
        public string Name { get; set; }
        public SuiteState_INF State { get; set; }
        public virtual Parameter_INF Parameter { get; set; }

        #endregion






        #region Constructors

        public Suite_INF()
        {
            SuiteId = Guid.NewGuid();
        }

        #endregion
    }
}
