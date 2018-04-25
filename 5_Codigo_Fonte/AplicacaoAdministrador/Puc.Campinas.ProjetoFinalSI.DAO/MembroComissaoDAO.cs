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

    public class MembroComissaoDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public List<MembroComissao> ObterMembroComissao()
        {
            List<MembroComissao> listMembrosComissao = new List<MembroComissao>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select m.IdMembroComissao, m.IdPolitico, m.IdCargoComissao, m.IdComissao, m.DtInclusao, m.Login , ");
            sbQuery.Append("c.Nome as NomeComissao, cc.Nome as NomeCargoComissao ");
            sbQuery.Append("from MembroComissao m ");
            sbQuery.Append("inner join Comissao c on c.IdComissao = m.IdComissao ");
            sbQuery.Append("inner join CargoComissao cc on cc.IdCargoComissao = m.IdCargoComissao ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtMembroComissao = ds.Tables[0];

                        for (int i = 0; i < dtMembroComissao.Rows.Count; i++)
                        {
                            MembroComissao membroComissao = PopularEntidade(dtMembroComissao, i);
                            listMembrosComissao.Add(membroComissao);
                        }
                    }
                }
            }

            return listMembrosComissao;
        }

        public List<MembroComissao> ObterMembroComissaoByIdPolitico(int idPolitico)
        {
            List<MembroComissao> listMembrosComissoes = new List<MembroComissao>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select m.*, p.Nome as NomePolitico, c.Nome as NomeComissao, cc.Nome as NomeCargoComissao ");
            sbQuery.Append("from MembroComissao m ");
            sbQuery.Append("inner join Politico p on p.IdPolitico = m.IdPolitico ");
            sbQuery.Append("inner join Comissao c on c.IdComissao = m.IdComissao ");
            sbQuery.Append("inner join CargoComissao cc on cc.IdCargoComissao = m.IdCargoComissao ");
            sbQuery.Append(" where p.IdPolitico =  " + idPolitico);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtMembrosComissoes = ds.Tables[0];

                        for (int i = 0; i < dtMembrosComissoes.Rows.Count; i++)
                        {
                            MembroComissao membroComissao = PopularEntidade(dtMembrosComissoes, i);
                            listMembrosComissoes.Add(membroComissao);
                        }
                    }
                }
            }

            return listMembrosComissoes;
        }

        public List<MembroComissao> ObterMembroComissaoByIdComissao(int idComissao)
        {
            List<MembroComissao> listaMembrosComissao = new List<MembroComissao>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select m.*, p.Nome as NomePolitico, c.Nome as NomeComissao, c.Sigla as SiglaComissao, cc.Nome as NomeCargoComissao ");
            sbQuery.Append("from MembroComissao m ");
            sbQuery.Append("inner join Politico p on p.IdPolitico = m.IdPolitico ");
            sbQuery.Append("inner join Comissao c on c.IdComissao = m.IdComissao ");
            sbQuery.Append("inner join CargoComissao cc on cc.IdCargoComissao = m.IdCargoComissao ");
            sbQuery.Append(" where m.idComissao =  " + idComissao);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtMembrosComissoes = ds.Tables[0];

                        for (int i = 0; i < dtMembrosComissoes.Rows.Count; i++)
                        {
                            MembroComissao membroComissao = PopularEntidade(dtMembrosComissoes, i);
                            listaMembrosComissao.Add(membroComissao);
                        }
                    }
                }
            }

            return listaMembrosComissao;
        }

        public MembroComissao ObterMembroComissaoPresidente(int idComissao)
        {
            MembroComissao membroComissao = new MembroComissao();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select m.*, p.Nome as NomePolitico, c.Nome as NomeComissao, cc.Nome as NomeCargoComissao, pa.Sigla as Partido ");
            sbQuery.Append("from MembroComissao m ");
            sbQuery.Append("inner join Politico p on p.IdPolitico = m.IdPolitico ");
            sbQuery.Append("inner join Comissao c on c.IdComissao = m.IdComissao ");
            sbQuery.Append("inner join CargoComissao cc on cc.IdCargoComissao = m.IdCargoComissao ");
            sbQuery.Append("inner join Mandato ma on ma.IdPolitico = p.IdPolitico and ma.IsMandatoAtual = 1 ");
            sbQuery.Append("inner join Partido pa on pa.IdPartido = ma.IdPartido ");
            sbQuery.Append(" where m.idComissao =  " + idComissao);
            sbQuery.Append(" and m.idCargoComissao = 3 ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtMembrosComissoes = ds.Tables[0];

                        for (int i = 0; i < dtMembrosComissoes.Rows.Count; i++)
                        {
                            membroComissao = PopularEntidade(dtMembrosComissoes, i);
                        }
                    }
                }
            }

            return membroComissao;
        }

        public MembroComissao ObterMembroComissaoVicePresidente1(int idComissao)
        {
            MembroComissao membroComissao = new MembroComissao();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select m.*, p.Nome as NomePolitico, c.Nome as NomeComissao, cc.Nome as NomeCargoComissao, pa.Sigla as Partido ");
            sbQuery.Append("from MembroComissao m ");
            sbQuery.Append("inner join Politico p on p.IdPolitico = m.IdPolitico ");
            sbQuery.Append("inner join Comissao c on c.IdComissao = m.IdComissao ");
            sbQuery.Append("inner join CargoComissao cc on cc.IdCargoComissao = m.IdCargoComissao ");
            sbQuery.Append("inner join Mandato ma on ma.IdPolitico = p.IdPolitico and ma.IsMandatoAtual = 1 ");
            sbQuery.Append("inner join Partido pa on pa.IdPartido = ma.IdPartido ");
            sbQuery.Append(" where m.idComissao =  " + idComissao);
            sbQuery.Append(" and m.idCargoComissao = 4 ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtMembrosComissoes = ds.Tables[0];

                        for (int i = 0; i < dtMembrosComissoes.Rows.Count; i++)
                        {
                            membroComissao = PopularEntidade(dtMembrosComissoes, i);
                        }
                    }
                }
            }

            return membroComissao;
        }

        public MembroComissao ObterMembroComissaoVicePresidente2(int idComissao)
        {
            MembroComissao membroComissao = new MembroComissao();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select m.*, p.Nome as NomePolitico, c.Nome as NomeComissao, cc.Nome as NomeCargoComissao, pa.Sigla as Partido ");
            sbQuery.Append("from MembroComissao m ");
            sbQuery.Append("inner join Politico p on p.IdPolitico = m.IdPolitico ");
            sbQuery.Append("inner join Comissao c on c.IdComissao = m.IdComissao ");
            sbQuery.Append("inner join CargoComissao cc on cc.IdCargoComissao = m.IdCargoComissao ");
            sbQuery.Append("inner join Mandato ma on ma.IdPolitico = p.IdPolitico and ma.IsMandatoAtual = 1 ");
            sbQuery.Append("inner join Partido pa on pa.IdPartido = ma.IdPartido ");
            sbQuery.Append(" where m.idComissao =  " + idComissao);
            sbQuery.Append(" and m.idCargoComissao = 5 ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtMembrosComissoes = ds.Tables[0];

                        for (int i = 0; i < dtMembrosComissoes.Rows.Count; i++)
                        {
                            membroComissao = PopularEntidade(dtMembrosComissoes, i);
                        }
                    }
                }
            }

            return membroComissao;
        }

        public MembroComissao ObterMembroComissaoVicePresidente3(int idComissao)
        {
            MembroComissao membroComissao = new MembroComissao();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select m.*, p.Nome as NomePolitico, c.Nome as NomeComissao, cc.Nome as NomeCargoComissao, pa.Sigla as Partido ");
            sbQuery.Append("from MembroComissao m ");
            sbQuery.Append("inner join Politico p on p.IdPolitico = m.IdPolitico ");
            sbQuery.Append("inner join Comissao c on c.IdComissao = m.IdComissao ");
            sbQuery.Append("inner join CargoComissao cc on cc.IdCargoComissao = m.IdCargoComissao ");
            sbQuery.Append("inner join Mandato ma on ma.IdPolitico = p.IdPolitico and ma.IsMandatoAtual = 1 ");
            sbQuery.Append("inner join Partido pa on pa.IdPartido = ma.IdPartido ");
            sbQuery.Append(" where m.idComissao =  " + idComissao);
            sbQuery.Append(" and m.idCargoComissao = 6 ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtMembrosComissoes = ds.Tables[0];

                        for (int i = 0; i < dtMembrosComissoes.Rows.Count; i++)
                        {
                            membroComissao = PopularEntidade(dtMembrosComissoes, i);
                        }
                    }
                }
            }

            return membroComissao;
        }

        public List<MembroComissao> ObterMembroComissaoTitular(int idComissao)
        {
            List<MembroComissao> listaMembrosComissao = new List<MembroComissao>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select m.*, p.Nome as NomePolitico, c.Nome as NomeComissao, cc.Nome as NomeCargoComissao, pa.Sigla as Partido, ca.Nome as NomeCargo ");
            sbQuery.Append("from MembroComissao m ");
            sbQuery.Append("inner join Politico p on p.IdPolitico = m.IdPolitico ");
            sbQuery.Append("inner join Comissao c on c.IdComissao = m.IdComissao ");
            sbQuery.Append("inner join CargoComissao cc on cc.IdCargoComissao = m.IdCargoComissao ");
            sbQuery.Append("inner join Mandato ma on ma.IdPolitico = p.IdPolitico and ma.IsMandatoAtual = 1 ");
            sbQuery.Append("inner join Partido pa on pa.IdPartido = ma.IdPartido ");
            sbQuery.Append("inner join Cargo ca on ca.IdCargo = ma.IdCargo ");
            sbQuery.Append("where m.idComissao =  " + idComissao);
            sbQuery.Append("and m.idCargoComissao = 7 ");
            sbQuery.Append("order by p.Nome asc ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtMembrosComissoes = ds.Tables[0];

                        for (int i = 0; i < dtMembrosComissoes.Rows.Count; i++)
                        {
                            MembroComissao membroComissao = PopularEntidade(dtMembrosComissoes, i);
                            listaMembrosComissao.Add(membroComissao);
                        }
                    }
                }
            }

            return listaMembrosComissao;
        }

        public List<MembroComissao> ObterMembroComissaoSuplente(int idComissao)
        {
            List<MembroComissao> listaMembrosComissao = new List<MembroComissao>();

            Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select m.*, upper(p.Nome) as NomePolitico, c.Nome as NomeComissao, cc.Nome as NomeCargoComissao, pa.Sigla as Partido, ca.Nome as NomeCargo ");
            sbQuery.Append("from MembroComissao m ");
            sbQuery.Append("inner join Politico p on p.IdPolitico = m.IdPolitico ");
            sbQuery.Append("inner join Comissao c on c.IdComissao = m.IdComissao ");
            sbQuery.Append("inner join CargoComissao cc on cc.IdCargoComissao = m.IdCargoComissao ");
            sbQuery.Append("inner join Mandato ma on ma.IdPolitico = p.IdPolitico and ma.IsMandatoAtual = 1 ");
            sbQuery.Append("inner join Partido pa on pa.IdPartido = ma.IdPartido ");
            sbQuery.Append("inner join Cargo ca on ca.IdCargo = ma.IdCargo ");
            sbQuery.Append("where m.idComissao =  " + idComissao);
            sbQuery.Append(" and m.idCargoComissao = 8 ");
            sbQuery.Append("order by p.Nome asc ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtMembrosComissoes = ds.Tables[0];

                        for (int i = 0; i < dtMembrosComissoes.Rows.Count; i++)
                        {
                            MembroComissao membroComissao = PopularEntidade(dtMembrosComissoes, i);
                            listaMembrosComissao.Add(membroComissao);
                        }
                    }
                }
            }

            return listaMembrosComissao;
        }

        public MembroComissao ObterMembroComissaoById(int idMembroComissao)
        {
            MembroComissao membroComissao = new MembroComissao();
            
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append(" SELECT mc.IdMembroComissao, mc.IdPolitico, mc.IdCargoComissao, mc.IdComissao, mc.DtInclusao, mc.Login ");
            sbQuery.Append(" FROM MembroComissao mc ");
            sbQuery.Append(" WHERE mc.IdMembroComissao = " + idMembroComissao );
            sbQuery.Append(" ORDER BY IdMembroComissao DESC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtMembroComissao = ds.Tables[0];

                        for (int i = 0; i < dtMembroComissao.Rows.Count; i++)
                        {
                            membroComissao = PopularEntidade(dtMembroComissao, i);
                        }
                    }
                }
            }

            return membroComissao;
        }

        public string Incluir(MembroComissao membroComissao)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO MembroComissao VALUES(@pIdPolitico, @pIdCargoComissao, @pIdComissao, @pDtInclusao, @pLogin)";
            command.Parameters.Add(new SqlParameter("@pIdPolitico", membroComissao.IdPolitico));
            command.Parameters.Add(new SqlParameter("@pIdCargoComissao", membroComissao.IdCargoComissao));
            command.Parameters.Add(new SqlParameter("@pIdComissao", membroComissao.IdComissao));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", membroComissao.DtInclusao));
            command.Parameters.Add(new SqlParameter("@pLogin",      membroComissao.Login));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Alterar(MembroComissao membroComissao)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "UPDATE MembroComissao SET IdPolitico = @pIdPolitico, IdCargoComissao = @pIdCargoComissao, IdComissao = @pIdComissao ";
            command.CommandText += "WHERE IdMembroComissao = @pIdMembroComissao ";
            command.Parameters.Add(new SqlParameter("@pIdPolitico", membroComissao.IdPolitico));
            command.Parameters.Add(new SqlParameter("@pIdCargoComissao", membroComissao.IdCargoComissao));
            command.Parameters.Add(new SqlParameter("@pIdComissao", membroComissao.IdComissao));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Excluir(MembroComissao membroComissao)
        {
            object ret;

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("DELETE FROM MembroComissao WHERE IdMembroComissao = " + membroComissao.IdMembroComissao);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                ret = db.ExecuteNonQuery(dbCommand);
            }

            return ret.ToString();
        }

        private static MembroComissao PopularEntidade(DataTable dtMembroComissao, int i)
        {
            MembroComissao membroComissao = new MembroComissao();

            if (dtMembroComissao.Columns.Contains("IdmembroComissao"))
            {
                if (dtMembroComissao.Rows[i]["IdMembroComissao"] != DBNull.Value)
                {
                    membroComissao.IdMembroComissao = Convert.ToInt32(dtMembroComissao.Rows[i]["IdMembroComissao"].ToString());
                }
            }

            if (dtMembroComissao.Columns.Contains("IdPolitico"))
            {
                if (dtMembroComissao.Rows[i]["IdPolitico"] != DBNull.Value)
                {
                    membroComissao.IdPolitico = Convert.ToInt32(dtMembroComissao.Rows[i]["IdPolitico"].ToString());
                }
            }

            if (dtMembroComissao.Columns.Contains("IdCargoComissao"))
            {
                if (dtMembroComissao.Rows[i]["IdCargoComissao"] != DBNull.Value)
                {
                    membroComissao.IdCargoComissao = Convert.ToInt32(dtMembroComissao.Rows[i]["IdCargoComissao"]);
                }
            }

            if (dtMembroComissao.Columns.Contains("IdComissao"))
            {
                if (dtMembroComissao.Rows[i]["IdComissao"] != DBNull.Value)
                {
                    membroComissao.IdComissao = Convert.ToInt32(dtMembroComissao.Rows[i]["IdComissao"].ToString());
                }
            }

            if (dtMembroComissao.Columns.Contains("DtInclusao"))
            {
                if (dtMembroComissao.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    membroComissao.DtInclusao = Convert.ToDateTime(dtMembroComissao.Rows[i]["DtInclusao"].ToString());
                }
            }
            
            if (dtMembroComissao.Columns.Contains("Login"))
            {
                if (dtMembroComissao.Rows[i]["Login"] != DBNull.Value)
                {
                    membroComissao.Login = Convert.ToString(dtMembroComissao.Rows[i]["Login"]);
                }
            }

            if (dtMembroComissao.Columns.Contains("NomePolitico"))
            {
                if (dtMembroComissao.Rows[i]["NomePolitico"] != DBNull.Value)
                {
                    membroComissao.NomePolitico = Convert.ToString(dtMembroComissao.Rows[i]["NomePolitico"]);
                }
            }

            if (dtMembroComissao.Columns.Contains("NomeComissao"))
            {
                if (dtMembroComissao.Rows[i]["NomeComissao"] != DBNull.Value)
                {
                    membroComissao.NomeComissao = Convert.ToString(dtMembroComissao.Rows[i]["NomeComissao"]);
                }
            }

            if (dtMembroComissao.Columns.Contains("SiglaComissao"))
            {
                if (dtMembroComissao.Rows[i]["SiglaComissao"] != DBNull.Value)
                {
                    membroComissao.SiglaComissao = Convert.ToString(dtMembroComissao.Rows[i]["SiglaComissao"]);
                }
            }

            if (dtMembroComissao.Columns.Contains("NomeCargoComissao"))
            {
                if (dtMembroComissao.Rows[i]["NomeCargoComissao"] != DBNull.Value)
                {
                    membroComissao.NomeCargoComissao = Convert.ToString(dtMembroComissao.Rows[i]["NomeCargoComissao"]);
                }
            }

            if (dtMembroComissao.Columns.Contains("Partido"))
            {
                if (dtMembroComissao.Rows[i]["Partido"] != DBNull.Value)
                {
                    membroComissao.Partido = Convert.ToString(dtMembroComissao.Rows[i]["Partido"]);
                }
            }

            if (dtMembroComissao.Columns.Contains("NomeCargo"))
            {
                if (dtMembroComissao.Rows[i]["NomeCargo"] != DBNull.Value)
                {
                    membroComissao.NomeCargo = Convert.ToString(dtMembroComissao.Rows[i]["NomeCargo"]);
                }
            }
            
            return membroComissao;
        }
    }
}
