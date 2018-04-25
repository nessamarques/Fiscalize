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
    public class MembrosComissoesController : Controller
    {
        public ActionResult Index()
        {
            return View("MembrosComissoesPesquisa", new MembroComissaoModel());
        }

        public ActionResult PesquisarPolitico(int idPolitico)
        {
            MembroComissaoModel model = new MembroComissaoModel();

            string nomePolitico = new PoliticoBO().ObterPoliticoById(idPolitico).Nome;
            model.NomePolitico = nomePolitico;

            model.IdPolitico = idPolitico;

            List<MembroComissao> listMembrosComissoes = new List<MembroComissao>();

            listMembrosComissoes = new MembroComissaoBO().ObterMembroComissaoByIdPolitico(idPolitico);

            model.ListaMembrosComissoes = listMembrosComissoes;

            return View("MembrosComissoesList", model);
        }

        public ActionResult Create(int idPolitico)
        {
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 17).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 17).ToList()[0].PermissaoIncluir)
                {
                    MembroComissaoModel model = new MembroComissaoModel();
                    model.IdPolitico = idPolitico;

                    return View("MembrosComissoesCreate", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 17).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 17).ToList()[0].PermissaoExcluir)
                {
                    MembroComissaoBO membroComissaoBO = new MembroComissaoBO();
                    MembroComissao item = membroComissaoBO.ObterMembroComissaoById(id);

                    int idPolitico = item.IdPolitico;

                    string ret = membroComissaoBO.Excluir(new MembroComissao() { IdMembroComissao = id });
                    MembroComissaoModel model = new MembroComissaoModel();

                    model.ListaMembrosComissoes = membroComissaoBO.ObterMembroComissaoByIdPolitico(idPolitico);
                    model.NomePolitico = new PoliticoBO().ObterPoliticoById(idPolitico).Nome;

                    return this.View("MembrosComissoesList", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 17).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 17).ToList()[0].PermissaoAlterar)
                {
                    MembroComissaoModel model = new MembroComissaoModel(new MembroComissaoBO().ObterMembroComissaoById(id));
                    return this.View("MembrosComissoesEdit", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 17).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 17).ToList()[0].PermissaoConsultar)
                {
                    MembroComissaoModel model = new MembroComissaoModel(new MembroComissaoBO().ObterMembroComissaoById(id));
                    return this.View("MembrosComissoesDetails", model);
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
            MembroComissao membroComissao = new MembroComissao();

            membroComissao.IdMembroComissao = Convert.ToInt32(formCollection.GetValue("IdMembroComissao").AttemptedValue);
            membroComissao.IdPolitico = Convert.ToInt32(formCollection.GetValue("IdPolitico").AttemptedValue);
            membroComissao.IdCargoComissao = Convert.ToInt32(formCollection.GetValue("IdCargoComissao").AttemptedValue);
            membroComissao.IdComissao = Convert.ToInt32(formCollection.GetValue("IdComissao").AttemptedValue);
            membroComissao.DtInclusao = DateTime.Now;
            membroComissao.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            

            if (ModelState.IsValid)
            {
                string ret = new MembroComissaoBO().Incluir(membroComissao);
            }
            else
            {
                MembroComissaoModel model = new MembroComissaoModel(membroComissao);
                return this.View("MembrosComissoesCreate", model);
            }

            return RedirectToAction("RetornarTelaAnteriorPopulada", new { idPolitico = membroComissao.IdPolitico });
        }

        [HttpPost]
        public ActionResult Edit(FormCollection formCollection)
        {
            MembroComissao membroComissao = new MembroComissao();

            membroComissao.IdMembroComissao = Convert.ToInt32(formCollection.GetValue("IdMembroComissao").AttemptedValue);
            membroComissao.IdPolitico = Convert.ToInt32(formCollection.GetValue("IdPolitico").AttemptedValue);
            membroComissao.IdCargoComissao = Convert.ToInt32(formCollection.GetValue("IdCargoComissao").AttemptedValue);
            membroComissao.IdComissao = Convert.ToInt32(formCollection.GetValue("IdComissao").AttemptedValue);
            membroComissao.DtInclusao = DateTime.Now;
            membroComissao.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;

            if (ModelState.IsValid)
            {
                string ret = new MembroComissaoBO().Alterar(membroComissao);
            }
            else
            {
                MembroComissaoModel model = new MembroComissaoModel(membroComissao);
                return this.View("MembrosComissoesEdit", model);
            }

            return RedirectToAction("Index");

        }

        public ActionResult RetornarTelaAnteriorPopulada(int idPolitico)
        {
            MembroComissaoModel model = new MembroComissaoModel();

            string nomePolitico = new PoliticoBO().ObterPoliticoById(idPolitico).Nome;
            model.NomePolitico = nomePolitico;

            model.IdPolitico = idPolitico;

            List<MembroComissao> listMembrosComissoes = new List<MembroComissao>();

            listMembrosComissoes = new MembroComissaoBO().ObterMembroComissaoByIdPolitico(idPolitico);

            model.ListaMembrosComissoes = listMembrosComissoes;

            return View("MembrosComissoesList", model);
        }

        public ActionResult PesquisarRegistros(string param)
        {
            MembroComissaoModel model = new MembroComissaoModel();

            if (param != default(string) || !string.IsNullOrEmpty(param))
            {
                model.ListaPoliticos = new PoliticoBO().ObterPoliticos(param);
            }
            else
            {
                model.ListaPoliticos = new PoliticoBO().ObterPoliticos();
            }

            model.FiltroPesquisa = param;

            return View("MembrosComissoesPesquisa", model);
        }
    }
}
