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
    public class MandatosController : Controller
    {
        public ActionResult Index()
        {
            return View("MandatoPesquisa", new MandatoModel());
        }

        public ActionResult PesquisarPolitico(int idPolitico)
        {
            MandatoModel model = new MandatoModel();

            string nomePolitico = new PoliticoBO().ObterPoliticoById(idPolitico).Nome;

            model.NomePolitico = nomePolitico;

            model.IdPolitico = idPolitico;

            List<Mandato> listMandatos = new List<Mandato>();

            listMandatos = new MandatoBO().ObterMandatosByIdPolitico(idPolitico);

            model.ListaMandatos = listMandatos;

            List<Mandato> listaMandatoAux = new List<Mandato>();
            listaMandatoAux = listMandatos.Where(x => x.IsMandatoAtual.Equals(true)).ToList();

            if (listaMandatoAux.Count > 0)
                model.MandatoAtual = listaMandatoAux[0];
            else
                model.MandatoAtual = null;

            return View("MandatoList", model);
        
        }
        
        public ActionResult Create(int idPolitico)
        {
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 8).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 8).ToList()[0].PermissaoIncluir)
                {
                    MandatoModel model = new MandatoModel();
                    model.IdPolitico = idPolitico;

                    return View("MandatoCreate", model);
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
            Mandato mandato = new Mandato();

            mandato.IdPeriodoMandato = Convert.ToInt32(formCollection.GetValue("IdPeriodoMandato").AttemptedValue);
            mandato.IdPolitico = Convert.ToInt32(formCollection.GetValue("IdPolitico").AttemptedValue);
            mandato.IdCargo = Convert.ToInt32(formCollection.GetValue("IdCargo").AttemptedValue);
            mandato.IdPartido = Convert.ToInt32(formCollection.GetValue("IdPartido").AttemptedValue);
            mandato.IdCidade = Convert.ToInt32(formCollection.GetValue("IdCidade").AttemptedValue);
            mandato.IdEstado = Convert.ToInt32(formCollection.GetValue("IdEstado").AttemptedValue);

            if (!string.IsNullOrEmpty(formCollection.GetValue("Gabinete").AttemptedValue))
            {
                mandato.Gabinete = Convert.ToInt32(formCollection.GetValue("Gabinete").AttemptedValue);
            }

            if (!string.IsNullOrEmpty(formCollection.GetValue("Anexo").AttemptedValue))
            {
                mandato.Anexo = formCollection.GetValue("Anexo").AttemptedValue;
            }
            else
            {
                mandato.Anexo = string.Empty;
            }

            if (!string.IsNullOrEmpty(formCollection.GetValue("Telefone").AttemptedValue))
            {
                mandato.Telefone = formCollection.GetValue("Telefone").AttemptedValue;
            }
            else
            {
                mandato.Telefone = string.Empty;
            }

            if (!string.IsNullOrEmpty(formCollection.GetValue("Fax").AttemptedValue))
            {
                mandato.Fax = formCollection.GetValue("Fax").AttemptedValue;
            }
            else
            {
                mandato.Fax = string.Empty;
            }

            mandato.Endereco = Convert.ToString(formCollection.GetValue("Endereco").AttemptedValue);
            mandato.CEP = Convert.ToString(formCollection.GetValue("CEP").AttemptedValue);
            mandato.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            mandato.DtInclusao = DateTime.Now;

            if (!formCollection.GetValue("IsMandatoAtual").AttemptedValue.Contains(','))
            {
                mandato.IsMandatoAtual = Convert.ToBoolean(formCollection.GetValue("IsMandatoAtual").AttemptedValue);
            }
            else
            {
                mandato.IsMandatoAtual = Convert.ToBoolean(formCollection.GetValue("IsMandatoAtual").AttemptedValue.Split(',')[0]);
            }

            if (mandato.IsMandatoAtual == true)
            {
                List<Mandato> listMandatos = new List<Mandato>();
                listMandatos = new MandatoBO().ObterMandatosByIdPolitico(mandato.IdPolitico);
                List<Mandato> aux = new List<Mandato>();

                aux = listMandatos.Where(x => x.IsMandatoAtual == true).ToList();

                if (aux.Count > 0)
                {
                    ModelState.AddModelError("", "Já existe um mandato ativo para este Político.");
                }
            }

            if (ModelState.IsValid)
            {
                string ret = new MandatoBO().Incluir(mandato);
            }
            else
            {
                MandatoModel model = new MandatoModel(mandato);
                return this.View("MandatoCreate", model);
            }

            return RedirectToAction("RetornarTelaAnteriorPopulada", new { idPolitico = mandato.IdPolitico });
        }

        public string ObterCargosPeriodoMandato(int idPeriodoMandato)
        {
            CargoBO cargoBO = new CargoBO();

            List<Cargo> listCargo = cargoBO.ObterCargosByPeriodoMandato(idPeriodoMandato);

            JavaScriptSerializer jss = new JavaScriptSerializer();

            return jss.Serialize(listCargo);
        }

        public string ObterCidadesByIdEstado(int idEstado)
        {
            CidadeBO cidadesBO = new CidadeBO();

            List<Cidade> listaCidades = cidadesBO.ObterCidadeByIdEstado(idEstado);

            JavaScriptSerializer jss = new JavaScriptSerializer();

            return jss.Serialize(listaCidades);
        }

        public ActionResult EncerrarMandato(int idMandato, int idPolitico)
        {
            MandatoBO mandatoBO = new MandatoBO();

            string ret = mandatoBO.EncerrarMandato(idMandato);
            
            MandatoModel model = new MandatoModel();

            string nomePolitico = new PoliticoBO().ObterPoliticoById(idPolitico).Nome;

            model.NomePolitico = nomePolitico;

            model.IdPolitico = idPolitico;

            List<Mandato> listMandatos = new List<Mandato>();

            listMandatos = new MandatoBO().ObterMandatosByIdPolitico(idPolitico);

            model.ListaMandatos = listMandatos;

            List<Mandato> listaMandatoAux = new List<Mandato>();
            listaMandatoAux = listMandatos.Where(x => x.IsMandatoAtual.Equals(true)).ToList();

            if (listaMandatoAux.Count > 0)
                model.MandatoAtual = listaMandatoAux[0];
            else
                model.MandatoAtual = null;

            return View("MandatoList", model);
        }

        public ActionResult RetornarTelaAnteriorPopulada(int idPolitico)
        {
            MandatoModel model = new MandatoModel();

            string nomePolitico = new PoliticoBO().ObterPoliticoById(idPolitico).Nome;

            model.NomePolitico = nomePolitico;

            model.IdPolitico = idPolitico;

            List<Mandato> listMandatos = new List<Mandato>();

            listMandatos = new MandatoBO().ObterMandatosByIdPolitico(idPolitico);

            model.ListaMandatos = listMandatos;

            List<Mandato> listaMandatoAux = new List<Mandato>();
            listaMandatoAux = listMandatos.Where(x => x.IsMandatoAtual.Equals(true)).ToList();

            if (listaMandatoAux.Count > 0)
                model.MandatoAtual = listaMandatoAux[0];
            else
                model.MandatoAtual = null;

            return View("MandatoList", model);
        }

        public ActionResult PesquisarRegistros(string param)
        {
            MandatoModel model = new MandatoModel();

            if (param != default(string) || !string.IsNullOrEmpty(param))
            {
                model.ListaPoliticos = new PoliticoBO().ObterPoliticos(param);
            }
            else
            {
                model.ListaPoliticos = new PoliticoBO().ObterPoliticos();
            }

            model.FiltroPesquisa = param;

            return View("MandatoPesquisa", model);
        }
    }
}
