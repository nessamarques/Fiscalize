using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.DAO;

namespace Puc.Campinas.ProjetoFinalSI.BO
{
    public class PresencaBO
    {
        public List<Presenca> ObterPresencas()
        {
            return new PresencaDAO().ObterPresencas();
        }

        public List<Presenca> ObterPresencasByIdSessao(int idSessao)
        {
            return new PresencaDAO().ObterPresencasByIdSessao(idSessao);
        }

        public string Incluir(Presenca presenca)
        {
            return new PresencaDAO().Incluir(presenca);
        }

        public List<Presenca> ObterFaltasSessoes(int idCargo)
        {
            return new PresencaDAO().ObterFaltasSessoes(idCargo);
        }
    }
}
