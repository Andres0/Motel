using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DS.Motel.Clients.Web
{
    public class AuthorizeActionFilterAttribute : ActionFilterAttribute
    {
        //public override void OnActionExecuting(FilterExecutingContext filterContext)
        //{
        //    HttpSessionStateBase session = filterContext.HttpContext.Session;
        //    Controller controller = filterContext.Controller as Controller;

        //    if (controller != null)
        //    {
        //        if (session != null && session["System_Information"] == null)
        //        {
        //            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
        //        }
        //    }

        //    base.OnActionExecuting(filterContext);
        //}
    }
}