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
    public class AtividadeModel
    {
        public AtividadeModel()
        {
            this.ListaAtividades = new List<Atividade>();
            this.FiltroPesquisa = string.Empty;

            this.ListaPoliticos = new PoliticoBO().ObterPoliticos();
            this.ListaSituacoes = new SituacaoBO().ObterSituacao();

            this.DtInicio = null; 
            this.DtFim = null; 
        }

        public AtividadeModel(Atividade atividade)
        {
            this.IdAtividade = atividade.IdAtividade;
            this.IdPolitico = atividade.IdPolitico;
            this.NomeComissao = atividade.NomeComissao;
            this.IdSituacao = atividade.IdSituacao;
            this.NomeSituacao = atividade.NomeSituacao;
            this.DtInicio = atividade.DtInicio;
            this.DtFim = atividade.DtFim;
            this.Login = atividade.Login;
            this.DtInclusao = atividade.DtInclusao;
            
            this.FiltroPesquisa = string.Empty;

            this.ListaAtividades = new List<Atividade>();

            this.ListaPoliticos = new PoliticoBO().ObterPoliticos();
            this.ListaSituacoes = new SituacaoBO().ObterSituacao();
        }

        public int IdAtividade { get; set; }

        [Required(ErrorMessage = "Campo Politico é obrigatório")]
        public int IdPolitico { get; set; }

        [Required(ErrorMessage = "Campo Nome de Comissão é obrigatório")]
        public string NomeComissao { get; set; }

        [Required(ErrorMessage = "Campo Situação é obrigatório")]
        public int IdSituacao { get; set; }

        public string NomeSituacao { get; set; }

        //[RegularExpression("(0?[1-9]|[12][0-9]|3[01])[/ -](0?[1-9]|1[12])[/ -](19[0-9]{2}|[2][0-9][0-9]{2})", ErrorMessage = "Data inicial Inválida")]
        [RegularExpression("(0?[1-9]|1?[0-9]|2?[0-9]|3?[0-1])[/ -](0?[1-9]|1?[0-2])[/ -](19[0-9]{2}|[2][0-9]{3})", ErrorMessage = "Data inicial Inválida")]
        [Required(ErrorMessage = "Campo Data inicial da atividade é obrigatório")]
        public DateTime? DtInicio { get; set; }

        //[RegularExpression("(0?[1-9]|[12][0-9]|3[01])[/ -](0?[1-9]|1[12])[/ -](19[0-9]{2}|[2][0-9][0-9]{2})", ErrorMessage = "Data final da atividade Inválida")]
        [RegularExpression("(0?[1-9]|1?[0-9]|2?[0-9]|3?[0-1])[/ -](0?[1-9]|1?[0-2])[/ -](19[0-9]{2}|[2][0-9]{3})", ErrorMessage = "Data final da atividade Inválida")]
        //[Required(ErrorMessage = "Campo Data final da atividade é obrigatório")]
        public DateTime? DtFim { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }

        public List<Atividade> ListaAtividades { get; set; }

        public string ActionRequired { get; set; }

        public string FiltroPesquisa { get; set; }

        public List<Politico> ListaPoliticos { get; set; }

        public List<Situacao> ListaSituacoes { get; set; }

        public string NomePolitico { get; set; }

        public Mandato MandatoAtual { get; set; }

        public bool IsMandatoAtual { get; set; }
    }
}
