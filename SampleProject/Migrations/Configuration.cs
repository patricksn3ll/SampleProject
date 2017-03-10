namespace SampleProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<SampleProject.Models.SampleProjectContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "SampleProject.Models.SampleProjectContext";
        }

        protected override void Seed(SampleProject.Models.SampleProjectContext context)
        {
            context.Contacts.AddOrUpdate(
              c => c,
              new Contact { FirstName = "Patrick", LastName = "Snell", BirthDate = new DateTime(1974, 2, 22), EmailAddress = "sn3ll@hotmail.com" }
            );
        }
    }
}
