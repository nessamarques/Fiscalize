using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Puc.Campinas.ProjetoFinalSI.Entidade;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class CargoComissaoModel
    {
        public CargoComissaoModel(CargoComissao cargoComissao)
        {
            this.IdCargoComissao = cargoComissao.IdCargoComissao;
            this.Nome = cargoComissao.Nome;
            this.Descricao = cargoComissao.Descricao;
            this.Login = cargoComissao.Login;
            this.DtInclusao = cargoComissao.DtInclusao;
            this.FiltroPesquisa = string.Empty;

            this.ListaCargoComissao = new List<CargoComissao>();
        }

        public CargoComissaoModel()
        {
            this.ListaCargoComissao = new List<CargoComissao>();
            this.FiltroPesquisa = string.Empty;
        }

        public int IdCargoComissao { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Descrição é obrigatório")]
        public string Descricao { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }

        public List<CargoComissao> ListaCargoComissao { get; set; }

        public string FiltroPesquisa { get; set; }
    }
}
