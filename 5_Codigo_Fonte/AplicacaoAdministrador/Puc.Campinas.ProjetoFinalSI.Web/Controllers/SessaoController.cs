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
    public class SessaoController : Controller
    {
        public ActionResult Index()
        {
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 12).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 12).ToList()[0].PermissaoIncluir)
                {
                    SessaoModel model = new SessaoModel();
                    model.ListaSessoes = new SessaoBO().ObterSessoes();

                    return View("SessaoList", model);
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

        public ActionResult Create()
        {
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 12).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 12).ToList()[0].PermissaoIncluir)
                {
                    return View("SessaoCreate", new SessaoModel());
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 12).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 12).ToList()[0].PermissaoConsultar)
                {
                    return View("SessaoDetails", new SessaoModel(new SessaoBO().ObterSessaoById(id)));
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 12).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 12).ToList()[0].PermissaoExcluir)
                {
                    string ret = new SessaoBO().Excluir(new Sessao() { IdSessao = id });

                    SessaoModel model = new SessaoModel();
                    model.ListaSessoes = new SessaoBO().ObterSessoes();
                    
                    return this.View("SessaoList", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 12).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 12).ToList()[0].PermissaoAlterar)
                {
                    return View("SessaoEdit", new SessaoModel(new SessaoBO().ObterSessaoById(id)));
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

        public ActionResult PesquisarRegistros(string param)
        {
            SessaoModel model = new SessaoModel();

            if (param != default(string) || !string.IsNullOrEmpty(param))
            {
                model.ListaSessoes = new SessaoBO().ObterSessoes(param);
            }
            else
            {
                model.ListaSessoes = new SessaoBO().ObterSessoes();
            }

            model.FiltroPesquisa = param;

            return this.View("SessaoList", model);
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            Sessao sessao = new Sessao();

            sessao.Nome = formCollection.GetValue("Nome").AttemptedValue.ToUpper();
            sessao.Descricao = formCollection.GetValue("Descricao").AttemptedValue.ToUpper();
            sessao.DtInicial = Convert.ToDateTime(formCollection.GetValue("DtInicial").AttemptedValue);
            sessao.DtFinal = Convert.ToDateTime(formCollection.GetValue("DtFinal").AttemptedValue);

            if (sessao.DtInicial < sessao.DtInicial)
            {
                ModelState.AddModelError("Error", "Data Final menor que Data Inicial");
            }

            sessao.IdSituacao = Convert.ToInt32(formCollection.GetValue("IdSituacao").AttemptedValue);
            sessao.IdOrador = Convert.ToInt32(formCollection.GetValue("IdOrador").AttemptedValue);
            sessao.Pauta = Convert.ToString(formCollection.GetValue("Pauta").AttemptedValue);

            sessao.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            sessao.DtInclusao = DateTime.Now;

            if (ModelState.IsValid)
            {
                string ret = new SessaoBO().Incluir(sessao);
            }
            else
            {
                SessaoModel model = new SessaoModel(sessao);
                return this.View("CategoriasCreate", model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(FormCollection formCollection)
        {
            Sessao sessao = new Sessao();

            sessao.IdSessao = Convert.ToInt32(formCollection.GetValue("IdSessao").AttemptedValue);
            sessao.Nome = formCollection.GetValue("Nome").AttemptedValue.ToUpper();
            sessao.Descricao = formCollection.GetValue("Descricao").AttemptedValue.ToUpper();
            sessao.DtInicial = Convert.ToDateTime(formCollection.GetValue("DtInicial").AttemptedValue);
            sessao.DtFinal = Convert.ToDateTime(formCollection.GetValue("DtFinal").AttemptedValue);

            if (sessao.DtInicial < sessao.DtInicial)
            {
                ModelState.AddModelError("Error", "Data Final menor que Data Inicial");
            }

            sessao.IdSituacao = Convert.ToInt32(formCollection.GetValue("IdSituacao").AttemptedValue);
            sessao.IdOrador = Convert.ToInt32(formCollection.GetValue("IdOrador").AttemptedValue);
            sessao.Pauta = Convert.ToString(formCollection.GetValue("Pauta").AttemptedValue);

            sessao.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            sessao.DtInclusao = DateTime.Now;

            if (ModelState.IsValid)
            {
                string ret = new SessaoBO().Alterar(sessao);
            }
            else
            {
                SessaoModel model = new SessaoModel(sessao);
                return this.View("CategoriasCreate", model);
            }

            return RedirectToAction("Index");
        }

    }
}
