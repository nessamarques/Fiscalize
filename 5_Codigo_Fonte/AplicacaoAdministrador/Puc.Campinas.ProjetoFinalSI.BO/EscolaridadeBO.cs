namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class EscolaridadeBO
    {
        public List<Escolaridade> ObterEscolaridade()
        {
            return new EscolaridadeDAO().ObterEscolaridade();
        }

        public Escolaridade ObterEscolaridadeById(int idEscolaridade)
        {
            return new EscolaridadeDAO().ObterEscolaridadeById(idEscolaridade);
        }

        public List<Escolaridade> ObterEscolaridade(string pString)
        {
            return new EscolaridadeDAO().ObterEscolaridade(pString);
        }

    }
}
