using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;
using Puc.Campinas.ProjetoFinalSI.Web.Models;
using System.Web.Routing;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class FuncionalidadesController : Controller
    {
        public ActionResult Index()
        {
            FuncionalidadeModel model = new FuncionalidadeModel();
            model.ListaFuncionalidades = new FuncionalidadeBO().ObterFuncionalidades();
            return View("FuncionalidadesList", model);
        }

        public ActionResult Create()
        {
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 1).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 1).ToList()[0].PermissaoIncluir)
                {
                    FuncionalidadeModel model = new FuncionalidadeModel();
                    return this.View("FuncionalidadesCreate", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 1).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 1).ToList()[0].PermissaoExcluir)
                {
                    string ret = new FuncionalidadeBO().Excluir(new Funcionalidade() { IdFuncionalidade = id });
                    FuncionalidadeModel model = new FuncionalidadeModel();
                    model.ListaFuncionalidades = new FuncionalidadeBO().ObterFuncionalidades();
                    return this.View("FuncionalidadesList", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 1).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 1).ToList()[0].PermissaoAlterar)
                {
                    FuncionalidadeModel model = new FuncionalidadeModel(new FuncionalidadeBO().ObterFuncionalidadeById(id));
                    return this.View("FuncionalidadesEdit", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 1).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 1).ToList()[0].PermissaoConsultar)
                {
                    FuncionalidadeModel model = new FuncionalidadeModel(new FuncionalidadeBO().ObterFuncionalidadeById(id));
                    return this.View("FuncionalidadesDetails", model);
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
            FuncionalidadeModel model = new FuncionalidadeModel();

            if (param != default(string) || !string.IsNullOrEmpty(param))
            {
                model.ListaFuncionalidades = new FuncionalidadeBO().ObterFuncionalidades(param);
            }
            else
            {
                model.ListaFuncionalidades = new FuncionalidadeBO().ObterFuncionalidades();
            }

            model.FiltroPesquisa = param;

            return this.View("FuncionalidadesList", model);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection formCollection)
        {
            Funcionalidade funcionalidade = new Funcionalidade();

            funcionalidade.IdFuncionalidade = Convert.ToInt32(formCollection.GetValue("IdFuncionalidade").AttemptedValue);
            funcionalidade.Nome = formCollection.GetValue("Nome").AttemptedValue;
            funcionalidade.Descricao = formCollection.GetValue("Descricao").AttemptedValue;
            funcionalidade.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            funcionalidade.DtInclusao = DateTime.Now;

            if (new FuncionalidadeBO().ObterFuncionalidadeById(funcionalidade.IdFuncionalidade).Nome != funcionalidade.Nome)
            {
                if (new FuncionalidadeBO().NomeExiste(funcionalidade))
                {
                    ModelState.AddModelError("", "Já existe uma funcionalidade com o nome especificado.");
                }
            }

            if (ModelState.IsValid)
            {
                string ret = new FuncionalidadeBO().Alterar(funcionalidade);
            }
            else
            {
                FuncionalidadeModel model = new FuncionalidadeModel(funcionalidade);
                return this.View("FuncionalidadesEdit", model);
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            Funcionalidade funcionalidade = new Funcionalidade();

            funcionalidade.IdFuncionalidade = Convert.ToInt32(formCollection.GetValue("IdFuncionalidade").AttemptedValue);
            funcionalidade.Nome = formCollection.GetValue("Nome").AttemptedValue;
            funcionalidade.Descricao = formCollection.GetValue("Descricao").AttemptedValue;
            funcionalidade.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            funcionalidade.DtInclusao = DateTime.Now;

            if (new FuncionalidadeBO().ObterFuncionalidadeById(funcionalidade.IdFuncionalidade).Nome != funcionalidade.Nome)
            {
                if (new FuncionalidadeBO().NomeExiste(funcionalidade))
                {
                    ModelState.AddModelError("", "Já existe uma funcionalidade com o nome especificado.");
                }
            }

            if (ModelState.IsValid)
            {
                string ret = new FuncionalidadeBO().Incluir(funcionalidade);
            }
            else
            {
                FuncionalidadeModel model = new FuncionalidadeModel(funcionalidade);
                return this.View("FuncionalidadesCreate", model);
            }

            return RedirectToAction("Index");
        }
    }
}

