namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Mandato
    {

        public Mandato()
        {
            this.PeriodoMandato = new PeriodoMandato();
            //this.ObjPolitico = new Politico();
            //this.ObjCargo = new Cargo();
        }

        public int IdMandato { get; set; }

        public int IdPeriodoMandato { get; set; }

        public int IdPolitico { get; set; }
        
        public int IdCargo { get; set; }

        public int IdPartido { get; set; }

        public int IdCidade { get; set; }

        public int IdEstado { get; set; }

        public int Gabinete { get; set; }

        public string Anexo { get; set; }

        public string Telefone { get; set; }

        public string Fax { get; set; }

        public string Endereco { get; set; }

        public string CEP { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }

        public string NomePolitico { get; set; }

        public string Cargo { get; set; }

        public string Partido { get; set; }

        public string Periodo { get; set; }

        public bool IsMandatoAtual { get; set; }

        public PeriodoMandato PeriodoMandato { get; set; }

        //public Politico ObjPolitico { get; set; }

        //public Cargo ObjCargo { get; set; }
    }
}
