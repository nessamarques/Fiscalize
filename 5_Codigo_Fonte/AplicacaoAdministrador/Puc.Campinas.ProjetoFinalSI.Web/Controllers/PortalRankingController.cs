using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Web.Models;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;
using System.Text;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class PortalRankingController : Controller
    {
        //
        // GET: /PortalRanking/

        public ActionResult Index()
        {
            return View("Ranking", new PortalPoliticosModel());
        }

        [HttpPost]
        public ActionResult ObterPoliticosRanking(FormCollection frmCollection)
        {
            PortalPoliticosModel model = new PortalPoliticosModel();

            model.TipoRanking = frmCollection.GetValue("TipoRanking").AttemptedValue;

            model.IdCargo = Convert.ToInt32(frmCollection.GetValue("IdCargo").AttemptedValue);

            if (model.TipoRanking == "gastosPoliticos")
            {
                model.RankingDespesas = new DespesaBO().ObterRankingDespesa().Where(x => x.Mandato.IdCargo == model.IdCargo).ToList();
            }
            if (model.TipoRanking == "assiduidade")
            {
                model.RankingPresencas = new PresencaBO().ObterFaltasSessoes(model.IdCargo).Take(5).ToList();
            }

            return View("Ranking", model);
        }

    }
}
