using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using SampleProject.Models;

namespace SampleProject.Controllers
{
    [Authorize]
    [RoutePrefix("addresses")]
    public class AddressesController : Controller
    {
        private SampleProjectContext _db;
        private readonly IAuthenticationManager _auth;

        public AddressesController(IAuthenticationManager auth, SampleProjectContext db)
        {
            _auth = auth;
            _db = db;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult> Index(Guid id)
        {
            return View(await _db.Addresses.Where(a => a.ContactId == id).ToListAsync());
        }

        [Route("details")]
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = await _db.Addresses.FindAsync(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        [Route("create")]
        [HttpGet]
        public ActionResult Create(Guid? id)
        {
            Address a = new Address();
            a.ContactId = id.Value;
            return View(a);
        }

        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ContactId,AddressLineOne,AddressLineTwo,City,State,Zip,Type")] Address address)
        {
            if (ModelState.IsValid)
            {
                address.Id = Guid.NewGuid();
                _db.Addresses.Add(address);
                await _db.SaveChangesAsync();
                return RedirectToAction("edit", "contacts", new { id = address.ContactId });
            }

            return View(address);
        }

        [Route("edit")]
        [HttpGet]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = await _db.Addresses.FindAsync(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        [Route("edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ContactId,AddressLineOne,AddressLineTwo,City,State,Zip,Type")] Address address)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(address).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("edit", "contacts", new { id = address.ContactId });
            }
            return View(address);
        }

        [Route("delete")]
        [HttpGet]
        public async Task<ActionResult> Delete(Guid? id)
        {
            Address address = await _db.Addresses.FindAsync(id);
            _db.Addresses.Remove(address);
            await _db.SaveChangesAsync();
            return RedirectToAction("edit", "contacts", new { id = address.ContactId });
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
