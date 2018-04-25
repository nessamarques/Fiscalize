using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class MandatoModel
    {
        public MandatoModel(Mandato mandato)
        {
            this.IdCargo = mandato.IdCargo;
            this.IdPartido = mandato.IdPartido;
            this.IdPolitico = mandato.IdPolitico;
            this.IdPeriodoMandato = mandato.IdPeriodoMandato;

            this.ListaMandatos = new List<Mandato>();
            this.ListaCargos = new List<Cargo>();
            this.ListaPoliticos = new PoliticoBO().ObterPoliticos();
            this.ListaPartido = new PartidoBO().ObterPartidos();
            this.ListaPeriodoMandato = new PeriodoMandatoBO().ObterPeriodosMandatos();

            this.ListEstado = new EstadoBO().ObterEstado();
            this.ListCidade = new CidadeBO().ObterCidadeByIdEstado(mandato.IdEstado);
        }

        public MandatoModel()
        {
            this.ListaMandatos = new List<Mandato>();
            this.ListaCargos = new List<Cargo>();
            this.ListaPoliticos = new PoliticoBO().ObterPoliticos();
            this.ListaPartido = new PartidoBO().ObterPartidos();
            this.ListaPeriodoMandato = new PeriodoMandatoBO().ObterPeriodosMandatos();

            this.ListEstado = new EstadoBO().ObterEstado();
            this.ListCidade = new List<Cidade>();

            this.Gabinete = null;
        }

        public int IdMandato { get; set; }

        [Required(ErrorMessage = "Campo Período de Mandato é obrigatório")]
        public int IdPeriodoMandato { get; set; }

        [Required(ErrorMessage = "Campo Político é obrigatório")]
        public int IdPolitico { get; set; }

        [Required(ErrorMessage="Campo Cargo é obrigatório")]
        public int IdCargo { get; set; }

        [Required(ErrorMessage = "Campo Partido é obrigatório")]
        public int IdPartido{ get; set; }

        [Required(ErrorMessage = "Campo Cidade é obrigatório")]
        public int IdCidade { get; set; }

        [Required(ErrorMessage = "Campo Estado é obrigatório")]
        public int IdEstado { get; set; }

        public int? Gabinete { get; set; }

        public string Anexo { get; set; }

        public string Telefone { get; set; }

        public string Fax { get; set; }

        public string Endereco { get; set; }

        public string CEP { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }

        public bool IsMandatoAtual { get; set; }

        public List<Mandato> ListaMandatos { get; set; }

        public List<Cargo> ListaCargos { get; set; }

        public List<Politico> ListaPoliticos { get; set; }

        public List<PeriodoMandato> ListaPeriodoMandato { get; set; }

        public List<Partido> ListaPartido { get; set; }

        public string NomePolitico { get; set; }

        public Mandato MandatoAtual { get; set; }

        public string FiltroPesquisa { get; set; }

        public List<Cidade> ListCidade { get; set; }

        public List<Estado> ListEstado { get; set; }
    }
}