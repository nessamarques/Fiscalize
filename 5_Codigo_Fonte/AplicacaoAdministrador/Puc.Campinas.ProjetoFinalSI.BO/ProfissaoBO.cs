namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class ProfissaoBO
    {
        public List<Profissao> ObterProfissaoByIdPolitico(int idPolitico)
        {
            return new ProfissaoDAO().ObterProfissaoByIdPolitico(idPolitico);
        }
    }
}
