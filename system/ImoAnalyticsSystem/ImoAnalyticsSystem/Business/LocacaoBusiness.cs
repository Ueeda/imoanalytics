using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class LocacaoBusiness
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Locacao> GetLocacoes()
        {
            return db.Locacao.ToList();
        }
    }
}