using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TurboRango.Web.Controllers
{
    public class AvaliacaoController : Controller
    {
        // GET: Avaliacao
        public ActionResult Index()
        {
            return View();
        }

        // GET: Avaliacao/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Avaliacao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Avaliacao/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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
        public ActionResult Edit(int id)
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
        public ActionResult Delete(int id)
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
