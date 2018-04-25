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

    public class TipoProposicaoDAO
    {
        public List<TipoProposicao> ObterTipoProposicao()
        {
            List<TipoProposicao> listTipoProposicao = new List<TipoProposicao>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT TP.IDTIPOPROPOSICAO, UPPER(TP.NOME) AS NOME ");
            sbQuery.Append("FROM TIPOPROPOSICAO TP ORDER BY TP.Nome ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtTipoProposicao = ds.Tables[0];

                        for (int i = 0; i < dtTipoProposicao.Rows.Count; i++)
                        {
                            TipoProposicao tipoProposicao = PopularEntidade(dtTipoProposicao, i);
                            listTipoProposicao.Add(tipoProposicao);
                        }
                    }
                }
            }

            return listTipoProposicao;
        }

        public TipoProposicao ObterTipoProposicaoById(int idTipoProposicao)
        {
            TipoProposicao tipoProposicao = new TipoProposicao();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT TP.IDTIPOPROPOSICAO, UPPER(TP.NOME) AS NOME ");
            sbQuery.Append("FROM TIPOPROPOSICAO TP WHERE TP.IDTIPOPROPOSICAO = " + idTipoProposicao + " ORDER BY TP.NOME ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtTipoProposicao = ds.Tables[0];

                        for (int i = 0; i < dtTipoProposicao.Rows.Count; i++)
                        {
                            tipoProposicao = PopularEntidade(dtTipoProposicao, i);
                        }
                    }
                }
            }

            return tipoProposicao;
        }


        private static TipoProposicao PopularEntidade(DataTable dtTipoProposicao, int i)
        {
            TipoProposicao tipoProposicao = new TipoProposicao();

            if (dtTipoProposicao.Columns.Contains("IdTipoProposicao"))
            {
                if (dtTipoProposicao.Rows[i]["IdTipoProposicao"] != DBNull.Value)
                {
                    tipoProposicao.IdTipoProposicao = Convert.ToInt32(dtTipoProposicao.Rows[i]["IdTipoProposicao"].ToString());
                }
            }

            if (dtTipoProposicao.Columns.Contains("Nome"))
            {
                if (dtTipoProposicao.Rows[i]["Nome"] != DBNull.Value)
                {
                    tipoProposicao.Nome = Convert.ToString(dtTipoProposicao.Rows[i]["Nome"]);
                }
            }

            return tipoProposicao;
        }
    }
}
