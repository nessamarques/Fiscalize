using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Puc.Campinas.ProjetoFinalSI.Web.Models;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class PortalUsuariosController : Controller
    {
        public ActionResult Index()
        {
            return View("Registro", new PortalUsuariosModel());
        }

        public ActionResult Inicio()
        {
            return View("../Home/Inicio");
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            Usuario usuario = new Usuario();

            usuario.Nome = formCollection.GetValue("Nome").AttemptedValue;
            usuario.IdCidade = Convert.ToInt32(formCollection.GetValue("IdCidade").AttemptedValue);
            usuario.IdEstado = Convert.ToInt32(formCollection.GetValue("IdEstado").AttemptedValue);
            usuario.Telefone = formCollection.GetValue("Telefone").AttemptedValue;
            usuario.Celular = formCollection.GetValue("Celular").AttemptedValue;
            usuario.Email = formCollection.GetValue("Email").AttemptedValue;

            // GRUPO INSERIDO PELO PORTAL SEMPRE SERA ID = 3 (Cidadão).
            usuario.IdGrupo = 3;

            if (ValidarCpf(formCollection.GetValue("CPF").AttemptedValue))
            {
                usuario.CPF = formCollection.GetValue("CPF").AttemptedValue;

                if (new UsuarioBO().ObterUsuarioById(usuario.IdUsuario).Nome != usuario.Nome)
                {
                    if (new UsuarioBO().NomeExiste(usuario))
                    {
                        ModelState.AddModelError("", "Já existe um usuário com o CPF especificado.");
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "CPF Inválido");
            }

            usuario.UsuarioLogin = formCollection.GetValue("UsuarioLogin").AttemptedValue;
            usuario.Senha = formCollection.GetValue("Senha").AttemptedValue;

            string userConfirmSenha = formCollection.GetValue("SenhaConfirm").AttemptedValue;

            if (usuario.Senha != userConfirmSenha)
            {
                ModelState.AddModelError("", "Senha e Confirmação não conferem.");
            }

            usuario.Login = formCollection.GetValue("UsuarioLogin").AttemptedValue;
            usuario.DtInclusao = DateTime.Now;

            if (ModelState.IsValid)
            {
                string ret = new UsuarioBO().Incluir(usuario);
            }
            else
            {
                PortalUsuariosModel model = new PortalUsuariosModel(usuario);
                return this.View("PortalUsuarios", model);
            }

            return RedirectToAction("Inicio");
        }

        /// <summary>
        /// Verifica se o CPF informado é válido
        /// </summary>
        /// <param name="cpf">CPF para validação</param>
        /// <returns>Retorna True caso seja válido</returns>
        public static bool ValidarCpf(string cpf)
        {
            cpf = cpf.Replace("-", "");
            cpf = cpf.Replace(".", "");
            Regex reg = new Regex(@"(^(\d{3}.\d{3}.\d{3}-\d{2})|(\d{11})$)");

            if (!reg.IsMatch(cpf))
                return false;

            int d1, d2;
            int soma = 0;
            string digitado = "";
            string calculado = "";

            // Pesos para calcular o primeiro digito
            int[] peso1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            // Pesos para calcular o segundo digito
            int[] peso2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] n = new int[11];

            bool retorno = false;

            // Limpa a string
            cpf = cpf.Replace(".", "").Replace("-", "").Replace("/", "").Replace("\\", "");

            // Se o tamanho for < 11 entao retorna como inválido
            if (cpf.Length != 11)
            {
                return false;
            }

            // Caso coloque todos os numeros iguais
            switch (cpf)
            {
                case "11111111111":

                    return false;

                case "00000000000":

                    return false;

                case "2222222222":

                    return false;

                case "33333333333":

                    return false;

                case "44444444444":

                    return false;

                case "55555555555":

                    return false;

                case "66666666666":

                    return false;

                case "77777777777":

                    return false;

                case "88888888888":

                    return false;

                case "99999999999":

                    return false;
            }
            try
            {
                // Quebra cada digito do CPF
                n[0] = Convert.ToInt32(cpf.Substring(0, 1));

                n[1] = Convert.ToInt32(cpf.Substring(1, 1));

                n[2] = Convert.ToInt32(cpf.Substring(2, 1));

                n[3] = Convert.ToInt32(cpf.Substring(3, 1));

                n[4] = Convert.ToInt32(cpf.Substring(4, 1));

                n[5] = Convert.ToInt32(cpf.Substring(5, 1));

                n[6] = Convert.ToInt32(cpf.Substring(6, 1));

                n[7] = Convert.ToInt32(cpf.Substring(7, 1));

                n[8] = Convert.ToInt32(cpf.Substring(8, 1));

                n[9] = Convert.ToInt32(cpf.Substring(9, 1));

                n[10] = Convert.ToInt32(cpf.Substring(10, 1));
            }
            catch
            {
                return false;
            }

            // Calcula cada digito com seu respectivo peso
            for (int i = 0; i <= peso1.GetUpperBound(0); i++)
            {
                soma += (peso1[i] * Convert.ToInt32(n[i]));
            }

            // Pega o resto da divisao
            int resto = soma % 11;

            if (resto == 1 || resto == 0)
            {
                d1 = 0;
            }

            else
            {
                d1 = 11 - resto;
            }

            soma = 0;

            // Calcula cada digito com seu respectivo peso
            for (int i = 0; i <= peso2.GetUpperBound(0); i++)
            {
                soma += (peso2[i] * Convert.ToInt32(n[i]));
            }

            // Pega o resto da divisao
            resto = soma % 11;

            if (resto == 1 || resto == 0)
            {
                d2 = 0;
            }

            else
            {
                d2 = 11 - resto;
            }

            calculado = d1.ToString() + d2.ToString();
            digitado = n[9].ToString() + n[10].ToString();

            // Se os ultimos dois digitos calculados bater com
            // os dois ultimos digitos do cpf entao é válido
            if (calculado == digitado)
            {
                retorno = true;
            }
            else
            {
                retorno = false;
            }
            return retorno;
        }

        public string ObterCidadesByIdEstado(int idEstado)
        {
            CidadeBO cidadesBO = new CidadeBO();

            List<Cidade> listaCidades = cidadesBO.ObterCidadeByIdEstado(idEstado);

            JavaScriptSerializer jss = new JavaScriptSerializer();

            return jss.Serialize(listaCidades);
        }

    }
}
