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

    public class NoticiaDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public List<Noticia> ObterNoticia()
        {
            List<Noticia> listNoticia = new List<Noticia>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT N.IdNoticia, N.Titulo, N.Subtitulo, N.Resumo, N.Conteudo, N.IdLocalNoticia, N.LinkVideo, N.Ativo, N.DtInclusao, N.Login, N.Fonte, N.DtNoticia ");
            sbQuery.Append("FROM NOTICIA N ORDER BY N.IDNOTICIA DESC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtNoticia = ds.Tables[0];

                        for (int i = 0; i < dtNoticia.Rows.Count; i++)
                        {
                            Noticia noticia = PopularEntidade(dtNoticia, i);
                            listNoticia.Add(noticia);
                        }
                    }
                }
            }

            return listNoticia;
        }

        public Noticia ObterNoticiaById(int idNoticia)
        {
            Noticia noticia = new Noticia();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT N.IdNoticia, N.Titulo, N.Subtitulo, N.Resumo, N.Conteudo, N.IdLocalNoticia, N.LinkVideo, N.Ativo, N.DtInclusao, N.Login, N.Fonte, N.DtNoticia ");
            sbQuery.Append("FROM NOTICIA N WHERE N.IdNoticia = " + idNoticia);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtNoticia = ds.Tables[0];

                        for (int i = 0; i < dtNoticia.Rows.Count; i++)
                        {
                            noticia = PopularEntidade(dtNoticia, i);
                        }
                    }
                }
            }

            return noticia;
        }

        public List<Noticia> ObterNoticia(string pString)
        {
            List<Noticia> listNoticia = new List<Noticia>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT N.IdNoticia, N.Titulo, N.Subtitulo, N.Resumo, N.Conteudo, N.IdLocalNoticia, N.LinkVideo, N.Ativo, N.DtInclusao, N.Login, N.Fonte, N.DtNoticia ");
            sbQuery.Append("FROM NOTICIA N WHERE N.TITULO LIKE '%" + @pString + "%' ORDER BY N.IDNOTICIA DESC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtNoticia = ds.Tables[0];

                        for (int i = 0; i < dtNoticia.Rows.Count; i++)
                        {
                            Noticia noticia = PopularEntidade(dtNoticia, i);
                            listNoticia.Add(noticia);
                        }
                    }
                }
            }

            return listNoticia;
        }

        public List<Noticia> ObterNoticiaByIdLocalNoticia(int idLocalNoticia)
        {
            List<Noticia> listaNoticias = new List<Noticia>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT N.IdNoticia, N.Titulo, N.Subtitulo, N.Resumo, N.Conteudo, N.IdLocalNoticia, N.LinkVideo, N.Ativo, N.DtInclusao, N.Login, N.Fonte, N.DtNoticia ");
            sbQuery.Append("FROM NOTICIA N WHERE N.IdLocalNoticia = " + idLocalNoticia + " ORDER BY N.IDNOTICIA DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtNoticia = ds.Tables[0];

                        for (int i = 0; i < dtNoticia.Rows.Count; i++)
                        {
                            Noticia noticia = PopularEntidade(dtNoticia, i);
                            listaNoticias.Add(noticia);
                        }
                    }
                }
            }

            return listaNoticias;
        }

        public Noticia ObterUltimaNoticia()
        {
            Noticia noticia = new Noticia();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select * from noticia where idnoticia = (select MAX(idNoticia) from Noticia)");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtNoticia = ds.Tables[0];

                        for (int i = 0; i < dtNoticia.Rows.Count; i++)
                        {
                            noticia = PopularEntidade(dtNoticia, i);
                        }
                    }
                }
            }

            return noticia;
        }

        public List<Noticia> ObterNoticiasAtivas(int idLocalNoticia)
        {
            List<Noticia> listaNoticias = new List<Noticia>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT N.IdNoticia, upper(N.Titulo) as Titulo, N.Subtitulo, N.Resumo, N.Conteudo, N.IdLocalNoticia, N.LinkVideo, N.Ativo, N.DtInclusao, N.Login, N.Fonte, N.DtNoticia ");
            sbQuery.Append("FROM NOTICIA N WHERE N.IdLocalNoticia = " + idLocalNoticia + "AND N.Ativo = 1 ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtNoticia = ds.Tables[0];

                        for (int i = 0; i < dtNoticia.Rows.Count; i++)
                        {
                            Noticia noticia = PopularEntidade(dtNoticia, i);
                            listaNoticias.Add(noticia);
                        }
                    }
                }
            }

            return listaNoticias;
        }

        public string InativarNoticias(int idLocalNoticia)
        {
            object ret;

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT TOP 5 * into #Ativos FROM Noticia n where n.IdLocalNoticia = " + idLocalNoticia + " order by n.DtInclusao ");
            sbQuery.Append("UPDATE N set n.Ativo = 0 from Noticia n left join #Ativos a on n.IdNoticia = a.IdNoticia where a.IdNoticia is null and n.IdLocalNoticia = " + idLocalNoticia);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                ret = db.ExecuteNonQuery(dbCommand);
            }

            return ret.ToString();
        }

        public string Incluir(Noticia noticia)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO NOTICIA VALUES(@pTitulo, @pSubtitulo, @pResumo, @pConteudo, @pIdLocalNoticia, @pLinkVideo, @pAtivo, @pDtInclusao, @pLogin, @pFonte, @pDtNoticia )";
            command.Parameters.Add(new SqlParameter("@pTitulo", noticia.Titulo));

            if (noticia.Subtitulo != null)
            {
                command.Parameters.Add(new SqlParameter("@pSubtitulo", noticia.Subtitulo));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@pSubtitulo", DBNull.Value));
            }

            command.Parameters.Add(new SqlParameter("@pResumo", noticia.Resumo));
            command.Parameters.Add(new SqlParameter("@pConteudo", noticia.Conteudo));
            command.Parameters.Add(new SqlParameter("@pIdLocalNoticia", noticia.IdLocalNoticia));

            if (noticia.LinkVideo != null)
            {
                command.Parameters.Add(new SqlParameter("@pLinkVideo", noticia.LinkVideo));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@pLinkVideo", DBNull.Value));
            }

            command.Parameters.Add(new SqlParameter("@pAtivo", noticia.Ativo));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", noticia.DtInclusao));
            command.Parameters.Add(new SqlParameter("@pLogin", noticia.Login));

            if(!string.IsNullOrEmpty(noticia.Fonte))
            {
                command.Parameters.Add(new SqlParameter("@pFonte", noticia.Fonte));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@pFonte", DBNull.Value));
            }

            if(noticia.DtNoticia != null && noticia.DtNoticia != DateTime.MinValue)
            {
                command.Parameters.Add(new SqlParameter("@pDtNoticia", noticia.DtNoticia));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@pDtNoticia",DBNull.Value));
            }

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Alterar(Noticia noticia)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "UPDATE NOTICIA SET Titulo = @pTitulo, Subtitulo = @pSubtitulo, Resumo = @pResumo, Conteudo = @pConteudo, IdLocalNoticia = @pIdLocalNoticia, LinkVideo = @pLinkVideo, Ativo = @pAtivo, Fonte = @pFonte, DtNoticia = @pDtNoticia ";
            command.CommandText += "WHERE IdNoticia = @pIdNoticia";
            command.Parameters.Add(new SqlParameter("@pIdNoticia", noticia.IdNoticia));
            command.Parameters.Add(new SqlParameter("@pTitulo", noticia.Titulo));
            if (noticia.Subtitulo != null)
            {
                command.Parameters.Add(new SqlParameter("@pSubtitulo", noticia.Subtitulo));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@pSubtitulo", DBNull.Value));
            }
            command.Parameters.Add(new SqlParameter("@pResumo", noticia.Resumo));
            command.Parameters.Add(new SqlParameter("@pConteudo", noticia.Conteudo));
            command.Parameters.Add(new SqlParameter("@pIdLocalNoticia", noticia.IdLocalNoticia));
            if (noticia.LinkVideo != null)
            {
                command.Parameters.Add(new SqlParameter("@pLinkVideo", noticia.LinkVideo));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@pLinkVideo", DBNull.Value));
            }
            command.Parameters.Add(new SqlParameter("@pAtivo", noticia.Ativo));

            if (!string.IsNullOrEmpty(noticia.Fonte))
            {
                command.Parameters.Add(new SqlParameter("@pFonte", noticia.Fonte));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@pFonte", DBNull.Value));
            }

            if (noticia.DtNoticia != null && noticia.DtNoticia != DateTime.MinValue)
            {
                command.Parameters.Add(new SqlParameter("@pDtNoticia", noticia.DtNoticia));
            }
            else
            {
                command.Parameters.Add(new SqlParameter("@pDtNoticia", DBNull.Value));
            }

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Excluir(Noticia noticia)
        {
            object ret;

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("DELETE FROM NOTICIAPOLITICO WHERE IDNOTICIA = " + noticia.IdNoticia);
            sbQuery.Append("DELETE FROM IMAGEMNOTICIA WHERE IDNOTICIA = " + noticia.IdNoticia);
            sbQuery.Append(" DELETE FROM NOTICIA WHERE IDNOTICIA = " + noticia.IdNoticia);

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

        public void DesativarNoticia(int idNoticia)
        {
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("UPDATE NOTICIA SET ATIVO = 0 WHERE IDNOTICIA = " + idNoticia);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }
        }

        public void AtivarNoticia(int idNoticia)
        {
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("UPDATE NOTICIA SET ATIVO = 1 WHERE IDNOTICIA = " + idNoticia);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }
        }

        private static Noticia PopularEntidade(DataTable dtNoticia, int i)
        {
            Noticia noticia = new Noticia();

            if (dtNoticia.Columns.Contains("IdNoticia"))
            {
                if (dtNoticia.Rows[i]["IdNoticia"] != DBNull.Value)
                {
                    noticia.IdNoticia = Convert.ToInt32(dtNoticia.Rows[i]["IdNoticia"].ToString());
                    noticia.ListaImagens = new ImagemNoticiaDAO().ObterImagemByIdNoticia(noticia.IdNoticia);
                    noticia.ListaPoliticos = new NoticiaPoliticoDAO().ObterNoticiaPoliticoByIdNoticia(noticia.IdNoticia);
                }
            }

            if (dtNoticia.Columns.Contains("Titulo"))
            {
                if (dtNoticia.Rows[i]["Titulo"] != DBNull.Value)
                {
                    noticia.Titulo = dtNoticia.Rows[i]["Titulo"].ToString();
                }
            }

            if (dtNoticia.Columns.Contains("Subtitulo"))
            {
                if (dtNoticia.Rows[i]["Subtitulo"] != DBNull.Value)
                {
                    noticia.Subtitulo = dtNoticia.Rows[i]["Subtitulo"].ToString();
                }
            }

            if (dtNoticia.Columns.Contains("Resumo"))
            {
                if (dtNoticia.Rows[i]["Resumo"] != DBNull.Value)
                {
                    noticia.Resumo = dtNoticia.Rows[i]["Resumo"].ToString();
                }
            }

            if (dtNoticia.Columns.Contains("Conteudo"))
            {
                if (dtNoticia.Rows[i]["Conteudo"] != DBNull.Value)
                {
                    noticia.Conteudo = dtNoticia.Rows[i]["Conteudo"].ToString();
                }
            }

            if (dtNoticia.Columns.Contains("IdLocalNoticia"))
            {
                if (dtNoticia.Rows[i]["IdLocalNoticia"] != DBNull.Value)
                {
                    noticia.IdLocalNoticia = Convert.ToInt32(dtNoticia.Rows[i]["IdLocalNoticia"]);
                }
            }

            if (dtNoticia.Columns.Contains("LinkVideo"))
            {
                if (dtNoticia.Rows[i]["LinkVideo"] != DBNull.Value)
                {
                    noticia.LinkVideo = dtNoticia.Rows[i]["LinkVideo"].ToString();
                }
            }

            if (dtNoticia.Columns.Contains("Ativo"))
            {
                if (dtNoticia.Rows[i]["Ativo"] != DBNull.Value)
                {
                    noticia.Ativo = Convert.ToInt32(dtNoticia.Rows[i]["Ativo"]);
                }
            }

            if (dtNoticia.Columns.Contains("DtInclusao"))
            {
                if (dtNoticia.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    noticia.DtInclusao = Convert.ToDateTime(dtNoticia.Rows[i]["DtInclusao"]);
                }
            }

            if (dtNoticia.Columns.Contains("Login"))
            {
                if (dtNoticia.Rows[i]["Login"] != DBNull.Value)
                {
                    noticia.Login = dtNoticia.Rows[i]["Login"].ToString();
                }
            }

            if (dtNoticia.Columns.Contains("Fonte"))
            {
                if (dtNoticia.Rows[i]["Fonte"] != DBNull.Value)
                {
                    noticia.Fonte = dtNoticia.Rows[i]["Fonte"].ToString();
                }
            }

            if (dtNoticia.Columns.Contains("DtNoticia"))
            {
                if (dtNoticia.Rows[i]["DtNoticia"] != DBNull.Value)
                {
                    noticia.DtNoticia = Convert.ToDateTime(dtNoticia.Rows[i]["DtNoticia"]);
                }
            }

            return noticia;
        }
    }
}
