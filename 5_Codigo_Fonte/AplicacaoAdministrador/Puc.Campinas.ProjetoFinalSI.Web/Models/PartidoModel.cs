using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Puc.Campinas.ProjetoFinalSI.Entidade;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class PartidoModel
    {
        public PartidoModel(Partido partido)
        {
            this.IdPartido = partido.IdPartido;
            this.Sigla = partido.Sigla;
            this.Nome = partido.Nome;
            this.Deferimento = partido.Deferimento;
            this.PresidenteNacional = partido.PresidenteNacional;
            this.NroPartido = partido.NroPartido;
            this.Login = partido.Login;
            this.DtInclusao = partido.DtInclusao;
            this.FiltroPesquisa = string.Empty;

            this.ListaPartidos = new List<Partido>();
        }

        public PartidoModel()
        {
            this.ListaPartidos = new List<Partido>();
            this.FiltroPesquisa = string.Empty;
        }

        public int IdPartido { get; set; }

        [Required(ErrorMessage = "Campo Sigla é obrigatório")]
        public string Sigla { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Deferimento é obrigatório")]
        //[RegularExpression("(0?[1-9]|[12][0-9]|3[01])[/ -](0?[1-9]|1[12])[/ -](19[0-9]{2}|[2][0-9][0-9]{2})", ErrorMessage = "Data Inválida")]
        [RegularExpression("(0?[1-9]|1?[0-9]|2?[0-9]|3?[0-1])[/ -](0?[1-9]|1?[0-2])[/ -](19[0-9]{2}|[2][0-9]{3})", ErrorMessage = "Data de Deferimento inválida")]
        public DateTime? Deferimento { get; set; }

        [Required(ErrorMessage = "Campo Presidente é obrigatório")]
        public string PresidenteNacional { get; set; }

        [Required(ErrorMessage = "Campo Número do Partido é obrigatório")]
        public int? NroPartido { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }

        public List<Partido> ListaPartidos { get; set; }

        public string FiltroPesquisa { get; set; }
    }
}