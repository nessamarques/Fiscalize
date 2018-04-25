using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Web.Models;
using Puc.Campinas.ProjetoFinalSI.BO;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using System.Web.Routing;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class CategoriasController : Controller
    {
        public ActionResult Index()
        {
            CategoriaModel model = new CategoriaModel();
            model.ListaCategoria = new CategoriaBO().ObterCategoria();
            return View("CategoriasList", model);
        }

        public ActionResult Create()
        {
            CategoriaModel model = new CategoriaModel();

            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 4).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 4).ToList()[0].PermissaoIncluir)
                {
                    return this.View("CategoriasCreate", model);
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

                    CategoriaModel model = new CategoriaModel();
                    model.ListaCategoria = new CategoriaBO().ObterCategoria();

                    if (!new CategoriaBO().VerificaSeCategoriaEstaSendoUsada(id))
                    {
                        string ret = new CategoriaBO().Excluir(new Categoria() { IdCategoria = id });


                        model.ListaCategoria = new CategoriaBO().ObterCategoria();

                        return this.View("CategoriasList", model);


                    }
                    else
                    {
                        ModelState.AddModelError("", "Esta Categoria já está sendo utilizada. Não é permitido excluir.");
                        return this.View("CategoriasList", model);
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
                    CategoriaModel model = new CategoriaModel(new CategoriaBO().ObterCategoriaByIdCategoria(id)[0]);
                    return this.View("CategoriasEdit", model);
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
                    CategoriaModel model = new CategoriaModel(new CategoriaBO().ObterCategoriaByIdCategoria(id)[0]);
                    return this.View("CategoriasDetails", model);
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
            CategoriaModel model = new CategoriaModel();

            if (param != default(string) || !string.IsNullOrEmpty(param))
            {
                model.ListaCategoria = new CategoriaBO().ObterCategoria(param);
            }
            else
            {
                model.ListaCategoria = new CategoriaBO().ObterCategoria();
            }

            model.FiltroPesquisa = param;

            return this.View("CategoriasList", model);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection formCollection)
        {
            Categoria categoria = new Categoria();

            categoria.IdCategoria = Convert.ToInt32(formCollection.GetValue("IdCategoria").AttemptedValue);
            categoria.Nome = formCollection.GetValue("Nome").AttemptedValue.ToUpper();
            categoria.Descricao = formCollection.GetValue("Descricao").AttemptedValue.ToUpper();
            categoria.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            categoria.DtInclusao = DateTime.Now;

            if (new CategoriaBO().ObterCategoriaByIdCategoria(categoria.IdCategoria)[0].Nome != categoria.Nome)
            {
                if (new CategoriaBO().NomeExiste(categoria))
                {
                    ModelState.AddModelError("", "Já existe uma categoria com o Nome especificado.");
                }
            }

            if (ModelState.IsValid)
            {
                string ret = new CategoriaBO().Alterar(categoria);
            }
            else
            {
                CategoriaModel model = new CategoriaModel(categoria);
                return this.View("CategoriasEdit", model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            Categoria categoria = new Categoria();

            categoria.Nome = formCollection.GetValue("Nome").AttemptedValue.ToUpper();
            categoria.Descricao = formCollection.GetValue("Descricao").AttemptedValue.ToUpper();
            categoria.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            categoria.DtInclusao = DateTime.Now;

            if (new CategoriaBO().NomeExiste(categoria))
            {
                ModelState.AddModelError("", "Já existe uma categoria com o Nome especificado.");
            }

            if (ModelState.IsValid)
            {
                string ret = new CategoriaBO().Incluir(categoria);
            }
            else
            {
                CategoriaModel model = new CategoriaModel(categoria);
                return this.View("CategoriasCreate", model);
            }

            return RedirectToAction("Index");
        }


    }


}