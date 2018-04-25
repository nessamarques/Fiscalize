namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class NoticiaPoliticoBO
    {
        public List<NoticiaPolitico> ObterNoticiaPolitico()
        {
            return new NoticiaPoliticoDAO().ObterNoticiaPolitico();
        }

        public NoticiaPolitico ObterNoticiaPoliticoById(int idNoticiaPolitico)
        {
            return new NoticiaPoliticoDAO().ObterNoticiaPoliticoById(idNoticiaPolitico);
        }

        public string Incluir(NoticiaPolitico noticiaPolitico)
        {
            return new NoticiaPoliticoDAO().Incluir(noticiaPolitico);
        }

        public string Alterar(NoticiaPolitico noticiaPolitico)
        {
            return new NoticiaPoliticoDAO().Alterar(noticiaPolitico);
        }

        public string Excluir(NoticiaPolitico noticiaPolitico)
        {
            return new NoticiaPoliticoDAO().Excluir(noticiaPolitico);
        }

        public List<NoticiaPolitico> ObterNoticiaPoliticoByIdNoticia(int idNoticia)
        {
            return new NoticiaPoliticoDAO().ObterNoticiaPoliticoByIdNoticia(idNoticia);
        }

        public string ExcluirByIdNoticia(int idNoticia)
        {
            return new NoticiaPoliticoDAO().ExcluirByIdNoticia(idNoticia);
        }
    }
}
