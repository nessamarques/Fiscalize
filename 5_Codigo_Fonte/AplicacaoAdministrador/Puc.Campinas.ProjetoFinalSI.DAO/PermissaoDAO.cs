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

    public class PermissaoDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public List<Permissao> ObterPermissao()
        {
            List<Permissao> listPermissao = new List<Permissao>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT P.IDPERMISSAO, P.IDGRUPO, P.IDFUNCIONALIDADE, UPPER(G.NOME) as NomeGrupo, ");
            sbQuery.Append("UPPER(F.NOME) as NomeFuncionalidade FROM PERMISSAO P, GRUPO G, FUNCIONALIDADE F ");
            sbQuery.Append("WHERE P.IDGRUPO = G.IDGRUPO AND P.IDFUNCIONALIDADE = F.IDFUNCIONALIDADE ORDER BY P.IDPERMISSAO DESC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPermissao = ds.Tables[0];

                        for (int i = 0; i < dtPermissao.Rows.Count; i++)
                        {
                            Permissao permissao = PopularEntidade(dtPermissao, i);
                            listPermissao.Add(permissao);
                        }
                    }
                }
            }

            return listPermissao;
        }

        public List<Permissao> ObterPermissaoByIdGrupo(int idGrupo)
        {
            List<Permissao> listPermissao = new List<Permissao>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT * FROM PERMISSAO P WHERE P.IDGRUPO =" + idGrupo);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPermissao = ds.Tables[0];

                        for (int i = 0; i < dtPermissao.Rows.Count; i++)
                        {
                            Permissao permissao = PopularEntidade(dtPermissao, i);
                            listPermissao.Add(permissao);
                        }
                    }
                }
            }

            return listPermissao;
        }

        public Permissao ObterPermissaoById(int idPermissao)
        {
            Permissao permissao = new Permissao();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT P.IDPERMISSAO, P.IDGRUPO, P.IDFUNCIONALIDADE, UPPER(G.NOME) as NomeGrupo, ");
            sbQuery.Append("UPPER(F.NOME) as NomeFuncionalidade FROM PERMISSAO P, GRUPO G, FUNCIONALIDADE F ");
            sbQuery.Append("WHERE P.IDPERMISSAO = " + idPermissao + " AND P.IDGRUPO = G.IDGRUPO ");
            sbQuery.Append("AND P.IDFUNCIONALIDADE = F.IDFUNCIONALIDADE ORDER BY IDPERMISSAO DESC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPermissao = ds.Tables[0];

                        for (int i = 0; i < dtPermissao.Rows.Count; i++)
                        {
                            permissao = PopularEntidade(dtPermissao, i);
                        }
                    }
                }
            }

            return permissao;
        }

        public List<Permissao> ObterPermissao(string pString)
        {
            List<Permissao> listPermissao = new List<Permissao>();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT P.IDPERMISSAO, P.IDGRUPO, P.IDFUNCIONALIDADE, UPPER(G.NOME) as NomeGrupo, UPPER(F.NOME) as NomeFuncionalidade ");
            sbQuery.Append("FROM PERMISSAO P, GRUPO G, FUNCIONALIDADE F ");
            sbQuery.Append("WHERE P.NOME LIKE '%" + @pString + "%' AND P.IDGRUPO = G.IDGRUPO AND P.IDFUNCIONALIDADE = F.IDFUNCIONALIDADE ");
            sbQuery.Append("ORDER BY IDPERMISSAO DESC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPermissao = ds.Tables[0];

                        for (int i = 0; i < dtPermissao.Rows.Count; i++)
                        {
                            Permissao permissao = PopularEntidade(dtPermissao, i);
                            listPermissao.Add(permissao);
                        }
                    }
                }
            }

            return listPermissao;
        }

        public string Incluir(Permissao permissao)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO PERMISSAO(IdGrupo,IdFuncionalidade,PermissaoIncluir,PermissaoAlterar, PermissaoConsultar, PermissaoExcluir, Login, DtInclusao)";
            command.CommandText += " VALUES(@pIdGrupo, @pIdFuncionalidade, @pPermissaoIncluir, @pPermissaoAlterar, @pPermissaoConsultar, @pPermissaoExcluir, @pLogin, @pDtInclusao)";
            command.Parameters.Add(new SqlParameter("@pIdGrupo", permissao.IdGrupo));
            command.Parameters.Add(new SqlParameter("@pIdFuncionalidade", permissao.IdFuncionalidade));
            command.Parameters.Add(new SqlParameter("@pPermissaoIncluir", permissao.PermissaoIncluir));
            command.Parameters.Add(new SqlParameter("@pPermissaoAlterar", permissao.PermissaoAlterar));
            command.Parameters.Add(new SqlParameter("@pPermissaoConsultar", permissao.PermissaoConsultar));
            command.Parameters.Add(new SqlParameter("@pPermissaoExcluir", permissao.PermissaoExcluir));
            command.Parameters.Add(new SqlParameter("@pLogin", permissao.Login));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", permissao.DtInclusao));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Alterar(Permissao permissao)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "UPDATE PERMISSAO SET IdGrupo = @pIdGrupo, IdFuncionalidade = @pIdFuncionalidade ";
            command.CommandText += "WHERE IdPermissao = @pIdPermissao";
            command.Parameters.Add(new SqlParameter("@pIdPermissao", permissao.IdPermissao));
            command.Parameters.Add(new SqlParameter("@pIdGrupo", permissao.IdGrupo));
            command.Parameters.Add(new SqlParameter("@pIdFuncionalidade", permissao.IdFuncionalidade));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Excluir(Permissao permissao)
        {
            object ret;

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("DELETE FROM PERMISSAO WHERE IDPERMISSAO = " + permissao.IdPermissao);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                ret = db.ExecuteNonQuery(dbCommand);
            }

            return ret.ToString();
        }

        public string ExcluirPermissoesGrupo(int idGrupo)
        {
            object ret;

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("DELETE FROM PERMISSAO WHERE IDGRUPO = " + idGrupo);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                ret = db.ExecuteNonQuery(dbCommand);
            }

            return ret.ToString();
        }

        private static Permissao PopularEntidade(DataTable dtPermissao, int i)
        {
            Permissao permissao = new Permissao();

            if (dtPermissao.Columns.Contains("IdPermissao"))
            {
                if (dtPermissao.Rows[i]["IdPermissao"] != DBNull.Value)
                {
                    permissao.IdPermissao = Convert.ToInt32(dtPermissao.Rows[i]["IdPermissao"].ToString());
                }
            }

            if (dtPermissao.Columns.Contains("IdGrupo"))
            {
                if (dtPermissao.Rows[i]["IdGrupo"] != DBNull.Value)
                {
                    permissao.IdGrupo = Convert.ToInt32(dtPermissao.Rows[i]["IdGrupo"].ToString());
                }
            }

            if (dtPermissao.Columns.Contains("IdFuncionalidade"))
            {
                if (dtPermissao.Rows[i]["IdFuncionalidade"] != DBNull.Value)
                {
                    permissao.IdFuncionalidade = Convert.ToInt32(dtPermissao.Rows[i]["IdFuncionalidade"].ToString());
                }
            }

            if (dtPermissao.Columns.Contains("NomeGrupo"))
            {
                if (dtPermissao.Rows[i]["NomeGrupo"] != DBNull.Value)
                {
                    permissao.NomeGrupo = Convert.ToString(dtPermissao.Rows[i]["NomeGrupo"]);
                }
            }

            if (dtPermissao.Columns.Contains("NomeFuncionalidade"))
            {
                if (dtPermissao.Rows[i]["NomeFuncionalidade"] != DBNull.Value)
                {
                    permissao.NomeFuncionalidade = Convert.ToString(dtPermissao.Rows[i]["NomeFuncionalidade"]);
                }
            }

            if (dtPermissao.Columns.Contains("Login"))
            {
                if (dtPermissao.Rows[i]["Login"] != DBNull.Value)
                {
                    permissao.Login = Convert.ToString(dtPermissao.Rows[i]["Login"]);
                }
            }

            if (dtPermissao.Columns.Contains("DtInclusao"))
            {
                if (dtPermissao.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    permissao.DtInclusao = Convert.ToDateTime(dtPermissao.Rows[i]["DtInclusao"].ToString());
                }
            }

            if (dtPermissao.Columns.Contains("PermissaoAlterar"))
            {
                if (dtPermissao.Rows[i]["PermissaoAlterar"] != DBNull.Value)
                {
                    permissao.PermissaoAlterar = Convert.ToBoolean(dtPermissao.Rows[i]["PermissaoAlterar"]);
                }
            }

            if (dtPermissao.Columns.Contains("PermissaoConsultar"))
            {
                if (dtPermissao.Rows[i]["PermissaoConsultar"] != DBNull.Value)
                {
                    permissao.PermissaoConsultar = Convert.ToBoolean(dtPermissao.Rows[i]["PermissaoConsultar"]);
                }
            }

            if (dtPermissao.Columns.Contains("PermissaoExcluir"))
            {
                if (dtPermissao.Rows[i]["PermissaoExcluir"] != DBNull.Value)
                {
                    permissao.PermissaoExcluir = Convert.ToBoolean(dtPermissao.Rows[i]["PermissaoExcluir"]);
                }
            }

            if (dtPermissao.Columns.Contains("PermissaoIncluir"))
            {
                if (dtPermissao.Rows[i]["PermissaoIncluir"] != DBNull.Value)
                {
                    permissao.PermissaoIncluir = Convert.ToBoolean(dtPermissao.Rows[i]["PermissaoIncluir"]);
                }
            }

            return permissao;
        }
    }
}
