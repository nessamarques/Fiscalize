using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    public class Acompanhamento
    {
        public int IdAcompanhamento { get; set; }

        public int IdUsuario { get; set; }

        public int IdPolitico { get; set; }

        public int IsNoticia { get; set; }

        public int IsDespesa { get; set; }

        public int IsProposicao { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }

        public string NomePolitico { get; set; }
    }
}
