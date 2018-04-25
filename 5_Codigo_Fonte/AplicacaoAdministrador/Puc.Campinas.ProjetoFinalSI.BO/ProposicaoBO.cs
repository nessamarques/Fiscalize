using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.DAO;

namespace Puc.Campinas.ProjetoFinalSI.BO
{
    public class ProposicaoBO
    {
        public List<Proposicao> ObterProposicao()
        {
            return new ProposicaoDAO().ObterProposicao();
        }

        public List<Proposicao> ObterProposicao(string pString)
        {
            return new ProposicaoDAO().ObterProposicao(pString);
        }

        public Proposicao ObterProposicaoById(int id)
        {
            return new ProposicaoDAO().ObterProposicaoById(id);
        }

        public List<Proposicao> ObterProposicaoByIdPolitico(int idPolitico)
        {
            return new ProposicaoDAO().ObterProposicaoByIdPolitico(idPolitico);
        }

        public List<Proposicao> ObterProposicaoByIdSessao(int idSessao)
        {
            return new ProposicaoDAO().ObterProposicaoByIdSessao(idSessao);
        }

        public string Incluir(Proposicao proposicao)
        {
            return new ProposicaoDAO().Incluir(proposicao);
        }

        public string Alterar(Proposicao proposicao)
        {
            return new ProposicaoDAO().Alterar(proposicao);
        }

        public string Excluir(Proposicao proposicao)
        {
            return new ProposicaoDAO().Excluir(proposicao);
        }

        public bool VerificaSeProposicaoEstaSendoUsada(int idProposicao)
        {
            return new ProposicaoDAO().VerificaSeProposicaoEstaSendoUsada(idProposicao);
        }


    }
}
