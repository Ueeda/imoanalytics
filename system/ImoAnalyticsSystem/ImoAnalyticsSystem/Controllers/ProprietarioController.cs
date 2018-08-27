﻿using ImoAnalyticsSystem.Business;
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
    public class ProprietarioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProprietarioBusiness pb = new ProprietarioBusiness();

        // GET: Proprietario
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Proprietario.ToList());
        }

        // GET: Proprietario/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proprietario proprietario = db.Proprietario.Find(id);
            if (proprietario == null)
            {
                return HttpNotFound();
            }
            return View(proprietario);
        }

        // GET: Proprietario/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proprietario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,NomeCompleto,Cpf,Rg,DataNascimento,Telefone,Cep,Endereco,Numero,Bairro,Cidade,Estado,ContaBancaria,Agencia,Banco,Ativo,Email")] Proprietario proprietario)
        {
            string create = "";
            if (ModelState.IsValid)
            {
                create = pb.Create(proprietario);
                if (create.Equals("OK"))
                    return RedirectToAction("Index");
            }

            ModelState.AddModelError("Erro ao criar o proprietário: ", create);
            return View(proprietario);
        }

        // GET: Proprietario/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proprietario proprietario = db.Proprietario.Find(id);
            if (proprietario == null)
            {
                return HttpNotFound();
            }
            return View(proprietario);
        }

        // POST: Proprietario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,NomeCompleto,Cpf,Rg,DataNascimento,Telefone,Cep,Endereco,Numero,Bairro,Cidade,Estado,ContaBancaria,Agencia,Banco,Ativo,Email")] Proprietario proprietario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proprietario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proprietario);
        }

        // GET: Proprietario/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proprietario proprietario = db.Proprietario.Find(id);
            if (proprietario == null)
            {
                return HttpNotFound();
            }
            return View(proprietario);
        }

        // POST: Proprietario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Proprietario proprietario = db.Proprietario.Find(id);
            db.Proprietario.Remove(proprietario);
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