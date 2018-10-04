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
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Locacao
        [Authorize]
        public ActionResult Index()
        {
            var locacaos = db.Locacao.Include(l => l.ContratoDeLocacao).Include(l => l.Fiador).Include(l => l.Imovel).Include(l => l.Interessado);
            return View(locacaos.ToList());
        }

        // GET: Locacao/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locacao locacao = db.Locacao.Find(id);
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
            ViewBag.ContratoDeLocacaoId = new SelectList(db.ContratoDeLocacao, "ID", "ID");
            ViewBag.FiadorId = new SelectList(db.Fiador, "ID", "NomeCompleto");
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "TituloImovel");
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto");
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
            var locacao = new Locacao { DataOperacao = model.DataOperacao, InteressadoId = model.InteressadoId, ImovelId = model.ImovelId, FiadorId = model.FiadorId};
            var contratoLocacao = new ContratoDeLocacao { Valor = model.Valor, DataInicio = model.DataInicio, DataFim = model.DataFim, DataPagamento = model.DataPagamento};

            locacao.ContratoDeLocacao = contratoLocacao;
            
            if (ModelState.IsValid)
            {
                db.Locacao.Add(locacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContratoDeLocacaoId = new SelectList(db.ContratoDeLocacao, "ID", "ID", locacao.ContratoDeLocacaoId);
            ViewBag.FiadorId = new SelectList(db.Fiador, "ID", "NomeCompleto", locacao.FiadorId);
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "TituloImovel", locacao.ImovelId);
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto", locacao.InteressadoId);
            return View(locacao);
        }

        // GET: Locacao/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locacao locacao = db.Locacao.Find(id);
            if (locacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContratoDeLocacaoId = new SelectList(db.ContratoDeLocacao, "ID", "ID", locacao.ContratoDeLocacaoId);
            ViewBag.FiadorId = new SelectList(db.Fiador, "ID", "NomeCompleto", locacao.FiadorId);
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "TituloImovel", locacao.ImovelId);
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto", locacao.InteressadoId);
            return View(locacao);
        }

        // POST: Locacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,DataLocacao,ImovelId,InteressadoId,ContratoDeLocacaoId,FiadorId")] Locacao locacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContratoDeLocacaoId = new SelectList(db.ContratoDeLocacao, "ID", "ID", locacao.ContratoDeLocacaoId);
            ViewBag.FiadorId = new SelectList(db.Fiador, "ID", "NomeCompleto", locacao.FiadorId);
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "TituloImovel", locacao.ImovelId);
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto", locacao.InteressadoId);
            return View(locacao);
        }

        // GET: Locacao/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locacao locacao = db.Locacao.Find(id);
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
            Locacao locacao = db.Locacao.Find(id);
            db.Locacao.Remove(locacao);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}