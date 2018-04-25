namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class LocalNoticia
    {
        public int IdLocalNoticia { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public DateTime DtInclusao { get; set; }

        public string Login { get; set; }
    }
}
