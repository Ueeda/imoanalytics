using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class CartorioBusiness
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public DbSet<Cartorio> GetCartorios()
        {
            return db.Cartorio;
        }

        public Cartorio FindById(int? id)
        {
            return db.Cartorio.Find(id);
        }

        public string Create(Cartorio cartorio)
        {
            var nome = db.Cartorio.Where
                (
                    c => c.NomeCartorio == cartorio.NomeCartorio
                );

            if(nome.Count() == 0)
            {
                db.Cartorio.Add(cartorio);
                db.SaveChanges();
                return "OK";
            }
            string response = "";
            if (nome.Count() != 0)
                response += "Já existe um cartório com esse nome registrado no sistema.";
            return response;
        }

        public String Edit(Cartorio cartorio)
        {
            var nome = db.Cartorio.Where
                (
                    c => c.NomeCartorio == cartorio.NomeCartorio
                );

            if (nome.Count() == 0)
            {
                db.Entry(cartorio).State = EntityState.Modified;
                db.SaveChanges();
                return "OK";
            }
            string response = "";
            if (nome.Count() != 0)
                response += "Já existe um cartório com esse nome registrado no sistema.";
            return response;
        }

        public void Delete(Cartorio cartorio)
        {
            db.Cartorio.Remove(cartorio);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}