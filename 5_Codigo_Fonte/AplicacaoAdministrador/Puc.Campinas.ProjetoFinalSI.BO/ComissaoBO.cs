namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class ComissaoBO
    {
        public List<Comissao> ObterComissao()
        {
            return new ComissaoDAO().ObterComissao();
        }

        public Comissao ObterComissaoById(int idComissao)
        {
            return new ComissaoDAO().ObterComissaoById(idComissao);
        }

        public List<Comissao> ObterComissoes(string pString)
        {
            return new ComissaoDAO().ObterComissao(pString);
        }

        public string Incluir(Comissao comissao)
        {
            return new ComissaoDAO().Incluir(comissao);
        }

        public string Alterar(Comissao comissao)
        {
            return new ComissaoDAO().Alterar(comissao);
        }

        public string Excluir(Comissao comissao)
        {
            return new ComissaoDAO().Excluir(comissao);
        }

        public bool VerificaSeComissaoEstaSendoUsada(int idComissao)
        {
            return new ComissaoDAO().VerificaSeComissaoEstaSendoUsada(idComissao);
        }

        public bool NomeExiste(Comissao comissao)
        {
            return new ComissaoDAO().NomeExiste(comissao);
        }
    }
}
