using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.DAO;

namespace Puc.Campinas.ProjetoFinalSI.BO
{
    public class AcompanhamentoBO
    {
        public List<Acompanhamento> ObterAcompanhamentos()
        {
            return new AcompanhamentoDAO().ObterAcompanhamentos();
        }

        public List<Acompanhamento> ObterAcompanhamentosByIdUsuario(int idUsuario)
        {
            return new AcompanhamentoDAO().ObterAcompanhamentosByIdUsuario(idUsuario);
        }

        public string Incluir(Acompanhamento acompanhamento)
        {
            return new AcompanhamentoDAO().Incluir(acompanhamento);
        }

        public string Alterar(Acompanhamento acompanhamento)
        {
            return new AcompanhamentoDAO().Alterar(acompanhamento);
        }

        public string Excluir(Acompanhamento acompanhamento)
        {
            return new AcompanhamentoDAO().Excluir(acompanhamento);
        }

        public string Excluir(int idPolitico, int idUsuario)
        {
            return new AcompanhamentoDAO().Excluir(idPolitico, idUsuario);
        }
    }
}
