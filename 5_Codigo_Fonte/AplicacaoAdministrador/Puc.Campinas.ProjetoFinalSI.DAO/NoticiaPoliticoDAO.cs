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

    public class NoticiaPoliticoDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public List<NoticiaPolitico> ObterNoticiaPolitico()
        {
            List<NoticiaPolitico> listNoticiaPolitico = new List<NoticiaPolitico>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT NP.IdNoticiaPolitico, NP.IdNoticia, NP.IdPolitico ");
            sbQuery.Append("FROM NoticiaPolitico NP ORDER BY NP.IdNoticiaPolitico ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtNoticiaPolitico = ds.Tables[0];

                        for (int i = 0; i < dtNoticiaPolitico.Rows.Count; i++)
                        {
                            NoticiaPolitico noticiaPolitico = PopularEntidade(dtNoticiaPolitico, i);
                            listNoticiaPolitico.Add(noticiaPolitico);
                        }
                    }
                }
            }

            return listNoticiaPolitico;
        }

        public NoticiaPolitico ObterNoticiaPoliticoById(int idNoticiaPolitico)
        {
            NoticiaPolitico noticiaPolitico = new NoticiaPolitico();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT NP.IdNoticiaPolitico, NP.IdNoticia, NP.IdPolitico ");
            sbQuery.Append("FROM NoticiaPolitico NP WHERE NP.IdNoticiaPolitico = " + idNoticiaPolitico);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtNoticiaPolitico = ds.Tables[0];

                        for (int i = 0; i < dtNoticiaPolitico.Rows.Count; i++)
                        {
                            noticiaPolitico = PopularEntidade(dtNoticiaPolitico, i);
                        }
                    }
                }
            }

            return noticiaPolitico;
        }

        public List<NoticiaPolitico> ObterNoticiaPoliticoByIdNoticia(int idNoticia)
        {
            List<NoticiaPolitico> noticiaPoliticos = new List<NoticiaPolitico>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT NP.IdNoticiaPolitico, NP.IdNoticia, NP.IdPolitico ");
            sbQuery.Append("FROM NoticiaPolitico NP WHERE NP.IdNoticia = " + idNoticia);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtNoticiaPolitico = ds.Tables[0];

                        for (int i = 0; i < dtNoticiaPolitico.Rows.Count; i++)
                        {
                            NoticiaPolitico noticiaPolitico = PopularEntidade(dtNoticiaPolitico, i);
                            noticiaPoliticos.Add(noticiaPolitico);
                        }
                    }
                }
            }

            return noticiaPoliticos;
        }

        public string Incluir(NoticiaPolitico noticiaPolitico)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO NoticiaPolitico VALUES(@pIdNoticia, @pIdPolitico, @pDtInclusao, @pLogin)";
            command.Parameters.Add(new SqlParameter("@pIdNoticia", noticiaPolitico.IdNoticia));
            command.Parameters.Add(new SqlParameter("@pIdPolitico", noticiaPolitico.IdPolitico));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", noticiaPolitico.DtInclusao));
            command.Parameters.Add(new SqlParameter("@pLogin", noticiaPolitico.Login));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Alterar(NoticiaPolitico noticiaPolitico)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "UPDATE NoticiaPolitico SET IdNoticia = @pIdNoticia, IdPolitico = @pIdPolitico ";
            command.CommandText += "WHERE IdNoticiaPolitico = @pIdNoticiaPolitico";
            command.Parameters.Add(new SqlParameter("@pIdNoticiaPolitico", noticiaPolitico.IdNoticiaPolitico));
            command.Parameters.Add(new SqlParameter("@pIdNoticia", noticiaPolitico.IdNoticia));
            command.Parameters.Add(new SqlParameter("@pIdPolitico", noticiaPolitico.IdPolitico));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Excluir(NoticiaPolitico noticiaPolitico)
        {
            object ret;

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("DELETE FROM NoticiaPolitico WHERE IdNoticiaPolitico = " + noticiaPolitico.IdNoticiaPolitico);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                try
                {
                    ret = db.ExecuteNonQuery(dbCommand);
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }

            return ret.ToString();
        }

        public string ExcluirByIdNoticia(int idNoticia)
        {
            object ret;

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("DELETE FROM NoticiaPolitico WHERE IdNoticia = " + idNoticia);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                try
                {
                    ret = db.ExecuteNonQuery(dbCommand);
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }

            return ret.ToString();
        }

        private static NoticiaPolitico PopularEntidade(DataTable dtNoticiaPolitico, int i)
        {
            NoticiaPolitico noticiaPolitico = new NoticiaPolitico();

            if (dtNoticiaPolitico.Columns.Contains("IdNoticiaPolitico"))
            {
                if (dtNoticiaPolitico.Rows[i]["IdNoticiaPolitico"] != DBNull.Value)
                {
                    noticiaPolitico.IdNoticiaPolitico = Convert.ToInt32(dtNoticiaPolitico.Rows[i]["IdNoticiaPolitico"].ToString());
                }
            }

            if (dtNoticiaPolitico.Columns.Contains("IdNoticia"))
            {
                if (dtNoticiaPolitico.Rows[i]["IdNoticia"] != DBNull.Value)
                {
                    noticiaPolitico.IdNoticia = Convert.ToInt32(dtNoticiaPolitico.Rows[i]["IdNoticia"].ToString());
                }
            }

            if (dtNoticiaPolitico.Columns.Contains("IdPolitico"))
            {
                if (dtNoticiaPolitico.Rows[i]["IdPolitico"] != DBNull.Value)
                {
                    noticiaPolitico.IdPolitico = Convert.ToInt32(dtNoticiaPolitico.Rows[i]["IdPolitico"].ToString());
                }
            }

            if (dtNoticiaPolitico.Columns.Contains("DtInclusao"))
            {
                if (dtNoticiaPolitico.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    noticiaPolitico.DtInclusao = Convert.ToDateTime(dtNoticiaPolitico.Rows[i]["DtInclusao"]);
                }
            }

            if (dtNoticiaPolitico.Columns.Contains("Login"))
            {
                if (dtNoticiaPolitico.Rows[i]["Login"] != DBNull.Value)
                {
                    noticiaPolitico.Login = dtNoticiaPolitico.Rows[i]["Login"].ToString();
                }
            }

            return noticiaPolitico;
        }
    }
}
