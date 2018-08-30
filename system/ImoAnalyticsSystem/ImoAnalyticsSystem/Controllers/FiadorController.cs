using ImoAnalyticsSystem.Business;
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
    public class FiadorController : Controller
    {
        private FiadorBusiness fb = new FiadorBusiness();

        // GET: Fiador
        [Authorize]
        public ActionResult Index()
        {
            return View(fb.GetFiadores());
        }

        // GET: Fiador/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fiador fiador = fb.FindById(id);
            if (fiador == null)
            {
                return HttpNotFound();
            }
            return View(fiador);
        }

        // GET: Fiador/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fiador/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,NomeCompleto,Cpf,Rg,DataNascimento,Telefone,Cep,Endereco,Numero,Bairro,Cidade,Estado,Email,RegistroDeImovel,Renda")] Fiador fiador)
        {
            string create = "";
            if (ModelState.IsValid)
            {
                create = fb.Create(fiador);
                if(create.Equals("OK"))
                    return RedirectToAction("Index");
            }

            if(!create.Equals(""))
                ModelState.AddModelError("Erro ao criar o fiador: ", create);
            return View(fiador);
        }

        // GET: Fiador/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fiador fiador = fb.FindById(id);
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
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,NomeCompleto,Cpf,Rg,DataNascimento,Telefone,Cep,Endereco,Numero,Bairro,Cidade,Estado,Email,RegistroDeImovel,Renda")] Fiador fiador)
        {
            string edit = "";
            if (ModelState.IsValid)
            {
                edit = fb.Edit(fiador);
                if(edit.Equals("OK"))
                    return RedirectToAction("Index");
            }
            if (!edit.Equals(""))
                ModelState.AddModelError("Erro ao editar o fiador: ", edit);
            return View(fiador);
        }

        // GET: Fiador/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fiador fiador = fb.FindById(id);
            if (fiador == null)
            {
                return HttpNotFound();
            }
            return View(fiador);
        }

        // POST: Fiador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Fiador fiador = fb.FindById(id);
            fb.Delete(fiador);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                fb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}