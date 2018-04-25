namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class RedeSocialBO
    {
        public List<RedeSocial> ObterRedeSocialByIdPolitico(int idPolitico)
        {
            return new RedeSocialDAO().ObterRedeSocialByIdPolitico(idPolitico);
        }
    }
}
