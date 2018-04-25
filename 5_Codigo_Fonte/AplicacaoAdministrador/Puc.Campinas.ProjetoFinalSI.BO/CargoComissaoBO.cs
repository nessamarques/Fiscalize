namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class CargoComissaoBO
    {
        public List<CargoComissao> ObterCargoComissao()
        {
            return new CargoComissaoDAO().ObterCargoComissao();
        }

        public CargoComissao ObterCargoComissaoById(int idCargoComissao)
        {
            return new CargoComissaoDAO().ObterCargoComissaoById(idCargoComissao);
        }

        public List<CargoComissao> ObterCargoComissoes(string pString)
        {
            return new CargoComissaoDAO().ObterCargoComissao(pString);
        }

        public string Incluir(CargoComissao cargoComissao)
        {
            return new CargoComissaoDAO().Incluir(cargoComissao);
        }

        public string Alterar(CargoComissao cargoComissao)
        {
            return new CargoComissaoDAO().Alterar(cargoComissao);
        }

        public string Excluir(CargoComissao cargoComissao)
        {
            return new CargoComissaoDAO().Excluir(cargoComissao);
        }

        public bool VerificaSeCargoComissaoEstaSendoUsado(int idCargoComissao)
        {
            return new CargoComissaoDAO().VerificaSeCargoComissaoEstaSendoUsado(idCargoComissao);
        }

        public bool NomeExiste(CargoComissao cargoComissao)
        {
            return new CargoComissaoDAO().NomeExiste(cargoComissao);
        }
    }
}
