using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Web.Models;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;
using System.Text;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class PeriodoMandatoController : Controller
    {
        public ActionResult Index()
        {
            PeriodoMandatoModel periodoMandatoModel = new PeriodoMandatoModel();

            periodoMandatoModel.ListaPeriodosMandatos = new PeriodoMandatoBO().ObterPeriodosMandatos();

            return View("PeriodoMandatoList", periodoMandatoModel);
        }

        public ActionResult Create()
        {
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 6).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 6).ToList()[0].PermissaoIncluir)
                {
                    return this.View("PeriodoMandatoCreate", new PeriodoMandatoModel());
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
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 6).ToList().Count > 0)
            {
                PeriodoMandatoModel periodoMandatoModel = new PeriodoMandatoModel();

                periodoMandatoModel.ListaPeriodosMandatos = new PeriodoMandatoBO().ObterPeriodosMandatos();

                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 6).ToList()[0].PermissaoExcluir)
                {
                    if(!new PeriodoMandatoBO().VerificaSePeriodoMandatoEstaSendoUsado(id))
                    {
                        string ret = new PeriodoMandatoBO().Excluir(new PeriodoMandato() { IdPeriodoMandato = id });
                        return View("PeriodoMandatoList", periodoMandatoModel);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Este Período de Mandato já está sendo utilizado. Não é permitido excluir.");
                        return this.View("PeriodoMandatoList", periodoMandatoModel);
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

        public ActionResult Details(int id)
        {
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 6).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 6).ToList()[0].PermissaoConsultar)
                {
                    PeriodoMandato periodoMandato = new PeriodoMandato();
                    periodoMandato = new PeriodoMandatoBO().ObterPeriodosMandatosById(id);
                    PeriodoMandatoModel periodoMandatoModel = new PeriodoMandatoModel(periodoMandato);

                    foreach (CargoMandato cargoMandato in periodoMandato.ListCargoMandatos)
                    {
                        periodoMandatoModel.ElementosSelecionadosListaCargos += cargoMandato.IdCargo.ToString() + ",";
                    }

                    if (periodoMandato.Situacao == "1")
                        periodoMandatoModel.Situacao = "Ativo";
                    else
                        periodoMandatoModel.Situacao = "Inativo";

                    return this.View("PeriodoMandatoDetails", periodoMandatoModel);
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
            PeriodoMandato periodoMandato = new PeriodoMandato();

            periodoMandato.DtInicio = Convert.ToDateTime(formCollection.GetValue("DtInicio").AttemptedValue);
            periodoMandato.DtFim = Convert.ToDateTime(formCollection.GetValue("DtFim").AttemptedValue);

            if (periodoMandato.DtFim < periodoMandato.DtInicio)
            {
                ModelState.AddModelError("", "Data de Término menor que Data de Início");
            }

            if (formCollection.GetValue("hdnCargosSelecionados").AttemptedValue != string.Empty)
            {
                List<Cargo> listagemCargos = new List<Cargo>();

                listagemCargos = new CargoBO().ObterCargos();

                listagemCargos = listagemCargos.Where(x => formCollection.GetValue("hdnCargosSelecionados").AttemptedValue.Contains(x.IdCargo.ToString() + ",")).ToList();

                List<CargoMandato> listaCargoMandato = new List<CargoMandato>();

                foreach (Cargo c in listagemCargos)
                {
                    CargoMandato cargoMandato = new CargoMandato();
                    cargoMandato.IdCargo = c.IdCargo;
                    cargoMandato.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
                    cargoMandato.DtInclusao = DateTime.Now;
                    listaCargoMandato.Add(cargoMandato);
                    cargoMandato = null;
                }

                periodoMandato.ListCargoMandatos = listaCargoMandato;
            }
            else
            {
                ModelState.AddModelError("", "É necessário selecionar um Cargo");
            }


            periodoMandato.Situacao = Convert.ToString(formCollection.GetValue("Situacao").AttemptedValue);
            periodoMandato.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
            periodoMandato.DtInclusao = DateTime.Now;

            PeriodoMandatoModel model = new PeriodoMandatoModel(periodoMandato);

            if (ModelState.IsValid)
            {
                string ret = new PeriodoMandatoBO().Incluir(periodoMandato);
            }
            else
            {
                model.ElementosSelecionadosListaCargos = formCollection.GetValue("hdnCargosSelecionados").AttemptedValue;
                return this.View("PeriodoMandatoCreate", model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 6).ToList().Count > 0)
            {
                if (AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.Grupo.ListaPermissao.Where(x => x.IdFuncionalidade == 6).ToList()[0].PermissaoAlterar)
                {
                    PeriodoMandato periodoMandato = new PeriodoMandato();

                    periodoMandato = new PeriodoMandatoBO().ObterPeriodosMandatosById(id);

                    PeriodoMandatoModel periodoMandatoModel = new PeriodoMandatoModel(periodoMandato);

                    foreach (CargoMandato cargoMandato in periodoMandato.ListCargoMandatos)
                    {
                        periodoMandatoModel.ElementosSelecionadosListaCargos += cargoMandato.IdCargo.ToString() + ",";
                    }

                    if (periodoMandato.Situacao == "1")
                        periodoMandatoModel.Situacao = "Ativo";
                    else
                        periodoMandatoModel.Situacao = "Inativo";

                    return this.View("PeriodoMandatoEdit", periodoMandatoModel);
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
        public ActionResult Edit(FormCollection formCollection)
        {
            PeriodoMandato periodoMandato = new PeriodoMandato();

            periodoMandato.DtInicio = Convert.ToDateTime(formCollection.GetValue("DtInicio").AttemptedValue);
            periodoMandato.DtFim = Convert.ToDateTime(formCollection.GetValue("DtFim").AttemptedValue);
            periodoMandato.IdPeriodoMandato = Convert.ToInt32(formCollection.GetValue("IdPeriodoMandato").AttemptedValue);
            periodoMandato.Login = Convert.ToString(formCollection.GetValue("Login").AttemptedValue);
            periodoMandato.DtInclusao = Convert.ToDateTime(formCollection.GetValue("DtInclusao").AttemptedValue);

            if (periodoMandato.DtFim < periodoMandato.DtInicio)
            {
                ModelState.AddModelError("", "Data de Término menor que Data de Início");
            }

            if (formCollection.GetValue("hdnCargosSelecionados").AttemptedValue != string.Empty)
            {
                List<Cargo> listagemCargos = new List<Cargo>();

                listagemCargos = new CargoBO().ObterCargos();

                listagemCargos = listagemCargos.Where(x => formCollection.GetValue("hdnCargosSelecionados").AttemptedValue.Contains(x.IdCargo.ToString() + ",")).ToList();

                List<CargoMandato> listaCargoMandato = new List<CargoMandato>();

                foreach (Cargo c in listagemCargos)
                {
                    CargoMandato cargoMandato = new CargoMandato();
                    cargoMandato.IdCargo = c.IdCargo;
                    cargoMandato.Login = AutenticacaoUsuarioMVC3.Models.UsersRepository.UsuarioLogado.UsuarioLogin;
                    cargoMandato.DtInclusao = DateTime.Now;
                    listaCargoMandato.Add(cargoMandato);
                    cargoMandato = null;
                }

                periodoMandato.ListCargoMandatos = listaCargoMandato;
            }
            else
            {
                ModelState.AddModelError("", "É necessário selecionar um Cargo");
            }


            periodoMandato.Situacao = Convert.ToString(formCollection.GetValue("Situacao").AttemptedValue);

            PeriodoMandatoModel model = new PeriodoMandatoModel(periodoMandato);

            if (ModelState.IsValid)
            {
                string ret = new PeriodoMandatoBO().Alterar(periodoMandato);
            }
            else
            {
                model.ElementosSelecionadosListaCargos = formCollection.GetValue("hdnCargosSelecionados").AttemptedValue;
                return this.View("PeriodoMandatoEdit", model);
            }

            return RedirectToAction("Index");
        }

        public string IncluirCargoListagem(string cargosSelecionados)
        {
            List<Cargo> listagemCargos = new List<Cargo>();

            listagemCargos = new CargoBO().ObterCargos();

            listagemCargos = listagemCargos.Where(x => cargosSelecionados.Contains(x.IdCargo.ToString() + ",")).ToList();

            StringBuilder sb = new StringBuilder();

            foreach (Cargo item in listagemCargos)
            {
                sb.Append("<tr>");
                sb.Append("<td>" + item.Nome + "</td>");
                sb.Append("<td><a href=\"#\" class=\"ask\" onclick=\"excluirRegistroCargo(" + item.IdCargo.ToString() + ")\"><img src=\"../../Content/img/delete.png\" alt=\"\" title=\"\" border=\"0\" /></a></td>");
                sb.Append("</tr>");
            }

            return sb.ToString();
        }

        public string IncluirCargoListagemDetails(string cargosSelecionados)
        {
            List<Cargo> listagemCargos = new List<Cargo>();

            listagemCargos = new CargoBO().ObterCargos();

            listagemCargos = listagemCargos.Where(x => cargosSelecionados.Contains(x.IdCargo.ToString() + ",")).ToList();

            StringBuilder sb = new StringBuilder();

            foreach (Cargo item in listagemCargos)
            {
                sb.Append("<tr>");
                sb.Append("<td>" + item.Nome + "</td>");
                sb.Append("<td>&nbsp;</td>");
                sb.Append("</tr>");
            }

            return sb.ToString();
        }
    }
}
