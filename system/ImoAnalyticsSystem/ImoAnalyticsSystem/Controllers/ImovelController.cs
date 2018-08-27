﻿using ImoAnalyticsSystem.Data;
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
    public class ImovelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Imovel
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Imovel.ToList());
        }

        // GET: Imovel/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imovel imovel = db.Imovel.Find(id);
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
            ViewBag.ProprietarioId = new SelectList(db.Proprietario, "ID", "NomeCompleto");
            ViewBag.TipoImovelId = new SelectList(db.TipoImovel, "ID", "Tipo");
            ViewBag.CartorioId = new SelectList(db.Cartorio, "ID", "NomeCartorio");
            return View();
        }

        // POST: Imovel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,TituloImovel,Endereco,Complemento,Numero,Cep,Bairro,AnoConstrucao,Venda,Locacao,AreaPrivada,AreaTotal,VagasGaragem,QntBanheiros,QntDormitorios,QntSuites,Disponivel,Reservado,ValorVenda,ValorLocacao,ValorIptu,NomeCondominio,ValorCondominio,NumeroRegistroImovel,DescricaoImovel,ProprietarioId,TipoImovelId,CartorioId")] Imovel imovel)
        {
            if (ModelState.IsValid)
            {
                db.Imovel.Add(imovel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProprietarioId = new SelectList(db.Proprietario, "ID", "NomeCompleto");
            ViewBag.TipoImovelId = new SelectList(db.TipoImovel, "ID", "Tipo");
            ViewBag.CartorioId = new SelectList(db.Cartorio, "ID", "NomeCartorio");
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
            Imovel imovel = db.Imovel.Find(id);
            if (imovel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProprietarioId = new SelectList(db.Proprietario, "ID", "NomeCompleto", imovel.ProprietarioId);
            ViewBag.TipoImovelId = new SelectList(db.TipoImovel, "ID", "Tipo", imovel.TipoImovelId);
            ViewBag.CartorioId = new SelectList(db.Cartorio, "ID", "NomeCartorio", imovel.CartorioId);
            return View(imovel);
        }

        // POST: Imovel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,,TituloImovel,Endereco,Complemento,Numero,Cep,Bairro,AnoConstrucao,Venda,Locacao,AreaPrivada,AreaTotal,VagasGaragem,QntBanheiros,QntDormitorios,QntSuites,Disponivel,Reservado,ValorVenda,ValorLocacao,ValorIptu,NomeCondominio,ValorCondominio,NumeroRegistroImovel,DescricaoImovel,ProprietarioId,TipoImovelId,CartorioId")] Imovel imovel)
        {
            ViewBag.ProprietarioId = new SelectList(db.Proprietario, "ID", "NomeCompleto", imovel.ProprietarioId);
            ViewBag.TipoImovelId = new SelectList(db.TipoImovel, "ID", "Tipo", imovel.TipoImovelId);
            ViewBag.CartorioId = new SelectList(db.Cartorio, "ID", "NomeCartorio", imovel.CartorioId);
            if (ModelState.IsValid)
            {
                db.Entry(imovel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
            Imovel imovel = db.Imovel.Find(id);
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
            Imovel imovel = db.Imovel.Find(id);
            db.Imovel.Remove(imovel);
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