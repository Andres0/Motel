using DS.Motel.Business.Security.Entities;
using DS.Motel.Business.Security.Repositories;
using DS.Motel.Data.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Motel.Business.Security
{
    public class UserTypeManager : IUserTypeRepository
    {
        #region Fields & Properties

        UserTypeRepository _userTypeRepository { get; set; }

        #endregion






        #region Constructors

        public UserTypeManager(UserTypeRepository userTypeRepository)
        {
            _userTypeRepository = userTypeRepository;
        }

        #endregion






        #region Queries

        public IQueryable<UserType_SEC> GetAll()
        {
            return _userTypeRepository.GetAll();
        }

        #endregion
    }
}
