using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Puc.Campinas.ProjetoFinalSI.Entidade;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class ComissaoModel
    {
        public ComissaoModel(Comissao comissao)
        {
            this.IdComissao = comissao.IdComissao;
            this.Nome = comissao.Nome;
            this.Sigla = comissao.Sigla;
            this.Local = comissao.Local;
            this.Telefone = comissao.Telefone;
            this.Fax = comissao.Fax;
            this.Secretario = comissao.Secretario;
            this.Login = comissao.Login;
            this.DtInclusao = comissao.DtInclusao;
            this.FiltroPesquisa = string.Empty;

            this.ListaComissao = new List<Comissao>();
        }

        public ComissaoModel()
        {
            this.ListaComissao = new List<Comissao>();
            this.FiltroPesquisa = string.Empty;
        }

        public int IdComissao { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Sigla é obrigatório")]
        public string Sigla { get; set; }

        [Required(ErrorMessage = "Campo Local é obrigatório")]
        public string Local { get; set; }       

        [Required(ErrorMessage = "Campo Telefone é obrigatório")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Campo Fax é obrigatório")]
        public string Fax { get; set; }

        [Required(ErrorMessage = "Campo Secretário é obrigatório")]
        public string Secretario { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }

        public List<Comissao> ListaComissao { get; set; }

        public string FiltroPesquisa { get; set; }
    }
}
