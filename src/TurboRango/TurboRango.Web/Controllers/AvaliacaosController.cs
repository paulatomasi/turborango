using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TurboRango.Dominio;
using TurboRango.Web.Models;
using Microsoft.AspNet.Identity;

namespace TurboRango.Web.Controllers
{
    public class AvaliacaosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Avaliacaos
        public ActionResult Index(int? id)
        {
            return View(db.Avaliacaos.ToList());
        }

        // GET: Avaliacao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avaliacao avaliacao = db.Avaliacaos.Find(id);
            if (avaliacao == null)
            {
                return HttpNotFound();
            }
            return View(avaliacao);
        }

        // GET: Avaliacao/Create
        public ActionResult Create(int id)
        {
            return View();
        }

        // POST: Avaliacao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? id, [Bind(Include = "Id,Nota")] Avaliacao avaliacao)
        {
            avaliacao.Login = User.Identity.Name;
            avaliacao.Restaurante = db.Restaurantes.Find(id);
            avaliacao.Data = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (db.Avaliacaos.Where(x => x.Restaurante.Id == id && x.Login == User.Identity.Name).Count() == 0)
                {
                    db.Avaliacaos.Add(avaliacao);
                }
                else
                {
                    db.Entry(avaliacao).State = EntityState.Modified;
                }

                db.SaveChanges();
                return RedirectToAction("Index", "Avaliacaos");
            }
            return View(avaliacao);
        }

        // GET: Avaliacao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avaliacao avaliacao = db.Avaliacaos.Find(id);
            if (avaliacao == null)
            {
                return HttpNotFound();
            }
            return View(avaliacao);
        }

        // POST: Avaliacao/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Login,Nota,Data")] Avaliacao avaliacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(avaliacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Avaliacaos");
            }
            return View(avaliacao);
        }

        // GET: Avaliacao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avaliacao avaliacao = db.Avaliacaos.Find(id);
            if (avaliacao == null)
            {
                return HttpNotFound();
            }
            return View(avaliacao);
        }

        // POST: Avaliacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Avaliacao avaliacao = db.Avaliacaos.Find(id);
            db.Avaliacaos.Remove(avaliacao);
            db.SaveChanges();
            return RedirectToAction("Index", "Avaliacaos");
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
