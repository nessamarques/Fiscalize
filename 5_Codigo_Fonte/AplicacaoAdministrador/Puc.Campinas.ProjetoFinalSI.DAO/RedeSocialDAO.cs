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

    public class RedeSocialDAO
    {
        public List<RedeSocial> ObterRedeSocialByIdPolitico(int idPolitico)
        {
            List<RedeSocial> listRedeSocial = new List<RedeSocial>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT R.IDREDESOCIAL, R.IDTIPOREDESOCIAL, R.IDPOLITICO, R.ENDERECO ");
            sbQuery.Append("FROM REDESOCIAL R WHERE R.IDPOLITICO = " + idPolitico);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtRedeSocial = ds.Tables[0];

                        for (int i = 0; i < dtRedeSocial.Rows.Count; i++)
                        {
                           RedeSocial redeSocial = PopularEntidade(dtRedeSocial, i);
                           listRedeSocial.Add(redeSocial);
                        }
                    }
                }
            }

            return listRedeSocial;
        }

        private static RedeSocial PopularEntidade(DataTable dtRedeSocial, int i)
        {
            RedeSocial redeSocial = new RedeSocial();

            if (dtRedeSocial.Columns.Contains("IdRedeSocial"))
            {
                if (dtRedeSocial.Rows[i]["IdRedeSocial"] != DBNull.Value)
                {
                    redeSocial.IdRedeSocial = Convert.ToInt32(dtRedeSocial.Rows[i]["IdRedeSocial"].ToString());
                }
            }

            if (dtRedeSocial.Columns.Contains("IdTipoRedeSocial"))
            {
                if (dtRedeSocial.Rows[i]["IdTipoRedeSocial"] != DBNull.Value)
                {
                    redeSocial.IdTipoRedeSocial = Convert.ToInt32(dtRedeSocial.Rows[i]["IdTipoRedeSocial"]);
                }
            }

            if (dtRedeSocial.Columns.Contains("IdPolitico"))
            {
                if (dtRedeSocial.Rows[i]["IdPolitico"] != DBNull.Value)
                {
                    redeSocial.IdPolitico = Convert.ToInt32(dtRedeSocial.Rows[i]["IdPolitico"]);
                }
            }

            if (dtRedeSocial.Columns.Contains("Endereco"))
            {
                if (dtRedeSocial.Rows[i]["Endereco"] != DBNull.Value)
                {
                    redeSocial.Endereco = dtRedeSocial.Rows[i]["Endereco"].ToString();
                }
            }

            if (dtRedeSocial.Columns.Contains("Login"))
            {
                if (dtRedeSocial.Rows[i]["Login"] != DBNull.Value)
                {
                    redeSocial.Login = dtRedeSocial.Rows[i]["Login"].ToString();
                }
            }

            if (dtRedeSocial.Columns.Contains("DtInclusao"))
            {
                if (dtRedeSocial.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    redeSocial.DtInclusao = Convert.ToDateTime(dtRedeSocial.Rows[i]["DtInclusao"]);
                }
            }

            return redeSocial;
        }
    }
}
