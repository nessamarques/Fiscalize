namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Sessao
    {
        public int IdSessao { get; set; }

        public DateTime DtInicial { get; set; }

        public DateTime DtFinal { get; set; }
             
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public int IdSituacao { get; set; }

        public string Orador { get; set; }

        public int IdOrador { get; set; }

        public string Pauta { get; set; } //arquivo em pdf.

        public DateTime DtInclusao { get; set; }

        public string Login { get; set; }
    }
}
