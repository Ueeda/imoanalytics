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

        public void Create(Imovel imovel)
        {
            var historico = db.MudancaPreco.Where(p => p.ImovelId == imovel.ID).ToList();
            MudancaPreco ultimaMudanca = null;
            if (historico.Count() > 0)
                ultimaMudanca = historico.ElementAt(historico.Count - 1);

            if (imovel.Locacao != ultimaMudanca.Locacao || imovel.Venda != ultimaMudanca.Venda || imovel.ValorVenda != ultimaMudanca.ValorVenda || imovel.ValorLocacao != ultimaMudanca.ValorLocacao || ultimaMudanca == null)
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
                imovel.HistoricoPrecos = new List<MudancaPreco>();
                imovel.HistoricoPrecos.Add(historicoNew);
            }
        }
    }
}