using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class FuncionalidadeModel
    {
        public FuncionalidadeModel(Funcionalidade funcionalidade)
        {
            this.IdFuncionalidade = funcionalidade.IdFuncionalidade;
            this.Nome = funcionalidade.Nome;
            this.Descricao = funcionalidade.Descricao;
            this.Login = funcionalidade.Login;
            this.DtInclusao = funcionalidade.DtInclusao;
            this.FiltroPesquisa = string.Empty;

            this.ListaFuncionalidades = new List<Funcionalidade>();
        }

        public FuncionalidadeModel()
        {
            this.ListaFuncionalidades = new List<Funcionalidade>();
            this.FiltroPesquisa = string.Empty;

        }

        public int IdFuncionalidade { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Descricao é obrigatório")]
        public string Descricao { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }

        public List<Funcionalidade> ListaFuncionalidades { get; set; }

        public string ActionRequired { get; set; }

        public string FiltroPesquisa { get; set; }
    }
}