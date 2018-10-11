using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace ImoAnalyticsSystem.Business
{
    public class PropostaBusiness
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Proposta> GetPropostas()
        {

            return db.Proposta.Include(p => p.Imovel).Include(p => p.Interessado).ToList();

        }

        public List<Proposta> GetPropostasByImovelId(int? idImovel)
        {
            return db.Proposta.Include(p => p.Imovel).Include(p => p.Interessado).Where(p => p.ImovelId == idImovel).OrderBy(p => p.Data).ToList();
        }

        public Proposta FindById(int? id)
        {
            return db.Proposta.Find(id);
        }

        public string Create(Proposta proposta)
        {

            db.Proposta.Add(proposta);
            db.SaveChanges();
            return "OK";
        }

        public string Edit(Proposta proposta)
        {

            db.Entry(proposta).State = EntityState.Modified;
            db.SaveChanges();
            return "OK";
        }

        public void Delete(Proposta proposta)
        {
            db.Proposta.Remove(proposta);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }


    }
}