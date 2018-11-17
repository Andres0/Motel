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






        #region Validation



        #endregion
        
        
        
        
        
        
        #region Manipulation

        public void Add(UserType_SEC userType, bool commit)
        {
            try
            {
                _userTypeRepository.Add(userType, commit);
            }
            catch (Exception ex)
            {
            }
        }

        public void Edit(UserType_SEC userType, bool commit)
        {
            try
            {
                //Get original entity
                UserType_SEC userTypeToUpdate = _userTypeRepository.GetSingle(userType.UserTypeId);
                userTypeToUpdate.Name = userType.Name;

                _userTypeRepository.Edit(userTypeToUpdate, commit);
            }
            catch (Exception ex)
            {
            }
        }

        #endregion






        #region Queries

        public IQueryable<UserType_SEC> GetAll()
        {
            return _userTypeRepository.GetAll();
        }

        public UserType_SEC GetSingle(Guid? userTypeId)
        {
            return _userTypeRepository.GetSingle(userTypeId);
        }

        #endregion
    }
}
