using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Text;
using SampleProject.Infrastructure;

namespace SampleProject.Models
{
    public class SampleProjectContext : DbContext
    {
        public SampleProjectContext() : base("name=SampleProjectContext")
        {
            Database.SetInitializer(new SampleDBInitializer());
        }

        public System.Data.Entity.DbSet<SampleProject.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<SampleProject.Models.Contact> Contacts { get; set; }

        public System.Data.Entity.DbSet<SampleProject.Models.Address> Addresses { get; set; }
    }

    public class SampleDBInitializer : DropCreateDatabaseAlways<SampleProjectContext>
    {
        protected override void Seed(SampleProjectContext context)
        {
            if (context.Users.Where(x => x.EmailAddress == "sn3ll@hotmail.com").FirstOrDefault() == null)
            {
                string hashedPassword = Hash.ComputeHash("sn3ll!", "MD5", Encoding.ASCII.GetBytes(Infrastructure.Constants.PasswordSalt));
                context.Users.Add(
                  new User { Password= hashedPassword, EmailAddress = "sn3ll@hotmail.com" }
                );
                context.SaveChanges();
            }
            base.Seed(context);
        }
    }
}
