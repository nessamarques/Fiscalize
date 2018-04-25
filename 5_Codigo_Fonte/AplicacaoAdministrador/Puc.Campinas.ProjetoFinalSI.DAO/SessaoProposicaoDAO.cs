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

    public class SessaoProposicaoDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public List<SessaoProposicao> ObterSessaoProposicao()
        {
            List<SessaoProposicao> listaSessaoProposicao = new List<SessaoProposicao>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT * FROM SessaoProposicao sp ORDER BY sp.IdSessaoProposicao desc ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtSessaoProposicao = ds.Tables[0];

                        for (int i = 0; i < dtSessaoProposicao.Rows.Count; i++)
                        {
                            SessaoProposicao sessaoProposicao = PopularEntidade(dtSessaoProposicao, i);
                            listaSessaoProposicao.Add(sessaoProposicao);
                        }
                    }
                }
            }

            return listaSessaoProposicao;
        }

        public List<SessaoProposicao> ObterSessaoProposicaoByIdSessao(int idSessao)
        {
            List<SessaoProposicao> sessaoProposicao = new List<SessaoProposicao>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select * FROM SessaoProposicao sp WHERE sp.IdSessao = " + idSessao + "ORDER BY sp.IdSessaoProposicao DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtSessaoProposicao = ds.Tables[0];

                        for (int i = 0; i < dtSessaoProposicao.Rows.Count; i++)
                        {
                            sessaoProposicao.Add(PopularEntidade(dtSessaoProposicao, i));
                        }
                    }
                }
            }

            return sessaoProposicao;
        }

        private static SessaoProposicao PopularEntidade(DataTable dtSessaoProposicao, int i)
        {
            SessaoProposicao sessaoProposicao = new SessaoProposicao();

            if (dtSessaoProposicao.Columns.Contains("IdSessaoProposicao"))
            {
                if (dtSessaoProposicao.Rows[i]["IdSessaoProposicao"] != DBNull.Value)
                {
                    sessaoProposicao.IdSessaoProposicao = Convert.ToInt32(dtSessaoProposicao.Rows[i]["IdSessaoProposicao"]);
                }
            }

            if (dtSessaoProposicao.Columns.Contains("IdSessao"))
            {
                if (dtSessaoProposicao.Rows[i]["IdSessao"] != DBNull.Value)
                {
                    sessaoProposicao.IdSessao = Convert.ToInt32(dtSessaoProposicao.Rows[i]["IdSessao"]);
                }
            }

            if (dtSessaoProposicao.Columns.Contains("IdProposicao"))
            {
                if (dtSessaoProposicao.Rows[i]["IdProposicao"] != DBNull.Value)
                {
                    sessaoProposicao.IdProposicao = Convert.ToInt32(dtSessaoProposicao.Rows[i]["IdProposicao"]);
                    sessaoProposicao.Proposicao = new ProposicaoDAO().ObterProposicaoById(sessaoProposicao.IdProposicao);
                }
            }

            if (dtSessaoProposicao.Columns.Contains("DtInclusao"))
            {
                if (dtSessaoProposicao.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    sessaoProposicao.DtInclusao = Convert.ToDateTime(dtSessaoProposicao.Rows[i]["DtInclusao"]);
                }
            }

            if (dtSessaoProposicao.Columns.Contains("Login"))
            {
                if (dtSessaoProposicao.Rows[i]["Login"] != DBNull.Value)
                {
                    sessaoProposicao.Login = dtSessaoProposicao.Rows[i]["Login"].ToString();
                }
            }

            return sessaoProposicao;
        }
    }
}
