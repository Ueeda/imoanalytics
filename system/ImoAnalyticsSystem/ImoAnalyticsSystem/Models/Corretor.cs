using ImoAnalyticsSystem.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ImoAnalyticsSystem.Models
{
    public class Corretor : IdentityUser
    {
        // Propriedades do corretor
        [Display(Name = "Nome do corretor")]
        public String NomeCompleto { get; set; }

        [Display(Name = "CPF")]
        public String Cpf { get; set; }

        [Display(Name = "RG")]
        public String Rg { get; set; }

        [Display(Name = "Data de nascimento")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "CEP")]
        public String Cep { get; set; }

        [Display(Name = "Endereço")]
        public String Endereco { get; set; }

        [Display(Name = "Número")]
        public int Numero { get; set; }

        [Display(Name = "Bairro")]
        public String Bairro { get; set; }

        [Display(Name = "Cidade")]
        public String Cidade { get; set; }

        [Display(Name = "Estado")]
        public String Estado { get; set; }

        [Display(Name = "Creci")]
        public int Creci { get; set; }

        [Display(Name = "Ativo")]
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