namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NoticiaPolitico
    {
        public int IdNoticiaPolitico { get; set; }

        public int IdNoticia { get; set; }

        public int IdPolitico { get; set; }

        public DateTime DtInclusao { get; set; }

        public string Login { get; set; }
    }
}
