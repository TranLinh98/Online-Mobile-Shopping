using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebBanHang
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application["PeopleVisits"] = 0;
            Application["PeopleOnlines"] = 0;
        }
        protected void Session_Start()
        {
            Application.Lock();
            Application["PeopleVisits"] = (int)Application["PeopleVisits"] + 1;
            Application["PeopleOnlines"] = (int)Application["PeopleOnlines"] + 1;
            Application.UnLock();
        }
        protected void Session_End()
        {
            Application.Lock();          
            Application["PeopleOnlines"] = (int)Application["PeopleOnlines"] -1;
            Application.UnLock();
        }
    }
}
