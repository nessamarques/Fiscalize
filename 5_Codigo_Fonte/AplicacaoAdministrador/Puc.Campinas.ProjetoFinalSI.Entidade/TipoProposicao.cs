namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TipoProposicao
    {
        public int IdTipoProposicao { get; set; }

        public string Nome { get; set; }

        public string Sigla { get; set; }

        public DateTime DtInclusao { get; set; }

        public string Login { get; set; }
    }
}
