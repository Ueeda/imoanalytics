﻿using ImoAnalyticsSystem.Business;
using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Excel = Microsoft.Office.Interop.Excel;

namespace ImoAnalyticsSystem.Controllers
{
    public class ImovelController : Controller
    {
        private ImovelBusiness imovelBusiness = new ImovelBusiness();
        private TipoImovelBusiness tipoImovelBusiness = new TipoImovelBusiness();
        private CartorioBusiness cartorioBusiness = new CartorioBusiness();
        private ProprietarioBusiness proprietarioBusiness = new ProprietarioBusiness();
        private VisitaBusiness visitaBusiness = new VisitaBusiness();

        // GET: Imovel
        [Authorize]
        public ActionResult Index(bool? currentLocacao, bool? currentVenda, bool? locacao, bool? venda,
                                  decimal? currentVendaMin, decimal? currentVendaMax, decimal? vendaMin, decimal? vendaMax,
                                  decimal? currentLocacaoMin, decimal? currentLocacaoMax, decimal? locacaoMin, decimal? locacaoMax,
                                  string currentEndereco, string endereco,
                                  string currentBairro, string bairro,
                                  int? currentQuartos, int? quartos,
                                  int? currentSuites, int? suites,
                                  int? currentVagasGaragem, int? vagasGaragem,
                                  int? currentBanheiros, int? banheiros,
                                  int? currentTipoImovel, int? tipoImovel,
                                  int? page)
        {
            // Flag true para mostrar que o layout é para seguir o modelo de imóvel
            ViewBag.Imoveis = true;

            if (locacao != null || venda != null || vendaMin != null || vendaMax != null ||
                locacaoMin != null || locacaoMax != null || endereco != null || bairro != null|| 
                quartos != null || suites != null || vagasGaragem != null || banheiros != null ||
                tipoImovel != null)
            {
                page = 1;
            }
            else {
                // Atribuir valores para a locação
                if (locacao != null)
                    locacao = Convert.ToBoolean(locacao);
                else
                {
                    if (currentLocacao == null)
                        locacao = true;
                    else
                        locacao = currentLocacao;
                }

                // Atribuir valores para a venda
                if (venda != null)
                    venda = Convert.ToBoolean(venda);
                else
                {
                    if (currentVenda == null)
                        venda = true;
                    else
                        venda = currentVenda;
                }

                if (vendaMin == null)
                    vendaMin = currentVendaMin;

                if (vendaMax == null)
                    vendaMax = currentVendaMax;

                if (locacaoMin == null)
                    locacaoMin = currentLocacaoMin;

                if (locacaoMax == null)
                    locacaoMax = currentLocacaoMax;

                if (endereco == null)
                    endereco = currentEndereco;

                if (bairro == null)
                    bairro = currentBairro;

                if (quartos == null)
                    quartos = currentQuartos;

                if (suites == null)
                    suites = currentSuites;

                if (vagasGaragem == null)
                    vagasGaragem = currentVagasGaragem;

                if (banheiros == null)
                    banheiros = currentBanheiros;

                if (tipoImovel == null)
                    tipoImovel = currentTipoImovel;
            }

            ViewBag.TipoImovelId = new SelectList(tipoImovelBusiness.GetTiposImovel(), "ID", "Tipo", tipoImovel);

            ViewBag.CurrentLocacao = locacao;
            ViewBag.CurrentVenda = venda;
            ViewBag.CurrentVendaMin = vendaMin;
            ViewBag.CurrentVendaMax = vendaMax;
            ViewBag.CurrentLocacaoMin = locacaoMin;
            ViewBag.CurrentLocacaoMax = locacaoMax;
            ViewBag.CurrentEndereco = endereco;
            ViewBag.CurrentBairro = bairro;
            ViewBag.CurrentQuartos = quartos;
            ViewBag.CurrentSuites = suites;
            ViewBag.CurrentVagasGaragem = vagasGaragem;
            ViewBag.CurrentBanheiros = banheiros;
            ViewBag.CurrentTipoImovel = tipoImovel;
            List<Imovel> imoveis;
            
            if(locacao == true && venda == false)
            {
                imoveis = imovelBusiness.GetImoveisLocacao();
            }
            else if (locacao == false && venda == true)
            {
                imoveis = imovelBusiness.GetImoveisVenda();
            }
            else
            {
                imoveis = imovelBusiness.GetImoveisDisponiveis();
            }

            if (vendaMin != null)
                imoveis = imoveis.Where(i => i.ValorVenda >= vendaMin && i.Venda == true).ToList();

            if (vendaMax != null)
                imoveis = imoveis.Where(i => i.ValorVenda <= vendaMax && i.Venda == true).ToList();

            if (locacaoMin != null)
                imoveis = imoveis.Where(i => i.ValorLocacao >= locacaoMin && i.Locacao == true).ToList();

            if (locacaoMax != null)
                imoveis = imoveis.Where(i => i.ValorLocacao <= locacaoMax && i.Locacao == true).ToList();

            if (endereco != null)
                imoveis = imoveis.Where(i => i.Endereco.Contains(endereco)).ToList();

            if (bairro != null)
                imoveis = imoveis.Where(i => i.Bairro.Contains(bairro)).ToList();

            if (quartos != null)
                imoveis = imoveis.Where(i => i.QntDormitorios == quartos).ToList();

            if (suites != null)
                imoveis = imoveis.Where(i => i.QntSuites == suites).ToList();

            if (vagasGaragem != null)
                imoveis = imoveis.Where(i => i.VagasGaragem == vagasGaragem).ToList();

            if (banheiros != null)
                imoveis = imoveis.Where(i => i.QntBanheiros == banheiros).ToList();

            if (tipoImovel != null)
                imoveis = imoveis.Where(i => i.TipoImovelId == tipoImovel).ToList();
            
            if(imoveis.Count() == 0 && imovelBusiness.GetImoveis().Count() > 0)
                ViewBag.noResults = true;

            int pageSize = 9;
            int pageNumber = (page ?? 1);

            return View(imoveis.OrderBy(i => i.CodigoReferencia).ToPagedList(pageNumber, pageSize));
        }

