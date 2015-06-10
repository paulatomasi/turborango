using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TurboRango.Web.Controllers
{
    public class SorteiosController : Controller
    {
        // GET: Sorteios
        public ActionResult Index()
        {
            ViewBag.QtdRestaurantes = 33;
            return View();
        }
    }
}