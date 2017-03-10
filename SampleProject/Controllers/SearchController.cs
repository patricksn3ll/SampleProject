using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Threading.Tasks;
using System.Text;
using System.Security.Claims;

using SampleProject.Models;
using SampleProject.Infrastructure;

using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;


namespace SampleProject.Controllers
{
    [RoutePrefix("search")]
    [Authorize]
    public class SearchController : Controller
    {
        private SampleProjectContext _db;
        private readonly IAuthenticationManager _auth;

        public SearchController(IAuthenticationManager auth, SampleProjectContext db)
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

        [Route("search")]
        [HttpPost]
        public async Task<ActionResult> Search([Bind(Include = "FirstName,LastName")] Search model)
        {
            List<Contact> users = await _db.Contacts.Where(x => x.FirstName.ToLower().Contains(model.FirstName.ToLower()) || x.LastName.ToLower().Contains(model.LastName.ToLower())).ToListAsync();

            return View(users);
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