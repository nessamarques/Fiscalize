using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;
using System.ComponentModel.DataAnnotations;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class VotacaoModel
    {
        public VotacaoModel()
        {
            this.ListVotacao = new List<Votacao>();
            this.ListVoto = new VotoBO().ObterVoto();
            //this.ListPolitico = new PoliticoBO().ObterPoliticos();
            //this.ListPartido = new PartidoBO().ObterPartidos();
            this.ListaSessaoProposicao = new SessaoProposicaoBO().ObterSessaoProposicao();
        }

        public VotacaoModel(Votacao voto)
        {
            this.ListVotacao = new List<Votacao>();
            this.ListVoto = new VotoBO().ObterVoto();
            //this.ListPolitico = new PoliticoBO().ObterPoliticos();
            //this.ListPartido = new PartidoBO().ObterPartidos();
            this.ListaSessaoProposicao = new SessaoProposicaoBO().ObterSessaoProposicao();

            this.IdVotacao = voto.IdVotacao;
            this.IdSessaoProposicao = voto.IdSessaoProposicao;
            this.IdVoto = voto.IdVoto;
            this.IdMandato = voto.IdMandato;
        
        }

        public string FiltroPesquisa { get; set; }

        public List<SessaoProposicao> ListaSessaoProposicao { get; set; }
        
        public List<Votacao> ListVotacao { get; set; }

        public List<Voto> ListVoto { get; set; }

        public List<Politico> ListPolitico { get; set; }

        public List<Partido> ListPartido { get; set; }

        //[Required(ErrorMessage="O Campo Sessão é Obrigatório")]
        public int IdSessaoProposicao { get; set; }

        public int IdVotacao { get; set; }

        [Required(ErrorMessage = "O Campo Voto é Obrigatório")]
        public int IdVoto { get; set; }

        public int IdMandato { get; set; }
    }
}