using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.DAO;

namespace Puc.Campinas.ProjetoFinalSI.BO
{
    public class TipoProposicaoBO
    {
        public List<TipoProposicao> ObterTipoProposicao()
        {
            return new TipoProposicaoDAO().ObterTipoProposicao();
        }
    }
}
