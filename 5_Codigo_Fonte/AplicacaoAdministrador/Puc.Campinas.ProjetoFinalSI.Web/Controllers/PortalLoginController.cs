using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Web.Models;
using Puc.Campinas.ProjetoFinalSI.BO;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using AutenticacaoUsuarioMVC3.Models;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class PortalLoginController : Controller
    {
        public string ObterUsuario(string login, string senha)
        {
            UsuarioModel model = new UsuarioModel();
            UsuarioBO usuarioBO = new UsuarioBO();
            List<Usuario> listUsuario = new List<Usuario>();

            model.UsuarioLogin = login;
            model.Senha = senha;

            if (UsersRepository.AutenticarUsuario(login, senha))
            {
                return MontaMensagemUserLogado();

            }
            else
            {
                return "ERRO";
            }
        }

        public string VerificaUserLogado()
        {
            Usuario userLogado = UsersRepository.UsuarioLogado;

            if (userLogado != null)
            {
                return MontaMensagemUserLogado();
            }

            return "NOUSER";
        }

        private static string MontaMensagemUserLogado()
        {
            Usuario userLogado = UsersRepository.UsuarioLogado;

            string msgLogado;

            string painelControle = "<a style=\"font-size: 12px; color: #999999; line-height: 12px;\" href=\"/Home/Index\" target=\"_blank\">Painel de Controle</a>";
            string manutencaoNoticias = "<a style=\"font-size: 12px; color: #999999; line-height: 12px;\" href=\"/PortalNoticias/Index\">Manutenção de Notícias</a>";
            string sair = "<a style=\"font-size: 12px; color: #999999; line-height: 12px;\" href=\"#\" onclick=\"Logout();\">Sair</a>";

            if (userLogado.IdGrupo != 3)
            {
                msgLogado = "<div style=\"margin-bottom:10px; margin-right:10px;\">Bem vindo(a) " + "<b>" + userLogado.Nome + "</b>" + " | " + sair + " | " + painelControle + " | " + manutencaoNoticias + "</div>";
            }
            else
            {
                msgLogado = "Bem vindo(a) " + userLogado.Nome + sair;
            }

            return msgLogado;
        }

        public void Logout()
        {
            if (Request.Cookies["UserCookieAuthentication"] != null)
            {
                HttpCookie myCookie = new HttpCookie("UserCookieAuthentication");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
        }

    }
}
