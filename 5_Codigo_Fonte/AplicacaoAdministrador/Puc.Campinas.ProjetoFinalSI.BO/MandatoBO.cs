namespace Puc.Campinas.ProjetoFinalSI.BO.Mandatos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO.Mandato;

    public class MandatoBO
    {
        public List<Mandato> ObterMandatos()
        {
            return new MandatoDAO().ObterMandatos();
        }

        public string EncerrarMandato(int idMandato)
        {
            return new MandatoDAO().EncerrarMandato(idMandato);
        }

        public Mandato ObterMandatoById(int idMandato)
        {
            return new MandatoDAO().ObterMandatoById(idMandato);
        }

        public List<Mandato> ObterMandatosByIdPolitico(int idPolitico)
        {
            return new MandatoDAO().ObterMandatosByIdPolitico(idPolitico);
        }

        public string Incluir(Mandato mandato)
        {
            return new MandatoDAO().Incluir(mandato);
        }
    }
}
