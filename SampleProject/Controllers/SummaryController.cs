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
    [RoutePrefix("summary")]
    [Authorize]
    public class SummaryController : Controller
    {
        private SampleProjectContext _db;
        private readonly IAuthenticationManager _auth;

        public SummaryController(IAuthenticationManager auth, SampleProjectContext db)
        {
            _auth = auth;
            _db = db;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            Models.Summary model = new Models.Summary();

            model.TotalNumberOfContacts = await _db.Contacts.CountAsync();
            if (model.TotalNumberOfContacts > 0)
            {
                model.TotalNumberOfComputers = await _db.Contacts.SumAsync(u => u.NumberOfComputers);
                model.UsersWithHomeAddresses = await _db.Contacts.Where(u => u.Address.Where(a => a.Type == "0").FirstOrDefault() != null).CountAsync();
                model.UsersWithWorkAddresses = await _db.Contacts.Where(u => u.Address.Where(a => a.Type == "1").FirstOrDefault() != null).CountAsync();
            }
            return View(model);
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