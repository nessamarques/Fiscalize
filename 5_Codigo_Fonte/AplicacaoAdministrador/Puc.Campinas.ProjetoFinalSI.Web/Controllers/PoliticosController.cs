using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;
using Puc.Campinas.ProjetoFinalSI.Web.Models;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.IO;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class PoliticosController : Controller
    {
        public ActionResult Index()
        {
            PoliticoModel model = new PoliticoModel();
            model.ListaPoliticos = new PoliticoBO().ObterPoliticos();
            return View("PoliticosList", model);
        }

        public ActionResult Create()
        {
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 7).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 7).ToList()[0].PermissaoIncluir)
                {
                    PoliticoModel model = new PoliticoModel();
                    return this.View("PoliticosCreate", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 7).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 7).ToList()[0].PermissaoExcluir)
                {
                    string ret = new PoliticoBO().Excluir(new Politico() { IdPolitico = id });
                    PoliticoModel model = new PoliticoModel();
                    model.ListaPoliticos = new PoliticoBO().ObterPoliticos();
                    return this.View("PoliticosList", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 7).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 7).ToList()[0].PermissaoAlterar)
                {
                    PoliticoModel model = new PoliticoModel(new PoliticoBO().ObterPoliticoById(id));

                    return this.View("PoliticosEdit", model);
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 7).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 7).ToList()[0].PermissaoConsultar)
                {
                    PoliticoModel model = new PoliticoModel(new PoliticoBO().ObterPoliticoById(id));

                    return this.View("PoliticosDetails", model);
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

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetImage(int id)
        {
            Politico politico = new PoliticoBO().ObterPoliticoById(id);

            byte[] imageArray = (byte[])politico.Foto; // some method for returning the byte-array from db.

            return new FileContentResult(imageArray, "image/jpeg");
        }

        public ActionResult PesquisarRegistros(string param)
        {
            PoliticoModel model = new PoliticoModel();

            if (param != default(string) || !string.IsNullOrEmpty(param))
            {
                model.ListaPoliticos = new PoliticoBO().ObterPoliticos(param);
            }
            else
            {
                model.ListaPoliticos = new PoliticoBO().ObterPoliticos();
            }

            model.FiltroPesquisa = param;

            return this.View("PoliticosList", model);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection formCollection, HttpPostedFileBase file)
        {
            Politico politico = new Politico();

            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                System.Drawing.Image imagem = System.Drawing.Image.FromStream(file.InputStream);
                politico.Foto = imagem;
            }
            else
            {
                politico.Foto = null;
            }

            politico.Nome = formCollection.GetValue("Nome").AttemptedValue;
            politico.Sexo = formCollection.GetValue("Sexo").AttemptedValue;
            politico.DtNascimento = Convert.ToDateTime(formCollection.GetValue("DtNascimento").AttemptedValue);
            politico.IdCidadeNaturalidade = Convert.ToInt32(formCollection.GetValue("IdCidadeNaturalidade").AttemptedValue);
            politico.IdEstadoNaturalidade = Convert.ToInt32(formCollection.GetValue("IdEstadoNaturalidade").AttemptedValue);
            politico.IdPaisNaturalidade = Convert.ToInt32(formCollection.GetValue("IdPaisNaturalidade").AttemptedValue);

            politico.Email = formCollection.GetValue("Email").AttemptedValue;
            politico.IdEscolaridade = Convert.ToInt32(formCollection.GetValue("IdEscolaridade").AttemptedValue);
 
            if (ModelState.IsValid)
            {
                string ret = new PoliticoBO().Alterar(politico);
            }
            else
            {
                PoliticoModel model = new PoliticoModel(politico);
                return this.View("PoliticosCreate", model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection, HttpPostedFileBase file)
        {
            Politico politico = new Politico();

            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                System.Drawing.Image imagem = System.Drawing.Image.FromStream(file.InputStream);
                politico.Foto = imagem;
            }

            politico.Nome = formCollection.GetValue("Nome").AttemptedValue;
            politico.Sexo = formCollection.GetValue("Sexo").AttemptedValue;
            politico.DtNascimento = Convert.ToDateTime(formCollection.GetValue("DtNascimento").AttemptedValue);
            politico.IdCidadeNaturalidade = Convert.ToInt32(formCollection.GetValue("IdCidadeNaturalidade").AttemptedValue);
            politico.IdEstadoNaturalidade = Convert.ToInt32(formCollection.GetValue("IdEstadoNaturalidade").AttemptedValue);
            politico.IdPaisNaturalidade = Convert.ToInt32(formCollection.GetValue("IdPaisNaturalidade").AttemptedValue);
            politico.Email = formCollection.GetValue("Email").AttemptedValue;
            politico.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            politico.DtInclusao = DateTime.Now;
            politico.IdEscolaridade = Convert.ToInt32(formCollection.GetValue("IdEscolaridade").AttemptedValue);

            if (new PoliticoBO().NomeExiste(politico))
            {
                ModelState.AddModelError("", "Já existe um politico com o Nome especificado.");
            }

            if (ModelState.IsValid)
            {
                string ret = new PoliticoBO().Incluir(politico);
            }
            else
            {
                PoliticoModel model = new PoliticoModel(politico);
                return this.View("PoliticosCreate", model);
            }

            return RedirectToAction("Index");
        }

        public string ObterCidadesByIdEstado(int idEstado)
        {
            CidadeBO cidadesBO = new CidadeBO();

            List<Cidade> listaCidades = cidadesBO.ObterCidadeByIdEstado(idEstado);

            JavaScriptSerializer jss = new JavaScriptSerializer();

            return jss.Serialize(listaCidades);
        }
    }
}
