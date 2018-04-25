namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class CargoBO
    {
        public List<Cargo> ObterCargos()
        {
            return new CargoDAO().ObterCargos();
        }

        public Cargo ObterCargoById(int idCargo)
        {
            return new CargoDAO().ObterCargoById(idCargo);
        }

        public List<Cargo> ObterCargos(string pString)
        {
            return new CargoDAO().ObterCargos(pString);
        }

        public List<Cargo> ObterCargosByPeriodoMandato(int idPeriodoMandato)
        {
            return new CargoDAO().ObterCargosByPeriodoMandato(idPeriodoMandato);
        }

        public string Incluir(Cargo cargo)
        {
            return new CargoDAO().Incluir(cargo);
        }

        public string Alterar(Cargo cargo)
        {
            return new CargoDAO().Alterar(cargo);
        }

        public string Excluir(Cargo cargo)
        {
            return new CargoDAO().Excluir(cargo);
        }

        public bool NomeExiste(Cargo cargo)
        {
            return new CargoDAO().NomeExiste(cargo);
        }

        public bool VerificaSeCargoEstaSendoUsado(int idCargo)
        {
            return new CargoDAO().VerificaSeCargoEstaSendoUsado(idCargo);
        }
    }
}
