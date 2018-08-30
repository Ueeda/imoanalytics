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

        public List<Imovel> GetImoveis()
        {
            return db.Imovel.ToList();
        }

        public Imovel FindById(int? id)
        {
            return db.Imovel.Find(id);
        }

        public string Create(Imovel imovel)
        {
            db.Imovel.Add(imovel);
            db.SaveChanges();
            return "OK";
        }

        public string Edit(Imovel imovel)
        {
            db.Entry(imovel).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return "OK";
        }

        public void Delete(Imovel imovel)
        {
            db.Imovel.Remove(imovel);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}