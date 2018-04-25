using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    public class Despesa
    {
        public Despesa()
        {
            this.ListCategoria = new List<Categoria>();
            this.Categoria = new Categoria();
            this.Mandato = null;
            this.Politico = new Politico();
        }

        public Politico Politico { get; set; }

        public Categoria Categoria { get; set; }

        public Mandato Mandato{ get; set; }

        public int IdDespesa { get; set; }

        public List<Categoria> ListCategoria { get; set; }

        public int IdMandato { get; set; }

        public string Descricao { get; set; }

        public float Valor { get; set; }

        public string CPF_CNPJ_Forn { get; set; }

        public string NomeFornecedor { get; set; }

        public string NF { get; set; }

        public int MesDespesa { get; set; }

        public int AnoDespesa { get; set; }

        public DateTime DtInclusao { get; set; }

        public string Login { get; set; }

        public int IdCategoria { get; set; }

        public string NomePolitico { get; set; }
    }
}
