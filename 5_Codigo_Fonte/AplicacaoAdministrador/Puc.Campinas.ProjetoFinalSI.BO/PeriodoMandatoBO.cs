using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.DAO;

namespace Puc.Campinas.ProjetoFinalSI.BO
{
    public class PeriodoMandatoBO
    {
        public List<PeriodoMandato> ObterPeriodosMandatos()
        {
            return new PeriodoMandatoDAO().ObterPeriodosMandatos();
        }

        public List<PeriodoMandato> ObterPeriodosMandatosAtivos()
        {
            return new PeriodoMandatoDAO().ObterPeriodosMandatosAtivos();
        }

        public PeriodoMandato ObterPeriodosMandatosById(int id)
        {
            return new PeriodoMandatoDAO().ObterPeriodosMandatosById(id);
        }

        public string Incluir(PeriodoMandato periodoMandato)
        {
            return new PeriodoMandatoDAO().Incluir(periodoMandato);
        }

        public string Alterar(PeriodoMandato periodoMandato)
        {
            return new PeriodoMandatoDAO().Alterar(periodoMandato);
        }

        public string Excluir(PeriodoMandato periodoMandato)
        {
            return new PeriodoMandatoDAO().Excluir(periodoMandato);
        }

        public bool VerificaSePeriodoMandatoEstaSendoUsado(int idPeriodoMandato)
        {
            return new PeriodoMandatoDAO().VerificaSePeriodoMandatoEstaSendoUsado(idPeriodoMandato);
        }
    }
}
