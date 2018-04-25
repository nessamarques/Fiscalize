namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class SituacaoSessaoBO
    {
        public List<SituacaoSessao> ObterSituacao()
        {
            return new SituacaoSessaoDAO().ObterSituacaoSessao();
        }

        public SituacaoSessao ObterSituacaoSessaoById(int idSituacaoSessao)
        {
            return new SituacaoSessaoDAO().ObterSituacaoSessaoById(idSituacaoSessao);
        }

    }
}
