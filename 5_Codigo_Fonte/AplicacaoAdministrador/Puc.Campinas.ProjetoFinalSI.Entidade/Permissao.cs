namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Permissao
    {
        public int IdPermissao { get; set; }

        public int IdGrupo { get; set; }

        public int IdFuncionalidade { get; set; }

        public bool PermissaoIncluir { get; set; }

        public bool PermissaoAlterar { get; set; }
        
        public bool PermissaoConsultar { get; set; }
        
        public bool PermissaoExcluir { get; set; }

        public string NomeFuncionalidade { get; set; }

        public string NomeGrupo { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }

    }
}
