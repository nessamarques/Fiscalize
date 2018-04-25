using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    public class CargoMandato
    {
        public int IdCargoMandato { get; set; }

        public int IdCargo { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }

        public int IdPeriodoMandato { get; set; }
    }
}
