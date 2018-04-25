using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class EmailModel
    {
        public EmailModel()
        {
            this.listaAcompanhamentos = new List<Acompanhamento>();
            this.listaDespesasMarina = new List<Despesa>();
        }

        public EmailModel(Acompanhamento acompanhamento)
        {
            this.IdAcompanhamento = acompanhamento.IdAcompanhamento;
            this.IdUsuario = acompanhamento.IdUsuario;
            this.IdPolitico = acompanhamento.IdPolitico;
            this.IsNoticia = acompanhamento.IsNoticia;
            this.IsDespesa = acompanhamento.IsDespesa;
            this.IsProposicao = acompanhamento.IsProposicao;
            this.Login = acompanhamento.Login;
            this.DtInclusao = acompanhamento.DtInclusao;
            this.nomePolitico = acompanhamento.NomePolitico;
        }

        public int IdAcompanhamento { get; set; }

        public int IdUsuario { get; set; }

        public int IdPolitico { get; set; }

        public int IsNoticia { get; set; }

        public int IsDespesa { get; set; }

        public int IsProposicao { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }

        public List<Acompanhamento> listaAcompanhamentos {get; set; }

        public List<Despesa> listaDespesasAcelino { get; set; }

        public List<Despesa> listaDespesasMarina { get; set; }

        public List<Despesa> listaProposicoes { get; set; }

        public string nomePolitico { get; set; }
    }
}
