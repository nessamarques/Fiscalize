namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class SessaoProposicaoBO
    {
        public List<SessaoProposicao> ObterSessaoProposicao()
        {
            return new SessaoProposicaoDAO().ObterSessaoProposicao();
        }

        public List<SessaoProposicao> ObterSessaoProposicaoByIdSessao(int idSessao)
        {
            return new SessaoProposicaoDAO().ObterSessaoProposicaoByIdSessao(idSessao);
        }
    }
}
