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

    public class CargoComissaoDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public List<CargoComissao> ObterCargoComissao()
        {
            List<CargoComissao> listCargoComissao = new List<CargoComissao>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT CC.IDCARGOCOMISSAO, UPPER(CC.NOME) as Nome, CC.Descricao, CC.DTINCLUSAO, CC.LOGIN  ");
            sbQuery.Append("FROM CARGOCOMISSAO CC ORDER BY CC.IDCARGOCOMISSAO DESC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtCargoComissao = ds.Tables[0];

                        for (int i = 0; i < dtCargoComissao.Rows.Count; i++)
                        {
                            CargoComissao cargoComissao = PopularEntidade(dtCargoComissao, i);
                            listCargoComissao.Add(cargoComissao);
                        }
                    }
                }
            }

            return listCargoComissao;
        }

        public CargoComissao ObterCargoComissaoById(int idCargoComissao)
        {
            CargoComissao cargoComissao = new CargoComissao();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT CC.IDCARGOCOMISSAO, UPPER(CC.NOME) as Nome, CC.Descricao, CC.DTINCLUSAO, CC.LOGIN  ");
            sbQuery.Append("FROM CARGOCOMISSAO CC WHERE CC.IDCARGOCOMISSAO = " + idCargoComissao + " ORDER BY CC.IDCARGOCOMISSAO DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtCargoComissao = ds.Tables[0];

                        for (int i = 0; i < dtCargoComissao.Rows.Count; i++)
                        {
                            cargoComissao = PopularEntidade(dtCargoComissao, i);
                        }
                    }
                }
            }

            return cargoComissao;
        }

        public List<CargoComissao> ObterCargoComissao(string pString)
        {
            List<CargoComissao> listCargoComissao = new List<CargoComissao>();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT CC.IDCARGOCOMISSAO, UPPER(CC.NOME) as Nome, CC.Descricao, CC.DTINCLUSAO, CC.LOGIN  ");
            sbQuery.Append("FROM CARGOCOMISSAO CC WHERE CC.NOME LIKE '%" + @pString + "%' " + " ORDER BY CC.IDCARGOCOMISSAO DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtCargoComissao = ds.Tables[0];

                        for (int i = 0; i < dtCargoComissao.Rows.Count; i++)
                        {
                            CargoComissao cargoComissao = PopularEntidade(dtCargoComissao, i);
                            listCargoComissao.Add(cargoComissao);
                        }
                    }
                }
            }

            return listCargoComissao;
        }

        public string Incluir(CargoComissao cargoComissao)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO CARGOCOMISSAO VALUES(@pNome, @pDescricao, @pDtInclusao, @pLogin )";
            command.Parameters.Add(new SqlParameter("@pNome", cargoComissao.Nome));
            command.Parameters.Add(new SqlParameter("@pDescricao", cargoComissao.Descricao));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", cargoComissao.DtInclusao));
            command.Parameters.Add(new SqlParameter("@pLogin", cargoComissao.Login));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Alterar(CargoComissao cargoComissao)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "UPDATE CARGOCOMISSAO SET Nome = @pNome, Descricao = @pDescricao ";
            command.CommandText += "WHERE IdCargoComissao = @pIdCargoComissao ";
            command.Parameters.Add(new SqlParameter("@pIdCargoComissao", cargoComissao.IdCargoComissao));
            command.Parameters.Add(new SqlParameter("@pNome", cargoComissao.Nome));
            command.Parameters.Add(new SqlParameter("@pDescricao", cargoComissao.Descricao));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", cargoComissao.DtInclusao));
            command.Parameters.Add(new SqlParameter("@pLogin", cargoComissao.Login));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Excluir(CargoComissao cargoComissao)
        {
            object ret;

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("DELETE FROM CARGOCOMISSAO WHERE IDCARGOCOMISSAO = " + cargoComissao.IdCargoComissao);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                ret = db.ExecuteNonQuery(dbCommand);
            }

            return ret.ToString();
        }

        public bool NomeExiste(CargoComissao cargoComissao)
        {
            object ret;

            using (DbCommand dbCommand = db.GetSqlStringCommand("SELECT COUNT(*) FROM CARGOCOMISSAO WHERE NOME LIKE UPPER('" + cargoComissao.Nome.Trim() + "')"))
            {
                ret = db.ExecuteScalar(dbCommand);

                if (Convert.ToInt32(ret) > 0)
                    return true;
            }

            return false;
        }

        public bool VerificaSeCargoComissaoEstaSendoUsado(int idCargoComissao)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT 1 FROM MembroComissao WHERE IdCargoComissao = " + idCargoComissao;

            ret = db.ExecuteScalar(command);

            return Convert.ToBoolean(ret);
        }

        private static CargoComissao PopularEntidade(DataTable dtCargoComissao, int i)
        {
            CargoComissao cargoComissao = new CargoComissao();

            if (dtCargoComissao.Columns.Contains("IdCargoComissao"))
            {
                if (dtCargoComissao.Rows[i]["IdCargoComissao"] != DBNull.Value)
                {
                    cargoComissao.IdCargoComissao = Convert.ToInt32(dtCargoComissao.Rows[i]["IdCargoComissao"].ToString());
                }
            }

            if (dtCargoComissao.Columns.Contains("Nome"))
            {
                if (dtCargoComissao.Rows[i]["Nome"] != DBNull.Value)
                {
                    cargoComissao.Nome = Convert.ToString(dtCargoComissao.Rows[i]["Nome"]);
                }
            }

            if (dtCargoComissao.Columns.Contains("Descricao"))
            {
                if (dtCargoComissao.Rows[i]["Descricao"] != DBNull.Value)
                {
                    cargoComissao.Descricao = Convert.ToString(dtCargoComissao.Rows[i]["Descricao"]);
                }
            }
            
            if (dtCargoComissao.Columns.Contains("DtInclusao"))
            {
                if (dtCargoComissao.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    cargoComissao.DtInclusao = Convert.ToDateTime(dtCargoComissao.Rows[i]["DtInclusao"].ToString());
                }
            }

            if (dtCargoComissao.Columns.Contains("Login"))
            {
                if (dtCargoComissao.Rows[i]["Login"] != DBNull.Value)
                {
                    cargoComissao.Login = Convert.ToString(dtCargoComissao.Rows[i]["Login"]);
                }
            }

            return cargoComissao;
        }
    }
}

