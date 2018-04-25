using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    public class DetalhesPartido
    {
        public int IdDetalhesPartido { get; set; }

        public int IdPartido { get; set; }

        public string Endereco { get; set; }

        public int IdCidade { get; set; }

        public int IdEstado { get; set; }

        public string CEP { get; set; }

        public string Telefone { get; set; }

        public string Fax { get; set; }

        public string Site { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }
    }
}
