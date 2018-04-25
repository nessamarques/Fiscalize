namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Presenca
    {
        public Presenca()
        {
            this.Mandato = new Mandato();
            this.Politico = new Politico();
        }

        public int IdPresenca { get; set; }

        public int IdSessao { get; set; }

        public int IdPolitico { get; set; }

        public DateTime DtInclusao { get; set; }

        public string Login { get; set; }

        public Mandato Mandato { get; set; }

        public int NroFaltas { get; set; }

        public Politico Politico { get; set; }
    }
}
