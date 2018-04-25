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

    public class LocalNoticiaDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public List<LocalNoticia> ObterLocalNoticia()
        {
            List<LocalNoticia> listLocalNoticia = new List<LocalNoticia>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT LN.IdLocalNoticia, LN.Nome, LN.Descricao ");
            sbQuery.Append("FROM LocalNoticia LN ORDER BY LN.IDLocalNoticia ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtLocalNoticia = ds.Tables[0];

                        for (int i = 0; i < dtLocalNoticia.Rows.Count; i++)
                        {
                            LocalNoticia localNoticia = PopularEntidade(dtLocalNoticia, i);
                            listLocalNoticia.Add(localNoticia);
                        }
                    }
                }
            }

            return listLocalNoticia;
        }

        public LocalNoticia ObterLocalNoticiaById(int idLocalNoticia)
        {
            LocalNoticia localNoticia = new LocalNoticia();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT LN.IdLocalNoticia, LN.Nome, LN.Descricao ");
            sbQuery.Append("FROM LocalNoticia LN WHERE LN.IdLocalNoticia = " + idLocalNoticia);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtLocalNoticia = ds.Tables[0];

                        for (int i = 0; i < dtLocalNoticia.Rows.Count; i++)
                        {
                            localNoticia = PopularEntidade(dtLocalNoticia, i);
                        }
                    }
                }
            }

            return localNoticia;
        }

        public List<LocalNoticia> ObterLocalNoticia(string pString)
        {
            List<LocalNoticia> listLocalNoticia = new List<LocalNoticia>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT LN.IDLocalNoticia, upper(LN.Nome) as Nome, LN.Descricao ");
            sbQuery.Append("FROM LocalNoticia LN WHERE LN.Nome LIKE '%" + @pString + "%' ORDER BY LN.Nome ASC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtLocalNoticia = ds.Tables[0];

                        for (int i = 0; i < dtLocalNoticia.Rows.Count; i++)
                        {
                            LocalNoticia localNoticia = PopularEntidade(dtLocalNoticia, i);
                            listLocalNoticia.Add(localNoticia);
                        }
                    }
                }
            }

            return listLocalNoticia;
        }

        public string Incluir(LocalNoticia localNoticia)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO LocalNoticia VALUES(@pNome, @pDescricao, @pDtInclusao, @pLogin)";
            command.Parameters.Add(new SqlParameter("@pNome", localNoticia.Nome));
            command.Parameters.Add(new SqlParameter("@pDescricao", localNoticia.Descricao));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", localNoticia.DtInclusao));
            command.Parameters.Add(new SqlParameter("@pLogin", localNoticia.Login));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Alterar(LocalNoticia localNoticia)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "UPDATE LocalNoticia SET Nome = @pNome, Descricao = @pDescricao ";
            command.CommandText += "WHERE IdLocalNoticia = @pIdLocalNoticia";
            command.Parameters.Add(new SqlParameter("@pIdLocalNoticia", localNoticia.IdLocalNoticia));
            command.Parameters.Add(new SqlParameter("@pNome", localNoticia.Nome));
            command.Parameters.Add(new SqlParameter("@pDescricao", localNoticia.Descricao));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Excluir(LocalNoticia localNoticia)
        {
            object ret;

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("DELETE FROM LocalNoticia WHERE IdLocalNoticia = " + localNoticia.IdLocalNoticia);

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

        private static LocalNoticia PopularEntidade(DataTable dtLocalNoticia, int i)
        {
            LocalNoticia localNoticia = new LocalNoticia();

            if (dtLocalNoticia.Columns.Contains("IdLocalNoticia"))
            {
                if (dtLocalNoticia.Rows[i]["IdLocalNoticia"] != DBNull.Value)
                {
                    localNoticia.IdLocalNoticia = Convert.ToInt32(dtLocalNoticia.Rows[i]["IdLocalNoticia"].ToString());
                }
            }

            if (dtLocalNoticia.Columns.Contains("Nome"))
            {
                if (dtLocalNoticia.Rows[i]["Nome"] != DBNull.Value)
                {
                    localNoticia.Nome = dtLocalNoticia.Rows[i]["Nome"].ToString();
                }
            }

            if (dtLocalNoticia.Columns.Contains("Descricao"))
            {
                if (dtLocalNoticia.Rows[i]["Descricao"] != DBNull.Value)
                {
                    localNoticia.Descricao = dtLocalNoticia.Rows[i]["Descricao"].ToString();
                }
            }

            if (dtLocalNoticia.Columns.Contains("DtInclusao"))
            {
                if (dtLocalNoticia.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    localNoticia.DtInclusao = Convert.ToDateTime(dtLocalNoticia.Rows[i]["DtInclusao"]);
                }
            }

            if (dtLocalNoticia.Columns.Contains("Login"))
            {
                if (dtLocalNoticia.Rows[i]["Login"] != DBNull.Value)
                {
                    localNoticia.Login = dtLocalNoticia.Rows[i]["Login"].ToString();
                }
            }

            return localNoticia;
        }
    }
}
