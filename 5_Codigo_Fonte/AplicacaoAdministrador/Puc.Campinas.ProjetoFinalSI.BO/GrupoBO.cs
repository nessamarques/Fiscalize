namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class GrupoBO
    {
        public List<Grupo> ObterGrupos()
        {
            return new GrupoDAO().ObterGrupos();
        }

        public Grupo ObterGrupoById(int idGrupo)
        {
            return new GrupoDAO().ObterGrupoById(idGrupo);
        }

        public List<Grupo> ObterGrupos(string pString)
        {
            return new GrupoDAO().ObterGrupos(pString);
        }

        public string Incluir(Grupo grupo)
        {
            return new GrupoDAO().Incluir(grupo);
        }

        public string Alterar(Grupo grupo)
        {
            return new GrupoDAO().Alterar(grupo);
        }

        public string Excluir(Grupo grupo)
        {
            return new GrupoDAO().Excluir(grupo);
        }

        public bool NomeExiste(Grupo grupo)
        {
            return new GrupoDAO().NomeExiste(grupo);
        }
    }
}
