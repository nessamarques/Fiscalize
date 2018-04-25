namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Filiacao
    {
        public int IdFiliacao { get; set; }

        public int IdPolitico { get; set; }

        public string NomeMae { get; set; }

        public string NomePai { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }
    }
}
