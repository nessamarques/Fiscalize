using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class DetalhesPartidoModel
    {

        public DetalhesPartidoModel(DetalhesPartido detalhesPartido)
        {
            Partido partido = new PartidoBO().ObterPartidoById(detalhesPartido.IdPartido);
            Cidade cidade = new CidadeBO().ObterCidadeById(detalhesPartido.IdCidade);
            Estado estado = new EstadoBO().ObterEstadoById(detalhesPartido.IdEstado);

            this.NomePartido = partido.Nome;
            this.SiglaPartido = partido.Sigla;
            this.DeferimentoPartido = partido.Deferimento;
            this.PresidenteNacional = partido.PresidenteNacional;
            this.NroPartido = partido.NroPartido;
            this.Cidade = cidade.Nome;
            this.Estado = estado.Sigla;
        }

        public int IdDetalhesPartido { get; set; }

        public int IdPartido { get; set; }

        public string Endereco { get; set; }

        public int IdCidade { get; set; }

        public int IdEstado { get; set; }

        public string CEP { get; set; }

        public string Telefone { get; set; }

        public string Fax { get; set; }

        public string Site { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }

        public string NomePartido { get; set; }

        public string SiglaPartido { get; set; }

        public DateTime DeferimentoPartido { get; set; }

        public string PresidenteNacional { get; set; }

        public int NroPartido { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }
    }
}