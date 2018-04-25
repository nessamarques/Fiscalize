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

    public class UsuarioDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public int UsuarioValido(string login, string pass)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT IdUsuario FROM USUARIO WHERE USUARIOLOGIN = '" + login + "' AND SENHA = '" + pass + "'";

            ret = db.ExecuteScalar(command);

            return Convert.ToInt32(ret);
        }


        public List<Usuario> ObterUsuarios()
        {
            List<Usuario> listUsuarios = new List<Usuario>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT U.IDUSUARIO, U.IDGRUPO, U.NOME, U.CPF, U.IDCIDADE, U.IDESTADO, U.TELEFONE, U.CELULAR, U.EMAIL, U.USUARIOLOGIN, U.SENHA ");
            sbQuery.Append("FROM USUARIO U ORDER BY U.IDUSUARIO DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtUsuarios = ds.Tables[0];

                        for (int i = 0; i < dtUsuarios.Rows.Count; i++)
                        {
                            Usuario usuario = PopularEntidade(dtUsuarios, i);
                            listUsuarios.Add(usuario);
                        }
                    }
                }
            }

            return listUsuarios;
        }

        public Usuario ObterUsuarioById(int idUsuario)
        {
            Usuario usuario = new Usuario();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT U.IDUSUARIO, U.IDGRUPO, U.NOME, U.CPF, U.IDCIDADE, U.IDESTADO, U.TELEFONE, U.CELULAR, U.EMAIL, U.USUARIOLOGIN, U.SENHA ");
            sbQuery.Append("FROM USUARIO U WHERE U.IDUSUARIO = " + idUsuario + " ORDER BY IDUSUARIO DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtUsuario = ds.Tables[0];

                        for (int i = 0; i < dtUsuario.Rows.Count; i++)
                        {
                            usuario = PopularEntidade(dtUsuario, i);
                        }
                    }
                }
            }

            return usuario;
        }

        public List<Usuario> ObterUsuarios(string pString)
        {
            List<Usuario> listUsuarios = new List<Usuario>();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT U.IDUSUARIO, U.IDGRUPO, U.NOME, U.CPF, U.IDCIDADE, U.IDESTADO, U.TELEFONE, U.CELULAR, U.EMAIL, U.USUARIOLOGIN, U.SENHA ");
            sbQuery.Append("FROM USUARIO U WHERE U.NOME LIKE '%' + '" + @pString + "' + '%' " + "ORDER BY IDUSUARIO DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtUsuarios = ds.Tables[0];

                        for (int i = 0; i < dtUsuarios.Rows.Count; i++)
                        {
                            Usuario usuario = PopularEntidade(dtUsuarios, i);
                            listUsuarios.Add(usuario);
                        }
                    }
                }
            }

            return listUsuarios;
        }

        public List<Usuario> ObterUsuariosPortal(string pLogin, string pSenha)
        {
            List<Usuario> listUsuarios = new List<Usuario>();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT U.IDUSUARIO, U.IDGRUPO, U.NOME, U.CPF, U.IDCIDADE, U.IDESTADO, U.TELEFONE, U.CELULAR, U.EMAIL, U.USUARIOLOGIN, U.SENHA ");
            sbQuery.Append("FROM USUARIO U WHERE U.USUARIOLOGIN = '" + @pLogin + "' ");
            sbQuery.Append("AND U.SENHA = '" + @pSenha + "' ");
            sbQuery.Append("ORDER BY IDUSUARIO DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtUsuarios = ds.Tables[0];

                        for (int i = 0; i < dtUsuarios.Rows.Count; i++)
                        {
                            Usuario usuario = PopularEntidade(dtUsuarios, i);
                            listUsuarios.Add(usuario);
                        }
                    }
                }
            }

            return listUsuarios;
        }

        public string Incluir(Usuario usuario)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO USUARIO VALUES(@pIdGrupo, @pNome, @pCPF, @pIdCidade, @pIdEstado, @pTelefone, @pCelular, @pEmail, @pUsuarioLogin, @pSenha, @pLogin, @pDtInclusao)";

            command.Parameters.Add(new SqlParameter("@pIdGrupo", usuario.IdGrupo));
            command.Parameters.Add(new SqlParameter("@pNome", usuario.Nome));
            command.Parameters.Add(new SqlParameter("@pCPF", usuario.CPF.Replace(".", "").Replace(".", "").Replace("-", "")));
            command.Parameters.Add(new SqlParameter("@pIdCidade", usuario.IdCidade));
            command.Parameters.Add(new SqlParameter("@pIdEstado", usuario.IdEstado));
            command.Parameters.Add(new SqlParameter("@pTelefone", usuario.Telefone.Replace("(", "").Replace(")", "").Replace("-", "")));
            command.Parameters.Add(new SqlParameter("@pCelular", usuario.Celular.Replace("(", "").Replace(")", "").Replace("-", "")));
            command.Parameters.Add(new SqlParameter("@pEmail", usuario.Email));
            command.Parameters.Add(new SqlParameter("@pUsuarioLogin", usuario.UsuarioLogin));
            command.Parameters.Add(new SqlParameter("@pSenha", usuario.Senha));
            command.Parameters.Add(new SqlParameter("@pLogin", usuario.Login));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", usuario.DtInclusao));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Alterar(Usuario usuario)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "UPDATE USUARIO SET IdGrupo = @pIdGrupo, Nome = @pNome, CPF = @pCPF, IdCidade = @pIdCidade, IdEstado = @pIdEstado, Telefone = @pTelefone, Celular = @pCelular, Email = @pEmail, UsuarioLogin = @pUsuarioLogin, Senha = @pSenha ";
            command.CommandText += "WHERE IdUsuario = @pIdUsuario";
            command.Parameters.Add(new SqlParameter("@pIdUsuario", usuario.IdUsuario));
            command.Parameters.Add(new SqlParameter("@pIdGrupo", usuario.IdGrupo));
            command.Parameters.Add(new SqlParameter("@pNome", usuario.Nome));
            command.Parameters.Add(new SqlParameter("@pCPF", usuario.CPF.Replace(".", "").Replace(".", "").Replace("-", "")));
            command.Parameters.Add(new SqlParameter("@pIdCidade", usuario.IdCidade));
            command.Parameters.Add(new SqlParameter("@pIdEstado", usuario.IdEstado));
            command.Parameters.Add(new SqlParameter("@pTelefone", usuario.Telefone.Replace("(", "").Replace(")", "").Replace("-", "")));
            command.Parameters.Add(new SqlParameter("@pCelular", usuario.Celular.Replace("(", "").Replace(")", "").Replace("-", "")));
            command.Parameters.Add(new SqlParameter("@pEmail", usuario.Email));
            command.Parameters.Add(new SqlParameter("@pUsuarioLogin", usuario.UsuarioLogin));
            command.Parameters.Add(new SqlParameter("@pSenha", usuario.Senha));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Excluir(Usuario usuario)
        {
            object ret;

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("DELETE FROM USUARIO WHERE IDUSUARIO = " + usuario.IdUsuario);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                ret = db.ExecuteNonQuery(dbCommand);
            }

            return ret.ToString();
        }

        public bool NomeExiste(Usuario usuario)
        {
            object ret;

            using (DbCommand dbCommand = db.GetSqlStringCommand("SELECT COUNT(*) FROM USUARIO WHERE CPF LIKE UPPER('" + usuario.CPF.Trim() + "')"))
            {
                //Executar Comando no Banco.
                ret = db.ExecuteScalar(dbCommand);

                if (Convert.ToInt32(ret) > 0)
                    return true;
            }

            return false;
        }

        private static Usuario PopularEntidade(DataTable dtUsuario, int i)
        {
            Usuario usuario = new Usuario();

            if (dtUsuario.Columns.Contains("IdUsuario"))
            {
                if (dtUsuario.Rows[i]["IdUsuario"] != DBNull.Value)
                {
                    usuario.IdUsuario = Convert.ToInt32(dtUsuario.Rows[i]["IdUsuario"].ToString());
                }
            }

            if (dtUsuario.Columns.Contains("IdGrupo"))
            {
                if (dtUsuario.Rows[i]["IdGrupo"] != DBNull.Value)
                {
                    usuario.IdGrupo = Convert.ToInt32(dtUsuario.Rows[i]["IdGrupo"].ToString());
                    usuario.Grupo = new GrupoDAO().ObterGrupoById(usuario.IdGrupo);
                }
            }

            if (dtUsuario.Columns.Contains("Nome"))
            {
                if (dtUsuario.Rows[i]["Nome"] != DBNull.Value)
                {
                    usuario.Nome = Convert.ToString(dtUsuario.Rows[i]["Nome"]);
                }
            }

            if (dtUsuario.Columns.Contains("CPF"))
            {
                if (dtUsuario.Rows[i]["CPF"] != DBNull.Value)
                {
                    usuario.CPF = Convert.ToString(dtUsuario.Rows[i]["CPF"]);
                }
            }

            if (dtUsuario.Columns.Contains("IdCidade"))
            {
                if (dtUsuario.Rows[i]["IdCidade"] != DBNull.Value)
                {
                    usuario.IdCidade = Convert.ToInt32(dtUsuario.Rows[i]["IdCidade"]);
                }
            }

            if (dtUsuario.Columns.Contains("IdEstado"))
            {
                if (dtUsuario.Rows[i]["IdEstado"] != DBNull.Value)
                {
                    usuario.IdEstado = Convert.ToInt32(dtUsuario.Rows[i]["IdEstado"]);
                }
            }

            if (dtUsuario.Columns.Contains("Telefone"))
            {
                if (dtUsuario.Rows[i]["Telefone"] != DBNull.Value)
                {
                    usuario.Telefone = Convert.ToString(dtUsuario.Rows[i]["Telefone"]);
                }
            }

            if (dtUsuario.Columns.Contains("Celular"))
            {
                if (dtUsuario.Rows[i]["Celular"] != DBNull.Value)
                {
                    usuario.Celular = Convert.ToString(dtUsuario.Rows[i]["Celular"]);
                }
            }

            if (dtUsuario.Columns.Contains("Email"))
            {
                if (dtUsuario.Rows[i]["Email"] != DBNull.Value)
                {
                    usuario.Email = Convert.ToString(dtUsuario.Rows[i]["Email"]);
                }
            }


            if (dtUsuario.Columns.Contains("UsuarioLogin"))
            {
                if (dtUsuario.Rows[i]["UsuarioLogin"] != DBNull.Value)
                {
                    usuario.UsuarioLogin = Convert.ToString(dtUsuario.Rows[i]["UsuarioLogin"]);
                }
            }

            if (dtUsuario.Columns.Contains("Senha"))
            {
                if (dtUsuario.Rows[i]["Senha"] != DBNull.Value)
                {
                    usuario.Senha = Convert.ToString(dtUsuario.Rows[i]["Senha"]);
                }
            }

            if (dtUsuario.Columns.Contains("Login"))
            {
                if (dtUsuario.Rows[i]["Login"] != DBNull.Value)
                {
                    usuario.Login = Convert.ToString(dtUsuario.Rows[i]["Login"]);
                }
            }

            if (dtUsuario.Columns.Contains("DtInclusao"))
            {
                if (dtUsuario.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    usuario.DtInclusao = Convert.ToDateTime(dtUsuario.Rows[i]["DtInclusao"].ToString());
                }
            }

            return usuario;
        }
    }
}
