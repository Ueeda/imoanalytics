using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class InteressadoBusiness
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public string Create(Interessado interessado)
        {
            var cpf = db.Interessado.Where
                (
                    i => i.Cpf == interessado.Cpf
                );

            var rg = db.Interessado.Where
                (
                    i => i.Rg == interessado.Rg
                );

            var email = db.Interessado.Where
                (
                    i => i.Email == interessado.Email
                );

            if (cpf.Count() == 0 && rg.Count() == 0 && email.Count() == 0)
            {
                db.Interessado.Add(interessado);
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