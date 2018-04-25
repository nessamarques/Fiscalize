using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Puc.Campinas.ProjetoFinalSI.Web.Models;
using Puc.Campinas.ProjetoFinalSI.BO;
using Puc.Campinas.ProjetoFinalSI.Entidade;

namespace Puc.Campinas.ProjetoFinalSI.Web.Controllers
{
    public class EmailController : Controller
    {
        public ActionResult Index()
        {
            EmailModel model = new EmailModel();

            List<Acompanhamento> listaAcompanhamentos = new AcompanhamentoBO().ObterAcompanhamentos();

            List<Despesa> listaDespesasAcelino = new DespesaBO().ObterTop5DespesasByIdPolitico(2);
            List<Despesa> listaDespesasMarina = new DespesaBO().ObterTop5DespesasByIdPolitico(4);

            model.listaAcompanhamentos = listaAcompanhamentos;

            model.listaDespesasAcelino = listaDespesasAcelino;
            model.listaDespesasMarina = listaDespesasMarina;

            return this.PartialView("Email", model);
        }

    }
}
