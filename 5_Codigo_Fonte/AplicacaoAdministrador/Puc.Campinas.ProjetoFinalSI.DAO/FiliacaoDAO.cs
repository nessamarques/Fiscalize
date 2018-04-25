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

    public class FiliacaoDAO
    {
        public Filiacao ObterFiliacaoByIdPolitico(int idPolitico)
        {
            Filiacao filiacao = new Filiacao();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT F.IdFiliacao, F.IdPolitico, upper(F.NomeMae) as NomeMae, upper(F.NomePai) as NomePai ");
            sbQuery.Append("FROM FILIACAO F WHERE F.IDPOLITICO = " + idPolitico );

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtFiliacao = ds.Tables[0];

                        for (int i = 0; i < dtFiliacao.Rows.Count; i++)
                        {
                            filiacao = PopularEntidade(dtFiliacao, i);
                        }
                    }
                }
            }

            return filiacao;
        }

        private static Filiacao PopularEntidade(DataTable dtFiliacao, int i)
        {
            Filiacao filiacao = new Filiacao();

            if (dtFiliacao.Columns.Contains("IdFiliacao"))
            {
                if (dtFiliacao.Rows[i]["IdFiliacao"] != DBNull.Value)
                {
                    filiacao.IdFiliacao = Convert.ToInt32(dtFiliacao.Rows[i]["IdFiliacao"].ToString());
                }
            }

            if (dtFiliacao.Columns.Contains("IdPolitico"))
            {
                if (dtFiliacao.Rows[i]["IdPolitico"] != DBNull.Value)
                {
                    filiacao.IdPolitico = Convert.ToInt32(dtFiliacao.Rows[i]["IdPolitico"]);
                }
            }

            if (dtFiliacao.Columns.Contains("NomeMae"))
            {
                if (dtFiliacao.Rows[i]["NomeMae"] != DBNull.Value)
                {
                    filiacao.NomeMae = dtFiliacao.Rows[i]["NomeMae"].ToString();
                }
            }

            if (dtFiliacao.Columns.Contains("NomePai"))
            {
                if (dtFiliacao.Rows[i]["NomePai"] != DBNull.Value)
                {
                    filiacao.NomePai = dtFiliacao.Rows[i]["NomePai"].ToString();
                }
            }

            if (dtFiliacao.Columns.Contains("Login"))
            {
                if (dtFiliacao.Rows[i]["Login"] != DBNull.Value)
                {
                    filiacao.Login = dtFiliacao.Rows[i]["Login"].ToString();
                }
            }

            if (dtFiliacao.Columns.Contains("DtInclusao"))
            {
                if (dtFiliacao.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    filiacao.DtInclusao = Convert.ToDateTime(dtFiliacao.Rows[i]["DtInclusao"]);
                }
            }

            return filiacao;
        }
    }
}
