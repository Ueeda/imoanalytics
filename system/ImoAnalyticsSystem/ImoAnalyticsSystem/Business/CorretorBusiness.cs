using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class CorretorBusiness
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Corretor> GetCorretores()
        {
            return db.Users.ToList();
        }

        public List<Corretor> SearchCorretoresByNome(String searchString)
        {
            return db.Users.Where(p => p.NomeCompleto.Contains(searchString)).ToList();
        }

        public Corretor GetCurrentUser(string id)
        {
            return db.Users.Single(u => u.Id == id);
        }

        public string Edit(Corretor corretor)
        {
            var cpf = db.Users.Where
                (
                    f => f.Cpf == corretor.Cpf && f.Id != corretor.Id
                );

            var rg = db.Users.Where
                (
                    f => f.Rg == corretor.Rg && f.Id != corretor.Id
                );

            var email = db.Users.Where
                (
                    f => f.Email == corretor.Email && f.Id != corretor.Id
                );

            if (cpf.Count() == 0 && rg.Count() == 0 && email.Count() == 0)
            {
                db.Entry(corretor).State = EntityState.Modified;
                db.SaveChanges();
                return "OK";
            }
            string response = "";
            if (cpf.Count() != 0)
                response += "O CPF informado já foi cadastrado. ";
            if (rg.Count() != 0)
                response += "O RG informado já foi cadastrado. ";
            if (email.Count() != 0)
                response += "O e-mail informado já foi cadastrado. ";
            return response;
        }

    }
}