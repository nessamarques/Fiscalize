using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Puc.Campinas.ProjetoFinalSI.DAO.Mandato;

namespace Puc.Campinas.ProjetoFinalSI.DAO
{
    public class VotacaoDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public string Incluir(Votacao voto)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();

            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO VOTACAO VALUES (@IdSessaoProposicao, @IdVoto, @IdMandato, @DtInclusao, @Login)";

            command.Parameters.Add(new SqlParameter("@IdSessaoProposicao", voto.IdSessaoProposicao));
            command.Parameters.Add(new SqlParameter("@IdVoto", voto.IdVoto));
            command.Parameters.Add(new SqlParameter("@IdMandato", voto.IdMandato));
            command.Parameters.Add(new SqlParameter("@DtInclusao", voto.DtInclusao));
            command.Parameters.Add(new SqlParameter("@Login", voto.Login));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public List<Votacao> ObterVotacoes()
        {
            List<Votacao> listVotacao = new List<Votacao>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT * FROM VOTACAO ORDER BY IDVOTACAO DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtVotacao = ds.Tables[0];

                        for (int i = 0; i < dtVotacao.Rows.Count; i++)
                        {
                            Votacao votacao = PopularEntidade(dtVotacao, i);
                            listVotacao.Add(votacao);
                        }
                    }
                }
            }

            return listVotacao;
        }

        public List<Votacao> ObterVotacoesByIdSessaoProposicao(int idSessaoProposicao)
        {
            List<Votacao> listVotacao = new List<Votacao>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select distinct m.IdMandato, v.IdVoto from Sessao s ");
            sbQuery.Append("inner join SessaoProposicao sp on s.IdSessao = sp.IdSessao ");
            sbQuery.Append("inner join Convocado c on c.IdSessao = s.IdSessao ");
            sbQuery.Append("LEFT join Mandato m on m.IdCargo = c.IdCargo and m.IsMandatoAtual = 1 ");
            sbQuery.Append("LEFT join Votacao v on v.IdSessaoProposicao = sp.IdSessaoProposicao and v.IdMandato = m.IdMandato ");
            sbQuery.Append("where sp.IdSessaoProposicao = " + idSessaoProposicao);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtVotacao = ds.Tables[0];

                        for (int i = 0; i < dtVotacao.Rows.Count; i++)
                        {
                            Votacao votacao = PopularEntidade(dtVotacao, i);
                            listVotacao.Add(votacao);
                        }
                    }
                }
            }

            return listVotacao;
        }

        private static Votacao PopularEntidade(DataTable dtVotacao, int i)
        {
            Votacao votacao = new Votacao();

            if (dtVotacao.Columns.Contains("IdVotacao"))
            {
                if (dtVotacao.Rows[i]["IdVotacao"] != DBNull.Value)
                {
                    votacao.IdVotacao = Convert.ToInt32(dtVotacao.Rows[i]["IdVotacao"].ToString());
                }
            }

            if (dtVotacao.Columns.Contains("IdSessaoProposicao"))
            {
                if (dtVotacao.Rows[i]["IdSessaoProposicao"] != DBNull.Value)
                {
                    votacao.IdSessaoProposicao = Convert.ToInt32(dtVotacao.Rows[i]["IdSessaoProposicao"]);
                }
            }

            if (dtVotacao.Columns.Contains("IdVoto"))
            {
                if (dtVotacao.Rows[i]["IdVoto"] != DBNull.Value)
                {
                    votacao.IdVoto = Convert.ToInt32(dtVotacao.Rows[i]["IdVoto"]);
                }
            }

            if (dtVotacao.Columns.Contains("IdMandato"))
            {
                if (dtVotacao.Rows[i]["IdMandato"] != DBNull.Value)
                {
                    votacao.IdMandato = Convert.ToInt32(dtVotacao.Rows[i]["IdMandato"]);
                    votacao.Mandato = new MandatoDAO().ObterMandatoById(votacao.IdMandato);
                    votacao.Politico = new PoliticoDAO().ObterPoliticoById(votacao.Mandato.IdPolitico);
                }
            }

            if (dtVotacao.Columns.Contains("Login"))
            {
                if (dtVotacao.Rows[i]["Login"] != DBNull.Value)
                {
                    votacao.Login = Convert.ToString(dtVotacao.Rows[i]["Login"]);
                }
            }

            if (dtVotacao.Columns.Contains("DtInclusao"))
            {
                if (dtVotacao.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    votacao.DtInclusao = Convert.ToDateTime(dtVotacao.Rows[i]["DtInclusao"].ToString());
                }
            }

            return votacao;
        }
    }
}
