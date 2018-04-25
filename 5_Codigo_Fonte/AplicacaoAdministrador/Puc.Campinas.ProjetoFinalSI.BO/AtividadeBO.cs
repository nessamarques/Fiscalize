namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class AtividadeBO
    {
        public List<Atividade> ObterAtividades()
        {
            return new AtividadeDAO().ObterAtividades();
        }

        public Atividade ObterAtividadeById(int idAtividade)
        {
            return new AtividadeDAO().ObterAtividadeById(idAtividade);
        }

        public List<Atividade> ObterAtividadesByIdPolitico(int idPolitico)
        {
            return new AtividadeDAO().ObterAtividadesByIdPolitico(idPolitico);
        }

        public string Incluir(Atividade atividade)
        {
            return new AtividadeDAO().Incluir(atividade);
        }

        public string Alterar(Atividade atividade)
        {
            return new AtividadeDAO().Alterar(atividade);
        }

        public string Excluir(Atividade atividade)
        {
            return new AtividadeDAO().Excluir(atividade);
        }
    }
}
