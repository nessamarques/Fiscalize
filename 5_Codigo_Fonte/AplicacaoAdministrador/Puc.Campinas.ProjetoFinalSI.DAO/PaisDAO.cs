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

    public class PaisDAO
    {
        public List<Pais> ObterPais()
        {
            List<Pais> listPais = new List<Pais>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT P.IDPAIS,UPPER(P.NOME) AS NOME,UPPER(P.NACIONALIDADE) AS NACIONALIDADE ");
            sbQuery.Append("FROM PAIS P ORDER BY P.IDPAIS DESC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPais = ds.Tables[0];

                        for (int i = 0; i < dtPais.Rows.Count; i++)
                        {
                            Pais pais = PopularEntidade(dtPais, i);
                            listPais.Add(pais);
                        }
                    }
                }
            }

            return listPais;
        }

        public Pais ObterPaisById(int idPais)
        {
            Pais pais = new Pais();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT P.IDPAIS, UPPER(P.NOME) AS NOME, UPPER(P.NACIONALIDADE) AS NACIONALIDADE ");
            sbQuery.Append("FROM PAIS P WHERE P.IDPAIS = " + idPais + "ORDER BY IDPAIS DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPais = ds.Tables[0];

                        for (int i = 0; i < dtPais.Rows.Count; i++)
                        {
                            pais = PopularEntidade(dtPais, i);
                        }
                    }
                }
            }

            return pais;
        }

        public List<Pais> ObterPais(string pString)
        {
            List<Pais> listPais = new List<Pais>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT P.IDPAIS, UPPER(P.NOME) AS NOME, UPPER(P.NACIONALIDADE) AS NACIONALIDADE ");
            sbQuery.Append("FROM PAIS P WHERE P.NOME LIKE '%' + '" + @pString + "' + '%' " + "ORDER BY IDPAIS DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPais = ds.Tables[0];

                        for (int i = 0; i < dtPais.Rows.Count; i++)
                        {
                            Pais pais = PopularEntidade(dtPais, i);
                            listPais.Add(pais);
                        }
                    }
                }
            }

            return listPais;
        }

        private static Pais PopularEntidade(DataTable dtPais, int i)
        {
            Pais pais = new Pais();

            if (dtPais.Columns.Contains("IdPais"))
            {
                if (dtPais.Rows[i]["IdPais"] != DBNull.Value)
                {
                    pais.IdPais = Convert.ToInt32(dtPais.Rows[i]["IdPais"].ToString());
                }
            }

            if (dtPais.Columns.Contains("Nome"))
            {
                if (dtPais.Rows[i]["Nome"] != DBNull.Value)
                {
                    pais.Nome = Convert.ToString(dtPais.Rows[i]["Nome"]);
                }
            }

            if (dtPais.Columns.Contains("Nacionalidade"))
            {
                if (dtPais.Rows[i]["Nacionalidade"] != DBNull.Value)
                {
                    pais.Nacionalidade = Convert.ToString(dtPais.Rows[i]["Nacionalidade"]);
                }
            }

            return pais;
        }
    }
}
