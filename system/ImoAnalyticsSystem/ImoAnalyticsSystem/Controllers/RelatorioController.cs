using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using ImoAnalyticsSystem.Business;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImoAnalyticsSystem.Controllers
{
    public class RelatorioController : Controller
    {
        // GET: Relatorio
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OperacoesUltimosQuatroMeses()
        {
            DateTime? relatorioArg1 = DateTime.Now;
            DateTime? relatorioArg2 = DateTime.Now.AddMonths(-1);
            DateTime? relatorioArg3 = DateTime.Now.AddMonths(-2);
            DateTime? relatorioArg4 = DateTime.Now.AddMonths(-3);
            string[] legenda = new string[] { relatorioArg4.Value.ToString("MMMM"), relatorioArg3.Value.ToString("MMMM"), relatorioArg2.Value.ToString("MMMM"), relatorioArg1.Value.ToString("MMMM") };

            VendaBusiness vendaBusiness = new VendaBusiness();
            LocacaoBusiness locacaoBusiness = new LocacaoBusiness();

            var locacoes = locacaoBusiness.GetLocacoes();
            var vendas = vendaBusiness.GetVendas();
            List<Series> conteudoGrafico = new List<Series>();

            int[] vendasPeriodo = new int[] 
            {
                vendas.Where(v => v.DataVenda.Year == relatorioArg4.Value.Year && v.DataVenda.Month == relatorioArg4.Value.Month).Count(),
                vendas.Where(v => v.DataVenda.Year == relatorioArg3.Value.Year && v.DataVenda.Month == relatorioArg3.Value.Month).Count(),
                vendas.Where(v => v.DataVenda.Year == relatorioArg2.Value.Year && v.DataVenda.Month == relatorioArg2.Value.Month).Count(),
                vendas.Where(v => v.DataVenda.Year == relatorioArg1.Value.Year && v.DataVenda.Month == relatorioArg1.Value.Month).Count()
            };

            int[] locacoesPeriodo = new int[]
            {
                locacoes.Where(l => l.DataOperacao.Year == relatorioArg4.Value.Year && l.DataOperacao.Month == relatorioArg4.Value.Month).Count(),
                locacoes.Where(l => l.DataOperacao.Year == relatorioArg3.Value.Year && l.DataOperacao.Month == relatorioArg3.Value.Month).Count(),
                locacoes.Where(l => l.DataOperacao.Year == relatorioArg2.Value.Year && l.DataOperacao.Month == relatorioArg2.Value.Month).Count(),
                locacoes.Where(l => l.DataOperacao.Year == relatorioArg1.Value.Year && l.DataOperacao.Month == relatorioArg1.Value.Month).Count(),
            };

            conteudoGrafico.Add(new Series
            {
                Name = "Locações",
                Data = new DotNet.Highcharts.Helpers.Data(new object[] { locacoesPeriodo[0], locacoesPeriodo[1], locacoesPeriodo[2], locacoesPeriodo[3] })
            });

            conteudoGrafico.Add(new Series
            {
                Name = "Vendas",
                Data = new DotNet.Highcharts.Helpers.Data(new object[] { vendasPeriodo[0], vendasPeriodo[1], vendasPeriodo[2], vendasPeriodo[3] })
            });

            Highcharts columnChart = new Highcharts("columnchart");
            columnChart.InitChart(new Chart()
            {
                Type = DotNet.Highcharts.Enums.ChartTypes.Column,
                BackgroundColor = new BackColorOrGradient(System.Drawing.Color.White),
                Style = "fontWeight: 'bold', fontSize: '17px'",
                BorderColor = System.Drawing.Color.LightBlue,
                BorderRadius = 0,
                BorderWidth = 3
            });
            columnChart.SetTitle(new Title()
            {
                Text = "Vendas X locações"
            });
            columnChart.SetSubtitle(new Subtitle()
            {
                Text = "Realizadas nos últimos 4 meses"
            });
            columnChart.SetXAxis(new XAxis()
            {
                Type = AxisTypes.Category,
                Title = new XAxisTitle() { Text = "Meses", Style = "fontWeight: 'bold', fontSize: '14px'" },
                Categories = legenda
            });
            columnChart.SetYAxis(new YAxis()
            {
                Title = new YAxisTitle()
                {
                    Text = "Quantidade de vendas",
                    Style = "fontWeight: 'bold', fontSize: '14px'"
                },
                ShowFirstLabel = true,
                ShowLastLabel = true,
                Min = 0
            });
            columnChart.SetLegend(new Legend
            {
                Enabled = true,
                BorderColor = System.Drawing.Color.CornflowerBlue,
                BorderRadius = 6,
                BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#EEE"))
            });
            columnChart.SetSeries(conteudoGrafico.ToArray());
            return View(columnChart);
        }

        public ActionResult VendasPorCorretorUltimosQuatroMeses()
        {
            DateTime? relatorioArg1 = DateTime.Now;
            DateTime? relatorioArg2 = DateTime.Now.AddMonths(-1);
            DateTime? relatorioArg3 = DateTime.Now.AddMonths(-2);
            DateTime? relatorioArg4 = DateTime.Now.AddMonths(-3);
            string[] legenda = new string[]
            {
                relatorioArg4.Value.ToString("MMMM"),
                relatorioArg3.Value.ToString("MMMM"),
                relatorioArg2.Value.ToString("MMMM"),
                relatorioArg1.Value.ToString("MMMM")
            };
            CorretorBusiness corretorBusiness = new CorretorBusiness();
            VendaBusiness vendaBusiness = new VendaBusiness();
            List<Series> conteudoGrafico = new List<Series>();

            var corretores = corretorBusiness.GetCorretores();
            var vendas = vendaBusiness.GetVendas();
            Dictionary<string, int[]> vendasCorretor = new Dictionary<string, int[]>();
            foreach (Corretor corretor in corretores)
            {
                int[] vendasPeriodo = new int[]
                {
                    vendas.Where(v => v.DataVenda.Year == relatorioArg4.Value.Year && v.DataVenda.Month == relatorioArg4.Value.Month && v.CorretorId == corretor.Id).Count(),
                    vendas.Where(v => v.DataVenda.Year == relatorioArg3.Value.Year && v.DataVenda.Month == relatorioArg3.Value.Month && v.CorretorId == corretor.Id).Count(),
                    vendas.Where(v => v.DataVenda.Year == relatorioArg2.Value.Year && v.DataVenda.Month == relatorioArg2.Value.Month && v.CorretorId == corretor.Id).Count(),
                    vendas.Where(v => v.DataVenda.Year == relatorioArg1.Value.Year && v.DataVenda.Month == relatorioArg1.Value.Month && v.CorretorId == corretor.Id).Count()
                };
                vendasCorretor.Add(corretor.NomeCompleto, vendasPeriodo);
            }

            foreach(string nome in vendasCorretor.Keys)
            {
                var listaVendas = vendasCorretor.FirstOrDefault(c => c.Key == nome).Value;
                List<object> quantidadeVendas = new List<object>();
                foreach(int v in listaVendas)
                {
                    quantidadeVendas.Add(v);
                }
                conteudoGrafico.Add(new Series
                {
                    Name = nome,
                    Data = new DotNet.Highcharts.Helpers.Data(quantidadeVendas.ToArray())
                });
            }

            Highcharts columnChart = new Highcharts("columnchart");
            columnChart.InitChart(new Chart()
            {
                Type = DotNet.Highcharts.Enums.ChartTypes.Line,
                BackgroundColor = new BackColorOrGradient(System.Drawing.Color.White),
                Style = "fontWeight: 'bold', fontSize: '17px'",
                BorderColor = System.Drawing.Color.LightBlue,
                BorderRadius = 0,
                BorderWidth = 3
            });
            columnChart.SetTitle(new Title()
            {
                Text = "Vendas X locações"
            });
            columnChart.SetSubtitle(new Subtitle()
            {
                Text = "Realizadas nos últimos 4 meses"
            });
            columnChart.SetXAxis(new XAxis()
            {
                Type = AxisTypes.Category,
                Title = new XAxisTitle() { Text = "Meses", Style = "fontWeight: 'bold', fontSize: '14px'" },
                Categories = legenda
            });
            columnChart.SetYAxis(new YAxis()
            {
                Title = new YAxisTitle()
                {
                    Text = "Quantidade de vendas",
                    Style = "fontWeight: 'bold', fontSize: '14px'"
                },
                ShowFirstLabel = true,
                ShowLastLabel = true,
                Min = 0
            });
            columnChart.SetLegend(new Legend
            {
                Enabled = true,
                BorderColor = System.Drawing.Color.CornflowerBlue,
                BorderRadius = 6,
                BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#EEE"))
            });
            columnChart.SetSeries(conteudoGrafico.ToArray());
            return View(columnChart);
        }
    }
}