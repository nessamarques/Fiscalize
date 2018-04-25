namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class UsuarioBO
    {
        public List<Usuario> ObterUsuarios()
        {
            return new UsuarioDAO().ObterUsuarios();
        }

        public int UsuarioValido(string login, string pass)
        {
            return new UsuarioDAO().UsuarioValido(login, pass);
        }

        public Usuario ObterUsuarioById(int idUsuario)
        {
            return new UsuarioDAO().ObterUsuarioById(idUsuario);
        }

        public List<Usuario> ObterUsuarios(string pString)
        {
            return new UsuarioDAO().ObterUsuarios(pString);
        }

        public List<Usuario> ObterUsuariosPortal(string pLogin, string pSenha)
        {
            return new UsuarioDAO().ObterUsuariosPortal(pLogin, pSenha);
        }

        public string Incluir(Usuario usuario)
        {
            return new UsuarioDAO().Incluir(usuario);
        }

        public string Alterar(Usuario usuario)
        {
            return new UsuarioDAO().Alterar(usuario);
        }

        public string Excluir(Usuario usuario)
        {
            return new UsuarioDAO().Excluir(usuario);
        }

        public bool NomeExiste(Usuario usuario)
        {
            return new UsuarioDAO().NomeExiste(usuario);
        }
    }
}
