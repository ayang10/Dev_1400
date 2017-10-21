namespace Dev_1400.Migrations
{
    using Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Dev_1400.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Dev_1400.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //lamda expression, check to see if context, roles, look for any roles in the table name is "Admin"
            //if(!roleManager.RoleExists("Admin")) this if functions does the same thing
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            
            //assign me to the Admin role, if not already in it
            var uStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(uStore);

            if (userManager.FindByEmail("ayang014@gmail.com") == null)
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "ayang014@gmail.com",
                    Email = "ayang014@gmail.com",
                    FirstName = "A",
                    LastName = "Yang"
                }, "Password-1");
            }
            //assign me to the Admin role, if not already in it
            var userId = userManager.FindByEmail("ayang014@gmail.com").Id;
            if (!userManager.IsInRole(userId, "Admin"))
            {
                userManager.AddToRole(userId, "Admin");
            }
            
        }
    }
}
