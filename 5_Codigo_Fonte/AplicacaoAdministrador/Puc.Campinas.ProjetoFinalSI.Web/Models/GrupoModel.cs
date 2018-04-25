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
    public class GrupoModel
    {
        public GrupoModel(Grupo grupo)
        {
            this.IdGrupo = grupo.IdGrupo;
            this.Nome = grupo.Nome;
            this.Descricao = grupo.Descricao;
            this.Login = grupo.Login;
            this.DtInclusao = grupo.DtInclusao;
            this.FiltroPesquisa = string.Empty;

            this.ListaGrupos = new List<Grupo>();
            this.ListaPermissao = new List<Permissao>();
            this.ListaFuncionalidade = new FuncionalidadeBO().ObterFuncionalidades();
        }

        public GrupoModel()
        {
            this.ListaGrupos = new List<Grupo>();
            this.ListaPermissao = new List<Permissao>();
            this.FiltroPesquisa = string.Empty;
            this.ListaFuncionalidade = new FuncionalidadeBO().ObterFuncionalidades();
        }

        public int IdGrupo { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Descricao é obrigatório")]
        public string Descricao { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }

        public List<Grupo> ListaGrupos { get; set; }

        public string ActionRequired { get; set; }

        public string FiltroPesquisa { get; set; }

        public List<Permissao> ListaPermissao { get; set; }

        public List<Funcionalidade> ListaFuncionalidade { get; set; }

        public int? IdFuncionalidade { get; set; }

        public string ElementosSelecionadosListaFuncionalidades { get; set; }

        public string PermissoesSelecionadas { get; set; }
    }
}