using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.DAO;

namespace Puc.Campinas.ProjetoFinalSI.BO
{
    public class VotacaoBO
    {
        public string Incluir(Votacao voto)
        {
            return new VotacaoDAO().Incluir(voto);
        }
        
        public List<Votacao> ObterVotacoes()
        {
            return new VotacaoDAO().ObterVotacoes();
        }

        public List<Votacao> ObterVotacoesByIdSessaoProposicao(int idSessaoProposicao)
        {
            return new VotacaoDAO().ObterVotacoesByIdSessaoProposicao(idSessaoProposicao);
        }
    }
}
