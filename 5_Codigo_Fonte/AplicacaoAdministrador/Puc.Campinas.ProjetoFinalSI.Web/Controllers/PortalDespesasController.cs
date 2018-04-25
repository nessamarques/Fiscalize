using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Web.Models;
using System.Text;
using Puc.Campinas.ProjetoFinalSI.BO;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using System.Net.Mail;
using System.Net;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class PortalDespesasController : Controller
    {
        //
        // GET: /PortalDespesas/

        public ActionResult Index(int? id = null)
        {
            return View("Despesas", new PortalPoliticosModel());
        }

        public ActionResult ObterPoliticosByFiltroUsuario(string id)
        {
            return PartialView("_PartialListDespesas", new PoliticoBO().ObterPoliticoById(int.Parse(id)));
        }

        public ActionResult RedirectRanking(int id)
        {
            PortalPoliticosModel model = new PortalPoliticosModel();

            model.NomePolitico = new PoliticoBO().ObterPoliticoById(id).Nome;
            model.HdnIdPolitico = Convert.ToInt32(id);

            return View("Despesas", model);
        }
    }
}
