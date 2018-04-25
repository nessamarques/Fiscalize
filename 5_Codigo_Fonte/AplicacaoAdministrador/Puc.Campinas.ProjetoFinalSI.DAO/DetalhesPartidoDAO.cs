using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using System.Data.Common;
using System.Data;

namespace Puc.Campinas.ProjetoFinalSI.DAO
{
    public class DetalhesPartidoDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public DetalhesPartido ObterDetalhesPartidosByIdPartido(int IdPartido)
        {
           // List<DetalhesPartido> listDetalhesPartidos = new List<DetalhesPartido>();

            DetalhesPartido detalhesPartido = new DetalhesPartido();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT DP.IdDetalhesPartido, DP.IdPartido, DP.Endereco, DP.IdCidade, DP.IdEstado, DP.CEP, DP.Telefone, DP.Fax, DP.Site, DP.Email, DP.Login, DP.DtInclusao ");
            sbQuery.Append("FROM DetalhesPartido DP WHERE DP.IdPartido = " + IdPartido);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtDetalhesPartido = ds.Tables[0];

                        for (int i = 0; i < dtDetalhesPartido.Rows.Count; i++)
                        {
                            detalhesPartido = PopularEntidade(dtDetalhesPartido, i);
                            //listDetalhesPartidos.Add(detalhesPartido);
                        }
                    }
                }
            }

            return detalhesPartido;//listDetalhesPartidos;
        }

        private static DetalhesPartido PopularEntidade(DataTable dtDetalhesPartido, int i)
        {
            DetalhesPartido detalhesPartido = new DetalhesPartido();

            if (dtDetalhesPartido.Columns.Contains("IdDetalhesPartido"))
            {
                if (dtDetalhesPartido.Rows[i]["IdDetalhesPartido"] != DBNull.Value)
                {
                    detalhesPartido.IdDetalhesPartido = Convert.ToInt32(dtDetalhesPartido.Rows[i]["IdDetalhesPartido"].ToString());
                }
            }

            if (dtDetalhesPartido.Columns.Contains("IdPartido"))
            {
                if (dtDetalhesPartido.Rows[i]["IdPartido"] != DBNull.Value)
                {
                    detalhesPartido.IdPartido = Convert.ToInt32(dtDetalhesPartido.Rows[i]["IdPartido"].ToString());
                }
            }

            if (dtDetalhesPartido.Columns.Contains("Endereco"))
            {
                if (dtDetalhesPartido.Rows[i]["Endereco"] != DBNull.Value)
                {
                    detalhesPartido.Endereco = Convert.ToString(dtDetalhesPartido.Rows[i]["Endereco"]);
                }
            }

            if (dtDetalhesPartido.Columns.Contains("IdCidade"))
            {
                if (dtDetalhesPartido.Rows[i]["IdCidade"] != DBNull.Value)
                {
                    detalhesPartido.IdCidade = Convert.ToInt32(dtDetalhesPartido.Rows[i]["IdCidade"].ToString());
                }
            }

            if (dtDetalhesPartido.Columns.Contains("IdEstado"))
            {
                if (dtDetalhesPartido.Rows[i]["IdEstado"] != DBNull.Value)
                {
                    detalhesPartido.IdEstado = Convert.ToInt32(dtDetalhesPartido.Rows[i]["IdEstado"].ToString());
                }
            }

            if (dtDetalhesPartido.Columns.Contains("CEP"))
            {
                if (dtDetalhesPartido.Rows[i]["CEP"] != DBNull.Value)
                {
                    detalhesPartido.CEP = Convert.ToString(dtDetalhesPartido.Rows[i]["CEP"]);
                }
            }

            if (dtDetalhesPartido.Columns.Contains("Telefone"))
            {
                if (dtDetalhesPartido.Rows[i]["Telefone"] != DBNull.Value)
                {
                    detalhesPartido.Telefone = Convert.ToString(dtDetalhesPartido.Rows[i]["Telefone"]);
                }
            }

            if (dtDetalhesPartido.Columns.Contains("Fax"))
            {
                if (dtDetalhesPartido.Rows[i]["Fax"] != DBNull.Value)
                {
                    detalhesPartido.Fax = Convert.ToString(dtDetalhesPartido.Rows[i]["Fax"]);
                }
            }

            if (dtDetalhesPartido.Columns.Contains("Site"))
            {
                if (dtDetalhesPartido.Rows[i]["Site"] != DBNull.Value)
                {
                    detalhesPartido.Site = Convert.ToString(dtDetalhesPartido.Rows[i]["Site"]);
                }
            }

            if (dtDetalhesPartido.Columns.Contains("Email"))
            {
                if (dtDetalhesPartido.Rows[i]["Email"] != DBNull.Value)
                {
                    detalhesPartido.Email = Convert.ToString(dtDetalhesPartido.Rows[i]["Email"]);
                }
            }

            if (dtDetalhesPartido.Columns.Contains("Login"))
            {
                if (dtDetalhesPartido.Rows[i]["Login"] != DBNull.Value)
                {
                    detalhesPartido.Login = Convert.ToString(dtDetalhesPartido.Rows[i]["Login"]);
                }
            }

            if (dtDetalhesPartido.Columns.Contains("DtInclusao"))
            {
                if (dtDetalhesPartido.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    detalhesPartido.DtInclusao = Convert.ToDateTime(dtDetalhesPartido.Rows[i]["DtInclusao"].ToString());
                }
            }

            return detalhesPartido;
        }

    }
}
