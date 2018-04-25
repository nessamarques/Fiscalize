namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Proposicao
    {
        public Proposicao()
        {
            this.Tipo = new TipoProposicao();
            this.Mandato = new Mandato();
            this.Politico = new Politico();
        }

        public int IdProposicao { get; set; }

        public int IdTipo { get; set; }

        public int IdMandato { get; set; }
        
        public string Numero { get; set; }

        public string Ano { get; set; }

        public string Situacao { get; set; }

        public DateTime DtApresentacao { get; set; }

        public string Ementa { get; set; }

        public DateTime DtInclusao { get; set; }

        public string Login { get; set; }

        public TipoProposicao Tipo { get; set; }

        public Mandato Mandato { get; set; }

        public Politico Politico { get; set; }
    }
}
