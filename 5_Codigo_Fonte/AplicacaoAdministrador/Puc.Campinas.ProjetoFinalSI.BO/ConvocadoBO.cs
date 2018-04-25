namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class ConvocadoBO
    {
        public List<Convocado> ObterConvocados()
        {
            return new ConvocadoDAO().ObterConvocados();
        }

        public List<Convocado> ObterConvocadosByIdSessao(int idSessao)
        {
            return new ConvocadoDAO().ObterConvocadosByIdSessao(idSessao);
        }
    }
}
