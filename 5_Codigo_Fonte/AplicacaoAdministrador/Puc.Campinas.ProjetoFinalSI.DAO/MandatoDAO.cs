namespace Puc.Campinas.ProjetoFinalSI.DAO.Mandato
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

    public class MandatoDAO
    {
        public List<Mandato> ObterMandatos()
        {
            List<Mandato> listMandatos = new List<Mandato>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select m.*, p.Nome as NomePolitico, c.Nome as Cargo, pt.Nome as Partido,");
            sbQuery.Append("  cast(convert(varchar(10), pm.DtInicio, 103)as varchar) + ' - ' +  cast(convert(varchar(10), pm.DtFim, 103)as varchar) as Periodo");
            sbQuery.Append(" from Mandato m");
            sbQuery.Append(" inner join PeriodoMandato pm on pm.IdPeriodoMandato = m.IdPeriodoMandato");
            sbQuery.Append(" inner join Politico p on p.IdPolitico = m.IdPolitico");
            sbQuery.Append(" inner join Cargo c on c.IdCargo = m.IdCargo");
            sbQuery.Append(" inner join Partido pt on pt.IdPartido = m.IdPartido");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtMandatos = ds.Tables[0];

                        for (int i = 0; i < dtMandatos.Rows.Count; i++)
                        {
                            Mandato mandato = PopularEntidade(dtMandatos, i);
                            listMandatos.Add(mandato);
                        }
                    }
                }
            }

            return listMandatos;
        }

        public List<Mandato> ObterMandatosByIdPolitico(int idPolitico)
        {
            List<Mandato> listMandatos = new List<Mandato>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select m.*, p.Nome as NomePolitico, c.Nome as Cargo, pt.Nome as Partido,");
            sbQuery.Append("  cast(convert(varchar(10), pm.DtInicio, 103)as varchar) + ' - ' +  cast(convert(varchar(10), pm.DtFim, 103)as varchar) as Periodo");
            sbQuery.Append(" from Mandato m");
            sbQuery.Append(" inner join PeriodoMandato pm on pm.IdPeriodoMandato = m.IdPeriodoMandato");
            sbQuery.Append(" inner join Politico p on p.IdPolitico = m.IdPolitico");
            sbQuery.Append(" inner join Cargo c on c.IdCargo = m.IdCargo");
            sbQuery.Append(" inner join Partido pt on pt.IdPartido = m.IdPartido");
            sbQuery.Append(" where p.IdPolitico =  " + idPolitico);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtMandatos = ds.Tables[0];

                        for (int i = 0; i < dtMandatos.Rows.Count; i++)
                        {
                            Mandato mandato = PopularEntidade(dtMandatos, i);
                            listMandatos.Add(mandato);
                        }
                    }
                }
            }

            return listMandatos;
        }

        public Mandato ObterMandatoById(int idMandato)
        {
            Mandato mandato = new Mandato();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT * FROM MANDATO P WHERE P.IDMANDATO = " + idMandato);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtMandato = ds.Tables[0];

                        for (int i = 0; i < dtMandato.Rows.Count; i++)
                        {
                            mandato = PopularEntidade(dtMandato, i);
                        }
                    }
                }
            }

            return mandato;
        }

        public string Incluir(Mandato mandato)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();

            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO MANDATO VALUES( @pIdPeriodoMandato, @pIdPolitico, @pIdCargo, @pIdPartido, @pIdCidade, @pIdEstado, @pGabinete, @pAnexo, @pTelefone, @pFax, @pEndereco, @pCEP, @pLogin, @pDtInclusao, @pIsMandatoAtual )";
            command.Parameters.Add(new SqlParameter("@pIdPeriodoMandato", mandato.IdPeriodoMandato));
            command.Parameters.Add(new SqlParameter("@pIdPolitico", mandato.IdPolitico));
            command.Parameters.Add(new SqlParameter("@pIdCargo", mandato.IdCargo));
            command.Parameters.Add(new SqlParameter("@pIdPartido", mandato.IdPartido));
            command.Parameters.Add(new SqlParameter("@pIdCidade", mandato.IdCidade));
            command.Parameters.Add(new SqlParameter("@pIdEstado", mandato.IdEstado));
            command.Parameters.Add(new SqlParameter("@pGabinete", mandato.Gabinete));
            command.Parameters.Add(new SqlParameter("@pAnexo", mandato.Anexo));
            command.Parameters.Add(new SqlParameter("@pTelefone", !string.IsNullOrEmpty(mandato.Telefone) ? mandato.Telefone.Replace("(", "").Replace(")", "").Replace("-", "") : mandato.Telefone));
            command.Parameters.Add(new SqlParameter("@pFax", !string.IsNullOrEmpty(mandato.Fax) ? mandato.Fax.Replace("(", "").Replace(")", "").Replace("-", "") : mandato.Fax));
            command.Parameters.Add(new SqlParameter("@pEndereco", mandato.Endereco));
            command.Parameters.Add(new SqlParameter("@pCEP", mandato.CEP));
            command.Parameters.Add(new SqlParameter("@pLogin", mandato.Login));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", mandato.DtInclusao));
            command.Parameters.Add(new SqlParameter("@pIsMandatoAtual", mandato.IsMandatoAtual));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string EncerrarMandato(int idMandato)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret;

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("update Mandato set IsMandatoAtual = 0 where IdMandato = " + idMandato);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                ret = db.ExecuteNonQuery(dbCommand);
            }

            return ret.ToString();
        }

        private static Mandato PopularEntidade(DataTable dtMandato, int i)
        {
            Mandato mandato = new Mandato();

            if (dtMandato.Columns.Contains("IdMandato"))
            {
                if (dtMandato.Rows[i]["IdMandato"] != DBNull.Value)
                {
                    mandato.IdMandato = Convert.ToInt32(dtMandato.Rows[i]["IdMandato"].ToString());
                }
            }

            if (dtMandato.Columns.Contains("IdPeriodoMandato"))
            {
                if (dtMandato.Rows[i]["IdPeriodoMandato"] != DBNull.Value)
                {
                    mandato.IdPeriodoMandato = Convert.ToInt32(dtMandato.Rows[i]["IdPeriodoMandato"].ToString());
                    mandato.PeriodoMandato = new PeriodoMandatoDAO().ObterPeriodosMandatosById(mandato.IdPeriodoMandato);

                }
            }
            
            if (dtMandato.Columns.Contains("IdPolitico"))
            {
                if (dtMandato.Rows[i]["IdPolitico"] != DBNull.Value)
                {
                    mandato.IdPolitico = Convert.ToInt32(dtMandato.Rows[i]["IdPolitico"].ToString());
                }
            }

            if (dtMandato.Columns.Contains("IdCargo"))
            {
                if (dtMandato.Rows[i]["IdCargo"] != DBNull.Value)
                {
                    mandato.IdCargo = Convert.ToInt32(dtMandato.Rows[i]["IdCargo"].ToString());
                }
            }

            if (dtMandato.Columns.Contains("IdPartido"))
            {
                if (dtMandato.Rows[i]["IdPartido"] != DBNull.Value)
                {
                    mandato.IdPartido = Convert.ToInt32(dtMandato.Rows[i]["IdPartido"].ToString());
                }
            }

            if (dtMandato.Columns.Contains("IdCidade"))
            {
                if (dtMandato.Rows[i]["IdCidade"] != DBNull.Value)
                {
                    mandato.IdCidade = Convert.ToInt32(dtMandato.Rows[i]["IdCidade"].ToString());
                }
            }

            if (dtMandato.Columns.Contains("IdEstado"))
            {
                if (dtMandato.Rows[i]["IdEstado"] != DBNull.Value)
                {
                    mandato.IdEstado = Convert.ToInt32(dtMandato.Rows[i]["IdEstado"].ToString());
                }
            }

            if (dtMandato.Columns.Contains("Gabinete"))
            {
                if (dtMandato.Rows[i]["Gabinete"] != DBNull.Value)
                {
                    mandato.Gabinete = Convert.ToInt32(dtMandato.Rows[i]["Gabinete"]);
                }
            }

            if (dtMandato.Columns.Contains("Anexo"))
            {
                if (dtMandato.Rows[i]["Anexo"] != DBNull.Value)
                {
                    mandato.Anexo = Convert.ToString(dtMandato.Rows[i]["Anexo"]);
                }
            }

            if (dtMandato.Columns.Contains("Telefone"))
            {
                if (dtMandato.Rows[i]["Telefone"] != DBNull.Value)
                {
                    mandato.Telefone = Convert.ToString(dtMandato.Rows[i]["Telefone"]);
                }
            }

            if (dtMandato.Columns.Contains("Fax"))
            {
                if (dtMandato.Rows[i]["Fax"] != DBNull.Value)
                {
                    mandato.Telefone = Convert.ToString(dtMandato.Rows[i]["Fax"]);
                }
            }

            if (dtMandato.Columns.Contains("Endereco"))
            {
                if (dtMandato.Rows[i]["Endereco"] != DBNull.Value)
                {
                    mandato.Endereco = Convert.ToString(dtMandato.Rows[i]["Endereco"]);
                }
            }

            if (dtMandato.Columns.Contains("CEP"))
            {
                if (dtMandato.Rows[i]["CEP"] != DBNull.Value)
                {
                    mandato.CEP = Convert.ToString(dtMandato.Rows[i]["CEP"]);
                }
            }

            if (dtMandato.Columns.Contains("Login"))
            {
                if (dtMandato.Rows[i]["Login"] != DBNull.Value)
                {
                    mandato.Login = Convert.ToString(dtMandato.Rows[i]["Login"]);
                }
            }

            if (dtMandato.Columns.Contains("DtInclusao"))
            {
                if (dtMandato.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    mandato.DtInclusao = Convert.ToDateTime(dtMandato.Rows[i]["DtInclusao"].ToString());
                }
            }


            if (dtMandato.Columns.Contains("IsMandatoAtual"))
            {
                if (dtMandato.Rows[i]["IsMandatoAtual"] != DBNull.Value)
                {
                    mandato.IsMandatoAtual = Convert.ToBoolean(dtMandato.Rows[i]["IsMandatoAtual"].ToString());
                }
            }


            if (dtMandato.Columns.Contains("NomePolitico"))
            {
                if (dtMandato.Rows[i]["NomePolitico"] != DBNull.Value)
                {
                    mandato.NomePolitico = dtMandato.Rows[i]["NomePolitico"].ToString();
                }
            }

            if (dtMandato.Columns.Contains("Cargo"))
            {
                if (dtMandato.Rows[i]["Cargo"] != DBNull.Value)
                {
                    mandato.Cargo = dtMandato.Rows[i]["Cargo"].ToString();
                }
            }

            if (dtMandato.Columns.Contains("Partido"))
            {
                if (dtMandato.Rows[i]["Partido"] != DBNull.Value)
                {
                    mandato.Partido = dtMandato.Rows[i]["Partido"].ToString();
                }
            }

            if (dtMandato.Columns.Contains("Periodo"))
            {
                if (dtMandato.Rows[i]["Periodo"] != DBNull.Value)
                {
                    mandato.Periodo = dtMandato.Rows[i]["Periodo"].ToString();
                }
            }

            return mandato;
        }

    }
}
