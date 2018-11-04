using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class MudancaPrecoBusiness
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<MudancaPreco> ListAll()
        {
            return db.MudancaPreco.ToList();
        }

        public void StartNewHistory(Imovel imovel)
        {
            var historico = new MudancaPreco();
            historico.Locacao = imovel.Locacao;
            historico.Venda = imovel.Venda;
            if (imovel.Venda)
                historico.ValorVenda = imovel.ValorVenda;
            else
                historico.ValorVenda = 0;

            if (imovel.Locacao)
                historico.ValorLocacao = imovel.ValorLocacao;
            else
                historico.ValorLocacao = 0;

            historico.DataMudanca = DateTime.Now;
            historico.FirstRegister = true;

            imovel.HistoricoPrecos = new List<MudancaPreco>();
            imovel.HistoricoPrecos.Add(historico);
        }

        public void Create(Imovel imovel)
        {
            var historico = db.MudancaPreco.Where(p => p.ImovelId == imovel.ID).ToList();
            MudancaPreco ultimaMudanca = null;
            if (historico.Count() > 0)
                ultimaMudanca = historico.ElementAt(historico.Count - 1);

            if (ultimaMudanca != null)
            {
                if (imovel.Locacao != ultimaMudanca.Locacao || imovel.Venda != ultimaMudanca.Venda || imovel.ValorVenda != ultimaMudanca.ValorVenda || imovel.ValorLocacao != ultimaMudanca.ValorLocacao)
                {
                    var historicoNew = new MudancaPreco();
                    historicoNew.Locacao = imovel.Locacao;
                    historicoNew.Venda = imovel.Venda;
                    if (historicoNew.Venda)
                        historicoNew.ValorVenda = imovel.ValorVenda;
                    else
                        historicoNew.ValorVenda = 0;

                    if (historicoNew.Locacao)
                        historicoNew.ValorLocacao = imovel.ValorLocacao;
                    else
                        historicoNew.ValorLocacao = 0;

                    historicoNew.DataMudanca = DateTime.Now;
                    historicoNew.ImovelId = imovel.ID;
                    db.MudancaPreco.Add(historicoNew);
                    db.SaveChanges();
                }
            }
            else
            {
                var historicoNew = new MudancaPreco();
                historicoNew.Locacao = imovel.Locacao;
                historicoNew.Venda = imovel.Venda;
                if (historicoNew.Venda)
                    historicoNew.ValorVenda = imovel.ValorVenda;
                else
                    historicoNew.ValorVenda = 0;

                if (historicoNew.Locacao)
                    historicoNew.ValorLocacao = imovel.ValorLocacao;
                else
                    historicoNew.ValorLocacao = 0;

                historicoNew.DataMudanca = DateTime.Now;
                historicoNew.ImovelId = imovel.ID;
                historicoNew.FirstRegister = true;
                db.MudancaPreco.Add(historicoNew);
                db.SaveChanges();
            }
        }
    }
}