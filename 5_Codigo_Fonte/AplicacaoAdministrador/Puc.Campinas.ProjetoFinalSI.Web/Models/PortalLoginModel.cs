using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Puc.Campinas.ProjetoFinalSI.Entidade;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class PortalLoginModel
    {

        public PortalLoginModel(Usuario usuario)
        {
            this.Senha = usuario.Senha;
            this.Login = usuario.Login;
        }

        public PortalLoginModel()
        {
        }

        public string Login { get; set; }

        public string Senha { get; set; }
    }

}