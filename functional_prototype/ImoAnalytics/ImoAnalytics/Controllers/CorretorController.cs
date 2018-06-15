using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ImoAnalytics.Models;

namespace ImoAnalytics.Controllers
{
    public class CorretorController : Controller
    {
        private ImoAnalyticsContext db = new ImoAnalyticsContext();

        // GET: Corretor
        public ActionResult Index()
        {
            return View(db.Corretors.ToList());
        }

        // GET: Corretor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corretor corretor = db.Corretors.Find(id);
            if (corretor == null)
            {
                return HttpNotFound();
            }
            return View(corretor);
        }

        // GET: Corretor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Corretor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NomeCompleto,Cpf,Rg,DataNascimento,Telefone,Cep,Endereco,Numero,Bairro,Cidade,Estado,Creci,Email,Senha")] Corretor corretor)
        {
            if (ModelState.IsValid)
            {
                db.Corretors.Add(corretor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(corretor);
        }

        // GET: Corretor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corretor corretor = db.Corretors.Find(id);
            if (corretor == null)
            {
                return HttpNotFound();
            }
            return View(corretor);
        }

        // POST: Corretor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NomeCompleto,Cpf,Rg,DataNascimento,Telefone,Cep,Endereco,Numero,Bairro,Cidade,Estado,Creci,Email,Senha")] Corretor corretor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(corretor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(corretor);
        }

        // GET: Corretor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corretor corretor = db.Corretors.Find(id);
            if (corretor == null)
            {
                return HttpNotFound();
            }
            return View(corretor);
        }

        // POST: Corretor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Corretor corretor = db.Corretors.Find(id);
            db.Corretors.Remove(corretor);
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
