namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Convocado
    {
        public Convocado()
        {
            this.Cargo = new Cargo();
        }

        public int IdConvocado { get; set; }

        public int IdSessao { get; set; }

        public int IdCargo { get; set; }

        public DateTime DtInclusao { get; set; }

        public string Login { get; set; }

        public Cargo Cargo { get; set; }
    }
}
