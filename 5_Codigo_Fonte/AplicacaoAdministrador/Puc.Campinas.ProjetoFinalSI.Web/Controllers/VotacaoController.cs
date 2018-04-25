using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Web.Models;
using Puc.Campinas.ProjetoFinalSI.BO;
using Puc.Campinas.ProjetoFinalSI.Entidade;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class VotacaoController : Controller
    {
        public ActionResult Index()
        {
            VotacaoModel model = new VotacaoModel();
            model.ListVotacao = new VotacaoBO().ObterVotacoes();

            return View("VotacaoList",model);
        }

        public ActionResult Create()
        {
            VotacaoModel model = new VotacaoModel();

            return View("VotacaoCreate", model);
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            Votacao voto = new Votacao();

            ModelState modelState = new ModelState();

            if (!string.IsNullOrEmpty(formCollection.GetValue("IdSessaoProposicao").AttemptedValue))
            {
                voto.IdSessaoProposicao = Convert.ToInt32(formCollection.GetValue("IdSessaoProposicao").AttemptedValue);
            }
            else
            {
                modelState.Errors.Add("É necessário selecionar uma Sessão");
            }

            if (!string.IsNullOrEmpty(formCollection.GetValue("IdVoto").AttemptedValue))
            {
                voto.IdVoto = Convert.ToInt32(formCollection.GetValue("IdVoto").AttemptedValue);
            }
            else
            {
                modelState.Errors.Add("É necessário selecionar um Voto");
            }

            if (!string.IsNullOrEmpty(formCollection.GetValue("IdMandato").AttemptedValue))
            {
                voto.IdMandato = Convert.ToInt32(formCollection.GetValue("IdMandato").AttemptedValue);
            }
            //else
            //{
            //    modelState.Errors.Add("É necessário selecionar um Político");
            //}

            voto.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            voto.DtInclusao = DateTime.Now;
            
            if (modelState.Errors.Count == 0)
            {
                string ret = new VotacaoBO().Incluir(voto);
                return RedirectToAction("Index");
            }
            else
            { 
                VotacaoModel model = new VotacaoModel(voto);
                return View("VotacaoCreate", model);
            }
        }

    }
}
