namespace ImoAnalytics.Models
{
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Linq;

    public class ImoAnalyticsContext : DbContext
    {
        // Your context has been configured to use a 'ImoAnalyticsContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ImoAnalytics.Models.ImoAnalyticsContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ImoAnalyticsContext' 
        // connection string in the application configuration file.
        public ImoAnalyticsContext()
            : base(ConfigurationManager.ConnectionStrings["ImoAnalyticsContext"].ConnectionString)
        {
        }

        public virtual DbSet<TipoImovel> TipoImovel { get; set; }

        public virtual DbSet<Imovel> Imovel { get; set; }

        public virtual DbSet<Cartorio> Cartorio { get; set; }

        public virtual DbSet<Proprietario> Proprietario { get; set; }

        public virtual DbSet<Corretor> Corretor { get; set; }

        public virtual DbSet<Interessado> Interessado { get; set; }

        public virtual DbSet<Fiador> Fiador { get; set; }

        public virtual DbSet<ContratoDeLocacao> ContratoDeLocacao { get; set; }

        public virtual DbSet<Proposta> Proposta { get; set; }

        public DbSet<Venda> Venda { get; set; }

        public DbSet<Visita> Visita { get; set; }

        public DbSet<Locacao> Locacao { get; set; }
    }
}