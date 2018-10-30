using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ImoAnalyticsSystem.Business;
using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;

namespace ImoAnalyticsSystem.Controllers
{
    public class ImobiliariaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ImobiliariaBusiness imobiliariaBusiness = new ImobiliariaBusiness();

        // GET: Imobiliaria
        [Authorize]
        public ActionResult Index()
        {
            return RedirectToAction("Details", imobiliariaBusiness.GetInstance());
        }

        // GET: Imobiliaria/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imobiliaria imobiliaria = db.Imobiliaria.Find(id);
            if (imobiliaria == null)
            {
                return HttpNotFound();
            }
            return View(imobiliaria);
        }

        // GET: Imobiliaria/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Imobiliaria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,NomeImobiliaria,Endereco,EmailContato,TelefoneContato,ComissaoImobiliariaVenda,ComissaoCorretorVenda,TaxaAdministracaoLocacao")] Imobiliaria imobiliaria)
        {
            if (ModelState.IsValid)
            {
                db.Imobiliaria.Add(imobiliaria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(imobiliaria);
        }

        // GET: Imobiliaria/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imobiliaria imobiliaria = db.Imobiliaria.Find(id);
            if (imobiliaria == null)
            {
                return HttpNotFound();
            }
            return View(imobiliaria);
        }

        // POST: Imobiliaria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,NomeImobiliaria,Endereco,Numero,Complemento,Cidade,Estado,EmailContato,TelefoneContato,ComissaoImobiliariaVenda,ComissaoCorretorVenda,TaxaAdministracaoLocacao")] Imobiliaria imobiliaria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imobiliaria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(imobiliaria);
        }

        // GET: Imobiliaria/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imobiliaria imobiliaria = db.Imobiliaria.Find(id);
            if (imobiliaria == null)
            {
                return HttpNotFound();
            }
            return View(imobiliaria);
        }

        // POST: Imobiliaria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Imobiliaria imobiliaria = db.Imobiliaria.Find(id);
            db.Imobiliaria.Remove(imobiliaria);
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
