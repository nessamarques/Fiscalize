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
    public class PartidosController : Controller
    {
        public ActionResult Index()
        {
            PartidoModel model = new PartidoModel();
            model.ListaPartidos = new PartidoBO().ObterPartidos();
            return View("PartidosList", model);
        }

        public ActionResult Create()
        {
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 5).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 5).ToList()[0].PermissaoIncluir)
                {
                    PartidoModel model = new PartidoModel();
                    return this.View("PartidosCreate", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 5).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 5).ToList()[0].PermissaoExcluir)
                {
                    PartidoModel model = new PartidoModel();
                    model.ListaPartidos = new PartidoBO().ObterPartidos();

                    if (!new PartidoBO().VerificaSePartidoEstaSendoUsado(id))
                    {
                        string ret = new PartidoBO().Excluir(new Partido() { IdPartido = id });
                        return this.View("PartidosList", model);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Este Partido já está sendo utilizado. Não é permitido excluir.");
                        return this.View("PartidosList", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 5).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 5).ToList()[0].PermissaoAlterar)
                {
                    PartidoModel model = new PartidoModel(new PartidoBO().ObterPartidoById(id));
                    return this.View("PartidosEdit", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 5).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 5).ToList()[0].PermissaoConsultar)
                {
                    PartidoModel model = new PartidoModel(new PartidoBO().ObterPartidoById(id));
                    return this.View("PartidosDetails", model);
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
            PartidoModel model = new PartidoModel();

            if (param != default(string) || !string.IsNullOrEmpty(param))
            {
                model.ListaPartidos = new PartidoBO().ObterPartidos(param);
            }
            else
            {
                model.ListaPartidos = new PartidoBO().ObterPartidos();
            }

            model.FiltroPesquisa = param;

            return this.View("PartidosList", model);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection formCollection)
        {
            Partido partido = new Partido();

            partido.IdPartido = Convert.ToInt32(formCollection.GetValue("IdPartido").AttemptedValue);
            partido.Sigla = formCollection.GetValue("Sigla").AttemptedValue;
            partido.Nome = formCollection.GetValue("Nome").AttemptedValue;
            partido.PresidenteNacional = formCollection.GetValue("PresidenteNacional").AttemptedValue;

            if (ValidaData(formCollection.GetValue("Deferimento").AttemptedValue))
            {
                partido.Deferimento = Convert.ToDateTime(formCollection.GetValue("Deferimento").AttemptedValue);
            }
            else
            {
                ModelState.AddModelError("", "Data Inválida");
            }

            partido.NroPartido = int.Parse(formCollection.GetValue("NroPartido").AttemptedValue);

            partido.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            partido.DtInclusao = DateTime.Now;

            //if (new PartidoBO().ObterPartidoById(partido.IdPartido).Nome.Equals(partido.Nome))
            //{
            //    if (new PartidoBO().NomeExiste(partido))
            //    {
            //        ModelState.AddModelError("", "Já existe um partido com o mesmo Nome e/ou Sigla e/ou Número especificado.");
            //    }
            //}

            if (ModelState.IsValid)
            {
                string ret = new PartidoBO().Alterar(partido);
            }
            else
            {
                PartidoModel model = new PartidoModel(partido);
                return this.View("PartidosEdit", model);
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            Partido partido = new Partido();

            partido.IdPartido = Convert.ToInt32(formCollection.GetValue("IdPartido").AttemptedValue);
            partido.Sigla = formCollection.GetValue("Sigla").AttemptedValue;
            partido.Nome = formCollection.GetValue("Nome").AttemptedValue;
            partido.PresidenteNacional = formCollection.GetValue("PresidenteNacional").AttemptedValue;

            if (ValidaData(formCollection.GetValue("Deferimento").AttemptedValue))
            {
                partido.Deferimento = Convert.ToDateTime(formCollection.GetValue("Deferimento").AttemptedValue);
            }
            else
            {
                ModelState.AddModelError("", "Data Inválida");
            }

            partido.NroPartido = int.Parse(formCollection.GetValue("NroPartido").AttemptedValue);

            partido.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            partido.DtInclusao = DateTime.Now;

            if (new PartidoBO().ObterPartidoById(partido.IdPartido).Nome != partido.Nome)
            {
                if (new PartidoBO().NomeExiste(partido))
                {
                    ModelState.AddModelError("", "Já existe um partido com o mesmo Nome e/ou Sigla e/ou Número especificado.");
                }
            }

            if (ModelState.IsValid)
            {
                string ret = new PartidoBO().Incluir(partido);
            }
            else
            {
                PartidoModel model = new PartidoModel(partido);
                return this.View("PartidosCreate", model);
            }

            return RedirectToAction("Index");
        }

        public bool ValidaData(string sData)
        {
            try
            {
                DateTime.Parse(sData);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

