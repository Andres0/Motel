using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DS.Motel.Clients.Web.Areas.AddressBook.Controllers
{
    public class UserController : Controller
    {
        #region Fields & Properties

        #endregion






        #region Constructors

        public UserController()
        {

        }

        #endregion






        #region Events
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Navigator()
        {
            return PartialView();
        }
        #endregion




    }
}