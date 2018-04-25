using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Web.Models;
using System.Text;
using Puc.Campinas.ProjetoFinalSI.BO;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using System.IO;
using System.Web.Script.Serialization;
using AutenticacaoUsuarioMVC3.Models;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class PortalPoliticosController : Controller
    {
        public ActionResult Index(int? idPolitico = null, string nomePolitico = null)
        {
            PortalPoliticosModel model = new PortalPoliticosModel();

            if (idPolitico != null && idPolitico != 0 && nomePolitico != string.Empty && nomePolitico != null)
            {
                model.ObterBiografiaIDPolitico = Convert.ToInt32(idPolitico);
                model.ObterBiografiaNomePolitico = nomePolitico;
            }

            return View("Politicos", model);
        }

        public ActionResult ObterPoliticosByFiltroUsuario(int? idEstado, int? idCidade, int? idCargo, int? idPartido, int? idPolitico)
        {
            StringBuilder sb = new StringBuilder();

            PoliticoBO politicoBO = new PoliticoBO();

            List<Politico> list = politicoBO.ObterPoliticos(idEstado != null ? Convert.ToString(idEstado) : null,
                                                                                 idCidade != null ? Convert.ToString(idCidade) : null,
                                                                                 idCargo != null ? Convert.ToString(idCargo) : null,
                                                                                 idPartido != null ? Convert.ToString(idPartido) : null,
                                                                                 idPolitico != null ? Convert.ToString(idPolitico) : null);


            if (list.Count > 0)
            {
                return PartialView("_PartialListPolitico", list);
            }
            else
            {
                return new EmptyResult();
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetImage()
        {
            Politico politico = new PoliticoBO().ObterPoliticoById(1);

            byte[] imageArray = (byte[])politico.Foto; // some method for returning the byte-array from db.

            return new FileContentResult(imageArray, "image/jpeg");
        }

        public string ObterPoliticosAtivosByIdCargo(int idCargo)
        {
            PoliticoBO politicoBO = new PoliticoBO();

            List<Politico> politicos = politicoBO.ObterPoliticosAtivosByIdCargo(idCargo);

            JavaScriptSerializer jss = new JavaScriptSerializer();

            return jss.Serialize(politicos);
        }

        public string ObterPoliticosAtivosByCargoPartido(int idEstado, int idCidade, int? idCargo, int? idPartido)
        {
            PoliticoBO politicoBO = new PoliticoBO();

            List<Politico> politicos = politicoBO.ObterPoliticosByCargoPartido(idEstado, idCidade, idCargo, idPartido);

            JavaScriptSerializer jss = new JavaScriptSerializer();

            return jss.Serialize(politicos);
        }

        public string ObterCidadesByIdEstado(int idEstado)
        {
            CidadeBO cidadesBO = new CidadeBO();

            List<Cidade> listaCidades = cidadesBO.ObterCidadeByIdEstado(idEstado);

            JavaScriptSerializer jss = new JavaScriptSerializer();

            return jss.Serialize(listaCidades);
        }

        public PartialViewResult GetBiografiaPolitico(int idPolitico)
        {
            return this.PartialView("_PartialBiografiaPolitico", new BiografiaModel(idPolitico));
        }

        public string ObterNomePoliticoById(int idPolitico)
        {
            return new PoliticoBO().ObterPoliticoById(idPolitico).Nome;
        }

        public void SalvarAcompanhamento(int idPolitico)
        {
            if (UsersRepository.UsuarioLogado != null)
            {
                Acompanhamento ac = new Acompanhamento();

                ac.DtInclusao = DateTime.Now;
                ac.IdUsuario = UsersRepository.UsuarioLogado.IdUsuario;
                ac.IdPolitico = idPolitico;
                ac.Login = UsersRepository.UsuarioLogado.UsuarioLogin;

                AcompanhamentoBO bo = new AcompanhamentoBO();
                string ret = bo.Incluir(ac);
            }
        }

        public void ExcluirAcompanhamento(int idPolitico)
        {
            if (UsersRepository.UsuarioLogado != null)
            {
                AcompanhamentoBO bo = new AcompanhamentoBO();
                string ret = bo.Excluir(idPolitico, UsersRepository.UsuarioLogado.IdUsuario);
            }
        }

        public void EditAcompanhamento(int idPolitico, string idCheck, bool isChecked)
        {
            if (UsersRepository.UsuarioLogado != null)
            {
                Acompanhamento ac = new Acompanhamento();

                ac = new AcompanhamentoBO().ObterAcompanhamentosByIdUsuario(UsersRepository.UsuarioLogado.IdUsuario).Where(x => x.IdPolitico == idPolitico).ToList()[0];

                if (idCheck.Contains("IsNoticia"))
                {
                    ac.IsNoticia = isChecked ? 1 : 0;
                }
                else if (idCheck.Contains("IsDespesa"))
                {
                    ac.IsDespesa = isChecked ? 1 : 0;
                }
                else if (idCheck.Contains("IsProjetoLei"))
                {
                    ac.IsProposicao = isChecked ? 1 : 0;
                }

                AcompanhamentoBO bo = new AcompanhamentoBO();
                string ret = bo.Alterar(ac);
            }
        }

        public string LoadSelections()
        {
            if (UsersRepository.UsuarioLogado != null)
            {
                List<Acompanhamento> ac = new List<Acompanhamento>();

                ac = new AcompanhamentoBO().ObterAcompanhamentosByIdUsuario(UsersRepository.UsuarioLogado.IdUsuario);

                string chkValues = "";

                foreach (Acompanhamento item in ac)
                {
                    if (item.IsNoticia == 1)
                    {
                        chkValues += "IsNoticia" + item.IdPolitico + "-";
                    }

                    if (item.IsDespesa == 1)
                    {
                        chkValues += "IsDespesa" + item.IdPolitico + "-";
                    }

                    if (item.IsProposicao == 1)
                    {
                        chkValues += "IsProjetoLei" + item.IdPolitico + "-";
                    }
                }

                return chkValues;
            }
            else
            {
                return string.Empty;
            }
        }

        public string VerificarPoliticosAcompanhadosByUser()
        {
            if (UsersRepository.UsuarioLogado != null)
            {
                List<Acompanhamento> ac = new List<Acompanhamento>();

                ac = new AcompanhamentoBO().ObterAcompanhamentosByIdUsuario(UsersRepository.UsuarioLogado.IdUsuario);

                string listPoliticos = "";

                foreach (Acompanhamento item in ac)
                {
                    listPoliticos += item.IdPolitico + "-" + new PoliticoBO().ObterPoliticoById(item.IdPolitico).Nome + ",";
                }

                return listPoliticos;
            }
            else
            {
                return string.Empty;
            }
        }

        public JsonResult ObterPoliticosMandatosAtivosByStringNome(string searchTerm, int maxResults)
        {
            PoliticoBO bo = new PoliticoBO();

            return this.Json(bo.ObterPoliticos(searchTerm).Take(maxResults), JsonRequestBehavior.AllowGet);
        }
    }
}
