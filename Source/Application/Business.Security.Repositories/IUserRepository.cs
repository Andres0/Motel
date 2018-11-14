using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DS.Motel.Business.Security.Entities;

namespace DS.Motel.Business.Security.Repositories
{
    public interface IUserRepository
    {
        User_SEC GetUserByUsernameAndPassword(string username, string password);
    }
}
