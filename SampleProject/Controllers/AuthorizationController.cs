using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SampleProject.Infrastructure;
using SampleProject.Models;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SampleProject.Controllers
{
    [RoutePrefix("authorization")]
    public class AuthorizationController : Controller
    {
        private SampleProjectContext _db;
        private readonly IAuthenticationManager _auth;

        public AuthorizationController(IAuthenticationManager auth, SampleProjectContext db)
        {
            _auth = auth;
            _db = db;
        }

        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> Login([Bind(Include = "EmailAddress,Password")] Authorization model)
        {
            string hashedPassword = Hash.ComputeHash(model.Password, "MD5", Encoding.ASCII.GetBytes(Infrastructure.Constants.PasswordSalt));
            User user = await _db.Users.Where(u => u.EmailAddress == model.EmailAddress && u.Password == hashedPassword).FirstOrDefaultAsync();

            if (null == user)
            {
                return View("LoginResult");
            }
            else
            {
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, model.EmailAddress), }, DefaultAuthenticationTypes.ApplicationCookie);

                this._auth.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                }, identity);
            }

            return RedirectToAction("index", "contacts", null);
        }

        [Route("signout")]
        [HttpGet]
        public ActionResult Signout()
        {
            Request.GetOwinContext().Authentication.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home", null);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}