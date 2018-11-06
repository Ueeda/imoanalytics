using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class RelatorioBusiness
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Relatorio> GetAll()
        {
            return db.Relatorio.ToList();
        }

        public List<Relatorio> GetAllPublic(string id)
        {
            return db.Relatorio.Where(r => r.Privado == false && !r.Corretor.Id.Equals(id)).ToList();
        }

        public List<Relatorio> GetAllFromUser(string id)
        {
            return db.Relatorio.Where(r => r.CorretorId.Equals(id)).ToList();
        }

        public Relatorio FindById(int? id)
        {
            return db.Relatorio.Find(id);
        }

        public string Create(Relatorio relatorio)
        {
            db.Relatorio.Add(relatorio);
            db.SaveChanges();
            return "OK";
        }

        public string Edit(Relatorio relatorio)
        {
            db.Entry(relatorio).State = EntityState.Modified;
            db.SaveChanges();
            return "OK";
        }
    }
}