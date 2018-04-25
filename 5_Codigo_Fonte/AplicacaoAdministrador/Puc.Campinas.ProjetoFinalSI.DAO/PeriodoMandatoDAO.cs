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
    public class PeriodoMandatoDAO
    {
        public List<PeriodoMandato> ObterPeriodosMandatos()
        {
            //Instânciando a Lista Tipada.
            List<PeriodoMandato> listaPeriodoMandato = new List<PeriodoMandato>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            using (DbCommand dbCommand = db.GetSqlStringCommand("SELECT  * FROM PERIODOMANDATO PM ORDER BY PM.DTFIM DESC "))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPeriodoMandato = ds.Tables[0];

                        for (int i = 0; i < dtPeriodoMandato.Rows.Count; i++)
                        {
                            PeriodoMandato periodoMandato = PopularEntidade(dtPeriodoMandato, i);
                            listaPeriodoMandato.Add(periodoMandato);
                            periodoMandato = null;
                        }
                    }
                }
            }

            return listaPeriodoMandato;
        }

        public List<PeriodoMandato> ObterPeriodosMandatosAtivos()
        {
            //Instânciando a Lista Tipada.
            List<PeriodoMandato> listaPeriodoMandato = new List<PeriodoMandato>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            using (DbCommand dbCommand = db.GetSqlStringCommand("SELECT  * FROM PERIODOMANDATO PM WHERE PM.SITUACAO = 1 ORDER BY PM.DTFIM DESC "))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPeriodoMandato = ds.Tables[0];

                        for (int i = 0; i < dtPeriodoMandato.Rows.Count; i++)
                        {
                            PeriodoMandato periodoMandato = PopularEntidade(dtPeriodoMandato, i);
                            listaPeriodoMandato.Add(periodoMandato);
                            periodoMandato = null;
                        }
                    }
                }
            }

            return listaPeriodoMandato;
        }

        public PeriodoMandato ObterPeriodosMandatosById(int idPeriodoMandato)
        {
            //Instânciando a Lista Tipada.
            List<PeriodoMandato> listaPeriodoMandato = new List<PeriodoMandato>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            using (DbCommand dbCommand = db.GetSqlStringCommand("SELECT  * FROM PERIODOMANDATO PM WHERE PM.IDPERIODOMANDATO = " + idPeriodoMandato + " ORDER BY PM.DTFIM DESC "))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPeriodoMandato = ds.Tables[0];

                        for (int i = 0; i < dtPeriodoMandato.Rows.Count; i++)
                        {
                            PeriodoMandato periodoMandato = PopularEntidade(dtPeriodoMandato, i);
                            listaPeriodoMandato.Add(periodoMandato);
                            periodoMandato = null;
                        }
                    }
                }
            }

            return listaPeriodoMandato[0];
        }

        public string Incluir(PeriodoMandato periodoMandato)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();

            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO PERIODOMANDATO VALUES(@pSituacao, @pDtInicio, @pDtFim, @pLogin, @pDtInclusao)";

            command.Parameters.Add(new SqlParameter("@pSituacao", this.VerificaSituacao(periodoMandato.Situacao)));
            command.Parameters.Add(new SqlParameter("@pDtInicio", periodoMandato.DtInicio));
            command.Parameters.Add(new SqlParameter("@pDtFim", periodoMandato.DtFim));
            command.Parameters.Add(new SqlParameter("@pLogin", periodoMandato.Login));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", periodoMandato.DtInclusao));

            ret = db.ExecuteNonQuery(command);

            int newID = this.VerificarMaxId();
                        
            foreach (CargoMandato cargoMandato in periodoMandato.ListCargoMandatos)
            {
                SqlCommand commandCargoMandato = new SqlCommand();

                commandCargoMandato.CommandText = "INSERT INTO CARGOMANDATO VALUES(@pIdPeriodoMandato, @pIdCargo, @pLogin, @pDtInclusao)";
                
                commandCargoMandato.Parameters.Add(new SqlParameter("@pIdPeriodoMandato", newID));
                commandCargoMandato.Parameters.Add(new SqlParameter("@pIdCargo", cargoMandato.IdCargo));
                commandCargoMandato.Parameters.Add(new SqlParameter("@pLogin", cargoMandato.Login));
                commandCargoMandato.Parameters.Add(new SqlParameter("@pDtInclusao", cargoMandato.DtInclusao));

                ret = db.ExecuteNonQuery(commandCargoMandato);

                commandCargoMandato = null;
            }

            return ret.ToString();
        }

        public string Alterar(PeriodoMandato periodoMandato)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();

            SqlCommand command = new SqlCommand();

            command.CommandText = "UPDATE PERIODOMANDATO SET SITUACAO = @pSituacao, DTINICIO = @pDtInicio, DTFIM = @pDtFim ";
            command.CommandText += "WHERE IDPERIODOMANDATO = " + periodoMandato.IdPeriodoMandato;

            command.Parameters.Add(new SqlParameter("@pSituacao", this.VerificaSituacao(periodoMandato.Situacao)));
            command.Parameters.Add(new SqlParameter("@pDtInicio", periodoMandato.DtInicio));
            command.Parameters.Add(new SqlParameter("@pDtFim", periodoMandato.DtFim));

            ret = db.ExecuteNonQuery(command);

            int idPeriodoMandato = periodoMandato.IdPeriodoMandato;

            SqlCommand commandDeleteCargoMandato = new SqlCommand();

            commandDeleteCargoMandato.CommandText = "DELETE FROM CARGOMANDATO WHERE IDPERIODOMANDATO = " + idPeriodoMandato;

            ret = db.ExecuteNonQuery(commandDeleteCargoMandato);

            foreach (CargoMandato cargoMandato in periodoMandato.ListCargoMandatos)
            {
                SqlCommand commandCargoMandato = new SqlCommand();

                commandCargoMandato.CommandText = "INSERT INTO CARGOMANDATO VALUES(@pIdPeriodoMandato, @pIdCargo, @pLogin, @pDtInclusao)";

                commandCargoMandato.Parameters.Add(new SqlParameter("@pIdPeriodoMandato", idPeriodoMandato));
                commandCargoMandato.Parameters.Add(new SqlParameter("@pIdCargo", cargoMandato.IdCargo));
                commandCargoMandato.Parameters.Add(new SqlParameter("@pLogin", periodoMandato.Login));
                commandCargoMandato.Parameters.Add(new SqlParameter("@pDtInclusao", periodoMandato.DtInclusao));

                ret = db.ExecuteNonQuery(commandCargoMandato);

                commandCargoMandato = null;
            }

            return ret.ToString();
        }

        public string Excluir(PeriodoMandato periodoMandato)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();

            SqlCommand deleteCargosMandato = new SqlCommand();
            SqlCommand deletePeriodoMandato = new SqlCommand();

            deleteCargosMandato.CommandText = "DELETE FROM CARGOMANDATO WHERE IDPERIODOMANDATO = " + periodoMandato.IdPeriodoMandato;
            deletePeriodoMandato.CommandText = "DELETE FROM PERIODOMANDATO WHERE IDPERIODOMANDATO = " + periodoMandato.IdPeriodoMandato;

            ret = db.ExecuteNonQuery(deleteCargosMandato);
            ret = db.ExecuteNonQuery(deletePeriodoMandato);

            return ret.ToString();
        }

        private int VerificarMaxId()
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();

            SqlCommand command = new SqlCommand();

            command.CommandText = "select MAX(pm.IdPeriodoMandato) as IdPeriodoMandato from PeriodoMandato pm";

            ret = db.ExecuteScalar(command);

            return Convert.ToInt32(ret);
        }

        private int VerificaSituacao(string situacao)
        {
            if (situacao.ToUpper().Trim() == "ATIVO")
                return 1;
            else
                return 0;
        }

        private static PeriodoMandato PopularEntidade(DataTable dtPeriodoMandato, int i)
        {
            PeriodoMandato periodoMandato = new PeriodoMandato();

            if (dtPeriodoMandato.Columns.Contains("IdPeriodoMandato"))
            {
                if (dtPeriodoMandato.Rows[i]["IdPeriodoMandato"] != DBNull.Value)
                {
                    periodoMandato.IdPeriodoMandato = Convert.ToInt32(dtPeriodoMandato.Rows[i]["IdPeriodoMandato"].ToString());
                    periodoMandato.ListCargoMandatos = new CargoMandatoDAO().ObterCargosMandato(periodoMandato.IdPeriodoMandato);
                }
            }

            if (dtPeriodoMandato.Columns.Contains("Situacao"))
            {
                if (dtPeriodoMandato.Rows[i]["Situacao"] != DBNull.Value)
                {
                    periodoMandato.Situacao = Convert.ToString(dtPeriodoMandato.Rows[i]["Situacao"]);
                }
            }

            if (dtPeriodoMandato.Columns.Contains("DtInicio"))
            {
                if (dtPeriodoMandato.Rows[i]["DtInicio"] != DBNull.Value)
                {
                    periodoMandato.DtInicio = Convert.ToDateTime(dtPeriodoMandato.Rows[i]["DtInicio"]);
                }
            }

            if (dtPeriodoMandato.Columns.Contains("DtFim"))
            {
                if (dtPeriodoMandato.Rows[i]["DtFim"] != DBNull.Value)
                {
                    periodoMandato.DtFim = Convert.ToDateTime(dtPeriodoMandato.Rows[i]["DtFim"]);
                    periodoMandato.RangeDatas = periodoMandato.DtInicio.ToShortDateString() + " - " + periodoMandato.DtFim.ToShortDateString();
                }
            }

            if (dtPeriodoMandato.Columns.Contains("Login"))
            {
                if (dtPeriodoMandato.Rows[i]["Login"] != DBNull.Value)
                {
                    periodoMandato.Login = Convert.ToString(dtPeriodoMandato.Rows[i]["Login"]);
                }
            }

            if (dtPeriodoMandato.Columns.Contains("DtInclusao"))
            {
                if (dtPeriodoMandato.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    periodoMandato.DtInclusao = Convert.ToDateTime(dtPeriodoMandato.Rows[i]["DtInclusao"].ToString());
                }
            }


            return periodoMandato;
        }

        public bool VerificaSePeriodoMandatoEstaSendoUsado(int idPeriodoMandato)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT 1 FROM Mandato WHERE IdPeriodoMandato = " + idPeriodoMandato;

            ret = db.ExecuteScalar(command);

            return Convert.ToBoolean(ret);
        }
    }
}
