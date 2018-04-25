namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Classe que especifica os partidos políticos do sistema.
    /// </summary>
    public class Partido
    {
        /// <summary>
        /// Gets or sets do Código do Partido
        /// </summary>
        public int IdPartido { get; set; }
       
        /// <summary>
        /// Gets or sets da sigla do Partido
        /// </summary>
        public string Sigla { get; set; }

        /// <summary>
        /// Gets or sets do Nome do Partido
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Gets or sets do Deferimento do Partido
        /// </summary>
        public DateTime Deferimento { get; set; }

        /// <summary>
        /// Gets or sets do Presidente Nacional do Partido
        /// </summary>
        public string PresidenteNacional { get; set; }

        /// <summary>
        /// Gets or sets do Numero do Partido
        /// </summary>
        public int NroPartido { get; set; }

        /// <summary>
        /// Gets or sets do Login do Usuário que incluiu o Partido
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets da Data da inclusão do Partido.
        /// </summary>
        public DateTime DtInclusao { get; set; }
    }
}
