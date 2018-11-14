using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS.Motel.Business.Security.Entities;
using DS.Motel.Business.Security.Repositories;

namespace DS.Motel.Business.Security
{
    public class UserManager : IUserRepository
    {
        #region Fields & Properties

        public User_SEC GetUserByUsernameAndPassword(string username, string password)
        {
            return null;
        }

        #endregion
    }
}
