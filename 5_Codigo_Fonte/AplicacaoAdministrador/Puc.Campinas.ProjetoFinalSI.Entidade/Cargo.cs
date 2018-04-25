namespace Puc.Campinas.ProjetoFinalSI.Entidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Classe que especifica os cargos dos políticos do sistema.
    /// </summary>
    public class Cargo
    {
        /// <summary>
        /// Gets or sets do Código do Cargo
        /// </summary>
        public int IdCargo { get; set; }

        /// <summary>
        /// Gets or sets do Nome do Cargo
        /// </summary>      
        public string Nome { get; set; }

        /// <summary>
        /// Gets or sets da Descrição do Cargo
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Gets or sets do Login do Usuário que incluiu o Cargo
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets da Data da inclusão do Cargo.
        /// </summary>
        public DateTime DtInclusao { get; set; }

        }
}
