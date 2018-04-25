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

    public class PartidoDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public List<Partido> ObterPartidos()
        {
            List<Partido> listPartidos = new List<Partido>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT P.IDPARTIDO,P.SIGLA as SIGLA,UPPER(P.NOME) AS NOME,P.PRESIDENTENACIONAL,");
            sbQuery.Append("P.DEFERIMENTO,P.NROPARTIDO,P.LOGIN,P.DTINCLUSAO FROM PARTIDO P ORDER BY P.SIGLA ASC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPartidos = ds.Tables[0];

                        for (int i = 0; i < dtPartidos.Rows.Count; i++)
                        {
                            Partido partido = PopularEntidade(dtPartidos, i);
                            listPartidos.Add(partido);
                        }
                    }
                }
            }

            return listPartidos;
        }

        public Partido ObterPartidoById(int idPartido)
        {
            Partido partido = new Partido();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT P.IDPARTIDO,P.SIGLA as SIGLA,UPPER(P.NOME) AS NOME,P.PRESIDENTENACIONAL,P.DEFERIMENTO,P.NROPARTIDO,");
            sbQuery.Append("P.LOGIN,P.DTINCLUSAO FROM PARTIDO P WHERE P.IDPARTIDO = " + idPartido + "ORDER BY P.SIGLA ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPartido = ds.Tables[0];

                        for (int i = 0; i < dtPartido.Rows.Count; i++)
                        {
                            partido = PopularEntidade(dtPartido, i);
                        }
                    }
                }
            }

            return partido;
        }

        public List<Partido> ObterPartidos(string pString)
        {
            List<Partido> listPartidos = new List<Partido>();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT P.IDPARTIDO,P.SIGLA as SIGLA,UPPER(P.NOME) AS NOME,P.PRESIDENTENACIONAL,P.DEFERIMENTO,P.NROPARTIDO,");
            sbQuery.Append("P.LOGIN,P.DTINCLUSAO FROM PARTIDO P WHERE P.SIGLA LIKE '%' + '" + @pString + "' + '%' OR P.NOME LIKE '%' + '" + @pString + "' + '%' " + "ORDER BY P.SIGLA ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPartidos = ds.Tables[0];

                        for (int i = 0; i < dtPartidos.Rows.Count; i++)
                        {
                            Partido partido = PopularEntidade(dtPartidos, i);
                            listPartidos.Add(partido);
                        }
                    }
                }
            }

            return listPartidos;
        }

        public string Incluir(Partido partido)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO PARTIDO VALUES(@pSigla, @pNome, @pDeferimento, @pPresidenteNacional, @pNroPartido, @pLogin, @pDtInclusao)";
            command.Parameters.Add(new SqlParameter("@pSigla", partido.Sigla));
            command.Parameters.Add(new SqlParameter("@pNome", partido.Nome));
            command.Parameters.Add(new SqlParameter("@pDeferimento", partido.Deferimento));
            command.Parameters.Add(new SqlParameter("@pPresidenteNacional", partido.PresidenteNacional));
            command.Parameters.Add(new SqlParameter("@pNroPartido", partido.NroPartido));
            command.Parameters.Add(new SqlParameter("@pLogin", partido.Login));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", partido.DtInclusao));
            
            ret = db.ExecuteNonQuery(command);
            
            return ret.ToString();
        }

        public string Alterar(Partido partido)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "UPDATE PARTIDO SET Sigla = @pSigla, Nome = @pNome, Deferimento = @pDeferimento, PresidenteNacional = @pPresidenteNacional, NroPartido = @pNroPartido ";
            command.CommandText += "WHERE IdPartido = @pIdPartido";
            command.Parameters.Add(new SqlParameter("@pIdPartido", partido.IdPartido));
            command.Parameters.Add(new SqlParameter("@pSigla", partido.Sigla));
            command.Parameters.Add(new SqlParameter("@pNome", partido.Nome));
            command.Parameters.Add(new SqlParameter("@pDeferimento", partido.Deferimento));
            command.Parameters.Add(new SqlParameter("@pPresidenteNacional", partido.PresidenteNacional));
            command.Parameters.Add(new SqlParameter("@pNroPartido", partido.NroPartido));
   
            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Excluir(Partido partido)
        {
            object ret;

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("DELETE FROM PARTIDO WHERE IDPARTIDO = " + partido.IdPartido);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                try
                {
                    ret = db.ExecuteNonQuery(dbCommand);
                }
                catch (Exception e )
                {
                    //TODO: PARTIDO NÃO PODE SER EXCLUIDO POIS JA ESTA SENDO UTILIZADO POR UM MANDATO
                    throw(e);
                }
            }

            return ret.ToString();
        }

        public bool NomeExiste(Partido partido)
        {
            object ret;

            using (DbCommand dbCommand = db.GetSqlStringCommand("SELECT COUNT(*) FROM PARTIDO P WHERE UPPER(P.NOME) LIKE UPPER('" + partido.Nome.Trim() + "') or P.SIGLA LIKE '" + partido.Sigla.Trim() + "' or P.NROPARTIDO = "+ partido.NroPartido ))
            {
                //Executar Comando no Banco.
                ret = db.ExecuteScalar(dbCommand);

                if (Convert.ToInt32(ret) > 0)
                    return true;
            }

            return false;
        }

        private static Partido PopularEntidade(DataTable dtPartido, int i)
        {
            Partido partido = new Partido();

            if (dtPartido.Columns.Contains("IdPartido"))
            {
                if (dtPartido.Rows[i]["IdPartido"] != DBNull.Value)
                {
                    partido.IdPartido = Convert.ToInt32(dtPartido.Rows[i]["IdPartido"].ToString());
                }
            }

            if (dtPartido.Columns.Contains("Sigla"))
            {
                if (dtPartido.Rows[i]["Sigla"] != DBNull.Value)
                {
                    partido.Sigla = Convert.ToString(dtPartido.Rows[i]["Sigla"]);
                }
            }

            if (dtPartido.Columns.Contains("Nome"))
            {
                if (dtPartido.Rows[i]["Nome"] != DBNull.Value)
                {
                    partido.Nome = Convert.ToString(dtPartido.Rows[i]["Nome"]);
                }
            }

            if (dtPartido.Columns.Contains("Deferimento"))
            {
                if (dtPartido.Rows[i]["Deferimento"] != DBNull.Value)
                {
                    partido.Deferimento = Convert.ToDateTime(dtPartido.Rows[i]["Deferimento"]);
                }
            }

            if (dtPartido.Columns.Contains("PresidenteNacional"))
            {
                if (dtPartido.Rows[i]["PresidenteNacional"] != DBNull.Value)
                {
                    partido.PresidenteNacional = Convert.ToString(dtPartido.Rows[i]["PresidenteNacional"]);
                }
            }

            if (dtPartido.Columns.Contains("NroPartido"))
            {
                if (dtPartido.Rows[i]["NroPartido"] != DBNull.Value)
                {
                    partido.NroPartido = Convert.ToInt32(dtPartido.Rows[i]["NroPartido"]);
                }
            }

            if (dtPartido.Columns.Contains("Login"))
            {
                if (dtPartido.Rows[i]["Login"] != DBNull.Value)
                {
                    partido.Login = Convert.ToString(dtPartido.Rows[i]["Login"]);
                }
            }

            if (dtPartido.Columns.Contains("DtInclusao"))
            {
                if (dtPartido.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    partido.DtInclusao = Convert.ToDateTime(dtPartido.Rows[i]["DtInclusao"].ToString());
                }
            }

            return partido;
        }

        public bool VerificaSePartidoEstaSendoUsado(int idPartido)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT 1 FROM Mandato WHERE IdPartido = " + idPartido;
            
            ret = db.ExecuteScalar(command);
            
            return Convert.ToBoolean(ret);
        }
    }
}
