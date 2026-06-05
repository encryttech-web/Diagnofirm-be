using DiagnofirmAdmin.Handler;
using DiagnofirmAdmin.Model;

using Npgsql;
using NpgsqlTypes;

using System;
using System.Data;

namespace DiagnofirmAdmin.Control
{
    public class NewsletterControl
    {
        private readonly DALHandler dalhandler;

        public NewsletterControl()
        {
            dalhandler = new DALHandler();
        }

        // =========================================
        // GET ALL NEWSLETTER
        // =========================================
        public DataSet getnewsletter()
        {
            try
            {
                string query =
                    @"select diafrm.get_all_newsletter(@ref1);";

                NpgsqlParameter[] param =
                    new NpgsqlParameter[1];

                param[0] = new NpgsqlParameter(
                    "@ref1",
                    NpgsqlDbType.Refcursor
                )
                {
                    Value = "ref1"
                };

                return dalhandler
                    .getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // =========================================
        // GET NEWSLETTER BY ID
        // =========================================
        public DataSet getnewsletterbyid(
            getnewsletterbyidModel model
        )
        {
            try
            {
                string query =
                    @"select diafrm.get_newsletter_by_id(
                        @id,
                        @ref1
                    );";

                NpgsqlParameter[] param =
                    new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter(
                    "@id",
                    NpgsqlDbType.Integer
                )
                {
                    Value = model.Id
                };

                param[1] = new NpgsqlParameter(
                    "@ref1",
                    NpgsqlDbType.Refcursor
                )
                {
                    Value = "ref1"
                };

                return dalhandler
                    .getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // =========================================
        // ADD NEWSLETTER
        // =========================================
        public DataSet addnewsletter(addnewsletterModel model)
        {
            try
            {
                string query =
                @"select diafrm.add_newsletter(
                    @usr_id,
                    @version_no,
                    @letter_date,
                    @letter_image,
                    @letter_imgname,
                    @letter_file,
                    @letter_filename,
                    @letter_ord,
                    @is_active,
                    @username,
                    @ref1
                );";

                NpgsqlParameter[] param =
                    new NpgsqlParameter[10];

                param[0] = new NpgsqlParameter("@usr_id", NpgsqlDbType.Text) { Value = model.usr_id };
                param[1] = new NpgsqlParameter("@version_no", NpgsqlDbType.Text) { Value = model.version_no };
                param[2] = new NpgsqlParameter("@letter_date", NpgsqlDbType.Text) { Value = model.letter_date };
                param[3] = new NpgsqlParameter("@letter_image", NpgsqlDbType.Text) { Value = model.letter_image };
                param[4] = new NpgsqlParameter("@letter_imgname", NpgsqlDbType.Text) { Value = model.letter_imgname };
                param[5] = new NpgsqlParameter("@letter_file", NpgsqlDbType.Text) { Value = model.letter_file };
                param[6] = new NpgsqlParameter("@letter_filename", NpgsqlDbType.Text) { Value = model.letter_filename };
                param[7] = new NpgsqlParameter("@letter_ord", NpgsqlDbType.Text) { Value = model.letter_ord };
                param[8] = new NpgsqlParameter("@is_active", NpgsqlDbType.Text) { Value = model.is_active };
                param[9] = new NpgsqlParameter("@username", NpgsqlDbType.Text) { Value = model.username };

                // NOTE: refcursor should be last parameter
                NpgsqlParameter ref1 = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                var parameters = new NpgsqlParameter[]
                {
                    param[0], param[1], param[2], param[3], param[4],
                    param[5], param[6], param[7], param[8], param[9],
                    ref1
                };

                return dalhandler
                    .getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // =========================================
        // UPDATE NEWSLETTER
        // =========================================
        public DataSet updatenewsletter(updatenewsletterModel model)
        {
            try
            {
                string query =
                @"select diafrm.update_newsletter(
                    @nid,
                    @usrid,
                    @versionno,
                    @letterdate,
                    @letterimage,
                    @letterimgname,
                    @letterfile,
                    @letterfilename,
                    @letterord,
                    @isactive,
                    @username,
                    @ref1
                );";

                NpgsqlParameter[] param =
                    new NpgsqlParameter[11];

                param[0] = new NpgsqlParameter("@nid", NpgsqlDbType.Integer) { Value = model.nid };
                param[1] = new NpgsqlParameter("@usrid", NpgsqlDbType.Text) { Value = model.usrid };
                param[2] = new NpgsqlParameter("@versionno", NpgsqlDbType.Text) { Value = model.versionno };
                param[3] = new NpgsqlParameter("@letterdate", NpgsqlDbType.Text) { Value = model.letterdate };
                param[4] = new NpgsqlParameter("@letterimage", NpgsqlDbType.Text) { Value = model.letterimage };
                param[5] = new NpgsqlParameter("@letterimgname", NpgsqlDbType.Text) { Value = model.letterimgname };
                param[6] = new NpgsqlParameter("@letterfile", NpgsqlDbType.Text) { Value = model.letterfile };
                param[7] = new NpgsqlParameter("@letterfilename", NpgsqlDbType.Text) { Value = model.letterfilename };
                param[8] = new NpgsqlParameter("@letterord", NpgsqlDbType.Text) { Value = model.letterord };
                param[9] = new NpgsqlParameter("@isactive", NpgsqlDbType.Text) { Value = model.isactive };
                param[10] = new NpgsqlParameter("@username", NpgsqlDbType.Text) { Value = model.username };

                NpgsqlParameter ref1 = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                var parameters = new NpgsqlParameter[]
                {
                    param[0], param[1], param[2], param[3], param[4],
                    param[5], param[6], param[7], param[8], param[9],
                    param[10], ref1
                };

                return dalhandler
                    .getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // =========================================
        // DELETE NEWSLETTER
        // =========================================
        public DataSet deletenewsletter(deletenewsletterModel model)
        {
            try
            {
                string query =
                    @"select diafrm.delete_newsletter(
                        @nid,
                        @ref1
                    );";

                NpgsqlParameter[] param =
                    new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@nid", NpgsqlDbType.Integer)
                {
                    Value = model.nid
                };

                param[1] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler
                    .getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= IMAGE =================
        public DataSet getnewsletterImagebyId(newsletterimageviewModel model)
        {
            try
            {
                string query = @"select public.get_newsletterImagebyid(@newsletterid,@username,@ref1,@ref2);";

                NpgsqlParameter[] param = new NpgsqlParameter[4];

                param[0] = new NpgsqlParameter("@newsletterid", NpgsqlDbType.Text)
                {
                    Value = model.newsletterid
                };

                param[1] = new NpgsqlParameter("@username", NpgsqlDbType.Text)
                {
                    Value = model.username
                };

                param[2] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                param[3] = new NpgsqlParameter("@ref2", NpgsqlDbType.Refcursor)
                {
                    Value = "ref2"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (Exception ex)
            {
                throw new DataException("", ex);
            }
        }
    }
}