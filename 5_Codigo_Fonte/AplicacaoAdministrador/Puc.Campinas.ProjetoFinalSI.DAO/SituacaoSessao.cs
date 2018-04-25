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

    public class SituacaoSessaoDAO
    {
        public List<SituacaoSessao> ObterSituacaoSessao()
        {
            List<SituacaoSessao> listSituacaoSessao = new List<SituacaoSessao>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT S.IDSituacaoSessao, UPPER(S.NOME) AS NOME ");
            sbQuery.Append("FROM SituacaoSessao S ORDER BY S.Nome ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtSituacaoSessao = ds.Tables[0];

                        for (int i = 0; i < dtSituacaoSessao.Rows.Count; i++)
                        {
                            SituacaoSessao situacaoSessao = PopularEntidade(dtSituacaoSessao, i);
                            listSituacaoSessao.Add(situacaoSessao);
                        }
                    }
                }
            }

            return listSituacaoSessao;
        }

        public SituacaoSessao ObterSituacaoSessaoById(int idSituacaoSessao)
        {
            SituacaoSessao situacaoSessao = new SituacaoSessao();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT S.IDSituacaoSessao,UPPER(S.NOME) AS NOME ");
            sbQuery.Append("FROM SituacaoSessao S WHERE S.IDSituacaoSessao = " + idSituacaoSessao + " ORDER BY S.NOME ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtSituacaoSessao = ds.Tables[0];

                        for (int i = 0; i < dtSituacaoSessao.Rows.Count; i++)
                        {
                            situacaoSessao = PopularEntidade(dtSituacaoSessao, i);
                        }
                    }
                }
            }

            return situacaoSessao;
        }


        private static SituacaoSessao PopularEntidade(DataTable dtSituacaoSessao, int i)
        {
            SituacaoSessao situacaoSessao = new SituacaoSessao();

            if (dtSituacaoSessao.Columns.Contains("IdSituacaoSessao"))
            {
                if (dtSituacaoSessao.Rows[i]["IdSituacaoSessao"] != DBNull.Value)
                {
                    situacaoSessao.IdSituacaoSessao = Convert.ToInt32(dtSituacaoSessao.Rows[i]["IdSituacaoSessao"].ToString());
                }
            }

            if (dtSituacaoSessao.Columns.Contains("Nome"))
            {
                if (dtSituacaoSessao.Rows[i]["Nome"] != DBNull.Value)
                {
                    situacaoSessao.Nome = Convert.ToString(dtSituacaoSessao.Rows[i]["Nome"]);
                }
            }

            return situacaoSessao;
        }
    }
}
