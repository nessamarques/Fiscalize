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

    public class EscolaridadeDAO
    {
        public List<Escolaridade> ObterEscolaridade()
        {
            List<Escolaridade> listEscolaridade = new List<Escolaridade>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT E.IDESCOLARIDADE, UPPER(E.NOME) AS NOME ");
            sbQuery.Append("FROM ESCOLARIDADE E ORDER BY E.NOME ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtEscolaridade = ds.Tables[0];

                        for (int i = 0; i < dtEscolaridade.Rows.Count; i++)
                        {
                            Escolaridade escolaridade = PopularEntidade(dtEscolaridade, i);
                            listEscolaridade.Add(escolaridade);
                        }
                    }
                }
            }

            return listEscolaridade;
        }

        public Escolaridade ObterEscolaridadeById(int idEscolaridade)
        {
            Escolaridade escolaridade = new Escolaridade();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT E.IDESCOLARIDADE, UPPER(E.NOME) AS NOME ");
            sbQuery.Append("FROM ESCOLARIDADE E WHERE E.IDESCOLARIDADE = " + idEscolaridade + " ORDER BY E.NOME ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtEscolaridade = ds.Tables[0];

                        for (int i = 0; i < dtEscolaridade.Rows.Count; i++)
                        {
                            escolaridade = PopularEntidade(dtEscolaridade, i);
                        }
                    }
                }
            }

            return escolaridade;
        }

        public List<Escolaridade> ObterEscolaridade(string pString)
        {
            List<Escolaridade> listEscolaridade = new List<Escolaridade>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT E.IDESCOLARIDADE, UPPER(E.NOME) AS NOME ");
            sbQuery.Append("FROM ESCOLARIDADE E WHERE E.NOME LIKE '%' + '" + @pString + "' + '%' " + " ORDER BY E.NOME ASC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtEscolaridade = ds.Tables[0];

                        for (int i = 0; i < dtEscolaridade.Rows.Count; i++)
                        {
                            Escolaridade escolaridade = PopularEntidade(dtEscolaridade, i);
                            listEscolaridade.Add(escolaridade);
                        }
                    }
                }
            }

            return listEscolaridade;
        }

        private static Escolaridade PopularEntidade(DataTable dtEscolaridade, int i)
        {
            Escolaridade escolaridade = new Escolaridade();

            if (dtEscolaridade.Columns.Contains("IdEscolaridade"))
            {
                if (dtEscolaridade.Rows[i]["IdEscolaridade"] != DBNull.Value)
                {
                    escolaridade.IdEscolaridade = Convert.ToInt32(dtEscolaridade.Rows[i]["IdEscolaridade"].ToString());
                }
            }

            if (dtEscolaridade.Columns.Contains("Nome"))
            {
                if (dtEscolaridade.Rows[i]["Nome"] != DBNull.Value)
                {
                    escolaridade.Nome = Convert.ToString(dtEscolaridade.Rows[i]["Nome"]);
                }
            }

            return escolaridade;
        }
    }
}
