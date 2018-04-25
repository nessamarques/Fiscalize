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

    public class ConvocadoDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public List<Convocado> ObterConvocados()
        {
            List<Convocado> listaConvocados = new List<Convocado>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT * FROM Convocado c ORDER BY c.IdConvocado desc ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtConvocado = ds.Tables[0];

                        for (int i = 0; i < dtConvocado.Rows.Count; i++)
                        {
                            Convocado convocado = PopularEntidade(dtConvocado, i);
                            listaConvocados.Add(convocado);
                        }
                    }
                }
            }

            return listaConvocados;
        }

        public List<Convocado> ObterConvocadosByIdSessao(int idSessao)
        {
            List<Convocado> convocado = new List<Convocado>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select * FROM Convocado c WHERE c.IdSessao = " + idSessao + "ORDER BY c.IdConvocado DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtConvocado = ds.Tables[0];

                        for (int i = 0; i < dtConvocado.Rows.Count; i++)
                        {
                            convocado.Add(PopularEntidade(dtConvocado, i));
                        }
                    }
                }
            }

            return convocado;
        }

        private static Convocado PopularEntidade(DataTable dtConvocados, int i)
        {
            Convocado convocado = new Convocado();

            if (dtConvocados.Columns.Contains("IdConvocado"))
            {
                if (dtConvocados.Rows[i]["IdConvocado"] != DBNull.Value)
                {
                    convocado.IdConvocado = Convert.ToInt32(dtConvocados.Rows[i]["IdConvocado"]);
                }
            }

            if (dtConvocados.Columns.Contains("IdSessao"))
            {
                if (dtConvocados.Rows[i]["IdSessao"] != DBNull.Value)
                {
                    convocado.IdSessao = Convert.ToInt32(dtConvocados.Rows[i]["IdSessao"]);
                }
            }

            if (dtConvocados.Columns.Contains("IdCargo"))
            {
                if (dtConvocados.Rows[i]["IdCargo"] != DBNull.Value)
                {
                    convocado.IdCargo = Convert.ToInt32(dtConvocados.Rows[i]["IdCargo"]);
                    convocado.Cargo = new CargoDAO().ObterCargoById(convocado.IdCargo);
                }
            }

            if (dtConvocados.Columns.Contains("DtInclusao"))
            {
                if (dtConvocados.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    convocado.DtInclusao = Convert.ToDateTime(dtConvocados.Rows[i]["DtInclusao"]);
                }
            }

            if (dtConvocados.Columns.Contains("Login"))
            {
                if (dtConvocados.Rows[i]["Login"] != DBNull.Value)
                {
                    convocado.Login = dtConvocados.Rows[i]["Login"].ToString();
                }
            }

            return convocado;
        }
    }
}
