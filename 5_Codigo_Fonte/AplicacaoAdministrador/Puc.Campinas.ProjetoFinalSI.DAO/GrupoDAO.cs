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

    public class GrupoDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public List<Grupo> ObterGrupos()
        {
            List<Grupo> listGrupos = new List<Grupo>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT G.IDGRUPO, UPPER(G.NOME) as Nome, G.DESCRICAO as Descricao ");
            sbQuery.Append("FROM GRUPO G ORDER BY G.IDGRUPO DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtGrupos = ds.Tables[0];

                        for (int i = 0; i < dtGrupos.Rows.Count; i++)
                        {
                            Grupo grupo = PopularEntidade(dtGrupos, i);
                            listGrupos.Add(grupo);
                        }
                    }
                }
            }

            return listGrupos;
        }

        public Grupo ObterGrupoById(int idGrupo)
        {
            Grupo grupo = new Grupo();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT G.IDGRUPO, UPPER(G.NOME) AS Nome, G.DESCRICAO AS Descricao ");
            sbQuery.Append("FROM GRUPO G WHERE G.IDGRUPO = " + idGrupo + " ORDER BY IDGRUPO DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtGrupo = ds.Tables[0];

                        for (int i = 0; i < dtGrupo.Rows.Count; i++)
                        {
                            grupo = PopularEntidade(dtGrupo, i);
                        }
                    }
                }
            }

            return grupo;
        }

        public List<Grupo> ObterGrupos(string pString)
        {
            List<Grupo> listGrupos = new List<Grupo>();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT G.IDGRUPO, UPPER(G.NOME) AS NOME, G.DESCRICAO AS DESCRICAO ");
            sbQuery.Append("FROM GRUPO G WHERE G.NOME LIKE '%' + '" + @pString + "' + '%' " + "ORDER BY IDGRUPO DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtGrupos = ds.Tables[0];

                        for (int i = 0; i < dtGrupos.Rows.Count; i++)
                        {
                            Grupo grupo = PopularEntidade(dtGrupos, i);
                            listGrupos.Add(grupo);
                        }
                    }
                }
            }

            return listGrupos;
        }

        public string Incluir(Grupo grupo)
        {
            PermissaoDAO permissaoDAO = new PermissaoDAO();
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO GRUPO VALUES(@pNome, @pDescricao, @pLogin, @pDtInclusao)";
            command.Parameters.Add(new SqlParameter("@pNome", grupo.Nome));
            command.Parameters.Add(new SqlParameter("@pDescricao", grupo.Descricao));
            command.Parameters.Add(new SqlParameter("@pLogin", grupo.Login));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", grupo.DtInclusao));

            ret = db.ExecuteNonQuery(command);

            foreach (Permissao item in grupo.ListaPermissao)
            {
                item.IdGrupo = VerificarMaxId();
                permissaoDAO.Incluir(item);
            }

            return ret.ToString();
        }

        public string Alterar(Grupo grupo)
        {
            PermissaoDAO permissaoDAO = new PermissaoDAO();

            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "UPDATE GRUPO SET Nome = @pNome, Descricao = @pDescricao ";
            command.CommandText += "WHERE IdGrupo = @pIdGrupo";
            command.Parameters.Add(new SqlParameter("@pIdGrupo", grupo.IdGrupo));
            command.Parameters.Add(new SqlParameter("@pNome", grupo.Nome));
            command.Parameters.Add(new SqlParameter("@pDescricao", grupo.Descricao));

            ret = db.ExecuteNonQuery(command);

            permissaoDAO.ExcluirPermissoesGrupo(grupo.IdGrupo);

            foreach (Permissao item in grupo.ListaPermissao)
            {
                item.IdGrupo = grupo.IdGrupo;
                permissaoDAO.Incluir(item);
            }

            return ret.ToString();
        }

        public string Excluir(Grupo grupo)
        {
            object ret;

            StringBuilder sbQuery = new StringBuilder();

            PermissaoDAO permissaoDAO = new PermissaoDAO();

            permissaoDAO.ExcluirPermissoesGrupo(grupo.IdGrupo);

            sbQuery.Append("DELETE FROM GRUPO WHERE IDGRUPO = " + grupo.IdGrupo);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                ret = db.ExecuteNonQuery(dbCommand);
            }

            return ret.ToString();
        }

        public bool NomeExiste(Grupo grupo)
        {
            object ret;

            using (DbCommand dbCommand = db.GetSqlStringCommand("SELECT COUNT(*) FROM GRUPO WHERE UPPER(NOME) LIKE UPPER('" + grupo.Nome.Trim() + "')"))
            {
                //Executar Comando no Banco.
                ret = db.ExecuteScalar(dbCommand);

                if (Convert.ToInt32(ret) > 0)
                    return true;
            }

            return false;
        }

        private static Grupo PopularEntidade(DataTable dtGrupo, int i)
        {
            Grupo grupo = new Grupo();

            if (dtGrupo.Columns.Contains("IdGrupo"))
            {
                if (dtGrupo.Rows[i]["IdGrupo"] != DBNull.Value)
                {
                    grupo.IdGrupo = Convert.ToInt32(dtGrupo.Rows[i]["IdGrupo"].ToString());
                    grupo.ListaPermissao = new PermissaoDAO().ObterPermissaoByIdGrupo(grupo.IdGrupo);
                }
            }

            if (dtGrupo.Columns.Contains("Nome"))
            {
                if (dtGrupo.Rows[i]["Nome"] != DBNull.Value)
                {
                    grupo.Nome = Convert.ToString(dtGrupo.Rows[i]["Nome"]);
                }
            }

            if (dtGrupo.Columns.Contains("Descricao"))
            {
                if (dtGrupo.Rows[i]["Descricao"] != DBNull.Value)
                {
                    grupo.Descricao = Convert.ToString(dtGrupo.Rows[i]["Descricao"]);
                }
            }

            if (dtGrupo.Columns.Contains("Login"))
            {
                if (dtGrupo.Rows[i]["Login"] != DBNull.Value)
                {
                    grupo.Login = Convert.ToString(dtGrupo.Rows[i]["Login"]);
                }
            }

            if (dtGrupo.Columns.Contains("DtInclusao"))
            {
                if (dtGrupo.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    grupo.DtInclusao = Convert.ToDateTime(dtGrupo.Rows[i]["DtInclusao"].ToString());
                }
            }

            return grupo;
        }

        private int VerificarMaxId()
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();

            SqlCommand command = new SqlCommand();

            command.CommandText = "select MAX(pm.IdGrupo) as IdGrupo from Grupo pm";

            ret = db.ExecuteScalar(command);

            return Convert.ToInt32(ret);
        }
    }
}
