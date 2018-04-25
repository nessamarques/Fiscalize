using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;
using System.ComponentModel.DataAnnotations;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class PortalUsuariosModel
    {
        public PortalUsuariosModel(Usuario usuario)
        {
            this.IdUsuario = usuario.IdUsuario;
            this.ListaGrupos = new List<Grupo>();
            this.Nome = usuario.Nome;
            this.CPF = usuario.CPF;
            this.ListaEstados = new List<Estado>();
            this.ListaCidades = new List<Cidade>();
            this.Telefone = usuario.Telefone;
            this.Celular = usuario.Celular;
            this.Email = usuario.Email;
            this.UsuarioLogin = usuario.UsuarioLogin;
            this.Senha = usuario.Senha;
            this.Login = usuario.Login;
            this.DtInclusao = usuario.DtInclusao;
            this.FiltroPesquisa = string.Empty;
            this.SenhaConfirm = usuario.Senha;
            this.IdEstado = usuario.IdEstado;
            this.IdCidade = usuario.IdCidade;
            this.IdGrupo = usuario.IdGrupo;

            this.ListaUsuarios = new List<Usuario>();
            this.ListaEstados = new EstadoBO().ObterEstado();
            this.ListaGrupos = new GrupoBO().ObterGrupos();
            this.ListaCidades = new CidadeBO().ObterCidadeByIdEstado(usuario.IdEstado);
        }

        public PortalUsuariosModel()
        {
            this.ListaUsuarios = new List<Usuario>();
            this.ListaGrupos = new List<Grupo>();
            this.ListaEstados = new List<Estado>();
            this.ListaCidades = new List<Cidade>();

            this.FiltroPesquisa = string.Empty;

            this.ListaEstados = new EstadoBO().ObterEstado();
            this.ListaGrupos = new GrupoBO().ObterGrupos();
        }

        public int IdUsuario { get; set; }

        public int IdGrupo { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        public int IdCidade { get; set; }

        public int IdEstado { get; set; }

        public string Telefone { get; set; }

        public string Celular { get; set; }

        [RegularExpression("([a-zA-Z0-9]+[a-zA-Z0-9_.-]+@{1}[a-zA-Z0-9_.-]*\\.+[a-z]{2,4})", ErrorMessage = "Email Inválido.")]
        public string Email { get; set; }

        public string UsuarioLogin { get; set; }

        public string Senha { get; set; }

        public string SenhaConfirm { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }

        public List<Usuario> ListaUsuarios { get; set; }

        public List<Grupo> ListaGrupos { get; set; }

        public List<Cidade> ListaCidades { get; set; }

        public List<Estado> ListaEstados { get; set; }

        public string ActionRequired { get; set; }

        public string FiltroPesquisa { get; set; }
    }
}