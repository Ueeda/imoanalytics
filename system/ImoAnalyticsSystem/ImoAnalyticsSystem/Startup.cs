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
            CreateImobiliaria();
        }

        private void CreateImobiliaria()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var imobiliaria = context.Imobiliaria.Find(1);

            if(imobiliaria == null)
            {
                imobiliaria = new Imobiliaria();
                imobiliaria.EmailContato = "contato@toyoimoveis.com.br";
                imobiliaria.Endereco = "Rua Arion Niepce da Silva";
                imobiliaria.NomeImobiliaria = "Toyo Imoveis";
                imobiliaria.TelefoneContato = "(41) 3014-8008";
                imobiliaria.TaxaAdministracaoLocacao = 0.1;
                imobiliaria.ComissaoCorretorVenda = 0.6;
                imobiliaria.ComissaoImobiliariaVenda = 0.6;
                context.Imobiliaria.Add(imobiliaria);
                context.SaveChanges();
            }
        }

        private void CreateRoles()
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
