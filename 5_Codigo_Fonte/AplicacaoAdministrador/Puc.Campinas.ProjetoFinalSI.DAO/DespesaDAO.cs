using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Puc.Campinas.ProjetoFinalSI.DAO.Mandato;

namespace Puc.Campinas.ProjetoFinalSI.DAO
{
    public class DespesaDAO
    {
        public List<Despesa> ObterDespesa()
        {
            //Instânciando a Lista Tipada.
            List<Despesa> listaDespesa = new List<Despesa>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT d.IdCategoria, d.IdMandato, d.Descricao, d.CPF_CNPJ_Forn, d.NomeFornecedor, d.NF, d.MesDespesa, d.AnoDespesa, SUM(d.Valor) as Valor ");
            sbQuery.Append("FROM DESPESA d ");
            sbQuery.Append("inner join Mandato m on m.IdMandato = d.idmandato ");
            sbQuery.Append("group by m.IdPolitico, IsMandatoAtual, d.Descricao, d.IdCategoria, d.idmandato, ");
            sbQuery.Append("d.CPF_CNPJ_Forn, d.NomeFornecedor, d.DtDespesa having IsMandatoAtual = 1 ORDER BY d.DESCRICAO ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtDespesa = ds.Tables[0];

                        for (int i = 0; i < dtDespesa.Rows.Count; i++)
                        {
                            Despesa despesa = PopularEntidade(dtDespesa, i);
                            listaDespesa.Add(despesa);
                            despesa = null;
                        }
                    }
                }
            }

