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

    public class FuncionalidadeDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public List<Funcionalidade> ObterFuncionalidades()
        {
            List<Funcionalidade> listFuncionalidades = new List<Funcionalidade>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT F.IDFUNCIONALIDADE, UPPER(F.NOME) as Nome, F.DESCRICAO as Descricao ");
            sbQuery.Append("FROM FUNCIONALIDADE F ORDER BY F.IDFUNCIONALIDADE DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtFuncionalidades = ds.Tables[0];

                        for (int i = 0; i < dtFuncionalidades.Rows.Count; i++)
                        {
                            Funcionalidade funcionalidade = PopularEntidade(dtFuncionalidades, i);
                            listFuncionalidades.Add(funcionalidade);
                        }
                    }
                }
            }

            return listFuncionalidades;
        }

        public Funcionalidade ObterFuncionalidadeById(int idFuncionalidade)
        {
            Funcionalidade funcionalidade = new Funcionalidade();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT F.IDFUNCIONALIDADE, UPPER(F.NOME) as Nome, F.DESCRICAO as Descricao ");
            sbQuery.Append("FROM FUNCIONALIDADE F WHERE F.IDFUNCIONALIDADE = " + idFuncionalidade + " ORDER BY IDFUNCIONALIDADE DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtFuncionalidade = ds.Tables[0];

                        for (int i = 0; i < dtFuncionalidade.Rows.Count; i++)
                        {
                            funcionalidade = PopularEntidade(dtFuncionalidade, i);
                        }
                    }
                }
            }

            return funcionalidade;
        }

        public List<Funcionalidade> ObterFuncionalidades(string pString)
        {
            List<Funcionalidade> listFuncionalidades = new List<Funcionalidade>();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT F.IDFUNCIONALIDADE, UPPER(F.NOME) as Nome, F.DESCRICAO as Descricao ");
            sbQuery.Append("FROM FUNCIONALIDADE F WHERE F.NOME LIKE '%" + @pString + "%' ORDER BY IDFUNCIONALIDADE DESC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtFuncionalidades = ds.Tables[0];

                        for (int i = 0; i < dtFuncionalidades.Rows.Count; i++)
                        {
                            Funcionalidade funcionalidade = PopularEntidade(dtFuncionalidades, i);
                            listFuncionalidades.Add(funcionalidade);
                        }
                    }
                }
            }

            return listFuncionalidades;
        }

        public string Incluir(Funcionalidade funcionalidade)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO FUNCIONALIDADE VALUES(@pNome, @pDescricao, @pLogin, @pDtInclusao)";
            command.Parameters.Add(new SqlParameter("@pNome", funcionalidade.Nome));
            command.Parameters.Add(new SqlParameter("@pDescricao", funcionalidade.Descricao));
            command.Parameters.Add(new SqlParameter("@pLogin", funcionalidade.Login));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", funcionalidade.DtInclusao));

            //new LogUtilDAO().SalvarUsuarioLog(funcionalidade.Login);
            
            ret = db.ExecuteNonQuery(command);

            

            return ret.ToString();
        }

        public string Alterar(Funcionalidade funcionalidade)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "UPDATE FUNCIONALIDADE SET Nome = @pNome, Descricao = @pDescricao ";
            command.CommandText += "WHERE IdFuncionalidade = @pIdFuncionalidade";
            command.Parameters.Add(new SqlParameter("@pIdFuncionalidade", funcionalidade.IdFuncionalidade));
            command.Parameters.Add(new SqlParameter("@pNome", funcionalidade.Nome));
            command.Parameters.Add(new SqlParameter("@pDescricao", funcionalidade.Descricao));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Excluir(Funcionalidade funcionalidade)
        {
            object ret;

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("DELETE FROM FUNCIONALIDADE WHERE IDFUNCIONALIDADE = " + funcionalidade.IdFuncionalidade);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                ret = db.ExecuteNonQuery(dbCommand);
            }

            return ret.ToString();
        }

        public bool NomeExiste(Funcionalidade funcionalidade)
        {
            object ret;

            using (DbCommand dbCommand = db.GetSqlStringCommand("SELECT COUNT(*) FROM FUNCIONALIDADE WHERE UPPER(NOME) LIKE UPPER('" + funcionalidade.Nome.Trim() + "')"))
            {
                //Executar Comando no Banco.
                ret = db.ExecuteScalar(dbCommand);

                if (Convert.ToInt32(ret) > 0)
                    return true;
            }

            return false;
        }

        private static Funcionalidade PopularEntidade(DataTable dtFuncionalidade, int i)
        {
            Funcionalidade funcionalidade = new Funcionalidade();

            if (dtFuncionalidade.Columns.Contains("IdFuncionalidade"))
            {
                if (dtFuncionalidade.Rows[i]["IdFuncionalidade"] != DBNull.Value)
                {
                    funcionalidade.IdFuncionalidade = Convert.ToInt32(dtFuncionalidade.Rows[i]["IdFuncionalidade"].ToString());
                }
            }

            if (dtFuncionalidade.Columns.Contains("Nome"))
            {
                if (dtFuncionalidade.Rows[i]["Nome"] != DBNull.Value)
                {
                    funcionalidade.Nome = Convert.ToString(dtFuncionalidade.Rows[i]["Nome"]);
                }
            }

            if (dtFuncionalidade.Columns.Contains("Descricao"))
            {
                if (dtFuncionalidade.Rows[i]["Descricao"] != DBNull.Value)
                {
                    funcionalidade.Descricao = Convert.ToString(dtFuncionalidade.Rows[i]["Descricao"]);
                }
            }

            if (dtFuncionalidade.Columns.Contains("Login"))
            {
                if (dtFuncionalidade.Rows[i]["Login"] != DBNull.Value)
                {
                    funcionalidade.Login = Convert.ToString(dtFuncionalidade.Rows[i]["Login"]);
                }
            }

            if (dtFuncionalidade.Columns.Contains("DtInclusao"))
            {
                if (dtFuncionalidade.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    funcionalidade.DtInclusao = Convert.ToDateTime(dtFuncionalidade.Rows[i]["DtInclusao"].ToString());
                }
            }

            return funcionalidade;
        }
    }
}
