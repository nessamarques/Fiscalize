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

    public class TipoRedeSocialDAO
    {
        public List<TipoRedeSocial> ObterTipoRedeSocial()
        {
            List<TipoRedeSocial> listTipoRedeSocial = new List<TipoRedeSocial>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT tr.IdTipoRedeSocial, upper(tr.Nome) as Nome, tr.Endereco ");
            sbQuery.Append("FROM TipoRedeSocial tr ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtTipoRedeSocial = ds.Tables[0];

                        for (int i = 0; i < dtTipoRedeSocial.Rows.Count; i++)
                        {
                            TipoRedeSocial tipoRedeSocial = PopularEntidade(dtTipoRedeSocial, i);
                            listTipoRedeSocial.Add(tipoRedeSocial);
                        }
                    }
                }
            }

            return listTipoRedeSocial;
        }
        
        public TipoRedeSocial ObterTipoRedeSocialById(int idTipoRedeSocial)
        {
            TipoRedeSocial tipoRedeSocial = new TipoRedeSocial();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT tr.IdTipoRedeSocial, tr.Nome, tr.Endereco ");
            sbQuery.Append("FROM TipoRedeSocial tr WHERE tr.IdTipoRedeSocial = " + idTipoRedeSocial);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtTipoRedeSocial = ds.Tables[0];

                        for (int i = 0; i < dtTipoRedeSocial.Rows.Count; i++)
                        {
                            tipoRedeSocial = PopularEntidade(dtTipoRedeSocial, i);
                        }
                    }
                }
            }

            return tipoRedeSocial;
        }

        private static TipoRedeSocial PopularEntidade(DataTable dtTipoRedeSocial, int i)
        {
            TipoRedeSocial tipoRedeSocial = new TipoRedeSocial();

            if (dtTipoRedeSocial.Columns.Contains("IdTipoRedeSocial"))
            {
                if (dtTipoRedeSocial.Rows[i]["IdTipoRedeSocial"] != DBNull.Value)
                {
                    tipoRedeSocial.IdTipoRedeSocial = Convert.ToInt32(dtTipoRedeSocial.Rows[i]["IdTipoRedeSocial"].ToString());
                }
            }

            if (dtTipoRedeSocial.Columns.Contains("Nome"))
            {
                if (dtTipoRedeSocial.Rows[i]["Nome"] != DBNull.Value)
                {
                    tipoRedeSocial.Nome = dtTipoRedeSocial.Rows[i]["Nome"].ToString();
                }
            }

            if (dtTipoRedeSocial.Columns.Contains("Endereco"))
            {
                if (dtTipoRedeSocial.Rows[i]["Endereco"] != DBNull.Value)
                {
                    tipoRedeSocial.Endereco = dtTipoRedeSocial.Rows[i]["Endereco"].ToString();
                }
            }

            if (dtTipoRedeSocial.Columns.Contains("Login"))
            {
                if (dtTipoRedeSocial.Rows[i]["Login"] != DBNull.Value)
                {
                    tipoRedeSocial.Login = dtTipoRedeSocial.Rows[i]["Login"].ToString();
                }
            }

            if (dtTipoRedeSocial.Columns.Contains("DtInclusao"))
            {
                if (dtTipoRedeSocial.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    tipoRedeSocial.DtInclusao = Convert.ToDateTime(dtTipoRedeSocial.Rows[i]["DtInclusao"]);
                }
            }

            return tipoRedeSocial;
        }
    }
}
