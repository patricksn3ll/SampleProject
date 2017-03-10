using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Microsoft.Owin.Security;
using System.Web;
using System.Diagnostics.CodeAnalysis;

using SampleProject.Models;

namespace SampleProject.App_Start
{
    [ExcludeFromCodeCoverage]
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<SampleProjectContext>(new InjectionFactory(c => new SampleProjectContext()));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}