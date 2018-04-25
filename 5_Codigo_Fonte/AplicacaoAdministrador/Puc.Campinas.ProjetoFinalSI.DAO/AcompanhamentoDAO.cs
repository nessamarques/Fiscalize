using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace Puc.Campinas.ProjetoFinalSI.DAO
{
    public class AcompanhamentoDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public List<Acompanhamento> ObterAcompanhamentos()
        {
            List<Acompanhamento> listAcompanhamento = new List<Acompanhamento>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT A.IdAcompanhamento, A.IdUsuario, A.IdPolitico, upper(p.Nome) as NomePolitico, A.IsNoticia, A.IsDespesa, A.IsProposicao, A.Login, A.DtInclusao ");
            sbQuery.Append("FROM Acompanhamento A ");
            sbQuery.Append("inner join politico p on p.IdPolitico = a.IdPolitico ");
            sbQuery.Append("ORDER BY A.IdAcompanhamento ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtAcompanhamento = ds.Tables[0];

                        for (int i = 0; i < dtAcompanhamento.Rows.Count; i++)
                        {
                            Acompanhamento acompanhamento = PopularEntidade(dtAcompanhamento, i);
                            listAcompanhamento.Add(acompanhamento);
                        }
                    }
                }
            }

            return listAcompanhamento;
        }

        public List<Acompanhamento> ObterAcompanhamentosByIdUsuario(int idUsuario)
        {
            List<Acompanhamento> listaAcompanhamentos = new List<Acompanhamento>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT A.IdAcompanhamento, A.IdUsuario, A.IdPolitico, A.IsNoticia, A.IsDespesa, A.IsProposicao, A.Login, A.DtInclusao ");
            sbQuery.Append("FROM Acompanhamento A WHERE A.IdUsuario = " + idUsuario + " ORDER BY A.IdAcompanhamento DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtAcompanhamento = ds.Tables[0];

                        for (int i = 0; i < dtAcompanhamento.Rows.Count; i++)
                        {
                            Acompanhamento acompanhamento = PopularEntidade(dtAcompanhamento, i);
                            listaAcompanhamentos.Add(acompanhamento);
                        }
                    }
                }
            }

            return listaAcompanhamentos;
        }

        public string Incluir(Acompanhamento acompanhamento)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO Acompanhamento VALUES(@pIdUsuario, @pIdPolitico, @pIsNoticia, @pIsDespesa, @pIsProposicao, @pLogin, @pDtInclusao)";
            command.Parameters.Add(new SqlParameter("@pIdUsuario", acompanhamento.IdUsuario));
            command.Parameters.Add(new SqlParameter("@pIdPolitico", acompanhamento.IdPolitico));
            command.Parameters.Add(new SqlParameter("@pIsNoticia", acompanhamento.IsNoticia));
            command.Parameters.Add(new SqlParameter("@pIsDespesa", acompanhamento.IsDespesa));
            command.Parameters.Add(new SqlParameter("@pIsProposicao", acompanhamento.IsProposicao));
            command.Parameters.Add(new SqlParameter("@pLogin", acompanhamento.Login));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", acompanhamento.DtInclusao));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Alterar(Acompanhamento acompanhamento)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "UPDATE Acompanhamento SET IsNoticia = @pIsNoticia, IsDespesa = @pIsDespesa, IsProposicao = @pIsProposicao ";
            command.CommandText += "WHERE IdUsuario = @pIdUsuario and IdPolitico = @pIdPolitico";
            command.Parameters.Add(new SqlParameter("@pIdUsuario", acompanhamento.IdUsuario));
            command.Parameters.Add(new SqlParameter("@pIdPolitico", acompanhamento.IdPolitico));
            command.Parameters.Add(new SqlParameter("@pIsNoticia", acompanhamento.IsNoticia));
            command.Parameters.Add(new SqlParameter("@pIsDespesa", acompanhamento.IsDespesa));
            command.Parameters.Add(new SqlParameter("@pIsProposicao", acompanhamento.IsProposicao));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Excluir(Acompanhamento acompanhamento)
        {
            object ret;

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("DELETE FROM Acompanhamento WHERE IdAcompanhamento = " + acompanhamento.IdAcompanhamento);

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

        public string Excluir(int idPolitico, int idUsuario)
        {
            object ret;

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("DELETE FROM Acompanhamento WHERE IdPolitico = " + idPolitico + " AND IdUsuario = " + idUsuario);

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

        private static Acompanhamento PopularEntidade(DataTable dtAcompanhamento, int i)
        {
            Acompanhamento acompanhamento = new Acompanhamento();

            if (dtAcompanhamento.Columns.Contains("IdAcompanhamento"))
            {
                if (dtAcompanhamento.Rows[i]["IdAcompanhamento"] != DBNull.Value)
                {
                    acompanhamento.IdAcompanhamento = Convert.ToInt32(dtAcompanhamento.Rows[i]["IdAcompanhamento"]);
                }
            }

            if (dtAcompanhamento.Columns.Contains("IdUsuario"))
            {
                if (dtAcompanhamento.Rows[i]["IdUsuario"] != DBNull.Value)
                {
                    acompanhamento.IdUsuario = Convert.ToInt32(dtAcompanhamento.Rows[i]["IdUsuario"]);
                }
            }

            if (dtAcompanhamento.Columns.Contains("IdPolitico"))
            {
                if (dtAcompanhamento.Rows[i]["IdPolitico"] != DBNull.Value)
                {
                    acompanhamento.IdPolitico = Convert.ToInt32(dtAcompanhamento.Rows[i]["IdPolitico"]);
                }
            }

            if (dtAcompanhamento.Columns.Contains("IsNoticia"))
            {
                if (dtAcompanhamento.Rows[i]["IsNoticia"] != DBNull.Value)
                {
                    acompanhamento.IsNoticia = Convert.ToInt32(dtAcompanhamento.Rows[i]["IsNoticia"]);
                }
            }

            if (dtAcompanhamento.Columns.Contains("IsDespesa"))
            {
                if (dtAcompanhamento.Rows[i]["IsDespesa"] != DBNull.Value)
                {
                    acompanhamento.IsDespesa = Convert.ToInt32(dtAcompanhamento.Rows[i]["IsDespesa"]);
                }
            }

            if (dtAcompanhamento.Columns.Contains("IsProposicao"))
            {
                if (dtAcompanhamento.Rows[i]["IsProposicao"] != DBNull.Value)
                {
                    acompanhamento.IsProposicao = Convert.ToInt32(dtAcompanhamento.Rows[i]["IsProposicao"]);
                }
            }
            
            if (dtAcompanhamento.Columns.Contains("Login"))
            {
                if (dtAcompanhamento.Rows[i]["Login"] != DBNull.Value)
                {
                    acompanhamento.Login = dtAcompanhamento.Rows[i]["Login"].ToString();
                }
            }


            if (dtAcompanhamento.Columns.Contains("DtInclusao"))
            {
                if (dtAcompanhamento.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    acompanhamento.DtInclusao = Convert.ToDateTime(dtAcompanhamento.Rows[i]["DtInclusao"]);
                }
            }


            if (dtAcompanhamento.Columns.Contains("NomePolitico"))
            {
                if (dtAcompanhamento.Rows[i]["NomePolitico"] != DBNull.Value)
                {
                    acompanhamento.NomePolitico = dtAcompanhamento.Rows[i]["NomePolitico"].ToString();
                }
            }


            return acompanhamento;
        }
    }
}
