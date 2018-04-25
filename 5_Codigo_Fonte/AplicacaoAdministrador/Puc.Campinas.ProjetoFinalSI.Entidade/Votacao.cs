namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Votacao
    {
        public Votacao()
        {
            this.Mandato = new Mandato();
            this.Politico = new Politico();
        }

        public int IdVotacao { get; set; }

        public int IdSessaoProposicao { get; set; }

        public int IdVoto { get; set; }

        public int IdMandato { get; set; }

        public DateTime DtInclusao { get; set; }

        public string Login { get; set; }

        public Mandato Mandato { get; set; }

        public Politico Politico { get; set; }
    }
}
