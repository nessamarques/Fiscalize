namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Atividade
    {
        public int IdAtividade { get; set; }

        public int IdPolitico { get; set; }

        public string NomeComissao { get; set; }

        public int IdSituacao { get; set; }

        public string NomeSituacao { get; set; }

        public DateTime DtInicio { get; set; }

        public DateTime DtFim { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }
    }
}
