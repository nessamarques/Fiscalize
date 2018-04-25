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
    public class CargosComissoesController : Controller
    {
        public ActionResult Index()
        {
            CargoComissaoModel model = new CargoComissaoModel();
            model.ListaCargoComissao = new CargoComissaoBO().ObterCargoComissao();
            return View("CargosComissoesList", model);
        }

        public ActionResult Create()
        {
            CargoComissaoModel model = new CargoComissaoModel();

            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 16).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 16).ToList()[0].PermissaoIncluir)
                {
                    return this.View("CargosComissoesCreate", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 16).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 16).ToList()[0].PermissaoExcluir)
                {
                    CargoComissaoModel model = new CargoComissaoModel();
                    model.ListaCargoComissao = new CargoComissaoBO().ObterCargoComissao();

                    if (!new CargoComissaoBO().VerificaSeCargoComissaoEstaSendoUsado(id))
                    {
                        string ret = new CargoComissaoBO().Excluir(new CargoComissao() { IdCargoComissao = id });
                        return this.View("CargosComissoesList", model);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Este cargo de comissão já está sendo utilizado. Não é permitido excluir.");
                        return this.View("CargosComissoesList", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 16).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 16).ToList()[0].PermissaoExcluir)
                {
                    CargoComissaoModel model = new CargoComissaoModel(new CargoComissaoBO().ObterCargoComissaoById(id));
                    return this.View("CargosComissoesEdit", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 16).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 16).ToList()[0].PermissaoConsultar)
                {
                    CargoComissaoModel model = new CargoComissaoModel(new CargoComissaoBO().ObterCargoComissaoById(id));
                    return this.View("CargosComissoesDetails", model);
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
            CargoComissaoModel model = new CargoComissaoModel();

            if (param != default(string) || !string.IsNullOrEmpty(param))
            {
                model.ListaCargoComissao = new CargoComissaoBO().ObterCargoComissoes(param);
            }
            else
            {
                model.ListaCargoComissao = new CargoComissaoBO().ObterCargoComissao();
            }

            model.FiltroPesquisa = param;

            return this.View("CargosComissoesList", model);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection formCollection)
        {
            CargoComissao cargoComissao = new CargoComissao();

            cargoComissao.IdCargoComissao = Convert.ToInt32(formCollection.GetValue("IdCargoComissao").AttemptedValue);
            cargoComissao.Nome = formCollection.GetValue("Nome").AttemptedValue.ToUpper();
            cargoComissao.Descricao = formCollection.GetValue("Descricao").AttemptedValue.ToUpper();
            cargoComissao.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            cargoComissao.DtInclusao = DateTime.Now;

            if (new CargoComissaoBO().ObterCargoComissaoById(cargoComissao.IdCargoComissao).Nome != cargoComissao.Nome)
            {
                if (new CargoComissaoBO().NomeExiste(cargoComissao))
                {
                    ModelState.AddModelError("", "Já existe um cargo de comissão com o nome especificado.");
                }
            }

            if (ModelState.IsValid)
            {
                string ret = new CargoComissaoBO().Alterar(cargoComissao);
            }
            else
            {
                CargoComissaoModel model = new CargoComissaoModel(cargoComissao);
                return this.View("CargosComissoesEdit", model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            CargoComissao cargoComissao = new CargoComissao();

            cargoComissao.Nome = formCollection.GetValue("Nome").AttemptedValue.ToUpper();
            cargoComissao.Descricao = formCollection.GetValue("Descricao").AttemptedValue;
            cargoComissao.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            cargoComissao.DtInclusao = DateTime.Now;

            if (new CargoComissaoBO().NomeExiste(cargoComissao))
            {
                ModelState.AddModelError("", "Já existe um cargo comissão com o nome especificado.");
            }

            if (ModelState.IsValid)
            {
                string ret = new CargoComissaoBO().Incluir(cargoComissao);
            }
            else
            {
                CargoComissaoModel model = new CargoComissaoModel(cargoComissao);
                return this.View("CargosComissoesCreate", model);
            }

            return RedirectToAction("Index");
        }
    }
}