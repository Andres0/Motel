using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Motel.Clients.Web.Areas.Security.Models.UserType
{
    public class NavigatorViewModel
    {
        #region Fields & Properties

        public Guid? UserTypeId { get; set; }
        public int GridPageSize { get; set; }
        public int GridNumberOfPage { get; set; }
        public List<NavigatorGridViewModel> UserTypes { get; set; }
        
        #endregion






        #region Constructors

        public NavigatorViewModel()
        {
            UserTypes = new List<NavigatorGridViewModel>();
        }

        #endregion
    }



    public class NavigatorGridViewModel
    {
        #region Fields & Properties

        public Guid UserTypeId { get; set; }
        public string Name { get; set; }

        #endregion
    }
}