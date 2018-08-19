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
    public class VendaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Venda
        public ActionResult Index()
        {
            var vendas = db.Venda.Include(v => v.Corretor).Include(v => v.Imovel).Include(v => v.Interessado);
            return View(vendas.ToList());
        }

        // GET: Venda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = db.Venda.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // GET: Venda/Create
        public ActionResult Create()
        {
            ViewBag.CorretorId = new SelectList(db.Users, "ID", "NomeCompleto");
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "ID");
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto");
            return View();
        }

        // POST: Venda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DataVenda,ImovelId,CorretorId,InteressadoId,ValorVenda,ComissaoImobiliaria,ComissaoCorretor")] Venda venda)
        {
            if (ModelState.IsValid)
            {
                venda.ComissaoImobiliaria = venda.ValorVenda * 0.06;
                venda.ComissaoCorretor = venda.ComissaoImobiliaria * 0.2;
                db.Venda.Add(venda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CorretorId = new SelectList(db.Users, "ID", "NomeCompleto", venda.CorretorId);
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "ID", venda.ImovelId);
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto", venda.InteressadoId);
            return View(venda);
        }

        // GET: Venda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = db.Venda.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            ViewBag.CorretorId = new SelectList(db.Users, "ID", "NomeCompleto", venda.CorretorId);
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "ID", venda.ImovelId);
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto", venda.InteressadoId);
            return View(venda);
        }

        // POST: Venda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DataVenda,ImovelId,CorretorId,InteressadoId,ValorVenda,ComissaoImobiliaria,ComissaoCorretor")] Venda venda)
        {
            if (ModelState.IsValid)
            {
                venda.ComissaoImobiliaria = venda.ValorVenda * 0.06;
                venda.ComissaoCorretor = venda.ComissaoImobiliaria * 0.2;
                db.Entry(venda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CorretorId = new SelectList(db.Users, "ID", "NomeCompleto", venda.CorretorId);
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "ID", venda.ImovelId);
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto", venda.InteressadoId);
            return View(venda);
        }

        // GET: Venda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = db.Venda.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // POST: Venda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venda venda = db.Venda.Find(id);
            db.Venda.Remove(venda);
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