using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Puc.Campinas.ProjetoFinalSI.Entidade;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class CargoModel
    {
        public CargoModel(Cargo cargo)
        {
            this.IdCargo = cargo.IdCargo;
            this.Nome = cargo.Nome;
            this.Descricao = cargo.Descricao;
            this.Login = cargo.Login;
            this.DtInclusao = cargo.DtInclusao;
            this.FiltroPesquisa = string.Empty;

            this.ListaCargos = new List<Cargo>();
        }

        public CargoModel()
        {
            this.ListaCargos = new List<Cargo>();
            this.FiltroPesquisa = string.Empty;
        }

        public int IdCargo { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Descrição é obrigatório")]
        public string Descricao { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }

        public List<Cargo> ListaCargos { get; set; }

        public string FiltroPesquisa { get; set; }
    }
}
