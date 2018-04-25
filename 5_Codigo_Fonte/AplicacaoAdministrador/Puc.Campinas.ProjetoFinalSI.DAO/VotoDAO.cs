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

    public class VotoDAO
    {
        public List<Voto> ObterVoto()
        {
            List<Voto> listVoto = new List<Voto>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT V.IDVOTO, UPPER(V.NOME) AS NOME ");
            sbQuery.Append("FROM VOTO V ORDER BY V.Nome ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtVoto = ds.Tables[0];

                        for (int i = 0; i < dtVoto.Rows.Count; i++)
                        {
                            Voto voto = PopularEntidade(dtVoto, i);
                            listVoto.Add(voto);
                        }
                    }
                }
            }

            return listVoto;
        }

        public Voto ObterVotoById(int idVoto)
        {
            Voto voto = new Voto();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT V.IDVOTO, UPPER(V.NOME) AS NOME ");
            sbQuery.Append("FROM VOTO V WHERE V.IDVOTO = " + idVoto + " ORDER BY V.NOME ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtVoto = ds.Tables[0];

                        for (int i = 0; i < dtVoto.Rows.Count; i++)
                        {
                            voto = PopularEntidade(dtVoto, i);
                        }
                    }
                }
            }

            return voto;
        }


        private static Voto PopularEntidade(DataTable dtVoto, int i)
        {
            Voto voto = new Voto();

            if (dtVoto.Columns.Contains("IdVoto"))
            {
                if (dtVoto.Rows[i]["IdVoto"] != DBNull.Value)
                {
                    voto.IdVoto = Convert.ToInt32(dtVoto.Rows[i]["IdVoto"].ToString());
                }
            }

            if (dtVoto.Columns.Contains("Nome"))
            {
                if (dtVoto.Rows[i]["Nome"] != DBNull.Value)
                {
                    voto.Nome = Convert.ToString(dtVoto.Rows[i]["Nome"]);
                }
            }

            return voto;
        }
    }
}
