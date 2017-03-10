using System.Web.Mvc;

namespace SampleProject.Controllers
{
    [RoutePrefix("home")]
    public class HomeController : Controller
    {
        [Authorize]
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}