using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class BiografiaModel
    {
        public BiografiaModel(int idPolitico)
        {
            this.Politico = new PoliticoBO().ObterPoliticoById(idPolitico);
            
            this.ListaCargo = new CargoBO().ObterCargos();
            this.ListaPartido = new PartidoBO().ObterPartidos();
            this.HistoricoPolitico = new PoliticoBO().ObterHistoricoPolitico(idPolitico);
            this.HistoricoAtividades = new AtividadeBO().ObterAtividadesByIdPolitico(idPolitico);
        }

        public Politico Politico { get; set; }

        public int IdCargo { get; set; }

        public int IdPartido { get; set; }

        public List<Cargo> ListaCargo { get; set; }

        public List<Partido> ListaPartido { get;set; }

        public List<Historico> HistoricoPolitico { get; set; }

        public List<Atividade> HistoricoAtividades { get; set; }

    }
}