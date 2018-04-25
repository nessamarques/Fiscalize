namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class FuncionalidadeBO
    {
        public List<Funcionalidade> ObterFuncionalidades()
        {
            return new FuncionalidadeDAO().ObterFuncionalidades();
        }

        public Funcionalidade ObterFuncionalidadeById(int idFuncionalidade)
        {
            return new FuncionalidadeDAO().ObterFuncionalidadeById(idFuncionalidade);
        }

        public List<Funcionalidade> ObterFuncionalidades(string pString)
        {
            return new FuncionalidadeDAO().ObterFuncionalidades(pString);
        }

        public string Incluir(Funcionalidade funcionalidade)
        {
            return new FuncionalidadeDAO().Incluir(funcionalidade);
        }

        public string Alterar(Funcionalidade funcionalidade)
        {
            return new FuncionalidadeDAO().Alterar(funcionalidade);
        }

        public string Excluir(Funcionalidade funcionalidade)
        {
            return new FuncionalidadeDAO().Excluir(funcionalidade);
        }

        public bool NomeExiste(Funcionalidade funcionalidade)
        {
            return new FuncionalidadeDAO().NomeExiste(funcionalidade);
        }
    }
}
