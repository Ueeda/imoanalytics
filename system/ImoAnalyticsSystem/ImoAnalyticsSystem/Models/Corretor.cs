using ImoAnalyticsSystem.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ImoAnalyticsSystem.Models
{
    public class Corretor : IdentityUser
    {
        // Propriedades do corretor
        public String NomeCompleto { get; set; }
        public String Cpf { get; set; }
        public String Rg { get; set; }
        public DateTime DataNascimento { get; set; }
        public String Cep { get; set; }
        public String Endereco { get; set; }
        public int Numero { get; set; }
        public String Bairro { get; set; }
        public String Cidade { get; set; }
        public String Estado { get; set; }
        public int Creci { get; set; }
        public bool Ativo { get; set; }

        //Métodos auto gerados pelo módulo Identity do Entity Framework
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Corretor> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    
}