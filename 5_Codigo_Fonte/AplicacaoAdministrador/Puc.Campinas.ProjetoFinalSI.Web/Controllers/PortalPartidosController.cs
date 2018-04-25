using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Puc.Campinas.ProjetoFinalSI.BO;
using System.Data;
using Puc.Campinas.ProjetoFinalSI.Web.Models;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class PortalPartidosController : Controller
    {
        public ActionResult Index()
        {
            PartidoModel model = new PartidoModel();

            List<Partido> listaPartidos = new PartidoBO().ObterPartidos();
            
            model.ListaPartidos = listaPartidos;

            return this.PartialView("Partidos", model);
        }


        public ActionResult DetalhesPartido(int idPartido)
        {
            DetalhesPartido detalhesPartido = new DetalhesPartidoBO().ObterDetalhesPartidoByIdPartido(idPartido);

            DetalhesPartidoModel model = new DetalhesPartidoModel(detalhesPartido);

            model.IdDetalhesPartido = detalhesPartido.IdDetalhesPartido;
            model.IdPartido = detalhesPartido.IdPartido;
            model.Endereco = detalhesPartido.Endereco;
            model.IdCidade = detalhesPartido.IdCidade;
            model.IdEstado = detalhesPartido.IdEstado;
            model.CEP = detalhesPartido.CEP;
            model.Telefone = detalhesPartido.Telefone;
            model.Fax = detalhesPartido.Fax;
            model.Site = detalhesPartido.Site;
            model.Email = detalhesPartido.Email;
            model.Login = detalhesPartido.Login;
            model.DtInclusao = detalhesPartido.DtInclusao;

            return this.PartialView("_DetalhesPartido", model);
        }

    }
}
