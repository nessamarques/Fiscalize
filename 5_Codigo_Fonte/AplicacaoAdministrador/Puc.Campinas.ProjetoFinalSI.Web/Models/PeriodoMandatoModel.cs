using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using System.ComponentModel.DataAnnotations;
using Puc.Campinas.ProjetoFinalSI.BO;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class PeriodoMandatoModel
    {
        public PeriodoMandatoModel(PeriodoMandato periodoMandato)
        {
            this.DtInicio = periodoMandato.DtInicio;
            this.DtFim = periodoMandato.DtFim;
            this.Situacao = periodoMandato.Situacao;
            this.IdPeriodoMandato = periodoMandato.IdPeriodoMandato;
            this.Login = periodoMandato.Login;
            this.DtInclusao = periodoMandato.DtInclusao;

            this.ListCargos = new List<CargoMandato>();
            this.ListaPeriodosMandatos = new List<PeriodoMandato>();
            this.FiltroPesquisa = string.Empty;
            
            this.ListaCargosSelecionar = new CargoBO().ObterCargos();
        }
        
        public PeriodoMandatoModel()
        {
            this.ListCargos = new List<CargoMandato>();
            this.ListaPeriodosMandatos = new List<PeriodoMandato>();
            this.FiltroPesquisa = string.Empty;

            this.DtInicio = null;
            this.DtFim = null;

            this.ListaCargosSelecionar = new CargoBO().ObterCargos();
        }

        public int IdPeriodoMandato { get; set; }

        public int IdCargoMandato { get; set; }

        [Required(ErrorMessage = "Campo Situacao é obrigatório")]
        public string Situacao { get; set; }

        [Required(ErrorMessage = "Campo Data de Início é obrigatório")]
        //[RegularExpression("(0?[1-9]|[12][0-9]|3[01])[/ -](0?[1-9]|1[12])[/ -](19[0-9]{2}|[2][0-9][0-9]{2})", ErrorMessage = "Data de Início Inválida")]
        [RegularExpression("(0?[1-9]|1?[0-9]|2?[0-9]|3?[0-1])[/ -](0?[1-9]|1?[0-2])[/ -](19[0-9]{2}|[2][0-9]{3})", ErrorMessage = "Data de Início Inválida")]
        public DateTime? DtInicio { get; set; }

        [Required(ErrorMessage = "Campo Data de Término é obrigatório")]
        //[RegularExpression("(0?[1-9]|[12][0-9]|3[01])[/ -](0?[1-9]|1[12])[/ -](19[0-9]{2}|[2][0-9][0-9]{2})", ErrorMessage = "Data de Término Inválida")]
        [RegularExpression("(0?[1-9]|1?[0-9]|2?[0-9]|3?[0-1])[/ -](0?[1-9]|1?[0-2])[/ -](19[0-9]{2}|[2][0-9]{3})", ErrorMessage = "Data de Término Inválida")]
        public DateTime? DtFim { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }

        public List<CargoMandato> ListCargos { get; set; }

        public string FiltroPesquisa { get; set; }

        public List<PeriodoMandato> ListaPeriodosMandatos { get; set; }

        public List<Cargo> ListaCargosSelecionar { get; set; }

        [Required(ErrorMessage = "É necessário escolher os Cargos pra este Período de Mandato")]
        public int IdCargo { get; set; }

        public string ElementosSelecionadosListaCargos { get; set; }

    }
}