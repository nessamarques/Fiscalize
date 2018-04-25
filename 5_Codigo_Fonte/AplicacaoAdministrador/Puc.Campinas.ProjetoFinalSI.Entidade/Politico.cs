using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    public class Politico
    {
        public Politico()
        {
            this.Escolaridade = new Escolaridade();
            this.EstadoCandidatura = new Estado();
            this.CidadeCandidatura = new Cidade();
            this.MandatoAtual = new Mandato();
            this.CidadeNaturalidade = new Cidade();
            this.EstadoNaturalidade = new Estado();
            this.ListaProfissoes = new List<Profissao>();
            this.Filiacao = new Filiacao();
            this.RedesSociais = new List<RedeSocial>();

            this.ListaDespesasMandatoAtual = new List<Despesa>();
        }

        public int IdPolitico { get; set; }

        public string Nome { get; set; }

        public int IdCidade { get; set; }

        public int IdEstado { get; set; }

        public string Sexo { get; set; }

        public object Foto { get; set; }

        public DateTime DtNascimento { get; set; }

        public int IdCidadeNaturalidade { get; set; }

        public int IdEstadoNaturalidade { get; set; }

        public int IdPaisNaturalidade { get; set; }

        public int Gabinete { get; set; }

        public string Anexo { get; set; }

        public string Telefone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }

        public int IdEscolaridade { get; set; }

        public string Endereco { get; set; }

        public string CEP { get; set; }

        public Escolaridade Escolaridade { get; set; }

        public Cidade CidadeCandidatura { get; set; }

        public Estado EstadoCandidatura { get; set; }

        public Mandato MandatoAtual { get; set; }

        public Cidade CidadeNaturalidade { get; set; }

        public Estado EstadoNaturalidade { get; set; }

        public List<Profissao> ListaProfissoes { get; set; }

        public Filiacao Filiacao { get; set; }

        public List<Despesa> ListaDespesasMandatoAtual { get; set; }

        public List<RedeSocial> RedesSociais { get; set; }
    }
}
    