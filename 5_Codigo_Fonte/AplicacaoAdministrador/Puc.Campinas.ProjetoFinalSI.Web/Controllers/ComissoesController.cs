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
    public class ComissoesController : Controller
    {
        public ActionResult Index()
        {
            ComissaoModel model = new ComissaoModel();
            model.ListaComissao = new ComissaoBO().ObterComissao();
            return View("ComissoesList", model);
        }

        public ActionResult Create()
        {
            ComissaoModel model = new ComissaoModel();

            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 15).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 15).ToList()[0].PermissaoIncluir)
                {
                    return this.View("ComissoesCreate", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 15).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 15).ToList()[0].PermissaoExcluir)
                {
                    ComissaoModel model = new ComissaoModel();
                    model.ListaComissao = new ComissaoBO().ObterComissao();

                    if (!new ComissaoBO().VerificaSeComissaoEstaSendoUsada(id))
                    {
                        string ret = new ComissaoBO().Excluir(new Comissao() { IdComissao = id });
                        return this.View("ComissoesList", model);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Esta comissão já está sendo utilizada. Não é permitido excluir.");
                        return this.View("ComissoesList", model);
                    }
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 15).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 15).ToList()[0].PermissaoExcluir)
                {
                    ComissaoModel model = new ComissaoModel(new ComissaoBO().ObterComissaoById(id));
                    return this.View("ComissoesEdit", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 15).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 15).ToList()[0].PermissaoConsultar)
                {
                    ComissaoModel model = new ComissaoModel(new ComissaoBO().ObterComissaoById(id));
                    return this.View("ComissoesDetails", model);
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
            ComissaoModel model = new ComissaoModel();

            if (param != default(string) || !string.IsNullOrEmpty(param))
            {
                model.ListaComissao = new ComissaoBO().ObterComissoes(param);
            }
            else
            {
                model.ListaComissao = new ComissaoBO().ObterComissao();
            }

            model.FiltroPesquisa = param;

            return this.View("ComissoesList", model);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection formCollection)
        {
            Comissao comissao = new Comissao();

            comissao.IdComissao = Convert.ToInt32(formCollection.GetValue("IdComissao").AttemptedValue);
            comissao.Nome = formCollection.GetValue("Nome").AttemptedValue.ToUpper();
            comissao.Sigla = formCollection.GetValue("Sigla").AttemptedValue.ToUpper();
            comissao.Local = formCollection.GetValue("Local").AttemptedValue;
            comissao.Telefone = formCollection.GetValue("Telefone").AttemptedValue;
            comissao.Fax = formCollection.GetValue("Fax").AttemptedValue;
            comissao.Secretario = formCollection.GetValue("Secretario").AttemptedValue;
            comissao.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            comissao.DtInclusao = DateTime.Now;

            if (new ComissaoBO().ObterComissaoById(comissao.IdComissao).Nome != comissao.Nome)
            {
                if (new ComissaoBO().NomeExiste(comissao))
                {
                    ModelState.AddModelError("", "Já existe uma comissão com o nome especificado.");
                }
            }

            if (ModelState.IsValid)
            {
                string ret = new ComissaoBO().Alterar(comissao);
            }
            else
            {
                ComissaoModel model = new ComissaoModel(comissao);
                return this.View("ComissoesEdit", model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            Comissao comissao = new Comissao();

            comissao.Nome = formCollection.GetValue("Nome").AttemptedValue.ToUpper();
            comissao.Sigla = formCollection.GetValue("Sigla").AttemptedValue.ToUpper();
            comissao.Local = formCollection.GetValue("Local").AttemptedValue;
            comissao.Telefone = formCollection.GetValue("Telefone").AttemptedValue;
            comissao.Fax = formCollection.GetValue("Fax").AttemptedValue;
            comissao.Secretario = formCollection.GetValue("Secretario").AttemptedValue;
            comissao.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            comissao.DtInclusao = DateTime.Now;

            if (new ComissaoBO().NomeExiste(comissao))
            {
                ModelState.AddModelError("", "Já existe uma comissão com o nome especificado.");
            }

            if (ModelState.IsValid)
            {
                string ret = new ComissaoBO().Incluir(comissao);
            }
            else
            {
                ComissaoModel model = new ComissaoModel(comissao);
                return this.View("ComissoesCreate", model);
            }

            return RedirectToAction("Index");
        }
    }
}