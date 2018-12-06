using DS.Motel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Security
{
    public class UserTypeRepository
    {
        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion






        #region Manipulation
        public void Add(UserType userType)
        {
            _context.UserType.Add(userType);
            _context.SaveChanges();
        }

        public void Edit(UserType userType)
        {
            _context.UserType.Attach(userType);
            _context.Entry(userType).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Eliminar(Guid userTypeId)
        {
            UserType userType = GetSingle(userTypeId);
            _context.UserType.Remove(userType);
            _context.SaveChanges();
        }

        #endregion






        #region Queries

        public IQueryable<UserType> GetAll()
        {
            return _context.UserType;
        }

        public UserType GetSingle(Guid? userTypeId)
        {
            return _context.UserType.SingleOrDefault(s => s.UserTypeId == userTypeId);
        }

        #endregion
    }
}
