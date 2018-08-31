using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class ImobiliariaBusiness
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public Imobiliaria GetInstance()
        {
            return db.Imobiliaria.Find(1);
        }

        public String Edit(Imobiliaria imobiliaria)
        {
            db.Entry(imobiliaria).State = EntityState.Modified;
            db.SaveChanges();
            return "OK";
        }
    }
}