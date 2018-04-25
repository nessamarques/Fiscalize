using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Web.Models;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;
using AutenticacaoUsuarioMVC3.Models;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Text;
using System.Data;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class PortalNoticiasController : Controller
    {
        //
        // GET: /PortalNoticias/
        public ActionResult Index()
        {
            return View("SelecionarArea");
        }

        public ActionResult IncluirNoticia(int areaSelecionada)
        {
            NoticiaModel model = new NoticiaModel();
            model.AreaSelecionada = areaSelecionada;

            return this.PartialView("_PortalNoticiasInclusao", model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetImage(int id)
        {
            ImagemNoticia img = new ImagemNoticiaBO().ObterImagemNoticiaById(id);

            byte[] imageArray = (byte[])img.Imagem; // some method for returning the byte-array from db.

            return new FileContentResult(imageArray, "image/jpeg");
        }

        public ActionResult ManterNoticiasByArea(int areaSelecionada)
        {
            List<Noticia> ListagemNoticias = new List<Noticia>();
            ListagemNoticias = new NoticiaBO().ObterNoticiaByIdLocalNoticia(areaSelecionada);

            NoticiaModel model = new NoticiaModel();
            model.ListaNoticias = ListagemNoticias;

            model.AreaSelecionada = areaSelecionada;

            return this.PartialView("_PortalNoticiasListagem", model);
        }

        public ActionResult DetalharNoticia(int idNoticia)
        {
            Noticia noticia = new NoticiaBO().ObterNoticiaById(idNoticia);

            return this.PartialView("_PortalNoticiasDetalhes", noticia);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(FormCollection frmCollection, HttpPostedFileBase file0, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4, HttpPostedFileBase file5)
        {
            Usuario usuario = UsersRepository.UsuarioLogado;

            Noticia noticia = new Noticia();

            string politicosSelecionados = string.Empty;

            noticia.Login = usuario.UsuarioLogin != null ? usuario.UsuarioLogin : "";
            noticia.DtInclusao = DateTime.Now;

            if (!string.IsNullOrEmpty(frmCollection.GetValue("PoliticosSelecionados").AttemptedValue))
            {
                politicosSelecionados = frmCollection.GetValue("PoliticosSelecionados").AttemptedValue.Substring(0, frmCollection.GetValue("PoliticosSelecionados").AttemptedValue.Length - 1);
            }

            if (!string.IsNullOrEmpty(frmCollection.GetValue("Titulo").AttemptedValue))
            {
                noticia.Titulo = frmCollection.GetValue("Titulo").AttemptedValue;
            }

            if (!string.IsNullOrEmpty(frmCollection.GetValue("Subtitulo").AttemptedValue))
            {
                noticia.Subtitulo = frmCollection.GetValue("Subtitulo").AttemptedValue;
            }

            if (!string.IsNullOrEmpty(frmCollection.GetValue("Resumo").AttemptedValue))
            {
                noticia.Resumo = frmCollection.GetValue("Resumo").AttemptedValue;
            }

            if (!string.IsNullOrEmpty(frmCollection.GetValue("Conteudo").AttemptedValue))
            {
                noticia.Conteudo = frmCollection.GetValue("Conteudo").AttemptedValue;
            }

            noticia.Ativo = 1;

            if (!string.IsNullOrEmpty(frmCollection.GetValue("LinkVideo").AttemptedValue))
            {
                noticia.LinkVideo = frmCollection.GetValue("LinkVideo").AttemptedValue;
            }


            if (!string.IsNullOrEmpty(frmCollection.GetValue("AreaSelecionada").AttemptedValue))
            {
                noticia.IdLocalNoticia = Convert.ToInt32(frmCollection.GetValue("AreaSelecionada").AttemptedValue);
            }
            else
            {
                ModelState.AddModelError("ERRO", "Área Inválida");
            }

            NoticiaBO noticiaBO = new NoticiaBO();
            string ret = noticiaBO.Incluir(noticia);

            List<HttpPostedFileBase> listImagens = new List<HttpPostedFileBase>();
            listImagens.Add(file0);
            listImagens.Add(file1);
            listImagens.Add(file2);
            listImagens.Add(file3);
            listImagens.Add(file4);
            listImagens.Add(file5);

            foreach (HttpPostedFileBase item in listImagens)
            {
                if (item != null && item.ContentLength > 0)
                {
                    System.Drawing.Image imagem = System.Drawing.Image.FromStream(item.InputStream);

                    ImagemNoticia img = new ImagemNoticia();

                    img.DtInclusao = DateTime.Now;

                    if (listImagens.IndexOf(item) == 0)
                    {
                        img.IsPortal = 1;
                    }
                    else
                    {
                        img.IsPortal = 0;
                    }

                    img.IdNoticia = new NoticiaBO().ObterUltimaNoticia().IdNoticia;
                    img.Login = usuario.UsuarioLogin;
                    img.Imagem = imagem;
                    img.IdLocalNoticia = Convert.ToInt32(frmCollection.GetValue("AreaSelecionada").AttemptedValue);

                    ImagemNoticiaBO imgBO = new ImagemNoticiaBO();
                    imgBO.Incluir(img);

                    img = null;
                }
            }

            if (politicosSelecionados != string.Empty)
            {
                List<string> list = new List<string>();

                list = politicosSelecionados.Split(',').ToList();

                foreach (string item in list)
                {
                    NoticiaPolitico np = new NoticiaPolitico();
                    np.DtInclusao = DateTime.Now;
                    np.IdNoticia = new NoticiaBO().ObterUltimaNoticia().IdNoticia;
                    np.IdPolitico = Convert.ToInt32(item);
                    np.Login = usuario.UsuarioLogin;

                    NoticiaPoliticoBO bo = new NoticiaPoliticoBO();
                    bo.Incluir(np);
                }
            }


            NoticiaModel model = new NoticiaModel();
            model.AreaSelecionada = Convert.ToInt32(frmCollection.GetValue("AreaSelecionada").AttemptedValue);

            return View("SelecionarArea", model);
        }

        public ActionResult Delete(int id, int idAreaSelecionada)
        {
            NoticiaBO noticiaBO = new NoticiaBO();
            noticiaBO.Excluir(new Noticia() { IdNoticia = id });

            NoticiaModel model = new NoticiaModel();
            model.AreaSelecionada = idAreaSelecionada;

            return RedirectToAction("ManterNoticiasByArea", new { areaSelecionada = idAreaSelecionada });
        }

        public ActionResult DesativarNoticia(int id, int idAreaSelecionada)
        {
            NoticiaBO noticiaBO = new NoticiaBO();
            noticiaBO.DesativarNoticia(id);

            NoticiaModel model = new NoticiaModel();
            model.AreaSelecionada = idAreaSelecionada;

            return RedirectToAction("ManterNoticiasByArea", new { areaSelecionada = idAreaSelecionada });
        }

        public ActionResult AtivarNoticia(int id, int idAreaSelecionada)
        {
            NoticiaBO noticiaBO = new NoticiaBO();
            noticiaBO.AtivarNoticia(id);

            NoticiaModel model = new NoticiaModel();
            model.AreaSelecionada = idAreaSelecionada;

            return RedirectToAction("ManterNoticiasByArea", new { areaSelecionada = idAreaSelecionada });
        }

        public ActionResult Edit(int id, int idAreaSelecionada)
        {
            NoticiaBO noticiaBO = new NoticiaBO();
            Noticia noticia = noticiaBO.ObterNoticiaById(id);

            string politicosSelecionados = string.Empty;

            if (noticia.ListaPoliticos.Count > 0)
            {
                foreach (NoticiaPolitico item in noticia.ListaPoliticos)
                {
                    politicosSelecionados += item.IdPolitico + ",";
                }
            }

            NoticiaModel model = new NoticiaModel(noticia);
            model.PoliticosSelecionados = politicosSelecionados;

            model.AreaSelecionada = idAreaSelecionada;

            return View("_PortalNoticiasEdicao", model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(FormCollection frmCollection)
        {
            Noticia noticia = new Noticia();

            string politicosSelecionados = string.Empty;

            if (!string.IsNullOrEmpty(frmCollection.GetValue("PoliticosSelecionados").AttemptedValue))
            {
                politicosSelecionados = frmCollection.GetValue("PoliticosSelecionados").AttemptedValue.Substring(0, frmCollection.GetValue("PoliticosSelecionados").AttemptedValue.Length - 1);
            }

            if (!string.IsNullOrEmpty(frmCollection.GetValue("IdNoticia").AttemptedValue))
            {
                noticia.IdNoticia = Convert.ToInt32(frmCollection.GetValue("IdNoticia").AttemptedValue);
            }

            if (!string.IsNullOrEmpty(frmCollection.GetValue("Titulo").AttemptedValue))
            {
                noticia.Titulo = frmCollection.GetValue("Titulo").AttemptedValue;
            }

            if (!string.IsNullOrEmpty(frmCollection.GetValue("Subtitulo").AttemptedValue))
            {
                noticia.Subtitulo = frmCollection.GetValue("Subtitulo").AttemptedValue;
            }

            if (!string.IsNullOrEmpty(frmCollection.GetValue("Resumo").AttemptedValue))
            {
                noticia.Resumo = frmCollection.GetValue("Resumo").AttemptedValue;
            }

            if (!string.IsNullOrEmpty(frmCollection.GetValue("Conteudo").AttemptedValue))
            {
                noticia.Conteudo = frmCollection.GetValue("Conteudo").AttemptedValue;
            }

            noticia.Ativo = 1;

            if (!string.IsNullOrEmpty(frmCollection.GetValue("LinkVideo").AttemptedValue))
            {
                noticia.LinkVideo = frmCollection.GetValue("LinkVideo").AttemptedValue;
            }


            if (!string.IsNullOrEmpty(frmCollection.GetValue("AreaSelecionada").AttemptedValue))
            {
                noticia.IdLocalNoticia = Convert.ToInt32(frmCollection.GetValue("AreaSelecionada").AttemptedValue);
            }
            else
            {
                ModelState.AddModelError("ERRO", "Área Inválida");
            }

            if (politicosSelecionados != string.Empty)
            {
                NoticiaPoliticoBO bo = new NoticiaPoliticoBO();
                bo.ExcluirByIdNoticia(noticia.IdNoticia);

                List<string> list = new List<string>();

                list = politicosSelecionados.Split(',').ToList();

                foreach (string item in list)
                {
                    NoticiaPolitico np = new NoticiaPolitico();
                    np.DtInclusao = DateTime.Now;
                    np.IdNoticia = new NoticiaBO().ObterUltimaNoticia().IdNoticia;
                    np.IdPolitico = Convert.ToInt32(item);
                    np.Login = UsersRepository.UsuarioLogado.UsuarioLogin;

                    
                    bo.Incluir(np);
                }
            }

            NoticiaBO noticiaBO = new NoticiaBO();
            noticiaBO.Alterar(noticia);

            NoticiaModel model = new NoticiaModel();
            model.AreaSelecionada = Convert.ToInt32(frmCollection.GetValue("AreaSelecionada").AttemptedValue);

            return View("SelecionarArea", model);
        }

        public string ObterPoliticosByCargo(int idCargo)
        {
            PoliticoBO politicoBO = new PoliticoBO();

            List<Politico> listPolitico = politicoBO.ObterPoliticosAtivosByIdCargo(idCargo);

            JavaScriptSerializer jss = new JavaScriptSerializer();

            return jss.Serialize(listPolitico);
        }

        public string AdicionarPolitico(string politicos)
        {
            if (politicos == "") return string.Empty;

            politicos = politicos.Substring(0, politicos.Length - 1);

            List<string> politicosInclusao = politicos.Split(',').ToList();

            StringBuilder sb = new StringBuilder();

            sb.Append("<table style=\"width: 935px; border-radius: 4px; color: white; background-color: #3081C8; font-size: 15px; font-weight: 500; padding-left: 20px; line-height: 28px; margin-bottom: 5px; margin-top: 20px;\">");
            sb.Append("<tr>");
            sb.Append("<td style=\"padding-left: 10px; width: 80px; text-align: left;\">");
            sb.Append("Nome do Político");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<table id=\"tbPoliticos\" style=\"width: 935px; font-size: 15px; font-weight: 500; padding-left: 20px; line-height: 30px;\">");

            foreach (string p in politicosInclusao)
            {
                Politico item = new PoliticoBO().ObterPoliticoById(int.Parse(p));
                sb.Append("<tr style=\"border-bottom: 1px solid gainsboro;\">");
                sb.Append("<td style=\"width: 100%; text-align: left;\">");
                sb.Append(item.Nome);
                sb.Append("<td>");
                sb.Append("<td style=\"width: 50px; text-align: left;\">");
                sb.Append("<img style=\"margin: 5px; cursor:pointer;\" src=\"../../Content/img/trash1.png\" alt=\"Excluir Notícia\" title=\"Excluir Notícia\" onclick=\"excluirPolitico(" + item.IdPolitico + ");\"/>");
                sb.Append("<td>");
                sb.Append("<tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
    }
}
