namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class MembroComissao
    {
        public int IdMembroComissao { get; set; }

        public int IdPolitico { get; set; }

        public int IdCargoComissao { get; set; }

        public int IdComissao { get; set; }

        public DateTime DtInclusao { get; set; }

        public string Login { get; set; }

        public string NomePolitico { get; set; }

        public string NomeComissao { get; set; }

        public string SiglaComissao { get; set; }

        public string NomeCargoComissao { get; set; }

        public string Partido { get; set; }

        public string NomeCargo { get; set; }
    }
}
