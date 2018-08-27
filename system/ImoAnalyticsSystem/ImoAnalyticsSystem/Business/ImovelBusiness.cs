using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class ImovelBusiness
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public string Create(Imovel imovel)
        {
            db.Imovel.Add(imovel);
            db.SaveChanges();
            return "OK";
        }
    }
}