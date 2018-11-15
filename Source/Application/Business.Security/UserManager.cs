using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS.Motel.Business.Security.Entities;
using DS.Motel.Business.Security.Repositories;
using DS.Motel.Data.Security;

namespace DS.Motel.Business.Security
{
    public class UserManager : IUserRepository
    {
        #region Fields & Properties

        UserRepository _userRepository { get; set; }

        #endregion






        #region Constructors

        public UserManager(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion






        #region Others

        public User_SEC GetUserByUsernameAndPassword(string username, string password)
        {

            return _userRepository.GetUserByUsernameAndPassword(username, password);
        }

        #endregion
    }
}
