namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class LocalNoticiaBO
    {
        public List<LocalNoticia> ObterLocalNoticias()
        {
            return new LocalNoticiaDAO().ObterLocalNoticia();
        }

        public LocalNoticia ObterLocalNoticiaById(int idLocalNoticia)
        {
            return new LocalNoticiaDAO().ObterLocalNoticiaById(idLocalNoticia);
        }

        public List<LocalNoticia> ObterLocalNoticia(string pString)
        {
            return new LocalNoticiaDAO().ObterLocalNoticia(pString);
        }

        public string Incluir(LocalNoticia localNoticia)
        {
            return new LocalNoticiaDAO().Incluir(localNoticia);
        }

        public string Alterar(LocalNoticia localNoticia)
        {
            return new LocalNoticiaDAO().Alterar(localNoticia);
        }

        public string Excluir(LocalNoticia localNoticia)
        {
            return new LocalNoticiaDAO().Excluir(localNoticia);
        }
    }
}
