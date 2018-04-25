using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Web.Models;
using Puc.Campinas.ProjetoFinalSI.BO;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using System.Text;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class PortalPlenarioController : Controller
    {
        //
        // GET: /PortalPlenario/

        public ActionResult Index()
        {
            PortalPlenarioModel model = new PortalPlenarioModel();

            model.ListagemSessoes = new SessaoBO().ObterSessoes();

            return View("Plenario", model);
        }

        public string PesquisarSessoes(string nome, string situacao, DateTime? inicio, DateTime? termino)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Sessao item in new SessaoBO().ObterSessoes(nome, situacao, inicio, termino))
            {
                sb.Append("<tr style=\"border: 1px solid gainsboro; font-size: 12px; line-height: 20px;\">");
                sb.Append("<td style=\"width: 150px; text-align: left; padding-top: 5px; padding-left: 5px;\">");
                sb.Append(item.Nome);
                sb.Append("</td>");
                sb.Append("<td style=\"width: 650px; text-align: left; padding-top: 5px; padding-left: 5px;\">");
                sb.Append(item.Descricao);
                sb.Append("</td>");
                sb.Append("<td style=\"width: 100px; text-align: center; padding-top: 5px; padding-left: 5px;\">");

                switch (item.IdSituacao)
                {
                    case 1: sb.Append("Convocada"); break;
                    case 2: sb.Append("Em andamento"); break;
                    case 3: sb.Append("Cancelada"); break;
                    case 4: sb.Append("Encerrada"); break;
                }
                sb.Append("</td>");
                sb.Append("<td style=\"width: 100px; text-align: center; padding-top: 5px; padding-left: 5px;\">");
                sb.Append(item.DtInclusao.ToShortDateString());
                sb.Append("</td>");

                sb.Append("<td style=\"width: 100px; text-align: center; padding-top: 5px; padding-left: 5px;\">");
                if (item.DtFinal != null)
                {
                    sb.Append(item.DtFinal.ToShortDateString());
                }
                else
                {
                    sb.Append(" - ");
                }
                sb.Append("</td>");
                sb.Append("<td style=\"width: 50px; text-align: left; padding-top: 5px; padding-left: 5px;\">");
                sb.Append("<a href=\"/PortalPlenario/DetalharSessao?idSessao="+ item.IdSessao +"\"><img style=\"margin: 5px;\" src=\"../../Content/portal/img/detail.png\" alt=\"Detalhar partido\" title=\"Detalhar partido\" /></a>");
                sb.Append("</td>");
                sb.Append("</tr>");
            }

            return sb.ToString();
        }

        public ActionResult DetalharSessao(int idSessao)
        {
            PortalPlenarioModel model = new PortalPlenarioModel(new SessaoBO().ObterSessaoById(idSessao));
            
            return this.View("_DetalhesPlenario", model);
        }

    }
}
