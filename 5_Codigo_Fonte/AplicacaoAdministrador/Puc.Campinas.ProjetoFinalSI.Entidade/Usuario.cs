namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Usuario
    {
        public Usuario()
        {
             this.Grupo = new Grupo();
        }
        
        public int IdUsuario { get; set; }

        public int IdGrupo { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        public int IdCidade { get; set; }

        public int IdEstado { get; set; }

        public string Telefone { get; set; }

        public string Celular { get; set; }

        public string Email { get; set; }

        public string UsuarioLogin { get; set; }

        public string Senha { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }

        public Grupo Grupo { get; set; }

    }
}
