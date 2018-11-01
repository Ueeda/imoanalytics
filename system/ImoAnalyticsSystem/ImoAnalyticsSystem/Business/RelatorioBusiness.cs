using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
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

        public List<Relatorio> GetAllPublic()
        {
            return db.Relatorio.Where(r => r.Privado == false).ToList();
        }

        public List<Relatorio> GetAllFromUser(string id)
        {
            return db.Relatorio.Where(r => r.CorretorId.Equals(id)).ToList();
        }

        public Relatorio FindById(int? id)
        {
            return db.Relatorio.Find(id);
        }
    }
}