using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using SampleProject.Models;

namespace SampleProject.Controllers
{
    [Authorize]
    [RoutePrefix("contacts")]
    public class ContactsController : Controller
    {
        private SampleProjectContext _db;
        private readonly IAuthenticationManager _auth;

        public ContactsController(IAuthenticationManager auth, SampleProjectContext db)
        {
            _auth = auth;
            _db = db;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await _db.Contacts.ToListAsync());
        }

        [Route("details")]
        [HttpGet]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact user = await _db.Contacts.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [Route("create")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,Password,BirthDate,NumberOfComputers")] Contact user)
        {
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();
                user.Password = Infrastructure.Hash.ComputeHash(user.Password, "MD5", Encoding.ASCII.GetBytes(Infrastructure.Constants.PasswordSalt));
                _db.Contacts.Add(user);
                await _db.SaveChangesAsync();
                return RedirectToAction("index");
            }

            return View(user);
        }

        [Route("edit")]
        [HttpGet]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact user = await _db.Contacts.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [Route("edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,Password,BirthDate,NumberOfComputers")] Contact user)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(user).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                user = await _db.Contacts.FindAsync(user.Id);
                return RedirectToAction("index");
            }
            return View(user);
        }

        [Route("delete")]
        [HttpGet]  
        public async Task<ActionResult> Delete(Guid? id)
        {
            Contact user = await _db.Contacts.FindAsync(id);
            _db.Contacts.Remove(user);
            await _db.SaveChangesAsync();
            return RedirectToAction("index");
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
