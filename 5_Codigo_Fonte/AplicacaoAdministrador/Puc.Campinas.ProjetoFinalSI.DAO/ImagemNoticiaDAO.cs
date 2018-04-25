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
    using System.IO;

    public class ImagemNoticiaDAO
    {
        Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("ConectionTCC");

        public List<ImagemNoticia> ObterImagemNoticia()
        {
            List<ImagemNoticia> listImagemNoticia = new List<ImagemNoticia>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT im.IdImagemNoticia, im.Imagem, im.IdLocalNoticia, im.IsPortal ");
            sbQuery.Append("FROM ImagemNoticia im ORDER BY im.IdImagemNoticia ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtImagemNoticia = ds.Tables[0];

                        for (int i = 0; i < dtImagemNoticia.Rows.Count; i++)
                        {
                            ImagemNoticia imagemNoticia = PopularEntidade(dtImagemNoticia, i);
                            listImagemNoticia.Add(imagemNoticia);
                        }
                    }
                }
            }

            return listImagemNoticia;
        }

        public ImagemNoticia ObterImagemNoticiaById(int idImagemNoticia)
        {
            ImagemNoticia imagemNoticia = new ImagemNoticia();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT im.IdImagemNoticia, im.IdNoticia, im.Imagem, im.IdLocalNoticia, im.IsPortal ");
            sbQuery.Append("FROM ImagemNoticia im WHERE im.IdImagemNoticia = " + idImagemNoticia);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtImagemNoticia = ds.Tables[0];

                        for (int i = 0; i < dtImagemNoticia.Rows.Count; i++)
                        {
                            imagemNoticia = PopularEntidade(dtImagemNoticia, i);
                        }
                    }
                }
            }

            return imagemNoticia;
        }

        public List<ImagemNoticia> ObterImagemByIdNoticia(int idNoticia)
        {
            List<ImagemNoticia> listImagemNoticia = new List<ImagemNoticia>();

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("SELECT im.IdImagemNoticia, im.Imagem, im.IdLocalNoticia, im.IsPortal ");
            sbQuery.Append("FROM ImagemNoticia im ");
            sbQuery.Append("where im.IdNoticia = " + idNoticia);
            sbQuery.Append(" ORDER BY im.IdImagemNoticia ASC ");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                using (DataSet ds = db.ExecuteDataSet(dbCommand))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtImagemNoticia = ds.Tables[0];

                        for (int i = 0; i < dtImagemNoticia.Rows.Count; i++)
                        {
                            ImagemNoticia imagemNoticia = PopularEntidade(dtImagemNoticia, i);
                            listImagemNoticia.Add(imagemNoticia);
                        }
                    }
                }
            }

            return listImagemNoticia;
        }

        public string Incluir(ImagemNoticia imagemNoticia)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "INSERT INTO ImagemNoticia VALUES(@pIdNoticia, @pImagem, @pIdLocalNoticia, @pIsPortal, @pDtInclusao, @pLogin)";
            command.Parameters.Add(new SqlParameter("@pIdNoticia", imagemNoticia.IdNoticia));
            command.Parameters.Add(new SqlParameter("@pIdLocalNoticia", imagemNoticia.IdLocalNoticia));
            command.Parameters.Add(new SqlParameter("@pIsPortal", imagemNoticia.IsPortal));
            command.Parameters.Add(new SqlParameter("@pDtInclusao", imagemNoticia.DtInclusao));
            command.Parameters.Add(new SqlParameter("@pLogin", imagemNoticia.Login));
            if (imagemNoticia.Imagem != null)
            {
                command.Parameters.Add("@pImagem", SqlDbType.Image, 0).Value = ConvertImageToByteArray(imagemNoticia.Imagem, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                command.Parameters.Add("@pImagem", SqlDbType.Image, 0).Value = DBNull.Value;
            }


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

        public string Alterar(ImagemNoticia imagemNoticia)
        {
            object ret = default(object);

            StringBuilder sbQuery = new StringBuilder();
            SqlCommand command = new SqlCommand();

            command.CommandText = "UPDATE ImagemNoticia SET IdNoticia = @pIdNoticia, Imagem = @pImagem, IdLocalNoticia = @pIdLocalNoticia, IsPortal = @pIsPortal ";
            command.CommandText += "WHERE IdImagemNoticia = @pIdImagemNoticia";
            command.Parameters.Add(new SqlParameter("@pIdNoticia", imagemNoticia.IdNoticia));
            command.Parameters.Add(new SqlParameter("@pImagem", imagemNoticia.Imagem));            
            command.Parameters.Add(new SqlParameter("@pIdLocalNoticia", imagemNoticia.IdLocalNoticia));
            command.Parameters.Add(new SqlParameter("@pIsPortal", imagemNoticia.IsPortal));

            ret = db.ExecuteNonQuery(command);

            return ret.ToString();
        }

        public string Excluir(ImagemNoticia imagemNoticia)
        {
            object ret;

            StringBuilder sbQuery = new StringBuilder();

            sbQuery.Append("DELETE FROM ImagemNoticia WHERE IdImagemLocalNoticia = " + imagemNoticia.IdImagemNoticia);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
            {
                try
                {
                    ret = db.ExecuteNonQuery(dbCommand);
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }

            return ret.ToString();
        }

        private static ImagemNoticia PopularEntidade(DataTable dtImagemNoticia, int i)
        {
            ImagemNoticia imagemNoticia = new ImagemNoticia();

            if (dtImagemNoticia.Columns.Contains("IdImagemNoticia"))
            {
                if (dtImagemNoticia.Rows[i]["IdImagemNoticia"] != DBNull.Value)
                {
                    imagemNoticia.IdImagemNoticia = Convert.ToInt32(dtImagemNoticia.Rows[i]["IdImagemNoticia"].ToString());
                }
            }

            if (dtImagemNoticia.Columns.Contains("IdNoticia"))
            {
                if (dtImagemNoticia.Rows[i]["IdNoticia"] != DBNull.Value)
                {
                    imagemNoticia.IdNoticia = Convert.ToInt32(dtImagemNoticia.Rows[i]["IdNoticia"]);
                }
            }

            if (dtImagemNoticia.Columns.Contains("Imagem"))
            {
                if (dtImagemNoticia.Rows[i]["Imagem"] != DBNull.Value)
                {
                    imagemNoticia.Imagem = dtImagemNoticia.Rows[i]["Imagem"];
                }
            }

            if (dtImagemNoticia.Columns.Contains("IdLocalNoticia"))
            {
                if (dtImagemNoticia.Rows[i]["IdLocalNoticia"] != DBNull.Value)
                {
                    imagemNoticia.IdLocalNoticia = Convert.ToInt32(dtImagemNoticia.Rows[i]["IdLocalNoticia"]);
                }
            }

            if (dtImagemNoticia.Columns.Contains("IsPortal"))
            {
                if (dtImagemNoticia.Rows[i]["IsPortal"] != DBNull.Value)
                {
                    imagemNoticia.IsPortal = Convert.ToInt32(dtImagemNoticia.Rows[i]["IsPortal"]);
                }
            }

            if (dtImagemNoticia.Columns.Contains("DtInclusao"))
            {
                if (dtImagemNoticia.Rows[i]["DtInclusao"] != DBNull.Value)
                {
                    imagemNoticia.DtInclusao = Convert.ToDateTime(dtImagemNoticia.Rows[i]["DtInclusao"]);
                }
            }

            if (dtImagemNoticia.Columns.Contains("Login"))
            {
                if (dtImagemNoticia.Rows[i]["Login"] != DBNull.Value)
                {
                    imagemNoticia.Login = dtImagemNoticia.Rows[i]["Login"].ToString();
                }
            }

            return imagemNoticia;
        }
    }
}
