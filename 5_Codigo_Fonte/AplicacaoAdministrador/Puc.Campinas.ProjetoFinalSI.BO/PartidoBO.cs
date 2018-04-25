namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class PartidoBO
    {
        public List<Partido> ObterPartidos()
        {
            return new PartidoDAO().ObterPartidos();
        }

        public Partido ObterPartidoById(int idPartido)
        {
            return new PartidoDAO().ObterPartidoById(idPartido);
        }

        public List<Partido> ObterPartidos(string pString)
        {
            return new PartidoDAO().ObterPartidos(pString);
        }

        public string Incluir(Partido partido)
        {
            return new PartidoDAO().Incluir(partido);
        }

        public string Alterar(Partido partido)
        {
            return new PartidoDAO().Alterar(partido);
        }

        public string Excluir(Partido partido)
        {
            return new PartidoDAO().Excluir(partido);
        }

        public bool NomeExiste(Partido partido)
        {
            return new PartidoDAO().NomeExiste(partido);
        }

        public bool VerificaSePartidoEstaSendoUsado(int idPartido)
        {
            return new PartidoDAO().VerificaSePartidoEstaSendoUsado(idPartido);
        }
    }
}
