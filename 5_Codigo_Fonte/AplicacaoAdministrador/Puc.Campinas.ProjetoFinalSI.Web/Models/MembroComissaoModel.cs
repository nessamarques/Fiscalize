using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class MembroComissaoModel
    {
        public MembroComissaoModel()
        {
            this.ListaMembrosComissoes = new List<MembroComissao>();
            this.FiltroPesquisa = string.Empty;

            this.ListaPoliticos = new PoliticoBO().ObterPoliticos();
            this.ListaCargosComissoes = new CargoComissaoBO().ObterCargoComissao();
            this.ListaComissoes = new ComissaoBO().ObterComissao();
        }

        public MembroComissaoModel(MembroComissao membroComissao)
        {
            this.IdMembroComissao = membroComissao.IdMembroComissao;
            this.IdPolitico = membroComissao.IdPolitico;
            this.IdCargoComissao = membroComissao.IdCargoComissao;
            this.IdComissao = membroComissao.IdComissao;
            this.DtInclusao = membroComissao.DtInclusao;
            this.Login = membroComissao.Login;

            this.FiltroPesquisa = string.Empty;

            MembroComissaoBO membroComissaoBO = new MembroComissaoBO();
            this.ListaMembrosComissoes = membroComissaoBO.ObterMembroComissaoByIdComissao(IdComissao);

            this.ListaPoliticos = new PoliticoBO().ObterPoliticos();
            this.ListaComissoes = new ComissaoBO().ObterComissao();
            this.ListaCargosComissoes = new CargoComissaoBO().ObterCargoComissao();
        }

        public int IdMembroComissao { get; set; }

        [Required(ErrorMessage = "O preenchimento do campo Político é obrigatório")]
        public int IdPolitico { get; set; }

        [Required(ErrorMessage = "O preenchimento do campo Cargo de comissão é obrigatório")]
        public int IdCargoComissao { get; set; }

        [Required(ErrorMessage = "O preenchimento do campo Comissão é obrigatório")]
        public int IdComissao { get; set; }

        public DateTime DtInclusao { get; set; }

        public string Login { get; set; }
        

        public List<MembroComissao> ListaMembrosComissoes { get; set; }

        public string ActionRequired { get; set; }

        public string FiltroPesquisa { get; set; }

        public List<Politico> ListaPoliticos { get; set; }

        public List<CargoComissao> ListaCargosComissoes { get; set; }

        public List<Comissao> ListaComissoes { get; set; }

        public string NomePolitico { get; set; }

        public string NomeComissao { get; set; }

        public string SiglaComissao { get; set; }

        public string NomeCargoComissao { get; set; }

        public string NomePresidente { get; set; }

        public string VicePresidente1 { get; set; }

        public string VicePresidente2 { get; set; }

        public string VicePresidente3 { get; set; }

        public List<MembroComissao> listaTitulares { get; set; }

        public List<MembroComissao> listaSuplentes { get; set; }

        public string Secretario { get; set; }

        public string Local { get; set; }

        public string Telefone { get; set; }

        public string Fax { get; set; }

        public string NomeCargo { get; set; }
    }
}
