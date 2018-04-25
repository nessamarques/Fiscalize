using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;
using System.ComponentModel.DataAnnotations;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class ProposicaoModel
    {
        public ProposicaoModel(Proposicao proposicao)
        {
            this.IdProposicao = proposicao.IdProposicao;
            this.IdTipo = proposicao.IdProposicao;
            this.IdMandato = proposicao.IdMandato;
            this.Numero = proposicao.Numero;
            this.Ano = proposicao.Ano;
            this.Situacao = proposicao.Situacao;
            this.DtApresentacao = proposicao.DtApresentacao;
            this.Ementa = proposicao.Ementa;

            this.ListaPoliticos = new PoliticoBO().ObterPoliticos();
            this.ListaProposicao = new List<Proposicao>();
            this.ListTipo = new TipoProposicaoBO().ObterTipoProposicao();
            
        }

        public ProposicaoModel()
        {
            this.ListaProposicao = new List<Proposicao>();
            this.FiltroPesquisa = string.Empty;
            this.ListaPoliticos = new PoliticoBO().ObterPoliticos();
            this.ListTipo = new TipoProposicaoBO().ObterTipoProposicao();
            this.DtApresentacao = null;


        }

        public int IdProposicao { get; set; }

        public List<Politico> ListPolitico { get; set; }

        public List<Proposicao> ListaProposicao { get; set; }

        public int IdTipo { get; set; }

        public int IdMandato { get; set; }

        public string Numero { get; set; }

        public string Ano { get; set; }

        public string Situacao { get; set; }

        public DateTime? DtApresentacao { get; set; }

        public string Ementa { get; set; }

        public string FiltroPesquisa { get; set; }

        public List<Politico> ListaPoliticos { get; set; }

        public string NomePolitico { get; set; }

        public List<TipoProposicao> ListTipo { get; set; }

        public int IdPartido { get; set; }

        public string NomePartido { get; set; }


    }
}