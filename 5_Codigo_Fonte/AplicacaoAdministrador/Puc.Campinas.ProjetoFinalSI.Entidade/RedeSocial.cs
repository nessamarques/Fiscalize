namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class RedeSocial
    {
        public int IdRedeSocial { get; set; }

        public int IdTipoRedeSocial { get; set; }

        public int IdPolitico { get; set; }

        public string Endereco { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }
    }
}
