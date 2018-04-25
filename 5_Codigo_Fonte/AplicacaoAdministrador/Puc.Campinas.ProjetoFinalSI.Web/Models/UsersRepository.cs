using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using System.Web;
using Puc.Campinas.ProjetoFinalSI.BO;

namespace AutenticacaoUsuarioMVC3.Models
{
    public class UsersRepository
    {
        //Propriedade que verifica se o usuário encontra-se logado.
        public static Usuario UsuarioLogado
        {
            get
            {
                var Usuario = HttpContext.Current.Request.Cookies["UserCookieAuthentication"];
                if (Usuario == null)
                {
                    return null;
                }
                else
                {
                    string NovoToken = AutenticacaoUsuarioMVC3.Models.CryptographyRepository.Descriptografar(Usuario.Value.ToString());

                    int IDUsuario;

                    if (int.TryParse(NovoToken, out IDUsuario))
                    {
                        return GetUsuarioByID(IDUsuario);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        //Recuperando o usuário pelo ID
        public static Usuario GetUsuarioByID(int idUsuario)
        {
            return new UsuarioBO().ObterUsuarioById(idUsuario);
        }

        /// <summary>
        /// Com base no Username e no Password, este método autentica o usuário e o direciona para o local correto.
        /// </summary>
        /// <param name="_Username"></param>
        /// <param name="_Password"></param>
        /// <returns></returns>
        public static bool AutenticarUsuario(string user, string pass)
        {
            try
            {
                int idUser = new UsuarioBO().UsuarioValido(user, pass);

                if (idUser <= 0)
                {
                    return false;
                }
                else
                {
                    //Criando um objeto cookie
                    HttpCookie UserCookie = new HttpCookie("UserCookieAuthentication");

                    //Setando o ID do usuário no cookie
                    UserCookie.Value = AutenticacaoUsuarioMVC3.Models.CryptographyRepository.Criptografar(idUser.ToString());

                    //Definindo o prazo de vida do cookie
                    UserCookie.Expires = DateTime.Now.AddDays(1);

                    //Adicionando o cookie no contexto da aplicação
                    HttpContext.Current.Response.Cookies.Add(UserCookie);

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
