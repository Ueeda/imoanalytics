using ImoAnalyticsSystem.Business;
using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using ImoAnalyticsSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ImoAnalyticsSystem.Controllers
{
    public class LocacaoController : Controller
    {
        private LocacaoBusiness locacaoBusiness = new LocacaoBusiness();
        private FiadorBusiness fiadorBusiness = new FiadorBusiness();
        private InteressadoBusiness interessadoBusiness = new InteressadoBusiness();
        private ImovelBusiness imovelBusiness = new ImovelBusiness();

        // GET: Locacao
        [Authorize]
        public ActionResult Index()
        {
            var locacaos = locacaoBusiness.GetLocacoes();
            return View(locacaos);
        }

        // GET: Locacao/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locacao locacao = locacaoBusiness.FindById(id);
            if (locacao == null)
            {
                return HttpNotFound();
            }
            return View(locacao);
        }

        // GET: Locacao/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.FiadorId = new SelectList(fiadorBusiness.GetFiadores(), "ID", "NomeCompleto");
            ViewBag.ImovelId = new SelectList(imovelBusiness.GetImoveisDisponiveis(), "ID", "CodigoReferencia");
            ViewBag.InteressadoId = new SelectList(interessadoBusiness.GetInteressados(), "ID", "NomeCompleto");
            return View();
        }

        // POST: Locacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(LocacaoViewModel model)
        {
            string create = "";
            if (ModelState.IsValid)
            {
                create = locacaoBusiness.Create(model);
                if (create.Equals("OK"))
                    return RedirectToAction("Index");
            }

            if (!create.Equals(""))
                ModelState.AddModelError("Erro ao criar o cartório: ", create);

            ViewBag.FiadorId = new SelectList(fiadorBusiness.GetFiadores(), "ID", "NomeCompleto", model.FiadorId);
            ViewBag.ImovelId = new SelectList(imovelBusiness.GetImoveisDisponiveis(), "ID", "CodigoReferencia", model.ImovelId);
            ViewBag.InteressadoId = new SelectList(interessadoBusiness.GetInteressados(), "ID", "NomeCompleto", model.InteressadoId);
            return View(model);
        }

        // GET: Locacao/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locacao locacao = locacaoBusiness.FindById(id);
            if (locacao == null)
            {
                return HttpNotFound();
            }
            LocacaoViewModel model = new LocacaoViewModel();
            model.CodigoLocacao = locacao.CodigoLocacao;
            model.DataOperacao = locacao.DataOperacao;
            model.FiadorId = locacao.FiadorId;
            model.ImovelId = locacao.ImovelId;
            model.InteressadoId = locacao.InteressadoId;
            ViewBag.FiadorId = new SelectList(fiadorBusiness.GetFiadores(), "ID", "NomeCompleto", model.FiadorId);
            ViewBag.ImovelId = new SelectList(imovelBusiness.GetImoveisDisponiveis(), "ID", "CodigoReferencia", model.ImovelId);
            ViewBag.InteressadoId = new SelectList(interessadoBusiness.GetInteressados(), "ID", "NomeCompleto", model.InteressadoId);
            model.Valor = locacao.ContratoDeLocacao.Valor;
            model.DataInicio = locacao.ContratoDeLocacao.DataInicio;
            model.DataFim = locacao.ContratoDeLocacao.DataFim;
            model.DataPagamento = locacao.ContratoDeLocacao.DataPagamento;
            ViewBag.LocacaoId = locacao.ID;
            return View(model);
        }

        // POST: Locacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(LocacaoViewModel model)
        {
            string edit = "";
            if (ModelState.IsValid)
            {
                edit = locacaoBusiness.Edit(model, ViewBag.LocacaoId);
                if (edit.Equals("OK"))
                    return RedirectToAction("Details", new { id = ViewBag.LocacaoId });
            }

            if (!edit.Equals(""))
                ModelState.AddModelError("Erro ao editar a locação: ", edit);

            ViewBag.FiadorId = new SelectList(fiadorBusiness.GetFiadores(), "ID", "NomeCompleto", model.FiadorId);
            ViewBag.ImovelId = new SelectList(imovelBusiness.GetImoveisDisponiveis(), "ID", "CodigoReferencia", model.ImovelId);
            ViewBag.InteressadoId = new SelectList(interessadoBusiness.GetInteressados(), "ID", "NomeCompleto", model.InteressadoId);
            return View(model);
        }

        // GET: Locacao/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locacao locacao = locacaoBusiness.FindById(id);
            if (locacao == null)
            {
                return HttpNotFound();
            }
            return View(locacao);
        }

        // POST: Locacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Locacao locacao = locacaoBusiness.FindById(id);
            locacaoBusiness.Delete(locacao);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                locacaoBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}