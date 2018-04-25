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

    public class CategoriaDAO
    {
        public List<Categoria> ObterCategoria()
        {
            List<Categoria> listCategoria = new List<Categoria>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select C.IdCategoria, upper(C.Nome) as Nome, C.Descricao, C.Login, C.DtInclusao ");
            sbQuery.Append("from Categoria C ORDER BY C.Nome ASC");


            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtCategoria = ds.Tables[0];

                        for (int i = 0; i < dtCategoria.Rows.Count; i++)
                        {
                            Categoria categoria = PopularEntidade(dtCategoria, i);
                            listCategoria.Add(categoria);
                        }
                    }
                }
            }

            return listCategoria;
        }

        public List<Categoria> ObterCategoriaByIdCategoria(int idCategoria)
        {
            List<Categoria> listCategoria = new List<Categoria>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select C.IdCategoria, upper(C.Nome) as Nome, C.Descricao, C.Login, C.DtInclusao ");
            sbQuery.Append("FROM CATEGORIA C WHERE C.IDCATEGORIA = " + idCategoria + " ORDER BY C.Nome ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtCategoria = ds.Tables[0];

                        for (int i = 0; i < dtCategoria.Rows.Count; i++)
                        {
                            Categoria categoria = PopularEntidade(dtCategoria, i);
                            listCategoria.Add(categoria);
                        }
                    }
                }
            }

            return listCategoria;
        }


        public List<Categoria> ObterCategoria(string pString)
        {
            List<Categoria> listCategoria = new List<Categoria>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select C.IdCategoria, upper(C.Nome) as Nome, C.Descricao, C.Login, C.DtInclusao "); 
            sbQuery.Append("FROM CATEGORIA C WHERE C.Nome LIKE '%' + '" + @pString + "' + '%' " + " ORDER BY C.Nome ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtCategoria = ds.Tables[0];

                        for (int i = 0; i < dtCategoria.Rows.Count; i++)
                        {
                            Categoria categoria = PopularEntidade(dtCategoria, i);
                            listCategoria.Add(categoria);
                        }
                    }
                }
            }

            return listCategoria;
        }


        private static Categoria PopularEntidade(DataTable dtCategoria, int i)
        {
            Categoria categoria = new Categoria();

            if (dtCategoria.Columns.Contains("IdCategoria"))
            {
                if (dtCategoria.Rows[i]["IdCategoria"] != DBNull.Value)
                {
                    categoria.IdCategoria = Convert.ToInt32(dtCategoria.Rows[i]["IdCategoria"].ToString());
                }
            }

            if (dtCategoria.Columns.Contains("Nome"))
            {
                if (dtCategoria.Rows[i]["Nome"] != DBNull.Value)
                {
                    categoria.Nome = dtCategoria.Rows[i]["Nome"].ToString();
                }
            }

            if (dtCategoria.Columns.Contains("Descricao"))
            {
                if (dtCategoria.Rows[i]["Descricao"] != DBNull.Value)
                {
                    categoria.Descricao = dtCategoria.Rows[i]["Descricao"].ToString();
                }
            }

            if (dtCategoria.Columns.Contains("Login"))
            {
                if (dtCategoria.Rows[i]["Login"] != DBNull.Value)
                {
                    categoria.Login = dtCategoria.Rows[i]["Login"].ToString();
                }
            }

            if (dtCategoria.Columns.Contains("DtInclusao"))
            {
                if (dtCategoria.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    categoria.DtInclusao = Convert.ToDateTime(dtCategoria.Rows[i]["DtInclusao"].ToString());
                }
            }


            return categoria;
        }

        public bool VerificaSeCategoriaEstaSendoUsada(int idCategoria)
        {
            //Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            //object ret = default(object);

            //StringBuilder sbQuery = new StringBuilder();
            //SqlCommand command = new SqlCommand();

            //command.CommandText = "SELECT 1 FROM Despesa WHERE IdCategoria = " + idCategoria;

           // ret = db.ExecuteScalar(command);

         //   return Convert.ToBoolean(ret);
            return false;
        }

        public string Excluir(Categoria categoria)
        {

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");
            
            object ret;
            
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("DELETE FROM CATEGORIA WHERE IDCATEGORIA = " + categoria.IdCategoria);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                ret = db.ExecuteNonQuery(dbCommand);
            }

            return ret.ToString();
        }

        public bool NomeExiste(Categoria categoria)
        {
            //Iniciando Persistência no Banco de Dados.
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret;

            using (DbCommand dbCommand = db.GetSqlStringCommand("SELECT COUNT(*) FROM CATEGORIA WHERE UPPER(NOME) LIKE UPPER('" + categoria.Nome.Trim() + "')"))
            {
                //Executar Comando no Banco.
                ret = db.ExecuteScalar(dbCommand);

                if (Convert.ToInt32(ret) > 0)
                    return true;
            }

            return false;
        }

        public string Alterar(Categoria categoria)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");
            
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();

            SqlCommand command = new SqlCommand();

            command.CommandText = "UPDATE CATEGORIA SET Nome = @pNome, Descricao = @pDescricao ";
            command.CommandText += "WHERE IdCategoria = @pIdCategoria";
            command.Parameters.Add(new SqlParameter("@pIdCategoria", categoria.IdCategoria));
            command.Parameters.Add(new SqlParameter("@pNome", categoria.Nome));
            command.Parameters.Add(new SqlParameter("@pDescricao", categoria.Descricao));
           
            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Incluir(Categoria categoria)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");
 
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();

            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO CATEGORIA VALUES(@pNome, @pDescricao, @pDtInclusao, @pLogin) ";
            command.Parameters.Add(new SqlParameter("@pNome", categoria.Nome));
            command.Parameters.Add(new SqlParameter("@pDescricao", categoria.Descricao));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", categoria.DtInclusao));
            command.Parameters.Add(new SqlParameter("@pLogin", categoria.Login));
            ret = db.ExecuteNonQuery(command);

           return ret.ToString();
        }


    }
}


