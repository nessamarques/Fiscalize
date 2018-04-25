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
    public class CargosController : Controller
    {
        public ActionResult Index()
        {
            CargoModel model = new CargoModel();
            model.ListaCargos = new CargoBO().ObterCargos();
            return View("CargosList", model);
        }

        public ActionResult Create()
        {
            CargoModel model = new CargoModel();

            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 4).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 4).ToList()[0].PermissaoIncluir)
                {
                    return this.View("CargosCreate", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 4).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 4).ToList()[0].PermissaoExcluir)
                {
                    CargoModel model = new CargoModel();
                    model.ListaCargos = new CargoBO().ObterCargos();

                    if (!new CargoBO().VerificaSeCargoEstaSendoUsado(id))
                    {
                        string ret = new CargoBO().Excluir(new Cargo() { IdCargo = id });
                        return this.View("CargosList", model);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Este Cargo já está sendo utilizado. Não é permitido excluir.");
                        return this.View("CargosList", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 4).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 4).ToList()[0].PermissaoExcluir)
                {
                    CargoModel model = new CargoModel(new CargoBO().ObterCargoById(id));
                    return this.View("CargosEdit", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 4).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 4).ToList()[0].PermissaoConsultar)
                {
                    CargoModel model = new CargoModel(new CargoBO().ObterCargoById(id));
                    return this.View("CargosDetails", model);
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
            CargoModel model = new CargoModel();

            if (param != default(string) || !string.IsNullOrEmpty(param))
            {
                model.ListaCargos = new CargoBO().ObterCargos(param);
            }
            else
            {
                model.ListaCargos = new CargoBO().ObterCargos();
            }

            model.FiltroPesquisa = param;

            return this.View("CargosList", model);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection formCollection)
        {
            Cargo cargo = new Cargo();

            cargo.IdCargo = Convert.ToInt32(formCollection.GetValue("IdCargo").AttemptedValue);
            cargo.Nome = formCollection.GetValue("Nome").AttemptedValue.ToUpper();
            cargo.Descricao = formCollection.GetValue("Descricao").AttemptedValue;
            cargo.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            cargo.DtInclusao = DateTime.Now;

            if (new CargoBO().ObterCargoById(cargo.IdCargo).Nome != cargo.Nome)
            {
                if (new CargoBO().NomeExiste(cargo))
                {
                    ModelState.AddModelError("", "Já existe um cargo com o Nome especificado.");
                }
            }

            if (ModelState.IsValid)
            {
                string ret = new CargoBO().Alterar(cargo);
            }
            else
            {
                CargoModel model = new CargoModel(cargo);
                return this.View("CargosEdit", model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            Cargo cargo = new Cargo();
                           
            cargo.Nome = formCollection.GetValue("Nome").AttemptedValue.ToUpper();
            cargo.Descricao = formCollection.GetValue("Descricao").AttemptedValue;
            cargo.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            cargo.DtInclusao = DateTime.Now;

            if (new CargoBO().NomeExiste(cargo))
            {
                ModelState.AddModelError("", "Já existe um cargo com o Nome especificado.");
            }
            
            if (ModelState.IsValid)
            {
                string ret = new CargoBO().Incluir(cargo);
            }
            else
            {
                CargoModel model = new CargoModel(cargo);
                return this.View("CargosCreate", model);
            }

            return RedirectToAction("Index");
        }
    }
}