        // GET: Imovel/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropostaBusiness propostaBusiness = new PropostaBusiness();
            Imovel imovel = imovelBusiness.FindById(id);
            var listaVisitas = visitaBusiness.GetVisitasByImovelId(id);
            var listaProposta = propostaBusiness.GetPropostasByImovelId(id);
            ViewBag.visitas = listaVisitas;
            ViewBag.propostas = listaProposta;
            if (imovel == null)
            {
                return HttpNotFound();
            }
            return View(imovel);
        }

        // GET: Imovel/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ProprietarioId = new SelectList(proprietarioBusiness.GetProprietarios(), "ID", "NomeCompleto");
            ViewBag.TipoImovelId = new SelectList(tipoImovelBusiness.GetTiposImovel(), "ID", "Tipo");
            ViewBag.CartorioId = new SelectList(cartorioBusiness.GetCartorios(), "ID", "NomeCartorio");
            return View();
        }

        // POST: Imovel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,CodigoReferencia,TituloImovel,Endereco,Complemento,Cidade,Estado,Numero,Cep,Bairro,AnoConstrucao,Venda,Locacao,AreaPrivada,AreaTotal,VagasGaragem,QntBanheiros,QntDormitorios,QntSuites,Disponivel,Reservado,ValorVenda,ValorLocacao,ValorIptu,NomeCondominio,ValorCondominio,NumeroRegistroImovel,DescricaoImovel,ProprietarioId,TipoImovelId,CartorioId")] Imovel imovel, HttpPostedFileBase upload)
        {
            try
            {                
                string create = "";
                if (ModelState.IsValid)
                {
                    create = imovelBusiness.Create(imovel, upload);
                    if (create.Equals("OK"))
                        return RedirectToAction("Index");
                }

                if (!create.Equals(""))
                    ModelState.AddModelError("Erro ao criar o imovel: ", create);
                ViewBag.ProprietarioId = new SelectList(proprietarioBusiness.GetProprietarios(), "ID", "NomeCompleto");
                ViewBag.TipoImovelId = new SelectList(tipoImovelBusiness.GetTiposImovel(), "ID", "Tipo");
                ViewBag.CartorioId = new SelectList(cartorioBusiness.GetCartorios(), "ID", "NomeCartorio");
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(imovel);
        }

        // GET: Imovel/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imovel imovel = imovelBusiness.FindById(id);
            if (imovel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProprietarioId = new SelectList(proprietarioBusiness.GetProprietarios(), "ID", "NomeCompleto", imovel.ProprietarioId);
            ViewBag.TipoImovelId = new SelectList(tipoImovelBusiness.GetTiposImovel(), "ID", "Tipo", imovel.TipoImovelId);
            ViewBag.CartorioId = new SelectList(cartorioBusiness.GetCartorios(), "ID", "NomeCartorio", imovel.CartorioId);
            return View(imovel);
        }

        // POST: Imovel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,CodigoReferencia,TituloImovel,Endereco,Complemento,Cidade,Estado,Numero,Cep,Bairro,AnoConstrucao,Venda,Locacao,AreaPrivada,AreaTotal,VagasGaragem,QntBanheiros,QntDormitorios,QntSuites,Disponivel,Reservado,ValorVenda,ValorLocacao,ValorIptu,NomeCondominio,ValorCondominio,NumeroRegistroImovel,DescricaoImovel,ProprietarioId,TipoImovelId,CartorioId")] Imovel imovel, HttpPostedFileBase upload)
        {
            string edit = "";
            if (ModelState.IsValid)
            {
                edit = imovelBusiness.Edit(imovel, upload);
                if (edit.Equals("OK")) 
                    return RedirectToAction("Details", new { id = imovel.ID });
            }
            if (!edit.Equals(""))
                ModelState.AddModelError("Erro ao criar o imovel: ", edit);
            ViewBag.ProprietarioId = new SelectList(proprietarioBusiness.GetProprietarios(), "ID", "NomeCompleto", imovel.ProprietarioId);
            ViewBag.TipoImovelId = new SelectList(tipoImovelBusiness.GetTiposImovel(), "ID", "Tipo", imovel.TipoImovelId);
            ViewBag.CartorioId = new SelectList(cartorioBusiness.GetCartorios(), "ID", "NomeCartorio", imovel.CartorioId);
            return View(imovel);
        }

        // GET: Imovel/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imovel imovel = imovelBusiness.FindById(id);
            if (imovel == null)
            {
                return HttpNotFound();
            }
            return View(imovel);
        }

        // POST: Imovel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Imovel imovel = imovelBusiness.FindById(id);
            imovelBusiness.Delete(imovel);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                imovelBusiness.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Imovel/ImportExcel
        [HttpGet]
        [Authorize]
        public ActionResult ImportExcel()
        {
            ViewBag.ImportSuccess = "";
            ViewBag.ImportError = "";
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult ImportExcel(HttpPostedFileBase upload)
        {

            int contador = 0;
            

            if (upload != null && upload.ContentLength > 0)
            {
                Excel.Workbook MyBook = null;
                Excel.Application MyApp = null;
                Excel.Worksheet MySheet = null;
                var path = Path.GetFileName(upload.FileName);        
      
                MyApp = new Excel.Application();
                MyApp.Visible = false;
                MyBook = MyApp.Workbooks.Open(path);
                MySheet = (Excel.Worksheet)MyBook.Sheets[1];
                var lastRow = MySheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row;
               
                for (int index = 2; index < lastRow; index++)
                {

                    System.Array MyValues = (System.Array)MySheet.get_Range("A" +
                    index.ToString(), "AB" + index.ToString()).Cells.Value;               

                    Imovel imovel = new Imovel();
                    TipoImovel tipoImovel;
                    Proprietario proprietario;
                    Cartorio cartorio;



                    imovel.TituloImovel = MyValues.GetValue(1, 1).ToString();
                    imovel.Endereco = MyValues.GetValue(1, 2).ToString();
                    imovel.Complemento = MyValues.GetValue(1, 3).ToString();
                    imovel.Numero =  Convert.ToInt32(MyValues.GetValue(1, 4));
                    imovel.Cep = MyValues.GetValue(1, 5).ToString();
                    imovel.Bairro = MyValues.GetValue(1, 6).ToString();
                    imovel.AnoConstrucao = Convert.ToInt32(MyValues.GetValue(1, 7));
                    imovel.Venda = Convert.ToBoolean(MyValues.GetValue(1, 8).ToString());
                    imovel.Locacao = Convert.ToBoolean(MyValues.GetValue(1, 9).ToString());
                    imovel.AreaPrivada = Convert.ToInt32(MyValues.GetValue(1, 10));
                    imovel.AreaTotal = Convert.ToInt32(MyValues.GetValue(1, 11));
                    imovel.VagasGaragem = Convert.ToInt32(MyValues.GetValue(1, 12));
                    imovel.QntBanheiros = Convert.ToInt32(MyValues.GetValue(1, 13));
                    imovel.QntDormitorios = Convert.ToInt32(MyValues.GetValue(1, 14));
                    imovel.QntSuites = Convert.ToInt32(MyValues.GetValue(1, 15));
                    imovel.ValorVenda = Convert.ToDecimal(MyValues.GetValue(1, 16));
                    imovel.ValorLocacao = Convert.ToDecimal(MyValues.GetValue(1, 17));
                    imovel.ValorIptu = Convert.ToDecimal(MyValues.GetValue(1, 18));
                    imovel.NomeCondominio = MyValues.GetValue(1, 19).ToString();
                    imovel.ValorCondominio = Convert.ToDecimal(MyValues.GetValue(1, 20));
                    imovel.NumeroRegistroImovel = Convert.ToInt32(MyValues.GetValue(1, 21));
                    imovel.DescricaoImovel = MyValues.GetValue(1, 22).ToString();

                    tipoImovel = tipoImovelBusiness.FindByName(MyValues.GetValue(1, 23).ToString());
                    if (tipoImovel != null)
                        imovel.TipoImovelId = tipoImovel.ID;

                    cartorio = cartorioBusiness.FindByName(MyValues.GetValue(1, 24).ToString());
                    if (cartorio != null)
                        imovel.CartorioId = cartorio.ID;

                    proprietario = proprietarioBusiness.FindByCpf(MyValues.GetValue(1, 25).ToString());
                    if (proprietario != null)
                        imovel.ProprietarioId = proprietario.ID;

                    imovel.Cidade = MyValues.GetValue(1, 26).ToString();
                    imovel.Estado = MyValues.GetValue(1, 27).ToString();
                    imovel.CodigoReferencia = MyValues.GetValue(1, 28).ToString();

                    if (tipoImovel != null && cartorio != null && proprietario != null)
                    {
                        string response = imovelBusiness.Create(imovel, null);
                        if (response != "OK")
                        {
                            contador++;
                        }

                    }
                    else
                        contador++;
                        
                }
                

                MyBook.Close(0);
                MyApp.Quit();
            }

            if(contador == 0)
            {

                ViewBag.ImportSuccess = "Todos os registros foram importados com sucesso.";
                ViewBag.ImportError = "";
            }else
            {

                ViewBag.ImportSuccess = "";
                ViewBag.ImportError = "Não foi possível importar " + contador + " registro(s).";

            } 

             
            return View();
        }



    }
}