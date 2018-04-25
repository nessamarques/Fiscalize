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

    public class CidadeDAO
    {
        public List<Cidade> ObterCidade()
        {
            List<Cidade> listCidade = new List<Cidade>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT C.IDCIDADE, C.IDESTADO, UPPER(C.NOME) AS NOME ");
            sbQuery.Append("FROM CIDADE C ORDER BY C.NOME ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtCidade = ds.Tables[0];

                        for (int i = 0; i < dtCidade.Rows.Count; i++)
                        {
                            Cidade cidade = PopularEntidade(dtCidade, i);
                            listCidade.Add(cidade);
                        }
                    }
                }
            }

            return listCidade;
        }

        public List<Cidade> ObterCidadeByIdEstado(int idEstado)
        {
            List<Cidade> listCidade = new List<Cidade>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT C.IDCIDADE, C.IDESTADO, UPPER(C.NOME) AS NOME ");
            sbQuery.Append("FROM CIDADE C WHERE C.IDESTADO = " + idEstado + " ORDER BY C.NOME ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtCidade = ds.Tables[0];

                        for (int i = 0; i < dtCidade.Rows.Count; i++)
                        {
                            Cidade cidade = PopularEntidade(dtCidade, i);
                            listCidade.Add(cidade);
                        }
                    }
                }
            }

            return listCidade;
        }

        public Cidade ObterCidadeById(int idCidade)
        {
            Cidade cidade = new Cidade();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT C.IDCIDADE, C.IDESTADO, UPPER(C.NOME) AS NOME ");
            sbQuery.Append("FROM CIDADE C WHERE C.IDCIDADE = " + idCidade + " ORDER BY C.NOME ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtCidade = ds.Tables[0];

                        for (int i = 0; i < dtCidade.Rows.Count; i++)
                        {
                            cidade = PopularEntidade(dtCidade, i);
                        }
                    }
                }
            }

            return cidade;
        }

        public List<Cidade> ObterCidade(string pString)
        {
            List<Cidade> listCidade = new List<Cidade>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT C.IDCIDADE, C.IDESTADO, UPPER(C.NOME) AS NOME ");
            sbQuery.Append("FROM CIDADE C WHERE C.NOME LIKE '%' + '" + @pString + "' + '%' " + " ORDER BY C.NOME ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtCidade = ds.Tables[0];

                        for (int i = 0; i < dtCidade.Rows.Count; i++)
                        {
                            Cidade cidade = PopularEntidade(dtCidade, i);
                            listCidade.Add(cidade);
                        }
                    }
                }
            }

            return listCidade;
        }

        private static Cidade PopularEntidade(DataTable dtCidade, int i)
        {
            Cidade cidade = new Cidade();

            if (dtCidade.Columns.Contains("IdCidade"))
            {
                if (dtCidade.Rows[i]["IdCidade"] != DBNull.Value)
                {
                    cidade.IdCidade = Convert.ToInt32(dtCidade.Rows[i]["IdCidade"].ToString());
                }
            }

            if (dtCidade.Columns.Contains("Nome"))
            {
                if (dtCidade.Rows[i]["Nome"] != DBNull.Value)
                {
                    cidade.Nome = Convert.ToString(dtCidade.Rows[i]["Nome"]);
                }
            }

            return cidade;
        }
    }
}
