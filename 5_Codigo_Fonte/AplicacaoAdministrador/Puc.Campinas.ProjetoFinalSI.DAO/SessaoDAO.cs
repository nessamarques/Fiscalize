using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace Puc.Campinas.ProjetoFinalSI.DAO
{
    public class SessaoDAO
    {
        public string Incluir(Sessao sessao)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();

            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO SESSAO (DtInicial, DtFinal, Nome, Descricao, IdSituacao, IdOrador, Pauta, DtInclusao, Login) VALUES (@DtInicial, @DtFinal, @Nome, @Descricao, @IdSituacao, @IdOrador, @Pauta, @DtInclusao, @Login)";

            command.Parameters.Add(new SqlParameter("@DtInicial", sessao.DtInicial));
            command.Parameters.Add(new SqlParameter("@DtFinal", sessao.DtFinal));
            command.Parameters.Add(new SqlParameter("@Nome", sessao.Nome));
            command.Parameters.Add(new SqlParameter("@Descricao", sessao.Descricao));
            command.Parameters.Add(new SqlParameter("@IdSituacao", sessao.IdSituacao));
            command.Parameters.Add(new SqlParameter("@IdOrador", sessao.IdOrador));
            command.Parameters.Add(new SqlParameter("@Pauta", DBNull.Value));
            command.Parameters.Add(new SqlParameter("@DtInclusao", sessao.DtInclusao));
            command.Parameters.Add(new SqlParameter("@Login", sessao.Login));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Excluir(Sessao sessao)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();

            SqlCommand deleteSessao = new SqlCommand();

            deleteSessao.CommandText = "DELETE FROM SESSAO WHERE IDSESSAO = " + sessao.IdSessao;

            ret = db.ExecuteNonQuery(deleteSessao);

            return ret.ToString();
        }

        public string Alterar(Sessao sessao)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();

            SqlCommand command = new SqlCommand();

            command.CommandText = "UPDATE SESSAO SET Nome = @pNome, Descricao = @pDescricao, IdSituacao = @pIdSituacao, Pauta = @pPauta, IdOrador = @pIdOrador, DtInicial = @pDtInicial, DtFinal = @pDtFinal";
            command.CommandText += " WHERE IDSESSAO = " + sessao.IdSessao;

            command.Parameters.Add(new SqlParameter("@pNome", sessao.Nome));
            command.Parameters.Add(new SqlParameter("@pDescricao", sessao.Descricao));
            command.Parameters.Add(new SqlParameter("@pIdSituacao", sessao.IdSituacao));
            command.Parameters.Add(new SqlParameter("@pPauta", sessao.Pauta));
            command.Parameters.Add(new SqlParameter("@pIdOrador", sessao.IdOrador));
            command.Parameters.Add(new SqlParameter("@pDtInicial", sessao.DtInicial));
            command.Parameters.Add(new SqlParameter("@pDtFinal", sessao.DtFinal));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public List<Sessao> ObterSessoes(string nome = null, string situacao = null, DateTime? inicio = null, DateTime? termino = null)
        {
            //Instânciando a Lista Tipada.
            List<Sessao> listSessao = new List<Sessao>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SESSAO WHERE 1 = 1");
            if (nome != null && nome != string.Empty)
            {
                sb.Append(" AND Nome like '%' + '" + nome + "' + '%'");
            }
            
            if (situacao != null && situacao != string.Empty)
            {
                sb.Append(" AND IdSituacao = " + situacao);
            }

            if (inicio != null && inicio != DateTime.MinValue)
            {
                if (termino != null && termino != DateTime.MinValue)
                {
                    sb.Append(" AND dtinicial >= '" + inicio + "' and dtfinal <= '" + termino + "'");
                }
            }

            sb.Append("ORDER BY DTINICIAL ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtSessao = ds.Tables[0];

                        for (int i = 0; i < dtSessao.Rows.Count; i++)
                        {
                            Sessao sessao = PopularEntidade(dtSessao, i);
                            listSessao.Add(sessao);
                            sessao = null;
                        }
                    }
                }
            }

            return listSessao;
        }

        public Sessao ObterSessaoById(int idSessao)
        {
            //Instânciando a Lista Tipada.
            List<Sessao> listSessao = new List<Sessao>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            using (DbCommand dbCommand = db.GetSqlStringCommand("SELECT * FROM SESSAO WHERE IDSESSAO = " + idSessao))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtSessao = ds.Tables[0];

                        for (int i = 0; i < dtSessao.Rows.Count; i++)
                        {
                            Sessao sessao = PopularEntidade(dtSessao, i);
                            listSessao.Add(sessao);
                            sessao = null;
                        }
                    }
                }
            }

            return listSessao[0];
        }

        private static Sessao PopularEntidade(DataTable dtSessao, int i)
        {
            //Criando um objeto do tipo CARGO.
            Sessao sessao = new Sessao();

            if (dtSessao.Columns.Contains("IdSessao"))
            {
                if (dtSessao.Rows[i]["IdSessao"] != DBNull.Value)
                {
                    sessao.IdSessao = Convert.ToInt32(dtSessao.Rows[i]["IdSessao"].ToString());
                }
            }

            if (dtSessao.Columns.Contains("DtInicial"))
            {
                if (dtSessao.Rows[i]["DtInicial"] != DBNull.Value)
                {
                    sessao.DtInicial = Convert.ToDateTime(dtSessao.Rows[i]["DtInicial"]);
                }
            }

            if (dtSessao.Columns.Contains("DtFinal"))
            {
                if (dtSessao.Rows[i]["DtFinal"] != DBNull.Value)
                {
                    sessao.DtFinal = Convert.ToDateTime(dtSessao.Rows[i]["DtFinal"]);
                }
            }

            if (dtSessao.Columns.Contains("Nome"))
            {
                if (dtSessao.Rows[i]["Nome"] != DBNull.Value)
                {
                    sessao.Nome = Convert.ToString(dtSessao.Rows[i]["Nome"]);
                }
            }

            if (dtSessao.Columns.Contains("Descricao"))
            {
                if (dtSessao.Rows[i]["Descricao"] != DBNull.Value)
                {
                    sessao.Descricao = Convert.ToString(dtSessao.Rows[i]["Descricao"]);
                }
            }

            if (dtSessao.Columns.Contains("IdSituacao"))
            {
                if (dtSessao.Rows[i]["IdSituacao"] != DBNull.Value)
                {
                    sessao.IdSituacao = Convert.ToInt32(dtSessao.Rows[i]["IdSituacao"]);
                }
            }

            if (dtSessao.Columns.Contains("IdOrador"))
            {
                if (dtSessao.Rows[i]["IdOrador"] != DBNull.Value)
                {
                    sessao.IdOrador = Convert.ToInt32(dtSessao.Rows[i]["IdOrador"]);
                }
            }

            if (dtSessao.Columns.Contains("Pauta"))
            {
                if (dtSessao.Rows[i]["Pauta"] != DBNull.Value)
                {
                    sessao.Pauta = Convert.ToString(dtSessao.Rows[i]["Pauta"]);
                }
            }
            
            if (dtSessao.Columns.Contains("Login"))
            {
                if (dtSessao.Rows[i]["Login"] != DBNull.Value)
                {
                    sessao.Login = Convert.ToString(dtSessao.Rows[i]["Login"]);
                }
            }

            if (dtSessao.Columns.Contains("DtInclusao"))
            {
                if (dtSessao.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    sessao.DtInclusao = Convert.ToDateTime(dtSessao.Rows[i]["DtInclusao"].ToString());
                }
            }

            return sessao;
        }
    }
}
