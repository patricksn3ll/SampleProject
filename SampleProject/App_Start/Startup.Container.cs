using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin;

using Owin;

[assembly: Microsoft.Owin.OwinStartup(typeof(SampleProject.App_Start.Startup))]

namespace SampleProject.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            FilterConfig.Configure(GlobalFilters.Filters);
            RouteConfig.Configure(RouteTable.Routes);
            UnityConfig.RegisterComponents();

            ConfigureAuth(app);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Email;
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/authorization"),
            });
        }
    }
}