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
    public class InteressadoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Interessado
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Interessado.ToList());
        }

        // GET: Interessado/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interessado interessado = db.Interessado.Find(id);
            if (interessado == null)
            {
                return HttpNotFound();
            }
            return View(interessado);
        }

        // GET: Interessado/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Interessado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,NomeCompleto,Cpf,Rg,DataNascimento,Telefone,Cep,Endereco,Numero,Bairro,Cidade,Estado,Email")] Interessado interessado)
        {
            if (ModelState.IsValid)
            {
                db.Interessado.Add(interessado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(interessado);
        }

        // GET: Interessado/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interessado interessado = db.Interessado.Find(id);
            if (interessado == null)
            {
                return HttpNotFound();
            }
            return View(interessado);
        }

        // POST: Interessado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,NomeCompleto,Cpf,Rg,DataNascimento,Telefone,Cep,Endereco,Numero,Bairro,Cidade,Estado,Email")] Interessado interessado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interessado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(interessado);
        }

        // GET: Interessado/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interessado interessado = db.Interessado.Find(id);
            if (interessado == null)
            {
                return HttpNotFound();
            }
            return View(interessado);
        }

        // POST: Interessado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Interessado interessado = db.Interessado.Find(id);
            db.Interessado.Remove(interessado);
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