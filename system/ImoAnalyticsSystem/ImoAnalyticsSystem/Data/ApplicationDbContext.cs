using ImoAnalyticsSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<Corretor>
    {
        public ApplicationDbContext()
            : base("ImoAnalyticsSystemConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<TipoImovel> TipoImovel { get; set; }

        public virtual DbSet<Imovel> Imovel { get; set; }

        public virtual DbSet<Cartorio> Cartorio { get; set; }

        public virtual DbSet<Proprietario> Proprietario { get; set; }

        public virtual DbSet<Interessado> Interessado { get; set; }

        public virtual DbSet<Fiador> Fiador { get; set; }

        public virtual DbSet<ContratoDeLocacao> ContratoDeLocacao { get; set; }

        public virtual DbSet<Proposta> Proposta { get; set; }

        public virtual DbSet<Venda> Venda { get; set; }

        public virtual DbSet<Visita> Visita { get; set; }

        public virtual DbSet<Locacao> Locacao { get; set; }

        public virtual DbSet<Imobiliaria> Imobiliaria { get; set; }

        public virtual DbSet<Imagem> Imagens { get; set; }

        public virtual DbSet<MudancaPreco> MudancaPreco { get; set; }
    }
}