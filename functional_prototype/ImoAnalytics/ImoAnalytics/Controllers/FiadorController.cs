using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ImoAnalytics.Models
{
    public class FiadorController : Controller
    {
        private ImoAnalyticsContext db = new ImoAnalyticsContext();

        // GET: Fiador
        public ActionResult Index()
        {
            return View(db.Fiador.ToList());
        }

        // GET: Fiador/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fiador fiador = db.Fiador.Find(id);
            if (fiador == null)
            {
                return HttpNotFound();
            }
            return View(fiador);
        }

        // GET: Fiador/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fiador/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NomeCompleto,Cpf,Rg,DataNascimento,Telefone,Cep,Endereco,Numero,Bairro,Cidade,Estado,Email,RegistroDeImovel,Renda")] Fiador fiador)
        {
            if (ModelState.IsValid)
            {
                db.Fiador.Add(fiador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fiador);
        }

        // GET: Fiador/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fiador fiador = db.Fiador.Find(id);
            if (fiador == null)
            {
                return HttpNotFound();
            }
            return View(fiador);
        }

        // POST: Fiador/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NomeCompleto,Cpf,Rg,DataNascimento,Telefone,Cep,Endereco,Numero,Bairro,Cidade,Estado,Email,RegistroDeImovel,Renda")] Fiador fiador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fiador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fiador);
        }

        // GET: Fiador/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fiador fiador = db.Fiador.Find(id);
            if (fiador == null)
            {
                return HttpNotFound();
            }
            return View(fiador);
        }

        // POST: Fiador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fiador fiador = db.Fiador.Find(id);
            db.Fiador.Remove(fiador);
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
