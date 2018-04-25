namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class SituacaoBO
    {
        public List<Situacao> ObterSituacao()
        {
            return new SituacaoDAO().ObterSituacao();
        }

        public Situacao ObterSituacaoById(int idSituacao)
        {
            return new SituacaoDAO().ObterSituacaoById(idSituacao);
        }

    }
}
