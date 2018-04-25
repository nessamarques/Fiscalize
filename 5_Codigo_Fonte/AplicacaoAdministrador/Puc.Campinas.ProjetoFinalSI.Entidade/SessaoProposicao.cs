namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SessaoProposicao
    {
        public SessaoProposicao()
        {
            this.Proposicao = new Proposicao();
        }

        public int IdSessaoProposicao { get; set; }

        public int IdSessao { get; set; }

        public int IdProposicao { get; set; }

        public DateTime DtInclusao { get; set; }

        public string Login { get; set; }

        public Proposicao Proposicao { get; set; }
    }
}
