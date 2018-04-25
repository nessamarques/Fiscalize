namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class CidadeBO
    {
        public List<Cidade> ObterCidade()
        {
            return new CidadeDAO().ObterCidade();
        }

        public Cidade ObterCidadeById(int idCidade)
        {
            return new CidadeDAO().ObterCidadeById(idCidade);
        }

        public List<Cidade> ObterCidadeByIdEstado(int idEstado)
        {
            return new CidadeDAO().ObterCidadeByIdEstado(idEstado);
        }

        public List<Cidade> ObterCidade(string pString)
        {
            return new CidadeDAO().ObterCidade(pString);
        }

    }
}
