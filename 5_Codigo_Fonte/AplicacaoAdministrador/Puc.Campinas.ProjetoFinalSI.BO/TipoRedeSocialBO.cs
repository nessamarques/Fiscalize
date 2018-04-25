namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class TipoRedeSocialBO
    {
        public List<TipoRedeSocial> ObterTipoRedeSocial()
        {
            return new TipoRedeSocialDAO().ObterTipoRedeSocial();
        }

        public TipoRedeSocial ObterTipoRedeSocialById(int idTipoRedeSocial)
        {
            return new TipoRedeSocialDAO().ObterTipoRedeSocialById(idTipoRedeSocial);
        }
    }
}
