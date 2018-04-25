namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Comissao
    {
        public int IdComissao { get; set; }

        public string Nome { get; set; }

        public string Sigla { get; set; }

        public string Local { get; set; }

        public string Telefone { get; set; }

        public string Fax { get; set; }

        public string Secretario { get; set; }

        public DateTime DtInclusao { get; set; }

        public string Login { get; set; }
    }
}
