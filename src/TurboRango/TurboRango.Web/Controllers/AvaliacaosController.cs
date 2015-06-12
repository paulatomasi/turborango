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
            return View();
        }

        // GET: Avaliacao/Create
        public ActionResult Create(int? id)
        {
            return View();
        }

        // POST: Avaliacao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? id, [Bind(Include = "Id,Nota")] Avaliacao avaliacao)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Avaliacao/Edit/5
        public ActionResult Edit(int? id)
        {
            return View();
        }

        // POST: Avaliacao/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Avaliacao/Delete/5
        public ActionResult Delete(int? id)
        {
            return View();
        }

        // POST: Avaliacao/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
