using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Web.Models;
using Puc.Campinas.ProjetoFinalSI.BO;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO.Mandatos;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class DespesasController : Controller
    {
        public ActionResult Index()
        {
            return View("DespesasPesquisa", new DespesaModel());
        }

        public ActionResult PesquisarPolitico(int idPolitico)
        {
            DespesaModel model = new DespesaModel();

            model.IdMandato = new MandatoBO().ObterMandatosByIdPolitico(idPolitico)[0].IdMandato;
            model.NomePolitico = new PoliticoBO().ObterPoliticoById(idPolitico).Nome;
            model.IdPolitico = idPolitico;

            List<Despesa> listDespesas = new List<Despesa>();

            listDespesas = new DespesaBO().ObterDespesasByIdPolitico(idPolitico);
            model.ListaDespesa = listDespesas;

            return View("DespesasList", model);
        }

        public ActionResult Create(int idPolitico)
        {
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 9).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 9).ToList()[0].PermissaoIncluir)
                {
                    DespesaModel model = new DespesaModel();
                    model.IdPolitico = idPolitico;
                    model.NomePolitico = new PoliticoBO().ObterPoliticoById(idPolitico).Nome;
                    model.IdMandato = new MandatoBO().ObterMandatosByIdPolitico(idPolitico)[0].IdMandato;

                    return View("DespesasCreate", model);
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

        public ActionResult Delete(int idDespesa)
        {
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 9).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 9).ToList()[0].PermissaoExcluir)
                {
                    DespesaBO despesaBO = new DespesaBO();
                    Despesa despesa = despesaBO.ObterDespesaById(idDespesa);

                    int idMandato = despesa.IdMandato;

                    MandatoBO mandatoBO = new MandatoBO();
                    int idPolitico = mandatoBO.ObterMandatoById(idMandato).IdPolitico;

                    string ret = new DespesaBO().Excluir(new Despesa() { IdDespesa = idDespesa });
                    DespesaModel model = new DespesaModel();

                    model.ListaDespesa = new DespesaBO().ObterDespesasByIdPolitico(idPolitico);
                    model.NomePolitico = new PoliticoBO().ObterPoliticoById(idPolitico).Nome;
                    
                    return this.View("DespesasList", model);
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

        public ActionResult Edit(int idDespesa)
        {
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 9).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 9).ToList()[0].PermissaoAlterar)
                {
                    DespesaModel model = new DespesaModel(new DespesaBO().ObterDespesaById(idDespesa));

                    int idPolitico = new MandatoBO().ObterMandatoById(model.IdMandato).IdPolitico;
                    int idPartido = new MandatoBO().ObterMandatoById(model.IdMandato).IdPartido;

                    model.NomePolitico = new PoliticoBO().ObterPoliticoById(idPolitico).Nome;
                    model.NomePartido = new PartidoBO().ObterPartidoById(idPartido).Nome; 
                    return this.View("DespesasEdit", model);

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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 9).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 9).ToList()[0].PermissaoConsultar)
                {
                    DespesaModel model = new DespesaModel(new DespesaBO().ObterDespesaById(id));

                    int idPolitico = new MandatoBO().ObterMandatoById(model.IdMandato).IdPolitico;
                    int idPartido = new MandatoBO().ObterMandatoById(model.IdMandato).IdPartido;

                    model.NomePolitico = new PoliticoBO().ObterPoliticoById(idPolitico).Nome;
                    model.NomePartido = new PartidoBO().ObterPartidoById(idPartido).Nome; 

                    return this.View("DespesasDetails", model);

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
            Despesa despesa = new Despesa();

            despesa.IdDespesa = Convert.ToInt32(formCollection.GetValue("IdDespesa").AttemptedValue);
            despesa.IdMandato = Convert.ToInt32(formCollection.GetValue("IdMandato").AttemptedValue);
            despesa.Descricao = formCollection.GetValue("Descricao").AttemptedValue;
            despesa.IdCategoria = Convert.ToInt32(formCollection.GetValue("IdCategoria").AttemptedValue);
            despesa.NomeFornecedor = formCollection.GetValue("NomeFornecedor").AttemptedValue;
            despesa.CPF_CNPJ_Forn = formCollection.GetValue("CPF_CNPJ_Forn").AttemptedValue;
            despesa.Valor = Convert.ToSingle(Convert.ToDouble(formCollection.GetValue("Valor").AttemptedValue));
            despesa.MesDespesa = Convert.ToInt32(formCollection.GetValue("MesDespesa").AttemptedValue);
            despesa.AnoDespesa = Convert.ToInt32(formCollection.GetValue("AnoDespesa").AttemptedValue);
            despesa.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            despesa.DtInclusao = DateTime.Now;

            if (ModelState.IsValid)
            {
                string ret = new DespesaBO().Incluir(despesa);
            }
            else
            {
                DespesaModel model = new DespesaModel(despesa);
                return this.View("DespesasCreate", model);
            }

            return RedirectToAction("RetornarTelaAnteriorPopulada", new { idMandato = despesa.IdMandato });
        }

        [HttpPost]
        public ActionResult Edit(FormCollection formCollection)
        {
            Despesa despesa = new Despesa();

            despesa.IdDespesa = Convert.ToInt32(formCollection.GetValue("IdDespesa").AttemptedValue);
            despesa.IdMandato = Convert.ToInt32(formCollection.GetValue("IdMandato").AttemptedValue);
            despesa.Descricao = formCollection.GetValue("Descricao").AttemptedValue;
            despesa.IdCategoria = Convert.ToInt32(formCollection.GetValue("IdCategoria").AttemptedValue);
            despesa.NomeFornecedor = formCollection.GetValue("NomeFornecedor").AttemptedValue;
            despesa.CPF_CNPJ_Forn = formCollection.GetValue("CPF_CNPJ_Forn").AttemptedValue;
            despesa.Valor = Convert.ToSingle(Convert.ToDouble(formCollection.GetValue("Valor").AttemptedValue));
            despesa.MesDespesa = Convert.ToInt32(formCollection.GetValue("MesDespesa").AttemptedValue);
            despesa.AnoDespesa = Convert.ToInt32(formCollection.GetValue("AnoDespesa").AttemptedValue);
            despesa.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            despesa.DtInclusao = DateTime.Now;


            if (ModelState.IsValid)
            {
                string ret = new DespesaBO().Alterar(despesa);
            }
            else
            {
                DespesaModel model = new DespesaModel(despesa);
                return this.View("DespesasEdit", model);
            }

            return RedirectToAction("Index");

        }

        public ActionResult RetornarTelaAnteriorPopulada(int idPolitico)
        {
            DespesaModel model = new DespesaModel();

            string nomePolitico = new PoliticoBO().ObterPoliticoById(idPolitico).Nome;
            model.NomePolitico = nomePolitico;

            model.IdPolitico = idPolitico;

            int idPartido = new MandatoBO().ObterMandatosByIdPolitico(idPolitico)[0].IdPartido;
            string nomePartido = new PartidoBO().ObterPartidoById(idPartido).Nome;

            model.IdMandato = new MandatoBO().ObterMandatosByIdPolitico(idPolitico)[0].IdMandato;

            List<Despesa> listDespesas = new List<Despesa>();

            listDespesas = new DespesaBO().ObterDespesasByIdPolitico(idPolitico);

            model.ListaDespesa = listDespesas;

            return View("DespesasList", model);
        }

        public ActionResult PesquisarRegistros(string param)
        {
            DespesaModel model = new DespesaModel();

            if (param != default(string) || !string.IsNullOrEmpty(param))
            {
                model.ListaPoliticos = new PoliticoBO().ObterPoliticos(param);
            }
            else
            {
                model.ListaPoliticos = new PoliticoBO().ObterPoliticos();
            }

            model.FiltroPesquisa = param;

            return View("DespesasPesquisa", model);
        }
    }
    
}