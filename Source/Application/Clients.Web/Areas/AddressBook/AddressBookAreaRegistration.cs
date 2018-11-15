using System.Web.Mvc;

namespace DS.Motel.Clients.Web.Areas.AddressBook
{
    public class AddressBookAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AddressBook";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AddressBook_default",
                "AddressBook/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}