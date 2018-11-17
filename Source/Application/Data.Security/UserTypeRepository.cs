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






        #region Manipulation
        public void Add(UserType_SEC userType, bool commit)
        {
            _context.Security_UserType.Add(userType);
            _context.SaveChanges();
        }

        public void Edit(UserType_SEC userType, bool commit)
        {
            //_context.Entry(UserType_SEC).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        #endregion






        #region Queries

        public IQueryable<UserType_SEC> GetAll()
        {
            return _context.Security_UserType;
        }

        public UserType_SEC GetSingle(Guid? userTypeId)
        {
            return _context.Security_UserType.SingleOrDefault(s => s.UserTypeId == userTypeId);
        }

        #endregion
    }
}
