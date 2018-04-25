using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Puc.Campinas.ProjetoFinalSI.Entidade;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Puc.Campinas.ProjetoFinalSI.DAO.Mandato;


namespace Puc.Campinas.ProjetoFinalSI.DAO
{
    public class PoliticoDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public List<Politico> ObterPoliticos()
        {
            List<Politico> listPoliticos = new List<Politico>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT P.IdPolitico, upper(P.Nome) as Nome, P.Sexo, P.Foto, P.DtNascimento, P.IdCidadeNaturalidade, ");
            sbQuery.Append("P.IdEstadoNaturalidade, P.IdPaisNaturalidade, P.Email, P.Login, P.DtInclusao, P.IdEscolaridade ");
            sbQuery.Append("FROM POLITICO P ");
            sbQuery.Append("ORDER BY P.NOME ASC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPoliticos = ds.Tables[0];

                        for (int i = 0; i < dtPoliticos.Rows.Count; i++)
                        {
                            Politico politico = PopularEntidade(dtPoliticos, i);
                            listPoliticos.Add(politico);
                        }
                    }
                }
            }

            return listPoliticos;
        }

        public Politico ObterPoliticoById(int idPolitico)
        {
            Politico politico = new Politico();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT P.IdPolitico, upper(P.Nome) as Nome, P.Sexo, P.Foto, P.DtNascimento, P.IdCidadeNaturalidade, ");
            sbQuery.Append("P.IdEstadoNaturalidade, P.IdPaisNaturalidade, P.Email, P.Login, P.DtInclusao, P.IdEscolaridade ");
            sbQuery.Append("FROM POLITICO P ");
            sbQuery.Append("LEFT JOIN Mandato m on m.IdPolitico = P.IdPolitico and m.IsMandatoAtual = 1 ");
            sbQuery.Append("LEFT JOIN Filiacao f on f.IdPolitico = P.IdPolitico ");
            sbQuery.Append("WHERE P.IDPOLITICO = " + idPolitico);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPolitico = ds.Tables[0];

                        for (int i = 0; i < dtPolitico.Rows.Count; i++)
                        {
                            politico = PopularEntidade(dtPolitico, i);
                        }
                    }
                }
            }

            return politico;
        }

        public List<Politico> ObterPoliticosAtivosByIdCargo(int idCargo)
        {
            List<Politico> politicos = new List<Politico>();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT P.IdPolitico, upper(P.Nome) as Nome, P.Sexo, P.Foto, P.DtNascimento, P.IdCidadeNaturalidade, ");
            sbQuery.Append("P.IdEstadoNaturalidade, P.IdPaisNaturalidade, P.Email, P.Login, P.DtInclusao, P.IdEscolaridade ");
            sbQuery.Append("from Mandato m ");
            sbQuery.Append("inner join Politico p on p.IdPolitico = m.IdPolitico and m.IsMandatoAtual = 1 ");
            sbQuery.Append("where m.IdCargo =" + idCargo);
            sbQuery.Append("order by nome");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPolitico = ds.Tables[0];

                        for (int i = 0; i < dtPolitico.Rows.Count; i++)
                        {
                            politicos.Add(PopularEntidade(dtPolitico, i));
                        }
                    }
                }
            }

            return politicos;
        }

        public List<Politico> ObterPoliticos(string pString)
        {
            List<Politico> listPoliticos = new List<Politico>();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT P.IdPolitico, upper(P.Nome) as Nome, P.Sexo, P.Foto, P.DtNascimento, P.IdCidadeNaturalidade, ");
            sbQuery.Append("P.IdEstadoNaturalidade, P.IdPaisNaturalidade, P.Email, P.Login, P.DtInclusao, P.IdEscolaridade ");
            sbQuery.Append("FROM POLITICO P ");
            sbQuery.Append("WHERE P.NOME LIKE '%" + @pString + "' + '%' ");
            sbQuery.Append("ORDER BY IDPOLITICO DESC");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPoliticos = ds.Tables[0];

                        for (int i = 0; i < dtPoliticos.Rows.Count; i++)
                        {
                            Politico politico = PopularEntidade(dtPoliticos, i);
                            listPoliticos.Add(politico);
                        }
                    }
                }
            }

            return listPoliticos;
        }

        public List<Politico> ObterPoliticos(string idEstado, string idCidade, string idCargo, string idPartido, string idPolitico)
        {
            List<Politico> listPoliticos = new List<Politico>();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select * from Politico p ");
            sbQuery.Append("inner join Mandato m on m.IdPolitico = p.IdPolitico and m.IsMandatoAtual = 1 ");
            sbQuery.Append("inner join Cargo c on c.IdCargo = m.IdCargo ");
            sbQuery.Append("inner join Cidade i on i.IdCidade = m.IdCidade ");
            sbQuery.Append("inner join Estado e on e.IdEstado = m.IdEstado ");
            sbQuery.Append("inner join Partido t on m.IdPartido = t.IdPartido where 1 = 1");


            if (!string.IsNullOrEmpty(idEstado))
            {
                sbQuery.Append("and e.idEstado = " + idEstado);
            }

            if (!string.IsNullOrEmpty(idCidade) && !idCidade.Equals("Selecione..."))
            {
                sbQuery.Append(" and i.idCidade = " + idCidade);
            }

            if (!string.IsNullOrEmpty(idCargo))
            {
                sbQuery.Append(" and c.idCargo = " + idCargo);
            }

            if (!string.IsNullOrEmpty(idPartido))
            {
                sbQuery.Append("and t.idPartido = " + idPartido);
            }

            if (!string.IsNullOrEmpty(idPolitico) && !idPolitico.Equals("Selecione..."))
            {
                sbQuery.Append("and p.idPolitico = " + idPolitico);
            }

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPoliticos = ds.Tables[0];

                        for (int i = 0; i < dtPoliticos.Rows.Count; i++)
                        {
                            Politico politico = PopularEntidade(dtPoliticos, i);
                            listPoliticos.Add(politico);
                        }
                    }
                }
            }

            return listPoliticos;
        }

        public List<Politico> ObterPoliticosByCargoPartido(int idEstado, int idCidade, int? idCargo, int? idPartido)
        {
            List<Politico> politicos = new List<Politico>();
            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select * from Mandato m ");
            sbQuery.Append("inner join Politico p on p.IdPolitico = m.IdPolitico and m.IsMandatoAtual = 1 ");
            sbQuery.Append("where m.IdEstado = " + idEstado);
            sbQuery.Append("and m.IdCidade = " + idCidade);
            if (idCargo != null)
            {
                sbQuery.Append("and m.IdCargo = " + idCargo);
            }
            if (idPartido != null)
            {
                sbQuery.Append("and m.IdPartido = " + idPartido);
            }

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtPolitico = ds.Tables[0];

                        for (int i = 0; i < dtPolitico.Rows.Count; i++)
                        {
                            politicos.Add(PopularEntidade(dtPolitico, i));
                        }
                    }
                }
            }

            return politicos;
        }

        public string Incluir(Politico politico)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();

            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO POLITICO VALUES(@pNome, @pSexo, @pFoto, @pDtNascimento, @pIdCidadeNaturalidade, @pIdEstadoNaturalidade, @pIdPaisNaturalidade, @pEmail, @pLogin, @pDtInclusao, @pIdEscolaridade ) ";

            command.Parameters.Add(new SqlParameter("@pNome", politico.Nome));
            // command.Parameters.Add(new SqlParameter("@pIdCidade", politico.IdCidade));
            // command.Parameters.Add(new SqlParameter("@pIdEstado", politico.IdEstado));
            command.Parameters.Add(new SqlParameter("@pSexo", politico.Sexo.ToUpper()));
            if (politico.Foto != null)
            {
                command.Parameters.Add("@pFoto", SqlDbType.Image, 0).Value = ConvertImageToByteArray(politico.Foto, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                command.Parameters.Add("@pFoto", SqlDbType.Image, 0).Value = DBNull.Value;
            }
            command.Parameters.Add(new SqlParameter("@pDtNascimento", politico.DtNascimento));
            command.Parameters.Add(new SqlParameter("@pIdCidadeNaturalidade", politico.IdCidadeNaturalidade));
            command.Parameters.Add(new SqlParameter("@pIdEstadoNaturalidade", politico.IdEstadoNaturalidade));
            command.Parameters.Add(new SqlParameter("@pIdPaisNaturalidade", politico.IdPaisNaturalidade));
            //command.Parameters.Add(new SqlParameter("@pGabinete", politico.Gabinete));
            //command.Parameters.Add(new SqlParameter("@pAnexo", politico.Anexo));
            //command.Parameters.Add(new SqlParameter("@pTelefone", !string.IsNullOrEmpty(politico.Telefone) ? politico.Telefone.Replace("(", "").Replace(")", "").Replace("-", "") : politico.Telefone));
            //command.Parameters.Add(new SqlParameter("@pFax", !string.IsNullOrEmpty(politico.Fax) ? politico.Fax.Replace("(", "").Replace(")", "").Replace("-", "") : politico.Fax));
            command.Parameters.Add(new SqlParameter("@pEmail", politico.Email));
            command.Parameters.Add(new SqlParameter("@pLogin", politico.Login));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", politico.DtInclusao));
            command.Parameters.Add(new SqlParameter("@pIdEscolaridade", politico.IdEscolaridade));
            //command.Parameters.Add(new SqlParameter("@pEndereco", politico.Endereco));
            //command.Parameters.Add(new SqlParameter("@pCEP", politico.CEP));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        private byte[] ConvertImageToByteArray(object img, System.Drawing.Imaging.ImageFormat formatOfImage)
        {
            System.Drawing.Image imageToConvert = (System.Drawing.Image)img;

            byte[] ret;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    ret = ms.ToArray();
                }
            }
            catch (Exception) { throw; }
            return ret;
        }

        public string Alterar(Politico politico)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();

            SqlCommand command = new SqlCommand();

            if (politico.Foto != null)
            {
                command.CommandText = "UPDATE POLITICO SET Nome = @pNome, Sexo = @pSexo, Foto = @pFoto, DtNascimento = @pDtNascimento, IdCidadeNaturalidade = @pIdCidadeNaturalidade, IdEstadoNaturalidade = @pIdEstadoNaturalidade, IdPaisNaturalidade = @pIdPaisNaturalidade, Email = @pEmail, IdEscolaridade = @pIdEscolaridade ";
                command.CommandText += "WHERE IdPolitico = @pIdPolitico";
            }
            else
            {
                command.CommandText = "UPDATE POLITICO SET Nome = @pNome, Sexo = @pSexo, DtNascimento = @pDtNascimento, IdCidadeNaturalidade = @pIdCidadeNaturalidade, IdEstadoNaturalidade = @pIdEstadoNaturalidade, IdPaisNaturalidade = @pIdPaisNaturalidade, Email = @pEmail, IdEscolaridade = @pIdEscolaridade ";
                command.CommandText += "WHERE IdPolitico = @pIdPolitico";
            }
            command.Parameters.Add(new SqlParameter("@pNome", politico.Nome));
            //command.Parameters.Add(new SqlParameter("@pIdCidade", politico.IdCidade));
            //command.Parameters.Add(new SqlParameter("@pIdEstado", politico.IdEstado));
            command.Parameters.Add(new SqlParameter("@pSexo", politico.Sexo.ToUpper()));
            if (politico.Foto != null)
            {
                command.Parameters.Add("@pFoto", SqlDbType.Image, 0).Value = ConvertImageToByteArray(politico.Foto, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                command.Parameters.Add("@pFoto", SqlDbType.Image, 0).Value = DBNull.Value;
            }
            command.Parameters.Add(new SqlParameter("@pDtNascimento", politico.DtNascimento));
            command.Parameters.Add(new SqlParameter("@pIdCidadeNaturalidade", politico.IdCidadeNaturalidade));
            command.Parameters.Add(new SqlParameter("@pIdEstadoNaturalidade", politico.IdEstadoNaturalidade));
            command.Parameters.Add(new SqlParameter("@pIdPaisNaturalidade", politico.IdPaisNaturalidade));
            //command.Parameters.Add(new SqlParameter("@pGabinete", politico.Gabinete));
            //command.Parameters.Add(new SqlParameter("@pAnexo", politico.Anexo));
            //command.Parameters.Add(new SqlParameter("@pTelefone", !string.IsNullOrEmpty(politico.Telefone) ? politico.Telefone.Replace("(", "").Replace(")", "").Replace("-", "") : politico.Telefone));
            //command.Parameters.Add(new SqlParameter("@pFax", !string.IsNullOrEmpty(politico.Fax) ? politico.Fax.Replace("(", "").Replace(")", "").Replace("-", "") : politico.Fax));
            command.Parameters.Add(new SqlParameter("@pEmail", politico.Email));
            command.Parameters.Add(new SqlParameter("@pLogin", politico.Login));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", politico.DtInclusao));
            command.Parameters.Add(new SqlParameter("@pIdPolitico", politico.IdPolitico));
            command.Parameters.Add(new SqlParameter("@pIdEscolaridade", politico.IdEscolaridade));
            //command.Parameters.Add(new SqlParameter("@pEndereco", politico.Endereco));
            //command.Parameters.Add(new SqlParameter("@pCEP", politico.CEP));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Excluir(Politico politico)
        {
            object ret;

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("DELETE FROM POLITICO WHERE IDPOLITICO = " + politico.IdPolitico);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                ret = db.ExecuteNonQuery(dbCommand);
            }

            return ret.ToString();
        }

        public bool NomeExiste(Politico politico)
        {
            object ret;

            using (DbCommand dbCommand = db.GetSqlStringCommand("SELECT COUNT(*) FROM POLITICO WHERE UPPER(NOME) LIKE UPPER('" + politico.Nome.Trim() + "')"))
            {
                //Executar Comando no Banco.
                ret = db.ExecuteScalar(dbCommand);

                if (Convert.ToInt32(ret) > 0)
                    return true;
            }

            return false;
        }

        private static Politico PopularEntidade(DataTable dtPolitico, int i)
        {
            Politico politico = new Politico();

            if (dtPolitico.Columns.Contains("IdPolitico"))
            {
                if (dtPolitico.Rows[i]["IdPolitico"] != DBNull.Value)
                {
                    politico.IdPolitico = Convert.ToInt32(dtPolitico.Rows[i]["IdPolitico"].ToString());

                    List<Puc.Campinas.ProjetoFinalSI.Entidade.Mandato> list = new MandatoDAO().ObterMandatosByIdPolitico(politico.IdPolitico).Where(x => x.IsMandatoAtual = true).ToList();
                    if (list.Count > 0)
                    {
                        politico.MandatoAtual = list[0];

                        DespesaDAO d = new DespesaDAO();
                        List<Despesa> despesasMandatoAtual = d.ObterDespesasByIdPolitico(politico.IdPolitico).Where(x => x.IdMandato == politico.MandatoAtual.IdMandato).ToList();

                        politico.ListaDespesasMandatoAtual = despesasMandatoAtual;
                    }

                    politico.ListaProfissoes = new ProfissaoDAO().ObterProfissaoByIdPolitico(politico.IdPolitico);

                    politico.Filiacao = new FiliacaoDAO().ObterFiliacaoByIdPolitico(politico.IdPolitico);

                    politico.RedesSociais = new RedeSocialDAO().ObterRedeSocialByIdPolitico(politico.IdPolitico);
                }
            }

            if (dtPolitico.Columns.Contains("Nome"))
            {
                if (dtPolitico.Rows[i]["Nome"] != DBNull.Value)
                {
                    politico.Nome = dtPolitico.Rows[i]["Nome"].ToString();
                }
            }

            if (dtPolitico.Columns.Contains("IdCidade"))
            {
                if (dtPolitico.Rows[i]["IdCidade"] != DBNull.Value)
                {
                    politico.IdCidade = Convert.ToInt32(dtPolitico.Rows[i]["IdCidade"].ToString());
                    politico.CidadeCandidatura = new CidadeDAO().ObterCidadeById(politico.IdCidade);
                }
            }

            if (dtPolitico.Columns.Contains("IdEstado"))
            {
                if (dtPolitico.Rows[i]["IdEstado"] != DBNull.Value)
                {
                    politico.IdEstado = Convert.ToInt32(dtPolitico.Rows[i]["IdEstado"].ToString());
                    politico.EstadoCandidatura = new EstadoDAO().ObterEstadoById(politico.IdEstado);
                }
            }

            if (dtPolitico.Columns.Contains("Sexo"))
            {
                if (dtPolitico.Rows[i]["Sexo"] != DBNull.Value)
                {
                    politico.Sexo = dtPolitico.Rows[i]["Sexo"].ToString();
                }
            }

            if (dtPolitico.Columns.Contains("Foto"))
            {
                if (dtPolitico.Rows[i]["Foto"] != DBNull.Value)
                {
                    politico.Foto = dtPolitico.Rows[i]["Foto"];
                }
            }

            if (dtPolitico.Columns.Contains("DtNascimento"))
            {
                if (dtPolitico.Rows[i]["DtNascimento"] != DBNull.Value)
                {
                    politico.DtNascimento = Convert.ToDateTime(dtPolitico.Rows[i]["DtNascimento"].ToString());
                }
            }

            if (dtPolitico.Columns.Contains("IdCidadeNaturalidade"))
            {
                if (dtPolitico.Rows[i]["IdCidadeNaturalidade"] != DBNull.Value)
                {
                    politico.IdCidadeNaturalidade = Convert.ToInt32(dtPolitico.Rows[i]["IdCidadeNaturalidade"].ToString());
                    politico.CidadeNaturalidade = new CidadeDAO().ObterCidadeById(politico.IdCidadeNaturalidade);
                }
            }

            if (dtPolitico.Columns.Contains("IdEstadoNaturalidade"))
            {
                if (dtPolitico.Rows[i]["IdEstadoNaturalidade"] != DBNull.Value)
                {
                    politico.IdEstadoNaturalidade = Convert.ToInt32(dtPolitico.Rows[i]["IdEstadoNaturalidade"].ToString());
                    politico.EstadoNaturalidade = new EstadoDAO().ObterEstadoById(politico.IdEstadoNaturalidade);
                }
            }

            if (dtPolitico.Columns.Contains("IdPaisNaturalidade"))
            {
                if (dtPolitico.Rows[i]["IdPaisNaturalidade"] != DBNull.Value)
                {
                    politico.IdPaisNaturalidade = Convert.ToInt32(dtPolitico.Rows[i]["IdPaisNaturalidade"].ToString());
                }
            }

            if (dtPolitico.Columns.Contains("Gabinete"))
            {
                if (dtPolitico.Rows[i]["Gabinete"] != DBNull.Value)
                {
                    politico.Gabinete = Convert.ToInt32(dtPolitico.Rows[i]["Gabinete"].ToString());
                }
            }

            if (dtPolitico.Columns.Contains("Anexo"))
            {
                if (dtPolitico.Rows[i]["Anexo"] != DBNull.Value)
                {
                    politico.Anexo = dtPolitico.Rows[i]["Anexo"].ToString();
                }
            }

            if (dtPolitico.Columns.Contains("Telefone"))
            {
                if (dtPolitico.Rows[i]["Telefone"] != DBNull.Value)
                {
                    politico.Telefone = dtPolitico.Rows[i]["Telefone"].ToString();
                }
            }

            if (dtPolitico.Columns.Contains("Fax"))
            {
                if (dtPolitico.Rows[i]["Fax"] != DBNull.Value)
                {
                    politico.Fax = dtPolitico.Rows[i]["Fax"].ToString();
                }
            }

            if (dtPolitico.Columns.Contains("Email"))
            {
                if (dtPolitico.Rows[i]["Email"] != DBNull.Value)
                {
                    politico.Email = dtPolitico.Rows[i]["Email"].ToString();
                }
            }

            if (dtPolitico.Columns.Contains("Login"))
            {
                if (dtPolitico.Rows[i]["Login"] != DBNull.Value)
                {
                    politico.Login = Convert.ToString(dtPolitico.Rows[i]["Login"]);
                }
            }

            if (dtPolitico.Columns.Contains("DtInclusao"))
            {
                if (dtPolitico.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    politico.DtInclusao = Convert.ToDateTime(dtPolitico.Rows[i]["DtInclusao"].ToString());
                }
            }

            if (dtPolitico.Columns.Contains("IdEscolaridade"))
            {
                if (dtPolitico.Rows[i]["IdEscolaridade"] != DBNull.Value)
                {
                    politico.IdEscolaridade = Convert.ToInt32(dtPolitico.Rows[i]["IdEscolaridade"].ToString());
                    politico.Escolaridade = new EscolaridadeDAO().ObterEscolaridadeById(politico.IdEscolaridade);
                }
            }

            if (dtPolitico.Columns.Contains("Endereco"))
            {
                if (dtPolitico.Rows[i]["Endereco"] != DBNull.Value)
                {
                    politico.Endereco = dtPolitico.Rows[i]["Endereco"].ToString();
                }
            }


            if (dtPolitico.Columns.Contains("CEP"))
            {
                if (dtPolitico.Rows[i]["CEP"] != DBNull.Value)
                {
                    politico.CEP = dtPolitico.Rows[i]["CEP"].ToString();
                }
            }

            return politico;
        }

        public List<Historico> ObterHistoricoPolitico(int idPolitico)
        {
            List<Historico> listHistorico = new List<Historico>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("select c.Nome as Cargo, p.Nome as Partido, pm.DtInicio, pm.DtFim, m.IsMandatoAtual as Situacao ");
            sbQuery.Append("from mandato m ");
            sbQuery.Append("inner join Cargo c on c.IdCargo = m.IdCargo ");
            sbQuery.Append("inner join Partido p on p.IdPartido = m.IdPartido ");
            sbQuery.Append("inner join PeriodoMandato pm on pm.IdPeriodoMandato = m.IdPeriodoMandato ");
            sbQuery.Append("inner join Politico po on po.IdPolitico = m.IdPolitico ");
            sbQuery.Append("where m.IdPolitico = " + idPolitico + " ");
            sbQuery.Append("order by DtInicio desc");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtHistorico = ds.Tables[0];

                        for (int i = 0; i < dtHistorico.Rows.Count; i++)
                        {
                            Historico h = new Historico();

                            if (dtHistorico.Columns.Contains("Cargo"))
                            {
                                if (dtHistorico.Rows[i]["Cargo"] != DBNull.Value)
                                {
                                    h.Cargo = dtHistorico.Rows[i]["Cargo"].ToString();
                                }
                            }

                            if (dtHistorico.Columns.Contains("Partido"))
                            {
                                if (dtHistorico.Rows[i]["Partido"] != DBNull.Value)
                                {
                                    h.Partido = dtHistorico.Rows[i]["Partido"].ToString();
                                }
                            }

                            if (dtHistorico.Columns.Contains("DtInicio"))
                            {
                                if (dtHistorico.Rows[i]["DtInicio"] != DBNull.Value)
                                {
                                    h.DataInicio = Convert.ToDateTime(dtHistorico.Rows[i]["DtInicio"]); //.ToString();
                                }
                            }

                            if (dtHistorico.Columns.Contains("DtFim"))
                            {
                                if (dtHistorico.Rows[i]["DtFim"] != DBNull.Value)
                                {
                                    h.DataTermino = Convert.ToDateTime(dtHistorico.Rows[i]["DtFim"]); //.ToString();
                                }
                            }

                            if (dtHistorico.Columns.Contains("Situacao"))
                            {
                                if (dtHistorico.Rows[i]["Situacao"] != DBNull.Value)
                                {
                                    h.Situacao = Convert.ToBoolean(dtHistorico.Rows[i]["Situacao"]); //.ToString();
                                }
                            }

                            listHistorico.Add(h);
                        }
                    }
                }
            }
            return listHistorico;
        }
    }
}
