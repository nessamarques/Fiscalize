namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class PermissaoBO
    {
        public List<Permissao> ObterPermissao()
        {
            return new PermissaoDAO().ObterPermissao();
        }

        public Permissao ObterPermissaoById(int idPermissao)
        {
            return new PermissaoDAO().ObterPermissaoById(idPermissao);
        }

        public List<Permissao> ObterPermissao(string pString)
        {
            return new PermissaoDAO().ObterPermissao(pString);
        }

        public string Incluir(Permissao permissao)
        {
            return new PermissaoDAO().Incluir(permissao);
        }

        public string Alterar(Permissao permissao)
        {
            return new PermissaoDAO().Alterar(permissao);
        }

        public string Excluir(Permissao permissao)
        {
            return new PermissaoDAO().Excluir(permissao);
        }

    }
}
