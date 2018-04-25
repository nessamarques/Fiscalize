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

    public class SituacaoDAO
    {
        public List<Situacao> ObterSituacao()
        {
            List<Situacao> listSituacao = new List<Situacao>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT S.IDSITUACAO, UPPER(S.NOME) AS NOME ");
            sbQuery.Append("FROM SITUACAO S ORDER BY S.IDSITUACAO DESC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtSituacao = ds.Tables[0];

                        for (int i = 0; i < dtSituacao.Rows.Count; i++)
                        {
                            Situacao situacao = PopularEntidade(dtSituacao, i);
                            listSituacao.Add(situacao);
                        }
                    }
                }
            }

            return listSituacao;
        }

        public Situacao ObterSituacaoById(int idSituacao)
        {
            Situacao situacao = new Situacao();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT S.IDSITUACAO, UPPER(S.NOME) AS NOME ");
            sbQuery.Append("FROM SITUACAO S WHERE S.IDSITUACAO = " + idSituacao + "ORDER BY IDSITUACAO DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtSituacao = ds.Tables[0];

                        for (int i = 0; i < dtSituacao.Rows.Count; i++)
                        {
                            situacao = PopularEntidade(dtSituacao, i);
                        }
                    }
                }
            }

            return situacao;
        }

        private static Situacao PopularEntidade(DataTable dtSituacao, int i)
        {
            Situacao situacao = new Situacao();

            if (dtSituacao.Columns.Contains("IdSituacao"))
            {
                if (dtSituacao.Rows[i]["IdSituacao"] != DBNull.Value)
                {
                    situacao.IdSituacao = Convert.ToInt32(dtSituacao.Rows[i]["IdSituacao"].ToString());
                }
            }

            if (dtSituacao.Columns.Contains("Nome"))
            {
                if (dtSituacao.Rows[i]["Nome"] != DBNull.Value)
                {
                    situacao.Nome = Convert.ToString(dtSituacao.Rows[i]["Nome"]);
                }
            }

            return situacao;
        }
    }
}
