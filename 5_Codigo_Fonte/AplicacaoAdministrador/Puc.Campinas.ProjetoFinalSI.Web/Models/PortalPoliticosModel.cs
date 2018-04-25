using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;
using AutenticacaoUsuarioMVC3.Models;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class PortalPoliticosModel
    {

        public PortalPoliticosModel()
        {
            this.ListaCargos = new CargoBO().ObterCargos();
            this.ListaPoliticos = new List<Politico>();
            this.ListaEstados = new EstadoBO().ObterEstado();
            this.ListaCidades = new List<Cidade>();
            this.ListaPartidos = new PartidoBO().ObterPartidos();

            this.UsuarioLogado = UsersRepository.UsuarioLogado;

            this.RankingDespesas = new List<Despesa>();

            this.ObterBiografiaIDPolitico = 0;
            this.ObterBiografiaNomePolitico = string.Empty;

            this.RankingPresencas = new List<Presenca>();
        }

        public List<Cargo> ListaCargos { get; set; }

        public List<Politico> ListaPoliticos { get; set; }

        public List<Cidade> ListaCidades { get; set; }

        public List<Estado> ListaEstados { get; set; }

        public List<Partido> ListaPartidos { get; set; }

        public int IdPartido { get; set; }

        public int IdCargo { get; set; }

        public int IdPolitico { get; set; }

        public int IdCidade { get; set; }

        public int IdEstado { get; set; }

        public string Nome { get; set; }

        public Usuario UsuarioLogado { get; set; }

        public List<Despesa> RankingDespesas { get; set; }

        public List<Presenca> RankingPresencas { get; set; }

        public string NomePolitico { get; set; }

        public int HdnIdPolitico { get; set; }

        public int ObterBiografiaIDPolitico { get; set; }

        public string ObterBiografiaNomePolitico { get; set; }

        public string TipoRanking { get; set; }
    }
}