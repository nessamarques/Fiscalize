using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.DAO;

namespace Puc.Campinas.ProjetoFinalSI.BO
{
    public class DespesaBO
    {
        public List<Despesa> ObterDespesa()
        {
            return new DespesaDAO().ObterDespesa();
        }

        public List<Despesa> ObterDespesa(string pString)
        {
            return new DespesaDAO().ObterDespesa(pString);
        }

        public Despesa ObterDespesaById(int id)
        {
            return new DespesaDAO().ObterDespesaById(id);
        }

        public List<Despesa> ObterDespesasByIdPolitico(int idPolitico)
        {
            return new DespesaDAO().ObterDespesasByIdPolitico(idPolitico);
        }

        public List<Despesa> ObterTop5DespesasByIdPolitico(int idPolitico)
        {
            return new DespesaDAO().ObterTop5DespesasByIdPolitico(idPolitico);
        }

        public string Incluir(Despesa despesa)
        {
            return new DespesaDAO().Incluir(despesa);
        }

        public string Alterar(Despesa despesa)
        {
            return new DespesaDAO().Alterar(despesa);
        }

        public string Excluir(Despesa despesa)
        {
            return new DespesaDAO().Excluir(despesa);
        }

        public bool VerificaSeDespesaEstaSendoUsada(int idDespesa)
        {
            return new DespesaDAO().VerificaSeDespesaEstaSendoUsada(idDespesa);
        }

        public List<Despesa> ObterRankingDespesa()
        {
            return new DespesaDAO().ObterRankingDespesa();
        }


    }
}
