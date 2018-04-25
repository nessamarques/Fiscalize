

namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.DAO;
    using Puc.Campinas.ProjetoFinalSI.Entidade;

    public class CategoriaBO
    {
        public List<Categoria> ObterCategoria()
        {
            return new CategoriaDAO().ObterCategoria();
        }
        public List<Categoria> ObterCategoriaByIdCategoria(int idCategoria)
        {
            return new CategoriaDAO().ObterCategoriaByIdCategoria(idCategoria);
        }

        public List<Categoria> ObterCategoria(string pString)
        {
            return new CategoriaDAO().ObterCategoria(pString);
        }
        public bool VerificaSeCategoriaEstaSendoUsada(int idCategoria)
        {
            return new CategoriaDAO().VerificaSeCategoriaEstaSendoUsada(idCategoria);
        }

        public string Excluir(Categoria categoria)
        {
            return new CategoriaDAO().Excluir(categoria);
        }
        public bool NomeExiste(Categoria categoria)
        {
            return new CategoriaDAO().NomeExiste(categoria);
        }
        public string Alterar(Categoria categoria)
        {
            return new CategoriaDAO().Alterar(categoria);
        }
        public string Incluir(Categoria categoria)
        {
            return new CategoriaDAO().Incluir(categoria);
        }
    }
}
