namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class VotoBO
    {
        public List<Voto> ObterVoto()
        {
            return new VotoDAO().ObterVoto();
        }

        public Voto ObterVotoById(int idVoto)
        {
            return new VotoDAO().ObterVotoById(idVoto);
        }

    }
}
