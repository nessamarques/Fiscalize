using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using AutenticacaoUsuarioMVC3.Models;
using Puc.Campinas.ProjetoFinalSI.Web.Models;
using Puc.Campinas.ProjetoFinalSI.BO;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            return View("Login");
        }

        public ActionResult Index()
        {
            return View("_ViewLogada");
        }

        [HttpPost]
        public ActionResult Index(FormCollection frmLogin)
        {
            UsuarioModel model = new UsuarioModel();

            string user, pass;

            //Recuperando valores do formulário
            user = frmLogin["UsuarioLogin"];
            pass = frmLogin["Senha"];

            model.UsuarioLogin = user;
            model.Senha = pass;

            //Se alguma das informações
            if (UsersRepository.AutenticarUsuario(user, pass))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Login ou Senha Inválidos");
                return View("Login", model);
            }
        }

        public ActionResult Inicio()
        {
            NoticiaBO bo = new NoticiaBO();
            NoticiaModel model = new NoticiaModel();

            model.ListaNoticiasTop = bo.ObterNoticiaByIdLocalNoticia(1).Where(x => x.Ativo == 1).ToList();
            model.ListaNoticiasEsquerda = bo.ObterNoticiaByIdLocalNoticia(2).Where(x => x.Ativo == 1).ToList();
            model.ListaNoticiasDireita = bo.ObterNoticiaByIdLocalNoticia(3).Where(x => x.Ativo == 1).ToList();

            return View("Inicio", model);
        }

    }
}
