namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Voto
    {
        public int IdVoto { get; set; }

        public string Nome { get; set; }

        public DateTime DtInclusao { get; set; }

        public string Login { get; set; }
    }
}
