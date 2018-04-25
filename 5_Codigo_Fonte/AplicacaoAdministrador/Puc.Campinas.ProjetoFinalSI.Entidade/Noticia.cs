namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    public class Noticia
    {
        public Noticia()
        {
            this.ListaImagens = new List<ImagemNoticia>();
            this.ListaPoliticos = new List<NoticiaPolitico>();
        }

        public int IdNoticia { get; set; }

        public string Titulo { get; set; }
        
        public string Subtitulo { get; set; }

        public string Resumo { get; set; }

        public string Conteudo { get; set; }
        
        public int IdLocalNoticia { get; set; }

        public string LinkVideo { get; set; }

        public int Ativo { get; set; }
        
        public DateTime DtInclusao { get; set; }
        
        public string Login { get; set; }

        public string Fonte { get; set; }

        public DateTime DtNoticia { get; set; }

        public List<ImagemNoticia> ListaImagens { get; set; }
        
        public List<NoticiaPolitico> ListaPoliticos { get; set; }

    }
}
