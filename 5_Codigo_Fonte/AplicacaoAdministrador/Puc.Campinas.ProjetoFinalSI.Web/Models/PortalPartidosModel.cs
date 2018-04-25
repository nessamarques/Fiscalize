using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;

namespace Puc.Campinas.ProjetoFinalSI.Web.Models
{
    public class PortalPartidosModel
    {

        public PortalPartidosModel()
        {
            this.ListaPartidos = new PartidoBO().ObterPartidos();
        }

        public List<Partido> ListaPartidos { get; set; }

        public int IdPartido { get; set; }

        public string Sigla { get; set; }

        public string Nome { get; set; }
        
        public DateTime Deferimento { get; set; }

        public string PresidenteNacional { get; set; }

        public int NroPartido { get; set; }

        public string Login { get; set; }

        public DateTime DtInclusao { get; set; }
    }
}