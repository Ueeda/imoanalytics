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
using PagedList;

namespace ImoAnalyticsSystem.Controllers
{
    public class VisitaController : Controller
    {
        private VisitaBusiness visitaBusiness = new VisitaBusiness();
        private InteressadoBusiness interessadoBusiness = new InteressadoBusiness();
        private ImovelBusiness imovelBusiness = new ImovelBusiness();
        private CorretorBusiness corretorBusiness = new CorretorBusiness();

        // GET: Visita
        [Authorize]
        public ActionResult Index(DateTime? currentStart, DateTime? currentEnd, DateTime? startTime, DateTime? endTime, int? page)
        {
            if (startTime.HasValue || endTime.HasValue)
                page = 1;
            else
            {
                if (startTime.HasValue)
                    startTime = currentStart.Value.Date;
                else
                    startTime = currentStart;

                if (endTime.HasValue)
                    endTime = currentEnd.Value.Date;
                else
                    endTime = currentEnd;
            }

            ViewBag.CurrentStart = startTime;
            ViewBag.CurrentEnd = endTime;
            List<Visita> visitas;

            if (startTime.HasValue && endTime.HasValue)
            {
                //if (startTime > endTime)
                if (Nullable.Compare(startTime, endTime) > 0)
                {
                    ViewBag.invalidRange = true;
                    visitas = new List<Visita>();
                }
                else
                {
                    visitas = visitaBusiness.GetVisitasByStartAndEndTime(startTime, endTime);
                    if (visitas.Count() == 0)
                        ViewBag.noResults = true;
                }                
            }
            else if (startTime.HasValue && endTime == null)
            {
                visitas = visitaBusiness.GetVisitasByStartTime(startTime);
                if (visitas.Count() == 0)
                    ViewBag.noResults = true;
            }
            else if (startTime == null && endTime.HasValue)
            {
                visitas = visitaBusiness.GetVisitasByEndTime(endTime);
                if (visitas.Count() == 0)
                    ViewBag.noResults = true;
            }
            else
                visitas = visitaBusiness.GetVisitas();

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(visitas.OrderBy(v => v.Data).ToPagedList(pageNumber, pageSize));
        }

        // GET: Visita/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visita visita = visitaBusiness.FindById(id);
            if (visita == null)
            {
                return HttpNotFound();
            }
            return View(visita);
        }

        // GET: Visita/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CorretorId = new SelectList(corretorBusiness.GetCorretores(), "Id", "NomeCompleto");
            ViewBag.ImovelId = new SelectList(imovelBusiness.GetImoveisDisponiveis(), "ID", "ID");
            ViewBag.InteressadoId = new SelectList(interessadoBusiness.GetInteressados(), "ID", "NomeCompleto");
            return View();
        }

        // POST: Visita/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,InteressadoId,CorretorId,ImovelId,Data,Horario,Descricao")] Visita visita)
        {
            string create = "";
            if (ModelState.IsValid)
            {
                create = visitaBusiness.Create(visita);
                if(create.Equals("OK"))
                    return RedirectToAction("Index");
            }

            if (!create.Equals(""))
                ModelState.AddModelError("Erro ao criar a visita: ", create);
            ViewBag.CorretorId = new SelectList(corretorBusiness.GetCorretores(), "ID", "NomeCompleto", visita.CorretorId);
            ViewBag.ImovelId = new SelectList(imovelBusiness.GetImoveisDisponiveis(), "ID", "ID", visita.ImovelId);
            ViewBag.InteressadoId = new SelectList(interessadoBusiness.GetInteressados(), "ID", "NomeCompleto", visita.InteressadoId);
            return View(visita);
        }

        // GET: Visita/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visita visita = visitaBusiness.FindById(id);
            if (visita == null)
            {
                return HttpNotFound();
            }
            ViewBag.CorretorId = new SelectList(corretorBusiness.GetCorretores(), "ID", "NomeCompleto", visita.CorretorId);
            ViewBag.ImovelId = new SelectList(imovelBusiness.GetImoveisDisponiveis(), "ID", "ID", visita.ImovelId);
            ViewBag.InteressadoId = new SelectList(interessadoBusiness.GetInteressados(), "ID", "NomeCompleto", visita.InteressadoId);
            return View(visita);
        }

        // POST: Visita/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,InteressadoId,CorretorId,ImovelId,Data,Horario,Descricao")] Visita visita)
        {
            string edit = "";
            if (ModelState.IsValid)
            {
                edit = visitaBusiness.Edit(visita);
                if (edit.Equals("OK"))
                    return RedirectToAction("Index");
            }
            if (!edit.Equals(""))
                ModelState.AddModelError("Erro ao criar a visita: ", edit);
            ViewBag.CorretorId = new SelectList(corretorBusiness.GetCorretores(), "ID", "NomeCompleto", visita.CorretorId);
            ViewBag.ImovelId = new SelectList(imovelBusiness.GetImoveisDisponiveis(), "ID", "ID", visita.ImovelId);
            ViewBag.InteressadoId = new SelectList(interessadoBusiness.GetInteressados(), "ID", "NomeCompleto", visita.InteressadoId);
            return View(visita);
        }

        // GET: Visita/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visita visita = visitaBusiness.FindById(id);
            if (visita == null)
            {
                return HttpNotFound();
            }
            return View(visita);
        }

        // POST: Visita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Visita visita = visitaBusiness.FindById(id);
            visitaBusiness.Delete(visita);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                visitaBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}