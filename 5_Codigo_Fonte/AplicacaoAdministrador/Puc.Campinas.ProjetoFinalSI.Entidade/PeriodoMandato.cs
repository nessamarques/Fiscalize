using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    public class PeriodoMandato
    {
        public PeriodoMandato()
        {
            this.ListCargoMandatos = new List<CargoMandato>();
        }
        
        public int IdPeriodoMandato { get; set; }

        public string Situacao { get; set; }

        public DateTime DtInicio { get; set; }

        public DateTime DtFim { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }

        public List<CargoMandato> ListCargoMandatos { get; set; }

        public string RangeDatas { get; set; }

    }
}
