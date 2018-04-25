using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    public class Historico
    {
        public string Cargo { get; set; }

        public string Partido { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataTermino { get; set; }

        public bool Situacao { get; set; }
    }
}
