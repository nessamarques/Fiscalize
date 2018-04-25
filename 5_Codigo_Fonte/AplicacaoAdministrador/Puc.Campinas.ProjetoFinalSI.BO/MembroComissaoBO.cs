namespace Puc.Campinas.ProjetoFinalSI.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using Puc.Campinas.ProjetoFinalSI.DAO;

    public class MembroComissaoBO
    {
        public List<MembroComissao> ObterMembrosComissoes()
        {
            return new MembroComissaoBO().ObterMembrosComissoes();
        }

        public MembroComissao ObterMembroComissaoById(int idMembroComissao)
        {
            return new MembroComissaoDAO().ObterMembroComissaoById(idMembroComissao);
        }

        public List<MembroComissao> ObterMembroComissaoByIdPolitico(int idPolitico)
        {
            return new MembroComissaoDAO().ObterMembroComissaoByIdPolitico(idPolitico);
        }

        public List<MembroComissao> ObterMembroComissaoByIdComissao(int idComissao)
        {
            return new MembroComissaoDAO().ObterMembroComissaoByIdComissao(idComissao);
        }

        public MembroComissao ObterMembroComissaoPresidente(int idComissao)
        {
            return new MembroComissaoDAO().ObterMembroComissaoPresidente(idComissao);
        }

        public MembroComissao ObterMembroComissaoVicePresidente1(int idComissao)
        {
            return new MembroComissaoDAO().ObterMembroComissaoVicePresidente1(idComissao);
        }

        public MembroComissao ObterMembroComissaoVicePresidente2(int idComissao)
        {
            return new MembroComissaoDAO().ObterMembroComissaoVicePresidente2(idComissao);
        }

        public MembroComissao ObterMembroComissaoVicePresidente3(int idComissao)
        {
            return new MembroComissaoDAO().ObterMembroComissaoVicePresidente3(idComissao);
        }

        public List<MembroComissao> ObterMembroComissaoTitular(int idComissao)
        {
            return new MembroComissaoDAO().ObterMembroComissaoTitular(idComissao);
        }

        public List<MembroComissao> ObterMembroComissaoSuplente(int idComissao)
        {
            return new MembroComissaoDAO().ObterMembroComissaoSuplente(idComissao);
        }

        public string Incluir(MembroComissao membroComissao)
        {
            return new MembroComissaoDAO().Incluir(membroComissao);
        }

        public string Alterar(MembroComissao membroComissao)
        {
            return new MembroComissaoDAO().Alterar(membroComissao);
        }

        public string Excluir(MembroComissao membroComissao)
        {
            return new MembroComissaoDAO().Excluir(membroComissao);
        }
    }
}
