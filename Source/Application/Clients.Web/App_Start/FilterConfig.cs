using System.Web;
using System.Web.Mvc;

namespace DS.Motel.Clients.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new AuthorizeAttribute());  //para que funcione el forms autentication
            //filters.Add(new AuthorizationContext());
        }
    }
}
