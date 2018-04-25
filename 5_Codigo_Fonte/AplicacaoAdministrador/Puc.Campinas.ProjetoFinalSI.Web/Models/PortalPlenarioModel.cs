using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class PortalPlenarioModel : Controller
    {
        public PortalPlenarioModel()
        {
            this.ListaSituacaoSessao = new SituacaoSessaoBO().ObterSituacaoSessao();

            this.DataInicio = null;

            this.DateTermino = null;

            this.ListagemProposicoes = new List<SessaoProposicao>();

            this.ListagemVotacoesPresencas = new List<Votacao>();

        }

        public PortalPlenarioModel(Sessao sessao)
        {
            this.NomeSessao = sessao.Nome;
            this.Descricao = sessao.Descricao;
            this.DataInicio = sessao.DtInicial;
            this.DateTermino = sessao.DtFinal;
            this.IdSituacaoSessao = sessao.IdSituacao;
            this.IdOrador = sessao.IdOrador;
            this.IdSessao = sessao.IdSessao;

            this.Orador = new PoliticoBO().ObterPoliticoById(sessao.IdOrador);

            this.ListagemProposicoes = new SessaoProposicaoBO().ObterSessaoProposicaoByIdSessao(sessao.IdSessao);
            this.ListagemConvocados = new ConvocadoBO().ObterConvocadosByIdSessao(sessao.IdSessao);
            this.ListagemVotacoesPresencas = new List<Votacao>();

            foreach(SessaoProposicao p in this.ListagemProposicoes)
            {
                this.ListagemVotacoesPresencas.AddRange(new VotacaoBO().ObterVotacoesByIdSessaoProposicao(p.IdSessaoProposicao));
            }
        }

        public int IdSessao { get; set; }

        public List<Sessao> ListagemSessoes { get; set; }

        public string NomeSessao { get; set; }

        public List<SituacaoSessao> ListaSituacaoSessao { get; set; }

        public int IdSituacaoSessao { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DateTermino { get; set; }

        public string Descricao { get; set; }

        public int IdOrador { get; set; }

        public Politico Orador { get; set; }

        public List<SessaoProposicao> ListagemProposicoes { get; set; }

        public List<Convocado> ListagemConvocados { get; set; }

        public List<Votacao> ListagemVotacoesPresencas { get; set; }
    }
}
