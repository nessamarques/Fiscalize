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

    public class ProfissaoDAO
    {
        public List<Profissao> ObterProfissaoByIdPolitico(int idPolitico)
        {
            List<Profissao> profissao = new List<Profissao>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT P.IdProfissao, P.IdPolitico, upper(P.Nome) as Nome ");
            sbQuery.Append("FROM PROFISSAO P WHERE P.IDPOLITICO = " + idPolitico);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtProfissao = ds.Tables[0];

                        for (int i = 0; i < dtProfissao.Rows.Count; i++)
                        {
                            profissao.Add(PopularEntidade(dtProfissao, i));
                        }
                    }
                }
            }

            return profissao;
        }

        private static Profissao PopularEntidade(DataTable dtProfissao, int i)
        {
            Profissao profissao = new Profissao();

            if (dtProfissao.Columns.Contains("IdProfissao"))
            {
                if (dtProfissao.Rows[i]["IdProfissao"] != DBNull.Value)
                {
                    profissao.IdProfissao = Convert.ToInt32(dtProfissao.Rows[i]["IdProfissao"].ToString());
                }
            }

            if (dtProfissao.Columns.Contains("IdPolitico"))
            {
                if (dtProfissao.Rows[i]["IdPolitico"] != DBNull.Value)
                {
                    profissao.IdPolitico = Convert.ToInt32(dtProfissao.Rows[i]["IdPolitico"]);
                }
            }

            if (dtProfissao.Columns.Contains("Nome"))
            {
                if (dtProfissao.Rows[i]["Nome"] != DBNull.Value)
                {
                    profissao.Nome = dtProfissao.Rows[i]["Nome"].ToString();
                }
            }

            if (dtProfissao.Columns.Contains("Login"))
            {
                if (dtProfissao.Rows[i]["Login"] != DBNull.Value)
                {
                    profissao.Login = dtProfissao.Rows[i]["Login"].ToString();
                }
            }

            if (dtProfissao.Columns.Contains("DtInclusao"))
            {
                if (dtProfissao.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    profissao.DtInclusao = Convert.ToDateTime(dtProfissao.Rows[i]["DtInclusao"]);
                }
            }

            return profissao;
        }
    }
}
