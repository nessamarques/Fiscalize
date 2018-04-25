using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;
using System.ComponentModel.DataAnnotations;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class DespesaModel
    {
        public DespesaModel(Despesa despesa)
        {
            this.IdDespesa = despesa.IdDespesa;
            this.IdCategoria = despesa.IdCategoria;
            this.NomePolitico = despesa.NomePolitico;
            this.IdMandato = despesa.IdMandato;
            this.Descricao = despesa.Descricao;
            this.Valor = despesa.Valor;
            this.CPF_CNPJ_Forn = despesa.CPF_CNPJ_Forn;
            this.NomeFornecedor = despesa.NomeFornecedor;
            this.NF = despesa.NF;
            this.MesDespesa = despesa.MesDespesa;
            this.AnoDespesa = despesa.AnoDespesa;
            this.ListaPoliticos = new PoliticoBO().ObterPoliticos();
            this.ListaDespesa = new List<Despesa>();
            this.ListCategoria = new CategoriaBO().ObterCategoria();
        }

        public DespesaModel()
        {
            this.ListaDespesa = new List<Despesa>();
            this.FiltroPesquisa = string.Empty;
            this.ListaPoliticos = new PoliticoBO().ObterPoliticos();
            this.ListCategoria = new CategoriaBO().ObterCategoria();

            this.Valor = null;
            this.MesDespesa = null;
            this.AnoDespesa = null;
        }   
        
        public int IdDespesa { get; set; }
        
        public List<Categoria> ListCategoria { get; set; }

        public List<Politico> ListPolitico { get; set; }

        public List<Mandato> ListMandato { get; set; }

        public string Descricao { get; set; }

        public float? Valor { get; set; }

        public string CPF_CNPJ_Forn { get; set; }

        public string NomeFornecedor { get; set; }

        public string NF { get; set; }

        public int? MesDespesa { get; set; }

        public int? AnoDespesa { get; set; }

        public List<Despesa> ListaDespesa { get; set; }
        
        public string FiltroPesquisa { get; set; }

        public List<Politico> ListaPoliticos { get; set; }

        public string NomePolitico { get; set; }

        public int IdPolitico { get; set; }

        public int IdMandato { get; set; }

        public string NomePartido { get; set; }
        
        [Required(ErrorMessage = "Campo Categoria é obrigatório")]
        public int IdCategoria { get; set; }

    }
}