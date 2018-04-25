namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class NoticiaBO
    {
        public List<Noticia> ObterNoticias()
        {
            return new NoticiaDAO().ObterNoticia();
        }

        public Noticia ObterNoticiaById(int idNoticia)
        {
            return new NoticiaDAO().ObterNoticiaById(idNoticia);
        }

        public List<Noticia> ObterNoticia(string pString)
        {
            return new NoticiaDAO().ObterNoticia(pString);
        }

        public List<Noticia> ObterNoticiaByIdLocalNoticia(int idLocalNoticia)
        {
            return new NoticiaDAO().ObterNoticiaByIdLocalNoticia(idLocalNoticia);
        }

        public Noticia ObterUltimaNoticia()
        {
            return new NoticiaDAO().ObterUltimaNoticia();
        }

        public List<Noticia> ObterNoticiasAtivas(int idLocalNoticia)
        {
            return new NoticiaDAO().ObterNoticiasAtivas(idLocalNoticia);
        }

        public string InativarNoticias(int idLocalNoticia)
        {
            return new NoticiaDAO().InativarNoticias(idLocalNoticia);
        }

        public string Incluir(Noticia noticia)
        {
            return new NoticiaDAO().Incluir(noticia);
        }

        public string Alterar(Noticia noticia)
        {
            return new NoticiaDAO().Alterar(noticia);
        }

        public string Excluir(Noticia noticia)
        {
            return new NoticiaDAO().Excluir(noticia);
        }

        public void DesativarNoticia(int idNoticia)
        {
            new NoticiaDAO().DesativarNoticia(idNoticia);
        }

        public void AtivarNoticia(int idNoticia)
        {
            new NoticiaDAO().AtivarNoticia(idNoticia);
        }
    }
}
