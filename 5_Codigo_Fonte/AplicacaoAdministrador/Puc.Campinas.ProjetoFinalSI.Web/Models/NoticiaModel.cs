using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using System.ComponentModel.DataAnnotations;
using Puc.Campinas.ProjetoFinalSI.BO;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class NoticiaModel
    {
        public NoticiaModel(Noticia noticia)
        {
            this.IdNoticia = noticia.IdNoticia;
            this.Titulo = noticia.Titulo;
            this.Subtitulo = noticia.Subtitulo;
            this.Resumo = noticia.Resumo;
            this.Conteudo = noticia.Conteudo;
            this.IdLocalNoticia = noticia.IdLocalNoticia;
            this.Ativo = noticia.Ativo;
            this.DtInclusao = noticia.DtInclusao;
            this.Login = noticia.Login;
            this.Fonte = noticia.Fonte;
            this.DtInclusao = noticia.DtInclusao;
            this.LinKVideo = noticia.LinkVideo;

            this.ListaNoticias = new List<Noticia>();

            this.FiltroPesquisa = string.Empty;

            this.ListaNoticiasEsquerda = new List<Noticia>();

            this.ListaNoticiasDireita = new List<Noticia>();

            this.ListaNoticiasTop = new List<Noticia>();

            this.ListaCargos = new CargoBO().ObterCargos();

            this.ListaPoliticos = new List<Politico>();
        }
       
        public NoticiaModel()
        {
            this.ListaNoticias = new List<Noticia>();
            this.FiltroPesquisa = string.Empty;

            this.ListaCargos = new CargoBO().ObterCargos();

            this.ListaPoliticos = new List<Politico>();

            this.ListaNoticiasTop = new List<Noticia>();
        }

        public int IdNoticia { get; set; }

        public string Titulo { get; set; }

        public string Subtitulo { get; set; }

        public string Resumo { get; set; }

        public string Conteudo { get; set; }

        public int IdLocalNoticia { get; set; }

        public int Ativo { get; set; }

        public DateTime DtInclusao { get; set; }

        public string Login { get; set; }

        public string Fonte { get; set; }

        public DateTime DtNoticia { get; set; }

        public List<Noticia> ListaNoticias { get; set; }

        public string FiltroPesquisa { get; set; }

        public string LinKVideo { get; set; }

        public int AreaSelecionada { get; set; }

        public List<Noticia> ListaNoticiasEsquerda { get; set; }

        public List<Noticia> ListaNoticiasDireita { get; set; }

        public List<Noticia> ListaNoticiasTop { get; set; }

        public int IdCargo { get; set; }

        public int IdPolitico { get; set; }

        public List<Politico> ListaPoliticos { get; set; }

        public List<Cargo> ListaCargos { get; set; }

        public string PoliticosSelecionados { get; set; }
    }
}