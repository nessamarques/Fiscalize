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
    using Puc.Campinas.ProjetoFinalSI.DAO.Mandato;

    public class ProposicaoDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public List<Proposicao> ObterProposicao()
        {
            //Instânciando a Lista Tipada.
            List<Proposicao> listaProposicao = new List<Proposicao>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            using (DbCommand dbCommand = db.GetSqlStringCommand("SELECT  * FROM PROPOSICAO D ORDER BY D.PROPOSICAO ASC "))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtProposicao = ds.Tables[0];

                        for (int i = 0; i < dtProposicao.Rows.Count; i++)
                        {
                            Proposicao proposicao = PopularEntidade(dtProposicao, i);
                            listaProposicao.Add(proposicao);
                            proposicao = null;
                        }
                    }
                }
            }

            return listaProposicao;
        }

        public List<Proposicao> ObterProposicao(string pString)
        {
            List<Proposicao> listProposicao = new List<Proposicao>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select p.IdProposicao, p.IdTipo, p.IdMandato, p.ANO, p.Numero , p.Situacao , p.DtApresentacao, p.Ementa ");
            sbQuery.Append("FROM Proposicao p WHERE p.Ementa LIKE '%" + @pString + "%' ORDER BY p.Ementa ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtProposicao = ds.Tables[0];

                        for (int i = 0; i < dtProposicao.Rows.Count; i++)
                        {
                            Proposicao proposicao = PopularEntidade(dtProposicao, i);
                            listProposicao.Add(proposicao);
                        }
                    }
                }
            }

            return listProposicao;
        }

        public List<Proposicao> ObterProposicaoByIdPolitico(int idPolitico)
        {
            List<Proposicao> listProposicao = new List<Proposicao>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select a.*, p.Nome as NomePolitico ");
            sbQuery.Append(" from Proposicao a");
            sbQuery.Append(" inner join Politico p on p.IdPolitico = a.IdPolitico");
            sbQuery.Append(" where p.IdPolitico =  " + idPolitico);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtProposicao = ds.Tables[0];

                        for (int i = 0; i < dtProposicao.Rows.Count; i++)
                        {
                            Proposicao proposicao = PopularEntidade(dtProposicao, i);
                            listProposicao.Add(proposicao);
                        }
                    }
                }
            }

            return listProposicao;
        }

        public List<Proposicao> ObterProposicaoByIdSessao(int idSessao)
        {
            //List<Proposicao> listProposicao = new List<Proposicao>();

            //Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            //StringBuilder sbQuery = new StringBuilder();

            //sbQuery.Append("select a.*, p.Nome as NomePolitico ");
            //sbQuery.Append(" from Proposicao a");
            //sbQuery.Append(" inner join Politico p on p.IdPolitico = a.IdPolitico");
            //sbQuery.Append(" where p.IdPolitico =  " + idPolitico);

            //using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            //{
            //    using (DataSet ds = db.ExecuteDataSet(dbCommand))
            //    {
            //        if (ds != null && ds.Tables.Count > 0)
            //        {
            //            DataTable dtProposicao = ds.Tables[0];

            //            for (int i = 0; i < dtProposicao.Rows.Count; i++)
            //            {
            //                Proposicao proposicao = PopularEntidade(dtProposicao, i);
            //                listProposicao.Add(proposicao);
            //            }
            //        }
            //    }
            //}

            //return listProposicao;

            return null;
        }

        public Proposicao ObterProposicaoById(int idProposicao)
        {
            Proposicao proposicao = new Proposicao();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT p.IDPROPOSICAO, p.IdMandato, p.IDTIPO, p.NUMERO, p.ANO , p.SITUACAO, p.DTAPRESENTACAO, p.EMENTA ");
            sbQuery.Append("FROM PROPOSICAO p ");
            sbQuery.Append("WHERE p.IDPROPOSICAO = " + idProposicao + " ");
            sbQuery.Append("ORDER BY IDPROPOSICAO DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtProposicao = ds.Tables[0];

                        for (int i = 0; i < dtProposicao.Rows.Count; i++)
                        {
                            proposicao = PopularEntidade(dtProposicao, i);
                        }
                    }
                }
            }

            return proposicao;
        }

        public string Incluir(Proposicao proposicao)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO PROPOSICAO VALUES(@pIdTipo, @pIdMandato, @pNumero, @pAno, @pSituacao, @pDtApresentacao, @pEmenta, @pDtInclusao, @pLogin)";
            command.Parameters.Add(new SqlParameter("@pIdTipo", proposicao.IdTipo));
            command.Parameters.Add(new SqlParameter("@pIdMandato", proposicao.IdMandato));
            command.Parameters.Add(new SqlParameter("@pNumero", proposicao.Numero));
            command.Parameters.Add(new SqlParameter("@pAno", proposicao.Ano));
            command.Parameters.Add(new SqlParameter("@pSituacao", proposicao.Situacao));
            command.Parameters.Add(new SqlParameter("@pDtApresentacao", proposicao.DtApresentacao));
            command.Parameters.Add(new SqlParameter("@pEmenta", proposicao.Ementa));
            command.Parameters.Add(new SqlParameter("@pLogin", proposicao.Login));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", proposicao.DtInclusao));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Alterar(Proposicao proposicao)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "UPDATE PROPOSICAO SET IdTipo = @pIdTipo, IdMandato = @pIdMandato, Numero = @pNumero, Ano = @pAno, Situacao = @pSituacao, DtApresentacao = @pDtApresentacao, Ementa = @pEmenta ";
            command.CommandText += "WHERE IdProposicao = @pIdProposicao";
            command.Parameters.Add(new SqlParameter("@pIdProposicao", proposicao.IdProposicao));
            command.Parameters.Add(new SqlParameter("@pIdTipo", proposicao.IdTipo));
            command.Parameters.Add(new SqlParameter("@pIdMandato", proposicao.IdMandato));
            command.Parameters.Add(new SqlParameter("@pNumero", proposicao.Numero));
            command.Parameters.Add(new SqlParameter("@pAno", proposicao.Ano));
            command.Parameters.Add(new SqlParameter("@pSituacao", proposicao.Situacao));
            command.Parameters.Add(new SqlParameter("@pDtApresentacao", proposicao.DtApresentacao));
            command.Parameters.Add(new SqlParameter("@pEmenta", proposicao.Ementa));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Excluir(Proposicao proposicao)
        {
            object ret;

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("DELETE FROM PROPOSICAO WHERE IDPROPOSICAO = " + proposicao.IdProposicao);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                ret = db.ExecuteNonQuery(dbCommand);
            }

            return ret.ToString();
        }

        private static Proposicao PopularEntidade(DataTable dtProposicao, int i)
        {
            Proposicao proposicao = new Proposicao();

            if (dtProposicao.Columns.Contains("IdProposicao"))
            {
                if (dtProposicao.Rows[i]["IdProposicao"] != DBNull.Value)
                {
                    proposicao.IdProposicao = Convert.ToInt32(dtProposicao.Rows[i]["IdProposicao"].ToString());
                }
            }

            if (dtProposicao.Columns.Contains("IdTipo"))
            {
                if (dtProposicao.Rows[i]["IdTipo"] != DBNull.Value)
                {
                    proposicao.IdTipo = Convert.ToInt32(dtProposicao.Rows[i]["IdTipo"].ToString());
                    proposicao.Tipo = new TipoProposicaoDAO().ObterTipoProposicaoById(proposicao.IdTipo);
                }
            }

            if (dtProposicao.Columns.Contains("IdMandato"))
            {
                if (dtProposicao.Rows[i]["IdMandato"] != DBNull.Value)
                {
                    proposicao.IdMandato = Convert.ToInt32(dtProposicao.Rows[i]["IdMandato"].ToString());
                    proposicao.Mandato = new MandatoDAO().ObterMandatoById(proposicao.IdMandato);
                    proposicao.Politico = new PoliticoDAO().ObterPoliticoById(proposicao.Mandato.IdPolitico);
                }
            }

            if (dtProposicao.Columns.Contains("Numero"))
            {
                if (dtProposicao.Rows[i]["Numero"] != DBNull.Value)
                {
                    proposicao.Numero = Convert.ToString(dtProposicao.Rows[i]["Numero"]);
                }
            }

            if (dtProposicao.Columns.Contains("Ano"))
            {
                if (dtProposicao.Rows[i]["Ano"] != DBNull.Value)
                {
                    proposicao.Ano = Convert.ToString(dtProposicao.Rows[i]["Ano"]);
                }
            }

            if (dtProposicao.Columns.Contains("Situacao"))
            {
                if (dtProposicao.Rows[i]["Situacao"] != DBNull.Value)
                {
                    proposicao.Situacao = Convert.ToString(dtProposicao.Rows[i]["Situacao"]);
                }
            }

            if (dtProposicao.Columns.Contains("DtApresentacao"))
            {
                if (dtProposicao.Rows[i]["DtApresentacao"] != DBNull.Value)
                {
                    proposicao.DtApresentacao = Convert.ToDateTime(dtProposicao.Rows[i]["DtApresentacao"]);
                }
            }

            if (dtProposicao.Columns.Contains("Ementa"))
            {
                if (dtProposicao.Rows[i]["Ementa"] != DBNull.Value)
                {
                    proposicao.Ementa = Convert.ToString(dtProposicao.Rows[i]["Ementa"]);
                }
            }

            if (dtProposicao.Columns.Contains("DtInclusao"))
            {
                if (dtProposicao.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    proposicao.DtInclusao = Convert.ToDateTime(dtProposicao.Rows[i]["DtInclusao"].ToString());
                }
            }

            if (dtProposicao.Columns.Contains("Login"))
            {
                if (dtProposicao.Rows[i]["Login"] != DBNull.Value)
                {
                    proposicao.Login = Convert.ToString(dtProposicao.Rows[i]["Login"]);
                }
            }

            return proposicao;
        }

        public bool VerificaSeProposicaoEstaSendoUsada(int idProposicao)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT 1 FROM PROPOSICAO WHERE IDPROPOSICAO = " + idProposicao;

            ret = db.ExecuteScalar(command);

            return Convert.ToBoolean(ret);
        }
    }
}
