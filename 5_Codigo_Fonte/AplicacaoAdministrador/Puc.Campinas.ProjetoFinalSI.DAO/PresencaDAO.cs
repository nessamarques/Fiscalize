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
    public class PresencaDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public List<Presenca> ObterPresencas()
        {
            List<Presenca> listaPresencas = new List<Presenca>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT * FROM Presenca ORDER BY IdPresenca desc");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPresenca = ds.Tables[0];

                        for (int i = 0; i < dtPresenca.Rows.Count; i++)
                        {
                            Presenca presenca = PopularEntidade(dtPresenca, i);
                            listaPresencas.Add(presenca);
                        }
                    }
                }
            }

            return listaPresencas;
        }

        public List<Presenca> ObterPresencasByIdSessao(int idSessao)
        {
            List<Presenca> listaPresencas = new List<Presenca>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT * FROM Presenca p ");
            sbQuery.Append("where p.IdSessao = " + idSessao + " ORDER BY IdPresenca desc");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPresenca = ds.Tables[0];

                        for (int i = 0; i < dtPresenca.Rows.Count; i++)
                        {
                            Presenca presenca = PopularEntidade(dtPresenca, i);
                            listaPresencas.Add(presenca);
                        }
                    }
                }
            }

            return listaPresencas;
        }

        public string Incluir(Presenca presenca)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();

            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO Presenca VALUES (@IdSessao, @IdPolitico, @DtInclusao, @Login)";

            command.Parameters.Add(new SqlParameter("@IdSessao", presenca.IdSessao));
            command.Parameters.Add(new SqlParameter("@IdPolitico", presenca.IdPolitico));
            command.Parameters.Add(new SqlParameter("@DtInclusao", presenca.DtInclusao));
            command.Parameters.Add(new SqlParameter("@Login", presenca.Login));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        private static Presenca PopularEntidade(DataTable dtPresenca, int i)
        {
            Presenca presenca = new Presenca();

            if (dtPresenca.Columns.Contains("IdPresenca"))
            {
                if (dtPresenca.Rows[i]["IdPresenca"] != DBNull.Value)
                {
                    presenca.IdPresenca = Convert.ToInt32(dtPresenca.Rows[i]["IdPresenca"].ToString());
                }
            }

            if (dtPresenca.Columns.Contains("IdSessao"))
            {
                if (dtPresenca.Rows[i]["IdSessao"] != DBNull.Value)
                {
                    presenca.IdSessao = Convert.ToInt32(dtPresenca.Rows[i]["IdSessao"]);
                }
            }

            if (dtPresenca.Columns.Contains("IdPolitico"))
            {
                if (dtPresenca.Rows[i]["IdPolitico"] != DBNull.Value)
                {
                    presenca.IdPolitico = Convert.ToInt32(dtPresenca.Rows[i]["IdPolitico"]);
                }
            }

            if (dtPresenca.Columns.Contains("Login"))
            {
                if (dtPresenca.Rows[i]["Login"] != DBNull.Value)
                {
                    presenca.Login = Convert.ToString(dtPresenca.Rows[i]["Login"]);
                }
            }

            if (dtPresenca.Columns.Contains("DtInclusao"))
            {
                if (dtPresenca.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    presenca.DtInclusao = Convert.ToDateTime(dtPresenca.Rows[i]["DtInclusao"].ToString());
                }
            }

            return presenca;
        }

        public List<Presenca> ObterFaltasSessoes(int idCargo)
        {
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select distinct(m.idmandato), count(m.IdMandato) as Faltas from Sessao s ");
            sbQuery.Append("inner join SessaoProposicao sp on s.IdSessao = sp.IdSessao ");
            sbQuery.Append("inner join Convocado c on c.IdSessao = s.IdSessao and IdSituacao = 4 ");
            sbQuery.Append("LEFT join Mandato m on m.IdCargo = c.IdCargo and m.IsMandatoAtual = 1 ");
            sbQuery.Append("LEFT join Votacao v on v.IdSessaoProposicao = sp.IdSessaoProposicao and v.IdMandato = m.IdMandato "); 
            sbQuery.Append("group by m.idMandato, IdVoto, m.IdCargo ");
            sbQuery.Append("having IdVoto IS NULL and m.IdCargo =  " + idCargo);
            sbQuery.Append("order by count(m.IdMandato) desc ");

            List<Presenca> listaPresencas = new List<Presenca>();

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPresenca = ds.Tables[0];

                        for (int i = 0; i < dtPresenca.Rows.Count; i++)
                        {
                            Presenca presenca = new Presenca(); //PopularEntidade(dtPresenca, i);

                            if (dtPresenca.Columns.Contains("IdMandato"))
                            {
                                if (dtPresenca.Rows[i]["IdMandato"] != DBNull.Value)
                                {
                                    presenca.Mandato = new MandatoDAO().ObterMandatoById(Convert.ToInt32(dtPresenca.Rows[i]["IdMandato"]));
                                    presenca.Politico = new PoliticoDAO().ObterPoliticoById(presenca.Mandato.IdPolitico);
                                }
                            }

                            if (dtPresenca.Columns.Contains("Faltas"))
                            {
                                if (dtPresenca.Rows[i]["Faltas"] != DBNull.Value)
                                {
                                    presenca.NroFaltas = Convert.ToInt32(dtPresenca.Rows[i]["Faltas"].ToString());
                                }
                            }

                            
                            listaPresencas.Add(presenca);
                        }
                    }
                }
            }

            return listaPresencas;
        }
    }
}
