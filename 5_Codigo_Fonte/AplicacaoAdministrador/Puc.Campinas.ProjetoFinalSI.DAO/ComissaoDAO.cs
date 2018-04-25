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

    public class ComissaoDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public List<Comissao> ObterComissao()
        {
            List<Comissao> listComissao = new List<Comissao>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT C.IDCOMISSAO, UPPER(C.NOME) as Nome, Upper(C.Sigla) as Sigla, C.LOCAL, C.TELEFONE, C.FAX, C.SECRETARIO, C.DTINCLUSAO, C.LOGIN  ");
            sbQuery.Append("FROM COMISSAO C ORDER BY C.NOME ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtComissao = ds.Tables[0];

                        for (int i = 0; i < dtComissao.Rows.Count; i++)
                        {
                            Comissao comissao = PopularEntidade(dtComissao, i);
                            listComissao.Add(comissao);
                        }
                    }
                }
            }

            return listComissao;
        }

        public Comissao ObterComissaoById(int idComissao)
        {
            Comissao comissao = new Comissao();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT C.IDCOMISSAO, UPPER(C.NOME) AS Nome, Upper(C.Sigla) as Sigla, C.LOCAL, C.TELEFONE, C.FAX, C.SECRETARIO, C.DTINCLUSAO, C.LOGIN  ");
            sbQuery.Append("FROM COMISSAO C WHERE C.IDCOMISSAO = " + idComissao + " ORDER BY IDCOMISSAO DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtComissao = ds.Tables[0];

                        for (int i = 0; i < dtComissao.Rows.Count; i++)
                        {
                            comissao = PopularEntidade(dtComissao, i);
                        }
                    }
                }
            }

            return comissao;
        }

        public List<Comissao> ObterComissao(string pString)
        {
            List<Comissao> listComissao = new List<Comissao>();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT C.IDCOMISSAO, UPPER(C.NOME) AS Nome, Upper(C.Sigla) as Sigla, C.LOCAL, C.TELEFONE, C.FAX, C.SECRETARIO, C.DTINCLUSAO, C.LOGIN  ");
            sbQuery.Append("FROM COMISSAO C WHERE C.NOME LIKE '%' + '" + @pString + "' + '%' " + "ORDER BY IDCOMISSAO DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtComissao = ds.Tables[0];

                        for (int i = 0; i < dtComissao.Rows.Count; i++)
                        {
                            Comissao comissao = PopularEntidade(dtComissao, i);
                            listComissao.Add(comissao);
                        }
                    }
                }
            }

            return listComissao;
        }

        public string Incluir(Comissao comissao)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO COMISSAO VALUES( @pNome, @pSigla, @pLocal, @pTelefone, @pFax, @pSecretario, @pDtInclusao, @pLogin )";
            command.Parameters.Add(new SqlParameter("@pNome", comissao.Nome));
            command.Parameters.Add(new SqlParameter("@pSigla", comissao.Sigla));
            command.Parameters.Add(new SqlParameter("@pLocal", comissao.Local));
            command.Parameters.Add(new SqlParameter("@pTelefone", comissao.Telefone.Replace("(", "").Replace(")", "").Replace("-", "")));
            command.Parameters.Add(new SqlParameter("@pFax", comissao.Fax.Replace("(", "").Replace(")", "").Replace("-", "")));
            command.Parameters.Add(new SqlParameter("@pSecretario", comissao.Secretario));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", comissao.DtInclusao));
            command.Parameters.Add(new SqlParameter("@pLogin", comissao.Login));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Alterar(Comissao comissao)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "UPDATE COMISSAO SET Nome = @pNome, Sigla = @pSigla, Local = @pLocal, Telefone = @pTelefone, Fax = @pFax, Secretario = @pSecretario ";
            command.CommandText += "WHERE IdComissao = @pIdComissao";
            command.Parameters.Add(new SqlParameter("@pIdComissao", comissao.IdComissao));
            command.Parameters.Add(new SqlParameter("@pNome", comissao.Nome));
            command.Parameters.Add(new SqlParameter("@pSigla", comissao.Sigla));
            command.Parameters.Add(new SqlParameter("@pLocal", comissao.Local));
            command.Parameters.Add(new SqlParameter("@pTelefone", comissao.Telefone));
            command.Parameters.Add(new SqlParameter("@pFax", comissao.Fax));
            command.Parameters.Add(new SqlParameter("@pSecretario", comissao.Secretario));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Excluir(Comissao comissao)
        {
            object ret;

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("DELETE FROM COMISSAO WHERE IDCOMISSAO = " + comissao.IdComissao);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                ret = db.ExecuteNonQuery(dbCommand);
            }

            return ret.ToString();
        }

        public bool NomeExiste(Comissao comissao)
        {
            object ret;

            using (DbCommand dbCommand = db.GetSqlStringCommand("SELECT COUNT(*) FROM COMISSAO WHERE NOME LIKE UPPER('" + comissao.Nome.Trim() + "')"))
            {
                //Executar Comando no Banco.
                ret = db.ExecuteScalar(dbCommand);

                if (Convert.ToInt32(ret) > 0)
                    return true;
            }

            return false;
        }

        public bool VerificaSeComissaoEstaSendoUsada(int idComissao)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT 1 FROM MembroComissao WHERE IdComissao = " + idComissao;

            ret = db.ExecuteScalar(command);

            return Convert.ToBoolean(ret);
        }

        private static Comissao PopularEntidade(DataTable dtComissao, int i)
        {
            Comissao comissao = new Comissao();

            if (dtComissao.Columns.Contains("IdComissao"))
            {
                if (dtComissao.Rows[i]["IdComissao"] != DBNull.Value)
                {
                    comissao.IdComissao = Convert.ToInt32(dtComissao.Rows[i]["IdComissao"].ToString());
                }
            }

            if (dtComissao.Columns.Contains("Nome"))
            {
                if (dtComissao.Rows[i]["Nome"] != DBNull.Value)
                {
                    comissao.Nome = Convert.ToString(dtComissao.Rows[i]["Nome"]);
                }
            }

            if (dtComissao.Columns.Contains("Sigla"))
            {
                if (dtComissao.Rows[i]["Sigla"] != DBNull.Value)
                {
                    comissao.Sigla = Convert.ToString(dtComissao.Rows[i]["Sigla"]);
                }
            }

            if (dtComissao.Columns.Contains("Local"))
            {
                if (dtComissao.Rows[i]["Local"] != DBNull.Value)
                {
                    comissao.Local = Convert.ToString(dtComissao.Rows[i]["Local"]);
                }
            }

            if (dtComissao.Columns.Contains("Telefone"))
            {
                if (dtComissao.Rows[i]["Telefone"] != DBNull.Value)
                {
                    comissao.Telefone = Convert.ToString(dtComissao.Rows[i]["Telefone"]);
                }
            }

            if (dtComissao.Columns.Contains("Fax"))
            {
                if (dtComissao.Rows[i]["Fax"] != DBNull.Value)
                {
                    comissao.Fax = Convert.ToString(dtComissao.Rows[i]["Fax"]);
                }
            }

            if (dtComissao.Columns.Contains("Secretario"))
            {
                if (dtComissao.Rows[i]["Secretario"] != DBNull.Value)
                {
                    comissao.Secretario = Convert.ToString(dtComissao.Rows[i]["Secretario"]);
                }
            }

            if (dtComissao.Columns.Contains("DtInclusao"))
            {
                if (dtComissao.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    comissao.DtInclusao = Convert.ToDateTime(dtComissao.Rows[i]["DtInclusao"].ToString());
                }
            }
            
            if (dtComissao.Columns.Contains("Login"))
            {
                if (dtComissao.Rows[i]["Login"] != DBNull.Value)
                {
                    comissao.Login = Convert.ToString(dtComissao.Rows[i]["Login"]);
                }
            }

            return comissao;
        }
    }
}

