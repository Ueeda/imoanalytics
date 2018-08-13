using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ImoAnalyticsSystem.Models
{
    // You can add profile data for the user by adding more properties to your Corretor class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Corretor : IdentityUser
    {
        //public String NomeCompleto { get; set; }
        //public String Cpf { get; set; }
        //public String Rg { get; set; }
        //public DateTime DataNascimento { get; set; }
        //public String Telefone { get; set; }
        //public String Cep { get; set; }
        //public String Endereco { get; set; }
        //public int Numero { get; set; }
        //public String Bairro { get; set; }
        //public String Cidade { get; set; }
        //public String Estado { get; set; }
        //public int Creci { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Corretor> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<Corretor>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}