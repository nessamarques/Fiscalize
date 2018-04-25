namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Profissao
    {
        public int IdProfissao { get; set; }

        public int IdPolitico { get; set; }

        public string Nome { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }
    }
}
