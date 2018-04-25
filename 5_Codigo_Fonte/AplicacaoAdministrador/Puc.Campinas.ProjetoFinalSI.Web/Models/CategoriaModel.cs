using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Puc.Campinas.ProjetoFinalSI.Entidade;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class CategoriaModel
    {
        //private List<Categoria> list;

        public CategoriaModel()
        {
            this.ListaCategoria = new List<Categoria>();
            this.FiltroPesquisa = string.Empty;
        }     
        
        public CategoriaModel(Categoria categoria)
        {
            this.IdCategoria = categoria.IdCategoria;
            this.Nome = categoria.Nome;
            this.Descricao = categoria.Descricao;
        }
        
        public int IdCategoria { get; set; }     
        
        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Descrição é obrigatório")]
        public string Descricao { get; set; }

        public List<Categoria> ListaCategoria {get; set;}
        public string FiltroPesquisa { get; set; }
    }      
}