            return listaDespesa;
        }

        public List<Despesa> ObterDespesa(string pString)
        {
            List<Despesa> listDespesa = new List<Despesa>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT D.IdDespesa, D.IdCategoria, D.IdMandato, D.Descricao, D.Valor, D.CPF_CNPJ_Forn, D.NomeFornecedor, D.NF, D.MesDespesa, D.AnoDespesa, D.DtInclusao, D.Login ");
            sbQuery.Append("FROM DESPESA D WHERE D.Descricao LIKE '%" + @pString + "%' ORDER BY D.Descricao ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtDespesa = ds.Tables[0];

                        for (int i = 0; i < dtDespesa.Rows.Count; i++)
                        {
                            Despesa despesa = PopularEntidade(dtDespesa, i);
                            listDespesa.Add(despesa);
                        }
                    }
                }
            }

            return listDespesa;
        }

        public List<Despesa> ObterDespesasByIdPolitico(int idPolitico)
        {
            List<Despesa> listDespesas = new List<Despesa>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select d.*, p.Nome as NomePolitico ");
            sbQuery.Append(" from Despesa d");
            sbQuery.Append(" inner join Mandato m on m.IdMandato = d.IdMandato");
            sbQuery.Append(" inner join Politico p on p.IdPolitico = m.IdPolitico");
            sbQuery.Append(" where p.IdPolitico =  " + idPolitico);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtDespesas = ds.Tables[0];

                        for (int i = 0; i < dtDespesas.Rows.Count; i++)
                        {
                            Despesa despesa = PopularEntidade(dtDespesas, i);
                            listDespesas.Add(despesa);
                        }
                    }
                }
            }

            return listDespesas;
        }

        public List<Despesa> ObterTop5DespesasByIdPolitico(int idPolitico)
        {
            List<Despesa> listDespesas = new List<Despesa>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select top 5 d.*, p.Nome as NomePolitico ");
            sbQuery.Append(" from Despesa d");
            sbQuery.Append(" inner join Mandato m on m.IdMandato = d.IdMandato");
            sbQuery.Append(" inner join Politico p on p.IdPolitico = m.IdPolitico");
            sbQuery.Append(" where p.IdPolitico =  " + idPolitico);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtDespesas = ds.Tables[0];

                        for (int i = 0; i < dtDespesas.Rows.Count; i++)
                        {
                            Despesa despesa = PopularEntidade(dtDespesas, i);
                            listDespesas.Add(despesa);
                        }
                    }
                }
            }

            return listDespesas;
        }

        public Despesa ObterDespesaById(int idDespesa)
        {
            
            Despesa despesa = new Despesa();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            using (DbCommand dbCommand = db.GetSqlStringCommand("SELECT  * FROM DESPESA D WHERE D.IDDESPESA = " + idDespesa + " ORDER BY D.DTDESPESA DESC "))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtDespesa = ds.Tables[0];

                        for (int i = 0; i < dtDespesa.Rows.Count; i++)
                        {
                            despesa = PopularEntidade(dtDespesa, i);
                        }
                    }
                }
            }

            return despesa;
        }

        public List<Despesa> ObterRankingDespesa()
        { 
            List<Despesa> listDespesa = new List<Despesa>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT desp.idmandato, SUM(desp.Valor) as Valor FROM");
            sbQuery.Append(" DESPESA desp ");
            sbQuery.Append(" inner join Mandato m on m.IdMandato = desp.idmandato ");
            sbQuery.Append(" group by desp.idmandato ");
            sbQuery.Append(" ORDER BY SUM(desp.Valor) desc ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtDespesa = ds.Tables[0];

                        for (int i = 0; i < dtDespesa.Rows.Count; i++)
                        {
                            Despesa despesa = new Despesa();

                            if (dtDespesa.Columns.Contains("IdMandato"))
                            {
                                if (dtDespesa.Rows[i]["IdMandato"] != DBNull.Value)
                                {
                                    despesa.IdMandato = Convert.ToInt32(dtDespesa.Rows[i]["IdMandato"]);
                                    despesa.Mandato = new MandatoDAO().ObterMandatoById(despesa.IdMandato);
                                    despesa.Politico = new PoliticoDAO().ObterPoliticoById(despesa.Mandato.IdPolitico);
                                }
                            }

                            if (dtDespesa.Columns.Contains("Valor"))
                            {
                                if (dtDespesa.Rows[i]["Valor"] != DBNull.Value)
                                {
                                    despesa.Valor = float.Parse(dtDespesa.Rows[i]["Valor"].ToString());
                                }
                            }

                            listDespesa.Add(despesa);
                        }
                    }
                }
            }

            return listDespesa;
        }

        public string Incluir(Despesa despesa)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();

            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO DESPESA VALUES(@pIdCategoria, @pIdMandato, @pDescricao, @pValor, @pCPF_CNPJ_Forn, @pNomeFornecedor, @pNF, @pMesDespesa, @pAnoDespesa, @pDtInclusao, @pLogin)";

            command.Parameters.Add(new SqlParameter("@pIdCategoria", despesa.IdCategoria));
            command.Parameters.Add(new SqlParameter("@pIdMandato", despesa.IdMandato));
            command.Parameters.Add(new SqlParameter("@pDescricao", despesa.Descricao));
            command.Parameters.Add(new SqlParameter("@pValor", despesa.Valor));
            command.Parameters.Add(new SqlParameter("@pCPF_CNPJ_Forn", despesa.CPF_CNPJ_Forn));
            command.Parameters.Add(new SqlParameter("@pNomeFornecedor", despesa.NomeFornecedor));
            command.Parameters.Add(new SqlParameter("@pNF", despesa.NF));
            command.Parameters.Add(new SqlParameter("@pMesDespesa", despesa.MesDespesa));
            command.Parameters.Add(new SqlParameter("@pAnoDespesa", despesa.AnoDespesa));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", despesa.DtInclusao));
            command.Parameters.Add(new SqlParameter("@pLogin", despesa.Login));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Alterar(Despesa despesa)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();

            SqlCommand command = new SqlCommand();

            command.CommandText = "UPDATE DESPESA SET IDCATEGORIA = @pCategoria, IdMandato = @pIdMandato, DESCRICAO = @pDescricao, VALOR = @pValor, CPF_CNPJ_FORN = @pCPF_CNPJ_Forn, NOMEFORNECEDOR = @pNomeFornecedor, NF = @pNF, MesDespesa = @pMesDespesa, AnoDespesa = @pAnoDespesa, DTINCLUSAO = @pDtInclusao, LOGIN = @pLogin ";
            command.CommandText += "WHERE IDDESPESA = " + despesa.IdDespesa;

            command.Parameters.Add(new SqlParameter("@pIdCategoria", despesa.IdCategoria));
            command.Parameters.Add(new SqlParameter("@pIdMandato", despesa.IdMandato));
            command.Parameters.Add(new SqlParameter("@pDescricao", despesa.Descricao));
            command.Parameters.Add(new SqlParameter("@pValor", despesa.Valor));
            command.Parameters.Add(new SqlParameter("@pCPF_CNPJ_Forn", despesa.CPF_CNPJ_Forn));
            command.Parameters.Add(new SqlParameter("@pNomeFornecedor", despesa.NomeFornecedor));
            command.Parameters.Add(new SqlParameter("@pNF", despesa.NF));
            command.Parameters.Add(new SqlParameter("@pMesDespesa", despesa.MesDespesa));
            command.Parameters.Add(new SqlParameter("@pAnoDespesa", despesa.AnoDespesa));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", despesa.DtInclusao));
            command.Parameters.Add(new SqlParameter("@pLogin", despesa.Login));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Excluir(Despesa despesa)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();

            SqlCommand deleteDespesa = new SqlCommand();

            deleteDespesa.CommandText = "DELETE FROM DESPESA WHERE IDDESPESA = " + despesa.IdDespesa;

            ret = db.ExecuteNonQuery(deleteDespesa);

            return ret.ToString();
        }

        private static Despesa PopularEntidade(DataTable dtDespesa, int i)
        {
            Despesa despesa = new Despesa();

            if (dtDespesa.Columns.Contains("IdDespesa"))
            {
                if (dtDespesa.Rows[i]["IdDespesa"] != DBNull.Value)
                {
                    despesa.IdDespesa = Convert.ToInt32(dtDespesa.Rows[i]["IdDespesa"].ToString());
                }
            }

            if (dtDespesa.Columns.Contains("IdCategoria"))
            {
                if (dtDespesa.Rows[i]["IdCategoria"] != DBNull.Value)
                {
                    despesa.IdCategoria = Convert.ToInt32(dtDespesa.Rows[i]["IdCategoria"]);
                }
            }

            if (dtDespesa.Columns.Contains("IdMandato"))
            {
                if (dtDespesa.Rows[i]["IdMandato"] != DBNull.Value)
                {
                    despesa.IdMandato = Convert.ToInt32(dtDespesa.Rows[i]["IdMandato"]);
                    despesa.Mandato = new MandatoDAO().ObterMandatoById(despesa.IdMandato);

                    //despesa.Mandato.ObjPolitico = new PoliticoDAO().ObterPoliticoById(despesa.Mandato.IdPolitico);
                    //despesa.Mandato.ObjCargo = new CargoDAO().ObterCargoById(despesa.Mandato.IdCargo);
                }
            }

            if (dtDespesa.Columns.Contains("Descricao"))
            {
                if (dtDespesa.Rows[i]["Descricao"] != DBNull.Value)
                {
                    despesa.Descricao = Convert.ToString(dtDespesa.Rows[i]["Descricao"]);
                }
            }

            if (dtDespesa.Columns.Contains("Valor"))
            {
                if (dtDespesa.Rows[i]["Valor"] != DBNull.Value)
                {
                    despesa.Valor = float.Parse(dtDespesa.Rows[i]["Valor"].ToString());
                }
            }

            if (dtDespesa.Columns.Contains("CPF_CNPJ_Forn"))
            {
                if (dtDespesa.Rows[i]["CPF_CNPJ_Forn"] != DBNull.Value)
                {
                    despesa.CPF_CNPJ_Forn = Convert.ToString(dtDespesa.Rows[i]["CPF_CNPJ_Forn"]);
                }
            }

            if (dtDespesa.Columns.Contains("NomeFornecedor"))
            {
                if (dtDespesa.Rows[i]["NomeFornecedor"] != DBNull.Value)
                {
                    despesa.NomeFornecedor = Convert.ToString(dtDespesa.Rows[i]["NomeFornecedor"]);
                }
            }

            if (dtDespesa.Columns.Contains("NF"))
            {
                if (dtDespesa.Rows[i]["NF"] != DBNull.Value)
                {
                    despesa.NF = Convert.ToString(dtDespesa.Rows[i]["NF"]);
                }
            }            

            if (dtDespesa.Columns.Contains("MesDespesa"))
            {
                if (dtDespesa.Rows[i]["MesDespesa"] != DBNull.Value)
                {
                    despesa.MesDespesa = Convert.ToInt32(dtDespesa.Rows[i]["MesDespesa"]);
                }
            }

            if (dtDespesa.Columns.Contains("AnoDespesa"))
            {
                if (dtDespesa.Rows[i]["AnoDespesa"] != DBNull.Value)
                {
                    despesa.AnoDespesa = Convert.ToInt32(dtDespesa.Rows[i]["AnoDespesa"]);
                }
            }

            if (dtDespesa.Columns.Contains("DtInclusao"))
            {
                if (dtDespesa.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    despesa.DtInclusao = Convert.ToDateTime(dtDespesa.Rows[i]["DtInclusao"].ToString());
                }
            }

            if (dtDespesa.Columns.Contains("Login"))
            {
                if (dtDespesa.Rows[i]["Login"] != DBNull.Value)
                {
                    despesa.Login = Convert.ToString(dtDespesa.Rows[i]["Login"]);
                }
            }


            return despesa;
        }

        public bool VerificaSeDespesaEstaSendoUsada(int idDespesa)
        {
            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT 1 FROM DESPESA WHERE IDDESPESA = " + idDespesa;

            ret = db.ExecuteScalar(command);

            return Convert.ToBoolean(ret);
        }
    }
}
