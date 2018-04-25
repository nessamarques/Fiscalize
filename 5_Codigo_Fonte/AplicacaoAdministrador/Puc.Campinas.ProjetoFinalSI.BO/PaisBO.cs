namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class PaisBO
    {
        public List<Pais> ObterPais()
        {
            return new PaisDAO().ObterPais();
        }

        public Pais ObterPaisById(int idPais)
        {
            return new PaisDAO().ObterPaisById(idPais);
        }

        public List<Pais> ObterPais(string pString)
        {
            return new PaisDAO().ObterPais(pString);
        }

    }
}
