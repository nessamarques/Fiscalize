using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class UsuarioModel
    {
        public UsuarioModel(Usuario usuario)
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

        public UsuarioModel()
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

        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo CPF é obrigatório")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Campo Cidade é obrigatório")]
        public int IdCidade { get; set; }

        [Required(ErrorMessage = "Campo Estado é obrigatório")]
        public int IdEstado { get; set; }

        public string Telefone { get; set; }

        public string Celular { get; set; }

        [RegularExpression("([a-zA-Z0-9]+[a-zA-Z0-9_.-]+@{1}[a-zA-Z0-9_.-]*\\.+[a-z]{2,4})", ErrorMessage = "Email Inválido")]
        [Required(ErrorMessage = "Campo E-mail é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Login é obrigatório")]
        public string UsuarioLogin { get; set; }

        [Required(ErrorMessage = "Campo Senha é obrigatório")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Campo Confirmar Senha é obrigatório")]
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