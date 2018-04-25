using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace Puc.Campinas.ProjetoFinalSI.DAO
{
    public class CargoMandatoDAO
    {
        public List<CargoMandato> ObterCargosMandato(int idPeriodoMandato)
        {
            //Instânciando a Lista Tipada.
            List<CargoMandato> listaCargoMandato = new List<CargoMandato>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            using (DbCommand dbCommand = db.GetSqlStringCommand("SELECT  * FROM CARGOMANDATO WHERE IDPERIODOMANDATO = " + idPeriodoMandato))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtCargosMandato = ds.Tables[0];

                        for (int i = 0; i < dtCargosMandato.Rows.Count; i++)
                        {
                            CargoMandato cargoMandato = PopularEntidade(dtCargosMandato, i);
                            listaCargoMandato.Add(cargoMandato);
                            cargoMandato = null;
                        }
                    }
                }
            }

            return listaCargoMandato;
        }

        private static CargoMandato PopularEntidade(DataTable dtCargosMandato, int i)
        {
            CargoMandato cargoMandato = new CargoMandato();

            if (dtCargosMandato.Columns.Contains("IdCargoMandato"))
            {
                if (dtCargosMandato.Rows[i]["IdCargoMandato"] != DBNull.Value)
                {
                    cargoMandato.IdCargoMandato = Convert.ToInt32(dtCargosMandato.Rows[i]["IdCargoMandato"].ToString());
                }
            }

            if (dtCargosMandato.Columns.Contains("IdPeriodoMandato"))
            {
                if (dtCargosMandato.Rows[i]["IdPeriodoMandato"] != DBNull.Value)
                {
                    cargoMandato.IdPeriodoMandato = Convert.ToInt32(dtCargosMandato.Rows[i]["IdPeriodoMandato"].ToString());
                }
            }

            if (dtCargosMandato.Columns.Contains("IdCargo"))
            {
                if (dtCargosMandato.Rows[i]["IdCargo"] != DBNull.Value)
                {
                    cargoMandato.IdCargo = Convert.ToInt32(dtCargosMandato.Rows[i]["IdCargo"].ToString());
                }
            }

            if (dtCargosMandato.Columns.Contains("Login"))
            {
                if (dtCargosMandato.Rows[i]["Login"] != DBNull.Value)
                {
                    cargoMandato.Login = Convert.ToString(dtCargosMandato.Rows[i]["Login"]);
                }
            }

            if (dtCargosMandato.Columns.Contains("DtInclusao"))
            {
                if (dtCargosMandato.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    cargoMandato.DtInclusao = Convert.ToDateTime(dtCargosMandato.Rows[i]["DtInclusao"].ToString());
                }
            }

            return cargoMandato;
        }
    }
}
