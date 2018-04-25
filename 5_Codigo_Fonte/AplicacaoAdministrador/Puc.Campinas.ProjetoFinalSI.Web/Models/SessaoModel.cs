using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using System.ComponentModel.DataAnnotations;
using Puc.Campinas.ProjetoFinalSI.BO;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class SessaoModel
    {
        public SessaoModel()
        {
            this.ListaSessoes = new List<Sessao>();
            this.FiltroPesquisa = string.Empty;
            this.DtFinal = null;
            this.DtInicial = null;
            this.ListaSituacaoSessao = new SituacaoSessaoBO().ObterSituacaoSessao();//SituacaoBO().ObterSituacao();
            this.ListaPolitico = new PoliticoBO().ObterPoliticos();
        }

        public SessaoModel(Sessao sessao)
        {
            this.ListaSessoes = new List<Sessao>();
            this.ListaSituacaoSessao = new SituacaoSessaoBO().ObterSituacaoSessao();//SituacaoBO().ObterSituacao();
            this.ListaPolitico = new PoliticoBO().ObterPoliticos();

            this.IdSessao = sessao.IdSessao;
            this.DtInicial = sessao.DtInicial;
            this.DtFinal = sessao.DtFinal;
            this.Nome = sessao.Nome;
            this.Descricao = sessao.Descricao;
            this.IdSituacao = sessao.IdSituacao;
            this.Orador = sessao.Orador;
            this.IdOrador = sessao.IdOrador;
            this.Pauta = sessao.Pauta;
            
            //TODO Pauta.
        }

        public int IdSessao { get; set; }

        [Required(ErrorMessage = "Campo Data Inicial é obrigatório")]
        [RegularExpression("(0?[1-9]|1?[0-9]|2?[0-9]|3?[0-1])[/ -](0?[1-9]|1?[0-2])[/ -](19[0-9]{2}|[2][0-9]{3})", ErrorMessage = "Data Inicial inválida")]
        public DateTime? DtInicial { get; set; }

        [Required(ErrorMessage = "Campo Data Final é obrigatório")]
        [RegularExpression("(0?[1-9]|1?[0-9]|2?[0-9]|3?[0-1])[/ -](0?[1-9]|1?[0-2])[/ -](19[0-9]{2}|[2][0-9]{3})", ErrorMessage = "Data Final inválida")]
        public DateTime? DtFinal { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Descrição é obrigatório")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo Situação é obrigatório")]
        public int IdSituacao { get; set; }

        [Required(ErrorMessage = "Campo Orador é obrigatório")]
        public string Orador { get; set; }

        [Required(ErrorMessage = "Campo Orador é obrigatório")]
        public int IdOrador { get; set; }

        [Required(ErrorMessage = "Campo Pauta é obrigatório")]
        public string Pauta { get; set; } //arquivo em pdf.

        public List<Sessao> ListaSessoes { get; set; }

        public string FiltroPesquisa { get; set; }

        public List<SituacaoSessao> ListaSituacaoSessao { get; set; }

        public List<Politico> ListaPolitico { get; set; }
    }
}