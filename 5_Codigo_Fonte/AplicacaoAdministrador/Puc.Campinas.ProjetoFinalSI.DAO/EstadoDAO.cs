namespace Puc.Campinas.ProjetoFinalSI.DAO
{
    using System;
    using System.Text;
    using System.Data;
    using System.Data.Common;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Collections.Generic;
    using Puc.Campinas.ProjetoFinalSI.Entidade;
    using System.Configuration;
    using System.Data.SqlClient;

    public class EstadoDAO
    {
        public List<Estado> ObterEstado()
        {
            List<Estado> listEstado = new List<Estado>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT E.IDESTADO, E.IDPAIS, UPPER(E.SIGLA) AS SIGLA, UPPER(E.NOME) AS NOME ");
            sbQuery.Append("FROM ESTADO E ORDER BY E.SIGLA ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtEstado = ds.Tables[0];

                        for (int i = 0; i < dtEstado.Rows.Count; i++)
                        {
                            Estado estado = PopularEntidade(dtEstado, i);
                            listEstado.Add(estado);
                        }
                    }
                }
            }

            return listEstado;
        }

        public Estado ObterEstadoById(int idEstado)
        {
            Estado estado = new Estado();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT E.IDESTADO, E.IDPAIS, UPPER(E.SIGLA) AS SIGLA, UPPER(E.NOME) AS NOME ");
            sbQuery.Append("FROM ESTADO E WHERE E.IDESTADO = " + idEstado + "ORDER BY IDESTADO DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtEstado = ds.Tables[0];

                        for (int i = 0; i < dtEstado.Rows.Count; i++)
                        {
                            estado = PopularEntidade(dtEstado, i);
                        }
                    }
                }
            }

            return estado;
        }

        public List<Estado> ObterEstado(string pString)
        {
            List<Estado> listEstado = new List<Estado>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT E.IDESTADO, E.IDPAIS, UPPER(E.SIGLA) as SIGLA, UPPER(E.NOME) AS NOME ");
            sbQuery.Append("FROM ESTADO E WHERE E.SIGLA LIKE '%' + '" + @pString + "' + '%' OR E.NOME LIKE '%' + '" + @pString + "' + '%' " + "ORDER BY IDESTADO DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtEstado = ds.Tables[0];

                        for (int i = 0; i < dtEstado.Rows.Count; i++)
                        {
                            Estado estado = PopularEntidade(dtEstado, i);
                            listEstado.Add(estado);
                        }
                    }
                }
            }

            return listEstado;
        }

        private static Estado PopularEntidade(DataTable dtEstado, int i)
        {
            Estado estado = new Estado();

            if (dtEstado.Columns.Contains("IdEstado"))
            {
                if (dtEstado.Rows[i]["IdEstado"] != DBNull.Value)
                {
                    estado.IdEstado = Convert.ToInt32(dtEstado.Rows[i]["IdEstado"].ToString());
                }
            }

            if (dtEstado.Columns.Contains("IdPais"))
            {
                if (dtEstado.Rows[i]["IdPais"] != DBNull.Value)
                {
                    estado.IdPais = Convert.ToInt32(dtEstado.Rows[i]["IdPais"].ToString());
                }
            }

            if (dtEstado.Columns.Contains("Sigla"))
            {
                if (dtEstado.Rows[i]["Sigla"] != DBNull.Value)
                {
                    estado.Sigla = Convert.ToString(dtEstado.Rows[i]["Sigla"]);
                }
            }

            if (dtEstado.Columns.Contains("Nome"))
            {
                if (dtEstado.Rows[i]["Nome"] != DBNull.Value)
                {
                    estado.Nome = Convert.ToString(dtEstado.Rows[i]["Nome"]);
                }
            }

            return estado;
        }
    }
}
