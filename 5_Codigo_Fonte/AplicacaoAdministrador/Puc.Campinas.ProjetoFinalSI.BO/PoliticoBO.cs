using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.DAO;

namespace Puc.Campinas.ProjetoFinalSI.BO
{
    public class PoliticoBO
    {
        public List<Politico> ObterPoliticos()
        {
            return new PoliticoDAO().ObterPoliticos();
        }

        public Politico ObterPoliticoById(int idPolitico)
        {
            return new PoliticoDAO().ObterPoliticoById(idPolitico);
        }

        public List<Politico> ObterPoliticos(string pString)
        {
            return new PoliticoDAO().ObterPoliticos(pString);
        }

        public List<Politico> ObterPoliticos(string idEstado, string idCidade, string idCargo, string idPartido, string idPolitico)
        {
            return new PoliticoDAO().ObterPoliticos(idEstado, idCidade, idCargo, idPartido, idPolitico);
        }

        public string Incluir(Politico politico)
        {
            return new PoliticoDAO().Incluir(politico); 
        }

        public string Alterar(Politico politico)
        {
            return new PoliticoDAO().Alterar(politico); 
        }

        public string Excluir(Politico politico)
        {
            return new PoliticoDAO().Excluir(politico); 
        }

        public bool NomeExiste(Politico politico)
        {
            return new PoliticoDAO().NomeExiste(politico);
        }

        public List<Politico> ObterPoliticosAtivosByIdCargo(int idCargo)
        {
            return new PoliticoDAO().ObterPoliticosAtivosByIdCargo(idCargo);
        }

        public List<Politico> ObterPoliticosByCargoPartido(int idEstado, int idCidade, int? idCargo, int? idPartido)
        {
            return new PoliticoDAO().ObterPoliticosByCargoPartido(idEstado, idCidade, idCargo, idPartido);
        }
        
        public List<Historico> ObterHistoricoPolitico(int idPolitico)
        {
            return new PoliticoDAO().ObterHistoricoPolitico(idPolitico);
        }
    }
}
