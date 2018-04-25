namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class EstadoBO
    {
        public List<Estado> ObterEstado()
        {
            return new EstadoDAO().ObterEstado();
        }

        public Estado ObterEstadoById(int idEstado)
        {
            return new EstadoDAO().ObterEstadoById(idEstado);
        }

        public List<Estado> ObterEstado(string pString)
        {
            return new EstadoDAO().ObterEstado(pString);
        }

    }
}
