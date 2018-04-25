using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;
using Puc.Campinas.ProjetoFinalSI.Web.Models;
using System.Web.Routing;
using System.Text;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class GruposController : Controller
    {
        public ActionResult Index()
        {
            GrupoModel model = new GrupoModel();
            model.ListaGrupos = new GrupoBO().ObterGrupos();
            return View("GruposList", model);
        }

        public ActionResult Create()
        {
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 2).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 2).ToList()[0].PermissaoIncluir)
                {
                    GrupoModel model = new GrupoModel();
                    return this.View("GruposCreate", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 2).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 2).ToList()[0].PermissaoExcluir)
                {
                    string ret = new GrupoBO().Excluir(new Grupo() { IdGrupo = id });
                    GrupoModel model = new GrupoModel();
                    model.ListaGrupos = new GrupoBO().ObterGrupos();
                    return this.View("GruposList", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 2).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 2).ToList()[0].PermissaoAlterar)
                {
                    Grupo grupo = new GrupoBO().ObterGrupoById(id);

                    GrupoModel model = new GrupoModel(grupo);

                    foreach (Permissao p in grupo.ListaPermissao)
                    {
                        model.ElementosSelecionadosListaFuncionalidades += p.IdFuncionalidade + ",";

                        if (p.PermissaoIncluir) { model.PermissoesSelecionadas += "Incluir" + p.IdFuncionalidade + ","; }
                        if (p.PermissaoExcluir) { model.PermissoesSelecionadas += "Excluir" + p.IdFuncionalidade + ","; }
                        if (p.PermissaoConsultar) { model.PermissoesSelecionadas += "Consultar" + p.IdFuncionalidade + ","; }
                        if (p.PermissaoAlterar) { model.PermissoesSelecionadas += "Alterar" + p.IdFuncionalidade + ","; }
                    }

                    return this.View("GruposEdit", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 2).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 2).ToList()[0].PermissaoConsultar)
                {
                    Grupo grupo = new GrupoBO().ObterGrupoById(id);

                    GrupoModel model = new GrupoModel(grupo);

                    foreach (Permissao p in grupo.ListaPermissao)
                    {
                        model.ElementosSelecionadosListaFuncionalidades += p.IdFuncionalidade + ",";

                        if (p.PermissaoIncluir) { model.PermissoesSelecionadas += "Incluir" + p.IdFuncionalidade + ","; }
                        if (p.PermissaoExcluir) { model.PermissoesSelecionadas += "Excluir" + p.IdFuncionalidade + ","; }
                        if (p.PermissaoConsultar) { model.PermissoesSelecionadas += "Consultar" + p.IdFuncionalidade + ","; }
                        if (p.PermissaoAlterar) { model.PermissoesSelecionadas += "Alterar" + p.IdFuncionalidade + ","; }
                    }

                    return this.View("GruposDetails", model);
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
            GrupoModel model = new GrupoModel();

            if (param != default(string) || !string.IsNullOrEmpty(param))
            {
                model.ListaGrupos = new GrupoBO().ObterGrupos(param);
            }
            else
            {
                model.ListaGrupos = new GrupoBO().ObterGrupos();
            }

            model.FiltroPesquisa = param;

            return this.View("GruposList", model);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection formCollection)
        {
            Grupo grupo = new Grupo();

            List<Permissao> listPermissao = new List<Permissao>();

            List<string> funcionalidadesSelecionadas = (formCollection.GetValue("hdnFuncionalidadesSelecionadas").AttemptedValue).Split(',').ToList<string>();

            foreach (string item in funcionalidadesSelecionadas)
            {
                if (item != string.Empty)
                {
                    Permissao permissao = new Permissao();

                    permissao.IdFuncionalidade = int.Parse(item);

                    List<string> permissoesSelecionadas = (formCollection.GetValue("hdnPermissoesSelecionadas").AttemptedValue).Split(',').ToList<string>();

                    if (permissoesSelecionadas.Contains("Consultar" + item)) permissao.PermissaoConsultar = true; else permissao.PermissaoConsultar = false;
                    if (permissoesSelecionadas.Contains("Incluir" + item)) permissao.PermissaoIncluir = true; else permissao.PermissaoIncluir = false;
                    if (permissoesSelecionadas.Contains("Alterar" + item)) permissao.PermissaoAlterar = true; else permissao.PermissaoAlterar = false;
                    if (permissoesSelecionadas.Contains("Excluir" + item)) permissao.PermissaoExcluir = true; else permissao.PermissaoExcluir = false;

                    permissao.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
                    permissao.DtInclusao = DateTime.Now;

                    listPermissao.Add(permissao);
                }
            }

            grupo.ListaPermissao = listPermissao;
            grupo.IdGrupo = Convert.ToInt32(formCollection.GetValue("IdGrupo").AttemptedValue);
            grupo.Nome = formCollection.GetValue("Nome").AttemptedValue;
            grupo.Descricao = formCollection.GetValue("Descricao").AttemptedValue;

            if (ModelState.IsValid)
            {
                string ret = new GrupoBO().Alterar(grupo);
            }
            else
            {
                GrupoModel model = new GrupoModel(grupo);
                return this.View("GruposCreate", model);
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            Grupo grupo = new Grupo();

            List<Permissao> listPermissao = new List<Permissao>();

            List<string> funcionalidadesSelecionadas = (formCollection.GetValue("hdnFuncionalidadesSelecionadas").AttemptedValue).Split(',').ToList<string>();

            foreach (string item in funcionalidadesSelecionadas)
            {
                if (item != string.Empty)
                {
                    Permissao permissao = new Permissao();

                    permissao.IdFuncionalidade = int.Parse(item);

                    List<string> permissoesSelecionadas = (formCollection.GetValue("hdnPermissoesSelecionadas").AttemptedValue).Split(',').ToList<string>();

                    if (permissoesSelecionadas.Contains("Consultar" + item)) permissao.PermissaoConsultar = true; else permissao.PermissaoConsultar = false;
                    if (permissoesSelecionadas.Contains("Incluir" + item)) permissao.PermissaoIncluir = true; else permissao.PermissaoIncluir = false;
                    if (permissoesSelecionadas.Contains("Alterar" + item)) permissao.PermissaoAlterar = true; else permissao.PermissaoAlterar = false;
                    if (permissoesSelecionadas.Contains("Excluir" + item)) permissao.PermissaoExcluir = true; else permissao.PermissaoExcluir = false;

                    permissao.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
                    permissao.DtInclusao = DateTime.Now;

                    listPermissao.Add(permissao);
                }
            }

            grupo.ListaPermissao = listPermissao;
            grupo.Nome = formCollection.GetValue("Nome").AttemptedValue;
            grupo.Descricao = formCollection.GetValue("Descricao").AttemptedValue;
            grupo.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            grupo.DtInclusao = DateTime.Now;

            if (new GrupoBO().ObterGrupoById(grupo.IdGrupo).Nome != grupo.Nome)
            {
                if (new GrupoBO().NomeExiste(grupo))
                {
                    ModelState.AddModelError("", "Já existe um grupo com o nome especificado.");
                }
            }

            if (ModelState.IsValid)
            {
                string ret = new GrupoBO().Incluir(grupo);
            }
            else
            {
                GrupoModel model = new GrupoModel(grupo);
                return this.View("GruposCreate", model);
            }

            return RedirectToAction("Index");
        }

        public string IncluirFuncionalidadeListagem(string funcionalidadesSelecionadas, string permissoesSelecionadas, string tpAction)
        {
            List<Funcionalidade> listagemFuncionalidades = new List<Funcionalidade>();

            listagemFuncionalidades = new FuncionalidadeBO().ObterFuncionalidades();

            listagemFuncionalidades = listagemFuncionalidades.Where(x => funcionalidadesSelecionadas.Contains(x.IdFuncionalidade.ToString() + ",")).ToList();

            StringBuilder sb = new StringBuilder();

            foreach (Funcionalidade item in listagemFuncionalidades)
            {
                string isChecadoConsulta = string.Empty, isChecadoIncluir = string.Empty, isChecadoAlterar = string.Empty, isChecadoExcluir = string.Empty;

                if (permissoesSelecionadas != string.Empty)
                {
                    if (permissoesSelecionadas.Contains("Consultar" + item.IdFuncionalidade)) isChecadoConsulta = "checked=checked"; else isChecadoConsulta = string.Empty;
                    if (permissoesSelecionadas.Contains("Incluir" + item.IdFuncionalidade)) isChecadoIncluir = "checked=checked"; else isChecadoIncluir = string.Empty;
                    if (permissoesSelecionadas.Contains("Alterar" + item.IdFuncionalidade)) isChecadoAlterar = "checked=checked"; else isChecadoAlterar = string.Empty;
                    if (permissoesSelecionadas.Contains("Excluir" + item.IdFuncionalidade)) isChecadoExcluir = "checked=checked"; else isChecadoExcluir = string.Empty;
                }

                sb.Append("<tr>");

                if (tpAction != "D")
                {
                    sb.Append("<td>" + item.Nome + "</td>");
                    sb.Append("<td><input id=\"Consultar" + item.IdFuncionalidade + "\" type=\"checkbox\"" + isChecadoConsulta + " onclick=\"clickCheckBox(this.id)\">Consultar</td>");
                    sb.Append("<td><input id=\"Incluir" + item.IdFuncionalidade + "\" type=\"checkbox\" " + isChecadoIncluir + " onclick=\"clickCheckBox(this.id)\">Incluir</td>");
                    sb.Append("<td><input id=\"Alterar" + item.IdFuncionalidade + "\" type=\"checkbox\" " + isChecadoAlterar + " onclick=\"clickCheckBox(this.id)\">Alterar</td>");
                    sb.Append("<td><input id=\"Excluir" + item.IdFuncionalidade + "\" type=\"checkbox\" " + isChecadoExcluir + " onclick=\"clickCheckBox(this.id)\">Excluir</td>");
                    sb.Append("<td><a href=\"#\" class=\"ask\" onclick=\"excluirRegistroFuncionalidade(" + item.IdFuncionalidade.ToString() + ")\"><img src=\"../../Content/img/delete.png\" alt=\"\" title=\"\" border=\"0\" /></a></td>");
                }
                else
                {
                    sb.Append("<td>" + item.Nome + "</td>");

                    string srcNTemPermissao = "../../Content/img/delete.png";
                    string srcTemPermissao = "../../Content/img/1385431440_tick.png";

                    if (isChecadoConsulta != string.Empty)
                        sb.Append("<td><a href=\"#\" class=\"ask\")\"><img src=\"" + srcTemPermissao + "\" alt=\"\" title=\"\" border=\"0\" /></a> Consultar</td>");
                    else
                        sb.Append("<td><a href=\"#\" class=\"ask\")\"><img src=\"" + srcNTemPermissao + "\" alt=\"\" title=\"\" border=\"0\" /></a> Consultar</td>");

                    if (isChecadoIncluir != string.Empty)
                        sb.Append("<td><a href=\"#\" class=\"ask\")\"><img src=\"" + srcTemPermissao + "\" alt=\"\" title=\"\" border=\"0\" /></a> Incluir</td>");
                    else
                        sb.Append("<td><a href=\"#\" class=\"ask\")\"><img src=\"" + srcNTemPermissao + "\" alt=\"\" title=\"\" border=\"0\" /></a> Incluir</td>");

                    if (isChecadoAlterar != string.Empty)
                        sb.Append("<td><a href=\"#\" class=\"ask\")\"><img src=\"" + srcTemPermissao + "\" alt=\"\" title=\"\" border=\"0\" /></a> Alterar</td>");
                    else
                        sb.Append("<td><a href=\"#\" class=\"ask\")\"><img src=\"" + srcNTemPermissao + "\" alt=\"\" title=\"\" border=\"0\" /></a> Alterar</td>");

                    if (isChecadoExcluir != string.Empty)
                        sb.Append("<td><a href=\"#\" class=\"ask\")\"><img src=\"" + srcTemPermissao + "\" alt=\"\" title=\"\" border=\"0\" /></a> Excluir</td>");
                    else
                        sb.Append("<td><a href=\"#\" class=\"ask\")\"><img src=\"" + srcNTemPermissao + "\" alt=\"\" title=\"\" border=\"0\" /></a> Excluir</td>");

                    sb.Append("<td><a href=\"#\" class=\"ask\"></td>");
                }

                sb.Append("</tr>");
            }

            return sb.ToString();
        }
    }
}

