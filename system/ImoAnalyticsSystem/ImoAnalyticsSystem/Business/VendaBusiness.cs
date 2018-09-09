using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class VendaBusiness
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private ImobiliariaBusiness imobiliariaBusiness = new ImobiliariaBusiness();


        public List<Venda> getVendas()
        {
            return db.Venda.Include(v => v.Corretor).Include(v => v.Imovel).Include(v => v.Interessado).ToList();
        }

        public List<Venda> GetVendasByStartAndEndTime(DateTime? startTime, DateTime? endTime)
        {
            return db.Venda.Include(v => v.Corretor).Include(v => v.Imovel).Include(v => v.Interessado).Where(v => v.DataVenda >= startTime && v.DataVenda <= endTime).ToList();
        }

        public List<Venda> GetVendasByStartTime(DateTime? startTime)
        {
            return db.Venda.Include(v => v.Corretor).Include(v => v.Imovel).Include(v => v.Interessado).Where(v => v.DataVenda >= startTime).ToList();
        }

        public List<Venda> GetVendasByEndTime(DateTime? endTime)
        {
            return db.Venda.Include(v => v.Corretor).Include(v => v.Imovel).Include(v => v.Interessado).Where(v => v.DataVenda <= endTime).ToList();
        }

        public Venda FindById(int? id)
        {
            return db.Venda.Find(id);
        }

        public String Create(Venda venda)
        {
            CalculaComissoes(venda);
            db.Venda.Add(venda);
            db.SaveChanges();
            return "OK";
        }

        public String Edit(Venda venda)
        {
            CalculaComissoes(venda);
            db.Entry(venda).State = EntityState.Modified;
            CalculaComissoes(venda);
            db.SaveChanges();
            return "OK";
        }

        public void Delete(Venda venda)
        {
            db.Venda.Remove(venda);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        
        public void CalculaComissoes(Venda venda)
        {
            Imobiliaria imobiliaria = imobiliariaBusiness.GetInstance();
            venda.ComissaoImobiliaria = venda.ValorVenda * imobiliaria.ComissaoImobiliariaVenda;
            venda.ComissaoCorretor = venda.ComissaoImobiliaria * imobiliaria.ComissaoCorretorVenda;
        }



    }
}