using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class PortalContatoController : Controller
    {
        public ActionResult Index()
        {
            return View("ContatoPortal");
        }
    }
}
