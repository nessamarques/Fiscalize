namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ImagemNoticia
    {
        public int IdImagemNoticia { get; set; }

        public int IdNoticia { get; set; }

        public object Imagem { get; set; }

        public int IdLocalNoticia { get; set; }

        public int IsPortal { get; set; }

        public DateTime DtInclusao { get; set; }

        public string Login { get; set; }
    }
}
