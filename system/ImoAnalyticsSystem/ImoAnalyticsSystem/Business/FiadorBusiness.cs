using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class FiadorBusiness
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public string Create(Fiador fiador)
        {
            var cpf = db.Fiador.Where
                (
                    f => f.Cpf == fiador.Cpf
                );

            var rg = db.Fiador.Where
                (
                    f => f.Rg == fiador.Rg
                );

            var email = db.Fiador.Where
                (
                    f => f.Email == fiador.Email
                );

            if (cpf.Count() == 0 && rg.Count() == 0 && email.Count() == 0)
            {
                db.Fiador.Add(fiador);
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