namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class ImagemNoticiaBO
    {
        public List<ImagemNoticia> ObterImagem()
        {
            return new ImagemNoticiaDAO().ObterImagemNoticia();
        }

        public ImagemNoticia ObterImagemNoticiaById(int idImagemNoticia)
        {
            return new ImagemNoticiaDAO().ObterImagemNoticiaById(idImagemNoticia);
        }

        public List<ImagemNoticia> ObterImagemByIdNoticia(int idNoticia)
        {
            return new ImagemNoticiaDAO().ObterImagemByIdNoticia(idNoticia);
        }

        public string Incluir(ImagemNoticia imagemNoticia)
        {
            return new ImagemNoticiaDAO().Incluir(imagemNoticia);
        }

        public string Alterar(ImagemNoticia imagemNoticia)
        {
            return new ImagemNoticiaDAO().Alterar(imagemNoticia);
        }

        public string Excluir(ImagemNoticia imagemNoticia)
        {
            return new ImagemNoticiaDAO().Excluir(imagemNoticia);
        }
    }
}
