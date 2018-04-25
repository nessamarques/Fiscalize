using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO.Mandatos;
using Puc.Campinas.ProjetoFinalSI.Web.Models;
using System.Web.Routing;
using Puc.Campinas.ProjetoFinalSI.BO;
using System.Web.Script.Serialization;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class AtividadesController : Controller
    {
        public ActionResult Index()
        {
            return View("AtividadesPesquisa", new AtividadeModel());
        }

        public ActionResult PesquisarAtividades(int idPolitico)
        {
            AtividadeModel model = new AtividadeModel();

            string nomePolitico = new PoliticoBO().ObterPoliticoById(idPolitico).Nome;
            model.NomePolitico = nomePolitico;

            model.IdPolitico = idPolitico;

            List<Atividade> listAtividades = new List<Atividade>();

            listAtividades = new AtividadeBO().ObterAtividadesByIdPolitico(idPolitico);

            model.ListaAtividades = listAtividades;

            return View("AtividadesList", model);
        }

        public ActionResult Create(int idPolitico)
        {
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 9).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 9).ToList()[0].PermissaoIncluir)
                {
                    AtividadeModel model = new AtividadeModel();
                    model.IdPolitico = idPolitico;

                    return View("AtividadesCreate", model);
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
                    AtividadeBO atividadeBO = new AtividadeBO();
                    Atividade item = atividadeBO.ObterAtividadeById(id);

                    int idPolitico = item.IdPolitico;
                    
                    string ret = new AtividadeBO().Excluir(new Atividade() { IdAtividade = id });
                    AtividadeModel model = new AtividadeModel();

                    model.ListaAtividades = new AtividadeBO().ObterAtividadesByIdPolitico(idPolitico);
                    model.NomePolitico = new PoliticoBO().ObterPoliticoById(idPolitico).Nome;

                    return this.View("AtividadesList", model);
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
                    AtividadeModel model = new AtividadeModel(new AtividadeBO().ObterAtividadeById(id));
                    return this.View("AtividadesEdit", model);
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
                    AtividadeModel model = new AtividadeModel(new AtividadeBO().ObterAtividadeById(id));
                    return this.View("AtividadesDetails", model);
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
            Atividade atividade = new Atividade();

            atividade.IdAtividade = Convert.ToInt32(formCollection.GetValue("IdAtividade").AttemptedValue);
            atividade.IdPolitico = Convert.ToInt32(formCollection.GetValue("IdPolitico").AttemptedValue);
            atividade.NomeComissao = formCollection.GetValue("NomeComissao").AttemptedValue;
            atividade.IdSituacao = Convert.ToInt32(formCollection.GetValue("IdSituacao").AttemptedValue);
            atividade.DtInicio = Convert.ToDateTime(formCollection.GetValue("DtInicio").AttemptedValue);
            atividade.DtFim = Convert.ToDateTime(formCollection.GetValue("DtFim").AttemptedValue);
            atividade.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            atividade.DtInclusao = DateTime.Now;

            if (ModelState.IsValid)
            {
                string ret = new AtividadeBO().Incluir(atividade);
            }
            else
            {
                AtividadeModel model = new AtividadeModel(atividade);
                return this.View("AtividadesCreate", model);
            }

            return RedirectToAction("RetornarTelaAnteriorPopulada", new { idPolitico = atividade.IdPolitico });
        }

        [HttpPost]
        public ActionResult Edit(FormCollection formCollection)
        {
            Atividade atividade = new Atividade();

            atividade.IdAtividade = Convert.ToInt32(formCollection.GetValue("IdAtividade").AttemptedValue);
            atividade.IdPolitico = Convert.ToInt32(formCollection.GetValue("IdPolitico").AttemptedValue);
            atividade.NomeComissao = formCollection.GetValue("NomeComissao").AttemptedValue;
            atividade.IdSituacao = Convert.ToInt32(formCollection.GetValue("IdSituacao").AttemptedValue);
            atividade.DtInicio = Convert.ToDateTime(formCollection.GetValue("DtInicio").AttemptedValue);
            atividade.DtFim = Convert.ToDateTime(formCollection.GetValue("DtFim").AttemptedValue);
            atividade.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            atividade.DtInclusao = DateTime.Now;


            if (ModelState.IsValid)
            {
                string ret = new AtividadeBO().Alterar(atividade);
            }
            else
            {
                AtividadeModel model = new AtividadeModel(atividade);
                return this.View("AtividadesEdit", model);
            }

            return RedirectToAction("Index");

        }

        public ActionResult RetornarTelaAnteriorPopulada(int idPolitico)
        {
            AtividadeModel model = new AtividadeModel();

            string nomePolitico = new PoliticoBO().ObterPoliticoById(idPolitico).Nome;
            model.NomePolitico = nomePolitico;

            model.IdPolitico = idPolitico;

            List<Atividade> listAtividades = new List<Atividade>();

            listAtividades = new AtividadeBO().ObterAtividadesByIdPolitico(idPolitico);

            model.ListaAtividades = listAtividades;

            return View("AtividadesList", model);
        }

        public ActionResult PesquisarRegistros(string param)
        {
            AtividadeModel model = new AtividadeModel();

            if (param != default(string) || !string.IsNullOrEmpty(param))
            {
                model.ListaPoliticos = new PoliticoBO().ObterPoliticos(param);
            }
            else
            {
                model.ListaPoliticos = new PoliticoBO().ObterPoliticos();
            }

            model.FiltroPesquisa = param;

            return View("AtividadesPesquisa", model);
        }
    }
}
