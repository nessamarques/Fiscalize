using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Web.Models;
using Puc.Campinas.ProjetoFinalSI.BO;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO.Mandatos;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class ProposicaoController : Controller
    {
        public ActionResult Index()
        {
            return View("ProposicaoPesquisa", new ProposicaoModel());
        }

        public ActionResult PesquisarPolitico(int idPolitico)
        {
            ProposicaoModel model = new ProposicaoModel();

            string nomePolitico = new PoliticoBO().ObterPoliticoById(idPolitico).Nome;
            model.NomePolitico = nomePolitico;
            //model.IdPolitico = idPolitico;

            List<Proposicao> listProposicao = new List<Proposicao>();

            listProposicao = new ProposicaoBO().ObterProposicaoByIdPolitico(idPolitico);
            model.ListaProposicao = listProposicao;

            return View("ProposicaoList", model);
        }

        public ActionResult Create(int idPolitico)
        {
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 9).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 9).ToList()[0].PermissaoIncluir)
                {
                    ProposicaoModel model = new ProposicaoModel();
                    //model.IdPolitico = idPolitico;
                    model.NomePolitico = new PoliticoBO().ObterPoliticoById(idPolitico).Nome;

                    return View("ProposicaoCreate", model);
                }
                else
                {
                    return this.View("Erro");
                }
            }
            else
            {
                return this.View("Erro");
            }
        }

        public ActionResult Delete(int id)
        {
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 9).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 9).ToList()[0].PermissaoExcluir)
                {
                    ProposicaoBO proposicaoBO = new ProposicaoBO();
                    Proposicao item = proposicaoBO.ObterProposicaoById(id);

                    //int idPolitico = item.IdPolitico;

                    string ret = new ProposicaoBO().Excluir(new Proposicao() { IdProposicao = id });
                    ProposicaoModel model = new ProposicaoModel();

                    //model.ListaProposicao = new ProposicaoBO().ObterProposicaoByIdPolitico(idPolitico);
                    //model.NomePolitico = new PoliticoBO().ObterPoliticoById(idPolitico).Nome;



                    return this.View("ProposicaoList", model);
                }
                else
                {
                    return this.View("Erro");
                }
            }
            else
            {
                return this.View("Erro");
            }
        }

        public ActionResult Edit(int id)
        {
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 9).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 9).ToList()[0].PermissaoAlterar)
                {
                    ProposicaoModel model = new ProposicaoModel(new ProposicaoBO().ObterProposicaoById(id));
                   // model.NomePolitico = new PoliticoBO().ObterPoliticoById(model.IdPolitico).Nome;
                    return this.View("ProposicaoEdit", model);

                }
                else
                {
                    return this.View("Erro");
                }
            }
            else
            {
                return this.View("Erro");
            }
        }

        public ActionResult Details(int id)
        {
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 9).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 9).ToList()[0].PermissaoConsultar)
                {
                    ProposicaoModel model = new ProposicaoModel(new ProposicaoBO().ObterProposicaoById(id));
                    //model.NomePolitico = new PoliticoBO().ObterPoliticoById(model.IdPolitico).Nome;
                    return this.View("ProposicaoDetails", model);

                }
                else
                {
                    return this.View("Erro");
                }
            }
            else
            {
                return this.View("Erro");
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            Proposicao proposicao = new Proposicao();

            proposicao.IdProposicao = Convert.ToInt32(formCollection.GetValue("IdProposicao").AttemptedValue);
            proposicao.IdTipo = Convert.ToInt32(formCollection.GetValue("IdTipo").AttemptedValue);
            //proposicao.IdPolitico = Convert.ToInt32(formCollection.GetValue("IdPolitico").AttemptedValue);
            proposicao.Numero = formCollection.GetValue("Numero").AttemptedValue;
            proposicao.Ano = formCollection.GetValue("Ano").AttemptedValue;
            proposicao.Situacao = formCollection.GetValue("Situacao").AttemptedValue;
            proposicao.DtApresentacao = Convert.ToDateTime(formCollection.GetValue("DtApresentacao").AttemptedValue);
            proposicao.Ementa = formCollection.GetValue("Ementa").AttemptedValue;
            proposicao.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            proposicao.DtInclusao = DateTime.Now;

            if (ModelState.IsValid)
            {
                string ret = new ProposicaoBO().Incluir(proposicao);
            }
            else
            {
                ProposicaoModel model = new ProposicaoModel(proposicao);
                return this.View("ProposicaoCreate", model);
            }

            return RedirectToAction("RetornarTelaAnteriorPopulada", new { /*idPolitico = proposicao.IdPolitico*/ });
        }

        [HttpPost]
        public ActionResult Edit(FormCollection formCollection)
        {
            Proposicao proposicao = new Proposicao();

            proposicao.IdProposicao = Convert.ToInt32(formCollection.GetValue("IdProposicao").AttemptedValue);
            proposicao.IdTipo = Convert.ToInt32(formCollection.GetValue("IdTipo").AttemptedValue);
            //proposicao.IdPolitico = Convert.ToInt32(formCollection.GetValue("IdPolitico").AttemptedValue);
            proposicao.Numero = formCollection.GetValue("Numero").AttemptedValue;
            proposicao.Ano = formCollection.GetValue("Ano").AttemptedValue;
            proposicao.Situacao = formCollection.GetValue("Situacao").AttemptedValue;
            proposicao.DtApresentacao = Convert.ToDateTime(formCollection.GetValue("DtApresentacao").AttemptedValue);
            proposicao.Ementa = formCollection.GetValue("Ementa").AttemptedValue;
            proposicao.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            proposicao.DtInclusao = DateTime.Now;


            if (ModelState.IsValid)
            {
                string ret = new ProposicaoBO().Alterar(proposicao);
            }
            else
            {
                ProposicaoModel model = new ProposicaoModel(proposicao);
                return this.View("ProposicaoEdit", model);
            }

            return RedirectToAction("Index");

        }

        public ActionResult RetornarTelaAnteriorPopulada(int idPolitico)
        {
            ProposicaoModel model = new ProposicaoModel();

            string nomePolitico = new PoliticoBO().ObterPoliticoById(idPolitico).Nome;
            model.NomePolitico = nomePolitico;

            //model.IdPolitico = idPolitico;

            List<Proposicao> listProposicao = new List<Proposicao>();

            listProposicao = new ProposicaoBO().ObterProposicaoByIdPolitico(idPolitico);

            model.ListaProposicao = listProposicao;

            return View("ProposicaoList", model);
        }

        public ActionResult PesquisarRegistros(string param)
        {
            ProposicaoModel model = new ProposicaoModel();

            if (param != default(string) || !string.IsNullOrEmpty(param))
            {
                model.ListaPoliticos = new PoliticoBO().ObterPoliticos(param);
            }
            else
            {
                model.ListaPoliticos = new PoliticoBO().ObterPoliticos();
            }

            model.FiltroPesquisa = param;

            return View("ProposicaoPesquisa", model);
        }
    }

}