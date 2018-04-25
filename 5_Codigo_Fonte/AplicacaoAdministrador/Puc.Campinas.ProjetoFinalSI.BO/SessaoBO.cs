using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Puc.Campinas.ProjetoFinalSI.DAO;
using Puc.Campinas.ProjetoFinalSI.Entidade;

namespace Puc.Campinas.ProjetoFinalSI.BO
{
    public class SessaoBO
    {
        public string Incluir(Sessao sessao)
        {
            return new SessaoDAO().Incluir(sessao);
        }

        public string Alterar(Sessao sessao)
        {
            return new SessaoDAO().Alterar(sessao);
        }

        public string Excluir(Sessao sessao)
        {
            return new SessaoDAO().Excluir(sessao);
        }

        public Sessao ObterSessaoById(int idSessao)
        {
            return new SessaoDAO().ObterSessaoById(idSessao);
        }

        public List<Sessao> ObterSessoes(string nome = null, string situacao = null, DateTime? inicio = null, DateTime? termino = null)
        {
            return new SessaoDAO().ObterSessoes(nome, situacao, inicio, termino);
        }
    }
}