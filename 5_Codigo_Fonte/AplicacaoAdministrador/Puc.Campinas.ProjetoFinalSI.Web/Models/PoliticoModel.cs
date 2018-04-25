using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using System.ComponentModel.DataAnnotations;
using Puc.Campinas.ProjetoFinalSI.BO;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class PoliticoModel
    {
        public PoliticoModel()
        {
            this.ListaPoliticos = new List<Politico>();
            this.ListEstado = new List<Estado>();
            this.ListCidade = new List<Cidade>();
            this.ListPais = new List<Pais>();
            this.ListSexos = new List<SexoPolitico>();

            this.ListSexos.Add(new SexoPolitico() { Sigla = "F", Descr = "FEMININO" });
            this.ListSexos.Add(new SexoPolitico() { Sigla = "M", Descr = "MASCULINO" });

            this.FiltroPesquisa = string.Empty;

            this.DtNascimento = null;

            this.ListPais = new PaisBO().ObterPais();

            this.ListEstado = new EstadoBO().ObterEstado();

            this.ListEscolaridade = new EscolaridadeBO().ObterEscolaridade();
        }

        public PoliticoModel(Politico politico)
        {
            this.IdPolitico = politico.IdPolitico;
            this.Nome = politico.Nome;
            this.Sexo = politico.Sexo;
            this.Foto = politico.Foto;
            this.DtNascimento = politico.DtNascimento;
            this.IdCidadeNaturalidade = politico.IdCidadeNaturalidade;
            this.IdEstadoNaturalidade = politico.IdEstadoNaturalidade;
            this.IdPaisNaturalidade = politico.IdPaisNaturalidade;
            this.Email = politico.Email;
            this.IdEscolaridade = politico.IdEscolaridade;
            this.ListaPoliticos = new List<Politico>();

            this.ListPais = new List<Pais>();

            this.ListEstado = new List<Estado>();

            this.ListCidade = new List<Cidade>();

            this.ListSexos = new List<SexoPolitico>();

            this.ListSexos.Add(new SexoPolitico() { Sigla = "M", Descr = "MASCULINO" });
            this.ListSexos.Add(new SexoPolitico() { Sigla = "F", Descr = "FEMININO" });

            this.FiltroPesquisa = string.Empty;

            ListPais = new PaisBO().ObterPais();
            this.ListPais = ListPais;
            
            this.ListEstado = new EstadoBO().ObterEstado();
            this.ListCidade = new CidadeBO().ObterCidadeByIdEstado(politico.IdEstadoNaturalidade);

            ListEscolaridade = new EscolaridadeBO().ObterEscolaridade();
            this.ListEscolaridade = ListEscolaridade;
        }

        public int IdPolitico { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Sexo é obrigatório")]
        public string Sexo { get; set; }

        public object Foto { get; set; }

        [RegularExpression("(0?[1-9]|1?[0-9]|2?[0-9]|3?[0-1])[/ -](0?[1-9]|1?[0-2])[/ -](19[0-9]{2}|[2][0-9]{3})", ErrorMessage = "Data de Nascimento Inválida")]
        [Required(ErrorMessage = "Campo Data de nascimento é obrigatório")]
        public DateTime? DtNascimento { get; set; }

        [Required(ErrorMessage = "Campo Naturalidade é obrigatório")]
        public int IdCidadeNaturalidade { get; set; }

        [Required(ErrorMessage = "Campo Estado de naturalidade é obrigatório")]
        public int IdEstadoNaturalidade { get; set; }

        [Required(ErrorMessage = "Campo Pais de naturalidade é obrigatório")]
        public int IdPaisNaturalidade { get; set; }

        [RegularExpression("([a-zA-Z0-9]+[a-zA-Z0-9_.-]+@{1}[a-zA-Z0-9_.-]*\\.+[a-z]{2,4})", ErrorMessage="Email Inválido")]
        public string Email { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }

        [Required(ErrorMessage = "Campo Escolaridade é obrigatório")]
        public int IdEscolaridade { get; set; }

        public string NomeCargo { get; set; }

        public string SiglaPartido { get; set; }

        public List<Politico> ListaPoliticos { get; set; }

        public List<SexoPolitico> ListSexos { get; set; }

        public List<Cidade> ListCidade { get; set; }

        public List<Estado> ListEstado { get; set; }

        public List<Pais> ListPais { get; set; }

        public List<Escolaridade> ListEscolaridade { get; set; }

        public class SexoPolitico
        {
            public string Sigla { get; set; }
            public string Descr { get; set; }
        }

        public string FiltroPesquisa { get; set; }
    }
}