using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Web.Models;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class PortalComissoesController : Controller
    {

        public ActionResult Index()
        {
            ComissaoModel model = new ComissaoModel();

            List<Comissao> listaComissoes = new ComissaoBO().ObterComissao();

            model.ListaComissao = listaComissoes;

            return this.PartialView("Comissoes", model);
        }

        public ActionResult MembrosComissao(int idComissao)
        {
            MembroComissaoModel model = new MembroComissaoModel();
            MembroComissaoBO membroComissaoBO = new MembroComissaoBO();

            Comissao comissao = new ComissaoBO().ObterComissaoById(idComissao);
            List<MembroComissao> listaMembrosComissao = membroComissaoBO.ObterMembroComissaoByIdComissao(idComissao);
            List<MembroComissao> listaTitularesComissao = membroComissaoBO.ObterMembroComissaoTitular(idComissao);
            List<MembroComissao> listaSuplentesComissao = membroComissaoBO.ObterMembroComissaoSuplente(idComissao);
            MembroComissao presidente = membroComissaoBO.ObterMembroComissaoPresidente(idComissao);
            MembroComissao vicePresidente1 = membroComissaoBO.ObterMembroComissaoVicePresidente1(idComissao);
            MembroComissao vicePresidente2 = membroComissaoBO.ObterMembroComissaoVicePresidente2(idComissao);
            MembroComissao vicePresidente3 = membroComissaoBO.ObterMembroComissaoVicePresidente3(idComissao);

            if(listaMembrosComissao != null && listaMembrosComissao.Count > 0)
            {
                model.NomeComissao = listaMembrosComissao[0].NomeComissao + " - " + listaMembrosComissao[0].SiglaComissao;
            }

            model.Secretario = comissao.Secretario;
            model.Local = comissao.Local;
            model.Telefone = comissao.Telefone;
            model.Fax = comissao.Fax;

            model.listaTitulares = listaTitularesComissao;
            model.listaSuplentes = listaSuplentesComissao;
            model.NomePresidente = presidente.NomePolitico;
            model.VicePresidente1 = vicePresidente1.NomePolitico;
            model.VicePresidente2 = vicePresidente2.NomePolitico;
            model.VicePresidente3 = vicePresidente3.NomePolitico;

            return this.PartialView("_MembrosComissao", model);
        }

    }
}
