namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class FiliacaoBO
    {
        public Filiacao ObterFiliacaoByIdPolitico(int idPolitico)
        {
            return new FiliacaoDAO().ObterFiliacaoByIdPolitico(idPolitico);
        }
    }
}
