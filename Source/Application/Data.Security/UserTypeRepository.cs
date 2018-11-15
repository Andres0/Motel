using DS.Motel.Business.Security.Entities;
using DS.Motel.Business.Security.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Security
{
    public class UserTypeRepository : IUserTypeRepository
    {
        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion






        #region Queries

        public IQueryable<UserType_SEC> GetAll()
        {
            return _context.Security_UserType;
        }

        #endregion
    }
}
