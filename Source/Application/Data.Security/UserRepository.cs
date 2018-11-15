using DS.Motel.Business.Security.Entities;
using DS.Motel.Business.Security.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Data.Security
{
    public class UserRepository : IUserRepository
    {
        #region Fields & Properties

        private DsContext _context = new DsContext();

        #endregion




        public UserRepository()
        {
        }




        #region Queries

        public User_SEC GetUserByUsernameAndPassword(string username, string password)
        {
            return _context.Security_User.SingleOrDefault(s => s.Username == username && s.Password == password);
            //return new User_SEC();
        }

        #endregion
    }
}
