using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImoAnalyticsSystem.Startup))]
namespace ImoAnalyticsSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }

        public void CreateRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<Corretor>(new UserStore<Corretor>(context));

            if (!roleManager.RoleExists("Gerente"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Gerente";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Corretor"))
            {

                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Corretor";
                roleManager.Create(role);
            }
        }
    }
}
