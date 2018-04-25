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

    public class AtividadeDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public List<Atividade> ObterAtividades()
        {
            List<Atividade> listAtividades = new List<Atividade>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select a.IdAtividade, a.IdPolitico, upper(a.NomeComissao) as NomeComissao, a.IdSituacao, UPPER(s.Nome) as NomeSituacao, a.DtInicio, a.DtFim ");
            sbQuery.Append("from Atividade a, Situacao s ");
            sbQuery.Append("where a.IdSituacao = s.IdSituacao order by a.IdAtividade desc");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtAtividades = ds.Tables[0];

                        for (int i = 0; i < dtAtividades.Rows.Count; i++)
                        {
                            Atividade atividade = PopularEntidade(dtAtividades, i);
                            listAtividades.Add(atividade);
                        }
                    }
                }
            }

            return listAtividades;
        }

        public List<Atividade> ObterAtividadesByIdPolitico(int idPolitico)
        {
            List<Atividade> listAtividades = new List<Atividade>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select a.*, s.Nome as NomeSituacao, p.Nome as NomePolitico ");
            sbQuery.Append(" from Atividade a");
            sbQuery.Append(" inner join Politico p on p.IdPolitico = a.IdPolitico");
            sbQuery.Append(" inner join Situacao s on a.IdSituacao = s.IdSituacao");
            sbQuery.Append(" where p.IdPolitico =  " + idPolitico);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtAtividades = ds.Tables[0];

                        for (int i = 0; i < dtAtividades.Rows.Count; i++)
                        {
                            Atividade atividade = PopularEntidade(dtAtividades, i);
                            listAtividades.Add(atividade);
                        }
                    }
                }
            }

            return listAtividades;
        }

        public Atividade ObterAtividadeById(int idAtividade)
        {
            Atividade atividade = new Atividade();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT A.IDATIVIDADE, A.IDPOLITICO, UPPER(A.NOMECOMISSAO) as NomeComissao, A.IDSITUACAO, UPPER(S.NOME) as NomeSituacao , A.DTINICIO, A.DTFIM ");
            sbQuery.Append("FROM ATIVIDADE A, SITUACAO S ");
            sbQuery.Append("WHERE A.IDATIVIDADE = " + idAtividade + " AND A.IDSITUACAO = S.IDSITUACAO ");
            sbQuery.Append("ORDER BY IDATIVIDADE DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtAtividade = ds.Tables[0];

                        for (int i = 0; i < dtAtividade.Rows.Count; i++)
                        {
                            atividade = PopularEntidade(dtAtividade, i);
                        }
                    }
                }
            }

            return atividade;
        }

        public string Incluir(Atividade atividade)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO ATIVIDADE VALUES(@pIdPolitico, @pNomeComissao, @pIdSituacao, @pDtInicio, @pDtFim, @pLogin, @pDtInclusao)";
            command.Parameters.Add(new SqlParameter("@pIdPolitico", atividade.IdPolitico));
            command.Parameters.Add(new SqlParameter("@pNomeComissao", atividade.NomeComissao));
            command.Parameters.Add(new SqlParameter("@pIdSituacao", atividade.IdSituacao));
            command.Parameters.Add(new SqlParameter("@pDtInicio", atividade.DtInicio));
            command.Parameters.Add(new SqlParameter("@pDtFim", atividade.DtFim));
            command.Parameters.Add(new SqlParameter("@pLogin", atividade.Login));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", atividade.DtInclusao));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Alterar(Atividade atividade)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "UPDATE ATIVIDADE SET IdPolitico = @pIdPolitico, NomeComissao = @pNomeComissao, IdSituacao = @pIdSituacao, DtInicio = @DtInicio, DtFim = @DtFim ";
            command.CommandText += "WHERE IdAtividade = @pIdAtividade";
            command.Parameters.Add(new SqlParameter("@pIdAtividade", atividade.IdAtividade));
            command.Parameters.Add(new SqlParameter("@pIdPolitico", atividade.IdPolitico));
            command.Parameters.Add(new SqlParameter("@pNomeComissao", atividade.NomeComissao));
            command.Parameters.Add(new SqlParameter("@pIdSituacao", atividade.IdSituacao));
            command.Parameters.Add(new SqlParameter("@DtInicio", atividade.DtInicio));
            command.Parameters.Add(new SqlParameter("@DtFim", atividade.DtFim));            
            
            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Excluir(Atividade atividade)
        {
            object ret;

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("DELETE FROM ATIVIDADE WHERE IDATIVIDADE = " + atividade.IdAtividade);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                ret = db.ExecuteNonQuery(dbCommand);
            }

            return ret.ToString();
        }

        private static Atividade PopularEntidade(DataTable dtAtividade, int i)
        {
            Atividade atividade = new Atividade();

            if (dtAtividade.Columns.Contains("IdAtividade"))
            {
                if (dtAtividade.Rows[i]["IdAtividade"] != DBNull.Value)
                {
                    atividade.IdAtividade = Convert.ToInt32(dtAtividade.Rows[i]["IdAtividade"].ToString());
                }
            }

            if (dtAtividade.Columns.Contains("IdPolitico"))
            {
                if (dtAtividade.Rows[i]["IdPolitico"] != DBNull.Value)
                {
                    atividade.IdPolitico = Convert.ToInt32(dtAtividade.Rows[i]["IdPolitico"].ToString());
                }
            }

            if (dtAtividade.Columns.Contains("NomeComissao"))
            {
                if (dtAtividade.Rows[i]["NomeComissao"] != DBNull.Value)
                {
                    atividade.NomeComissao = Convert.ToString(dtAtividade.Rows[i]["NomeComissao"]);
                }
            }

            if (dtAtividade.Columns.Contains("IdSituacao"))
            {
                if (dtAtividade.Rows[i]["IdSituacao"] != DBNull.Value)
                {
                    atividade.IdSituacao = Convert.ToInt32(dtAtividade.Rows[i]["IdSituacao"].ToString());
                }
            }

            if (dtAtividade.Columns.Contains("NomeSituacao"))
            {
                if (dtAtividade.Rows[i]["NomeSituacao"] != DBNull.Value)
                {
                    atividade.NomeSituacao = Convert.ToString(dtAtividade.Rows[i]["NomeSituacao"]);
                }
            }

            if (dtAtividade.Columns.Contains("DtInicio"))
            {
                if (dtAtividade.Rows[i]["DtInicio"] != DBNull.Value)
                {
                    atividade.DtInicio = Convert.ToDateTime(dtAtividade.Rows[i]["DtInicio"]);
                }
            }

            if (dtAtividade.Columns.Contains("DtFim"))
            {
                if (dtAtividade.Rows[i]["DtFim"] != DBNull.Value)
                {
                    atividade.DtFim = Convert.ToDateTime(dtAtividade.Rows[i]["DtFim"]);
                }
            }

            if (dtAtividade.Columns.Contains("Login"))
            {
                if (dtAtividade.Rows[i]["Login"] != DBNull.Value)
                {
                    atividade.Login = Convert.ToString(dtAtividade.Rows[i]["Login"]);
                }
            }

            if (dtAtividade.Columns.Contains("DtInclusao"))
            {
                if (dtAtividade.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    atividade.DtInclusao = Convert.ToDateTime(dtAtividade.Rows[i]["DtInclusao"].ToString());
                }
            }

            return atividade;
        }
    }
}
