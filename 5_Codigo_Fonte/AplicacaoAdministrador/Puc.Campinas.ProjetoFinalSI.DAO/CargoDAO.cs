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

    public class CargoDAO
    {
        public List<Cargo> ObterCargos()
        {
            //Instânciando a Lista Tipada.
            List<Cargo> listCargos = new List<Cargo>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            using (DbCommand dbCommand = db.GetStoredProcCommand("STPCARGO;4"))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtCargo = ds.Tables[0];

                        for (int i = 0; i < dtCargo.Rows.Count; i++)
                        {
                            Cargo cargo = PopularEntidade(dtCargo, i);
                            listCargos.Add(cargo);
                            cargo = null;
                        }
                    }
                }
            }

            return listCargos;
        }

        public Cargo ObterCargoById(int idCargo)
        {
            //Instânciando a Lista Tipada.
            Cargo cargo = new Cargo();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            using (DbCommand dbCommand = db.GetStoredProcCommand("STPCARGO;5"))
            {
                db.AddInParameter(dbCommand, "IdCargo", DbType.Int16, idCargo);

                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtCargo = ds.Tables[0];

                        for (int i = 0; i < dtCargo.Rows.Count; i++)
                        {
                            cargo = PopularEntidade(dtCargo, i);
                        }
                    }
                }
            }

            return cargo;
        }

        public List<Cargo> ObterCargos(string pString)
        {
            //Instânciando a Lista Tipada.
            List<Cargo> listCargos = new List<Cargo>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            using (DbCommand dbCommand = db.GetStoredProcCommand("STPCARGO;6"))
            {
                db.AddInParameter(dbCommand, "pString", DbType.String, pString);

                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtCargo = ds.Tables[0];

                        for (int i = 0; i < dtCargo.Rows.Count; i++)
                        {
                            Cargo cargo = PopularEntidade(dtCargo, i);
                            listCargos.Add(cargo);
                            cargo = null;
                        }
                    }
                }
            }

            return listCargos;
        }

        public List<Cargo> ObterCargosByPeriodoMandato(int idPeriodoMandato)
        {
            //Instânciando a Lista Tipada.
            List<Cargo> listCargos = new List<Cargo>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sb = new StringBuilder();

            sb.Append("select * from PeriodoMandato pm inner join CargoMandato cm on cm.IdPeriodoMandato = ");
            sb.Append("pm.IdPeriodoMandato inner join Cargo c on c.IdCargo = cm.IdCargo where pm.IdPeriodoMandato = " + idPeriodoMandato);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtCargo = ds.Tables[0];

                        for (int i = 0; i < dtCargo.Rows.Count; i++)
                        {
                            Cargo cargo = PopularEntidade(dtCargo, i);
                            listCargos.Add(cargo);
                            cargo = null;
                        }
                    }
                }
            }

            return listCargos;
        }

        public string Incluir(Cargo cargo)
        {
            //Iniciando Persistência no Banco de Dados.
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret = default(object);

            using (DbCommand dbCommand = db.GetStoredProcCommand("STPCARGO;1"))
            {
                //Parâmetros da Stored Procedure.
                db.AddInParameter(dbCommand, "Nome", DbType.String, cargo.Nome);
                db.AddInParameter(dbCommand, "Descricao", DbType.String, cargo.Descricao);
                db.AddInParameter(dbCommand, "Login", DbType.String, cargo.Login);
                db.AddInParameter(dbCommand, "DtInclusao", DbType.DateTime, cargo.DtInclusao);

                //Executar Comando no Banco.
                ret = db.ExecuteNonQuery(dbCommand);
            }

            return ret.ToString();
        }

        public string Alterar(Cargo cargo)
        {
            //Iniciando Persistência no Banco de Dados.
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret;

            using (DbCommand dbCommand = db.GetStoredProcCommand("STPCARGO;2"))
            {
                //Parâmetros da Stored Procedure.
                db.AddInParameter(dbCommand, "IdCargo", DbType.Int32, cargo.IdCargo);
                db.AddInParameter(dbCommand, "Nome", DbType.String, cargo.Nome);
                db.AddInParameter(dbCommand, "Descricao", DbType.String, cargo.Descricao);
                db.AddInParameter(dbCommand, "Login", DbType.String, cargo.Login);
                db.AddInParameter(dbCommand, "DtInclusao", DbType.DateTime, cargo.DtInclusao);

                //Executar Comando no Banco.
                ret = db.ExecuteNonQuery(dbCommand);
            }

            return ret.ToString();
        }

        public string Excluir(Cargo cargo)
        {
            //Iniciando Persistência no Banco de Dados.
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret;

            using (DbCommand dbCommand = db.GetStoredProcCommand("STPCARGO;3"))
            {
                //Parâmetros da Stored Procedure.
                db.AddInParameter(dbCommand, "IdCargo", DbType.Int32, cargo.IdCargo);

                //Executar Comando no Banco.
                ret = db.ExecuteNonQuery(dbCommand);
            }

            return ret.ToString();
        }

        public bool NomeExiste(Cargo cargo)
        {
            //Iniciando Persistência no Banco de Dados.
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret;

            using (DbCommand dbCommand = db.GetSqlStringCommand("SELECT COUNT(*) FROM CARGO WHERE UPPER(NOME) LIKE UPPER('" + cargo.Nome.Trim() + "')"))
            {
                //Executar Comando no Banco.
                ret = db.ExecuteScalar(dbCommand);

                if (Convert.ToInt32(ret) > 0)
                    return true;
            }

            return false;
        }

        private static Cargo PopularEntidade(DataTable dtCargo, int i)
        {
            //Criando um objeto do tipo CARGO.
            Cargo cargo = new Cargo();

            if (dtCargo.Columns.Contains("IdCargo"))
            {
                if (dtCargo.Rows[i]["IdCargo"] != DBNull.Value)
                {
                    cargo.IdCargo = Convert.ToInt32(dtCargo.Rows[i]["IdCargo"].ToString());
                }
            }

            if (dtCargo.Columns.Contains("Nome"))
            {
                if (dtCargo.Rows[i]["Nome"] != DBNull.Value)
                {
                    cargo.Nome = Convert.ToString(dtCargo.Rows[i]["Nome"]);
                }
            }

            if (dtCargo.Columns.Contains("Descricao"))
            {
                if (dtCargo.Rows[i]["Descricao"] != DBNull.Value)
                {
                    cargo.Descricao = Convert.ToString(dtCargo.Rows[i]["Descricao"]);
                }
            }

            if (dtCargo.Columns.Contains("Login"))
            {
                if (dtCargo.Rows[i]["Login"] != DBNull.Value)
                {
                    cargo.Login = Convert.ToString(dtCargo.Rows[i]["Login"]);
                }
            }

            if (dtCargo.Columns.Contains("DtInclusao"))
            {
                if (dtCargo.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    cargo.DtInclusao = Convert.ToDateTime(dtCargo.Rows[i]["DtInclusao"].ToString());
                }
            }

            return cargo;
        }

        public bool VerificaSeCargoEstaSendoUsado(int idCargo)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");
            
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT * FROM Mandato WHERE IdCargo = " + idCargo;

            ret = db.ExecuteScalar(command);

            return Convert.ToBoolean(ret);
        }

    }
}
