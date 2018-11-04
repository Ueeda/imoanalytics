using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class TipoImovelBusiness
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<TipoImovel> GetTiposImoveisByName(String search)
        {
            return db.TipoImovel.Where(p => p.Tipo.Contains(search)).ToList();
        }

        public List<TipoImovel> GetTiposImovel()
        {
            return db.TipoImovel.ToList();
        }

        public TipoImovel FindById(int? id)
        {
            return db.TipoImovel.Find(id);
        }

        public TipoImovel FindByName(string name)
        {

            return db.TipoImovel.Where(p => p.Tipo.Equals(name)).FirstOrDefault();

        }

        public string Create(TipoImovel tipoImovel)
        {
            var tipo = db.TipoImovel.Where
                (
                    t => t.Tipo == tipoImovel.Tipo
                );

            if (tipo.Count() == 0)
            {
                db.TipoImovel.Add(tipoImovel);
                db.SaveChanges();
                return "OK";
            }
            string response = "";
            if (tipo.Count() != 0)
                response += "Esse tipo de imóvel já foi registrado no sistema.";
            return response;
        }

        public string Edit(TipoImovel tipoImovel)
        {
            var tipo = db.TipoImovel.Where
                (
                    t => String.Compare(t.Tipo, tipoImovel.Tipo, false) == 0 && t.ID != tipoImovel.ID
                );

            if (tipo.Count() == 0)
            {
                db.Entry(tipoImovel).State = EntityState.Modified;
                db.SaveChanges();
                return "OK";
            }
            string response = "";
            if (tipo.Count() != 0)
                response += "Esse tipo de imóvel já foi registrado no sistema.";
            return response;
        }

        public void Delete(TipoImovel tipoImovel)
        {
            db.TipoImovel.Remove(tipoImovel);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}