using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ImoAnalyticsSystem.Controllers
{
    public class ContratoDeLocacaoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContratoDeLocacao
        [Authorize]
        public ActionResult Index()
        {
            return View(db.ContratoDeLocacao.ToList());
        }

        // GET: ContratoDeLocacao/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContratoDeLocacao contratoDeLocacao = db.ContratoDeLocacao.Find(id);
            if (contratoDeLocacao == null)
            {
                return HttpNotFound();
            }
            return View(contratoDeLocacao);
        }

        // GET: ContratoDeLocacao/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContratoDeLocacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Valor,DataInicio,DataFim,DataPagamento")] ContratoDeLocacao contratoDeLocacao)
        {
            if (ModelState.IsValid)
            {
                db.ContratoDeLocacao.Add(contratoDeLocacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contratoDeLocacao);
        }

        // GET: ContratoDeLocacao/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContratoDeLocacao contratoDeLocacao = db.ContratoDeLocacao.Find(id);
            if (contratoDeLocacao == null)
            {
                return HttpNotFound();
            }
            return View(contratoDeLocacao);
        }

        // POST: ContratoDeLocacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Valor,DataInicio,DataFim,DataPagamento")] ContratoDeLocacao contratoDeLocacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contratoDeLocacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contratoDeLocacao);
        }

        // GET: ContratoDeLocacao/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContratoDeLocacao contratoDeLocacao = db.ContratoDeLocacao.Find(id);
            if (contratoDeLocacao == null)
            {
                return HttpNotFound();
            }
            return View(contratoDeLocacao);
        }

        // POST: ContratoDeLocacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            ContratoDeLocacao contratoDeLocacao = db.ContratoDeLocacao.Find(id);
            db.ContratoDeLocacao.Remove(contratoDeLocacao);
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