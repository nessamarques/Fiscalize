using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class HomePortalController : Controller
    {
        //
        // GET: /HomePortal/

        public ActionResult Index()
        {
            return View("_ViewPortal");
        }


        public ActionResult Login()
        {
            return View("Login");
        }

        //public ActionResult RedirectPortal()
        //{
        //    return View("_ViewPortal");
        //}

        public ActionResult Politicos()
        {
            return View("Politicos");
        }

        public ActionResult Registro()
        {
            return View("Registro");
        }

        public ActionResult Ajuda()
        {
            return View("Ajuda");
        }

    }
}
