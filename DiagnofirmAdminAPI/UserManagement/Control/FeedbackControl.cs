using DiagnofirmAdmin.Handler;
using DiagnofirmAdmin.Model;

using Npgsql;
using NpgsqlTypes;

using System;
using System.Data;

namespace DiagnofirmAdmin.Control
{
    public class FeedbackControl
    {
        private readonly DALHandler dalhandler;

        public FeedbackControl()
        {
            dalhandler = new DALHandler();
        }

        // =========================================
        // GET ALL FEEDBACK
        // =========================================
        public DataSet getfeedback()
        {
            try
            {
                string query =
                    @"select diafrm.get_all_feedback(@ref1);";

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
                    .ExecuteMultipleSelectQuery(
                        query,
                        param
                    );
            }
            catch (Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // =========================================
        // GET FEEDBACK BY ID
        // =========================================
        public DataSet getfeedbackbyid(
            getfeedbackbyidModel model
        )
        {
            try
            {
                string query =
                    @"select diafrm.get_feedback_by_id(
                        @feedbackid,
                        @ref1
                    );";

                NpgsqlParameter[] param =
                    new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter(
                    "@feedbackid",
                    NpgsqlDbType.Integer
                )
                {
                    Value = model.feedbackid
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
                    .ExecuteMultipleSelectQuery(
                        query,
                        param
                    );
            }
            catch (Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // =========================================
        // GET FEEDBACK BY USER ID
        // =========================================
        public DataSet getfeedbackbyuserid(
            getfeedbackbyuseridModel model
        )
        {
            try
            {
                string query =
                    @"select diafrm.get_feedback_by_userid(
                        @userid,
                        @ref1
                    );";

                NpgsqlParameter[] param =
                    new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter(
                    "@userid",
                    NpgsqlDbType.Text
                )
                {
                    Value = model.userid
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
                    .ExecuteMultipleSelectQuery(
                        query,
                        param
                    );
            }
            catch (Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // =========================================
        // ADD FEEDBACK
        // =========================================
        public DataSet addfeedback(
            addfeedbackModel model
        )
        {
            try
            {
                string query =
                @"select diafrm.add_feedback(

                    @userid,
                    @username,
                    @useremail,
                    @userrole,

                    @feedbackdesc,
                    @starrating,
                    @feedbackord,

                    @createdby,
                    @status,
                    @ref1

                );";

                NpgsqlParameter[] param =
                    new NpgsqlParameter[10];

                param[0] = new NpgsqlParameter(
                    "@userid",
                    NpgsqlDbType.Text
                )
                {
                    Value = model.userid
                };

                param[1] = new NpgsqlParameter(
                    "@username",
                    NpgsqlDbType.Text
                )
                {
                    Value = model.username
                };

                param[2] = new NpgsqlParameter(
                    "@useremail",
                    NpgsqlDbType.Text
                )
                {
                    Value = model.useremail
                };

                param[3] = new NpgsqlParameter(
                    "@userrole",
                    NpgsqlDbType.Text
                )
                {
                    Value = model.userrole
                };

                param[4] = new NpgsqlParameter(
                    "@feedbackdesc",
                    NpgsqlDbType.Text
                )
                {
                    Value = model.feedbackdesc
                };

                param[5] = new NpgsqlParameter(
                    "@starrating",
                    NpgsqlDbType.Text
                )
                {
                    Value = model.starrating
                };

                param[6] = new NpgsqlParameter(
                    "@feedbackord",
                    NpgsqlDbType.Text
                )
                {
                    Value = model.feedbackord
                };

                param[7] = new NpgsqlParameter(
                    "@createdby",
                    NpgsqlDbType.Text
                )
                {
                    Value = model.createdby
                };

                param[8] = new NpgsqlParameter(
                    "@status",
                    NpgsqlDbType.Text
                )
                {
                    Value = model.status
                };

                param[9] = new NpgsqlParameter(
                    "@ref1",
                    NpgsqlDbType.Refcursor
                )
                {
                    Value = "ref1"
                };

                return dalhandler
                    .getConnectionObject()
                    .ExecuteMultipleSelectQuery(
                        query,
                        param
                    );
            }
            catch (Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // =========================================
        // UPDATE FEEDBACK
        // =========================================
        public DataSet updatefeedback(
            updatefeedbackModel model
        )
        {
            try
            {
                string query =
                @"select diafrm.update_feedback(

                    @feedbackid,

                    @userid,
                    @username,
                    @useremail,
                    @userrole,

                    @feedbackdesc,
                    @starrating,
                    @feedbackord,

                    @updatedby,
                    @status,
                    @ref1

                );";

                NpgsqlParameter[] param =
                    new NpgsqlParameter[11];

                param[0] = new NpgsqlParameter(
                    "@feedbackid",
                    NpgsqlDbType.Integer
                )
                {
                    Value = model.feedbackid
                };

                param[1] = new NpgsqlParameter(
                    "@userid",
                    NpgsqlDbType.Text
                )
                {
                    Value = model.userid
                };

                param[2] = new NpgsqlParameter(
                    "@username",
                    NpgsqlDbType.Text
                )
                {
                    Value = model.username
                };

                param[3] = new NpgsqlParameter(
                    "@useremail",
                    NpgsqlDbType.Text
                )
                {
                    Value = model.useremail
                };

                param[4] = new NpgsqlParameter(
                    "@userrole",
                    NpgsqlDbType.Text
                )
                {
                    Value = model.userrole
                };

                param[5] = new NpgsqlParameter(
                    "@feedbackdesc",
                    NpgsqlDbType.Text
                )
                {
                    Value = model.feedbackdesc
                };

                param[6] = new NpgsqlParameter(
                    "@starrating",
                    NpgsqlDbType.Text
                )
                {
                    Value = model.starrating
                };

                param[7] = new NpgsqlParameter(
                    "@feedbackord",
                    NpgsqlDbType.Text
                )
                {
                    Value = model.feedbackord
                };

                param[8] = new NpgsqlParameter(
                    "@updatedby",
                    NpgsqlDbType.Text
                )
                {
                    Value = model.updatedby
                };

                param[9] = new NpgsqlParameter(
                    "@status",
                    NpgsqlDbType.Text
                )
                {
                    Value = model.status
                };

                param[10] = new NpgsqlParameter(
                    "@ref1",
                    NpgsqlDbType.Refcursor
                )
                {
                    Value = "ref1"
                };

                return dalhandler
                    .getConnectionObject()
                    .ExecuteMultipleSelectQuery(
                        query,
                        param
                    );
            }
            catch (Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // =========================================
        // DELETE FEEDBACK
        // =========================================
        public DataSet deletefeedback(
            deletefeedbackModel model
        )
        {
            try
            {
                string query =
                    @"select diafrm.delete_feedback(
                        @feedbackid,
                        @username,
                        @ref1
                    );";

                NpgsqlParameter[] param =
                    new NpgsqlParameter[3];

                param[0] = new NpgsqlParameter(
                    "@feedbackid",
                    NpgsqlDbType.Integer
                )
                {
                    Value = model.feedbackid
                };

                param[1] = new NpgsqlParameter(
                   "@username",
                   NpgsqlDbType.Text
               )
                {
                    Value = model.username
                };

                param[2] = new NpgsqlParameter(
                    "@ref1",
                    NpgsqlDbType.Refcursor
                )
                {
                    Value = "ref1"
                };

                return dalhandler
                    .getConnectionObject()
                    .ExecuteMultipleSelectQuery(
                        query,
                        param
                    );
            }
            catch (Exception ex)
            {
                throw new DataException("", ex);
            }
        }
    }
}