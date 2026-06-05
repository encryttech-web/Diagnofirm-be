using DiagnofirmAdmin.Handler;
using DiagnofirmAdmin.Model;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;

namespace DiagnofirmAdmin.Control
{
    public class FaqControl
    {
        private readonly DALHandler dalhandler;

        public FaqControl()
        {
            dalhandler = new DALHandler();
        }

        // ================= GET FAQ =================
        public DataSet getfaq()
        {
            try
            {
                string query = @"select diafrm.get_all_faq(@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[1];

                param[0] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (Exception ex)
            {
                throw new DataException("Error in getfaq", ex);
            }
        }

        // ================= GET FAQ BY ID =================
        public DataSet getfaqbyId(getfaqModel model)
        {
            try
            {
                string query = @"select diafrm.get_faq_by_id(@faqid,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@faqid", NpgsqlDbType.Integer)
                {
                    Value = model.faqid
                };

                param[1] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (Exception ex)
            {
                throw new DataException("Error in getfaqbyId", ex);
            }
        }

        // ================= GET FAQ BY Package ID =================
        public DataSet getfaqbypackageId(getfaqpackageModel model)
        {
            try
            {
                string query = @"select diafrm.get_faq_by_packageid(@packgid,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@packgid", NpgsqlDbType.Integer)
                {
                    Value = model.packgid
                };

                param[1] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (Exception ex)
            {
                throw new DataException("Error in getfaqbyId", ex);
            }
        }

        // ================= GET FAQ BY Subcategory ID =================
        public DataSet getfaqbysubcatId(getfaqsubcatModel model)
        {
            try
            {
                string query = @"select diafrm.get_faq_by_subactegoryid(@subcatid,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@subcatid", NpgsqlDbType.Integer)
                {
                    Value = model.subcatid
                };

                param[1] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (Exception ex)
            {
                throw new DataException("Error in getfaqbyId", ex);
            }
        }

        // ================= GET FAQ BY Product ID =================
        public DataSet getfaqbyproductId(getfaqproductModel model)
        {
            try
            {
                string query = @"select diafrm.get_faq_by_productid(@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@prodid", NpgsqlDbType.Integer)
                {
                    Value = model.prodid
                };

                param[1] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (Exception ex)
            {
                throw new DataException("Error in getfaqbyId", ex);
            }
        }

        // ================= GET FAQ BY Home =================
        public DataSet getfaqbyhomecheck()
        {
            try
            {
                string query = @"select diafrm.get_faq_by_homecheck(@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[1];

                param[0] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (Exception ex)
            {
                throw new DataException("Error in getfaqbyId", ex);
            }
        }

        // ================= ADD FAQ =================
        public DataSet addfaq(addfaqModel model)
        {
            try
            {
                string query = @"select diafrm.add_faq(
                    @prodid,
                    @subcatid,
                    @packgid,
                    @faqcode,
                    @faqname,
                    @faqdesc,
                    @faqord,
                    @faqques,
                    @faqans,
                    @faqhomecheck,
                    @username,
                    @status,
                    @ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[13];

                param[0] = new NpgsqlParameter("@prodid", NpgsqlDbType.Integer)
                {
                    Value = model.prodid
                };

                param[1] = new NpgsqlParameter("@subcatid", NpgsqlDbType.Integer)
                {
                    Value = model.subcatid
                };

                param[2] = new NpgsqlParameter("@packgid", NpgsqlDbType.Integer)
                {
                    Value = model.packgid
                };

                param[3] = new NpgsqlParameter("@faqcode", NpgsqlDbType.Text)
                {
                    Value = model.faqcode
                };

                param[4] = new NpgsqlParameter("@faqname", NpgsqlDbType.Text)
                {
                    Value = model.faqname
                };

                param[5] = new NpgsqlParameter("@faqdesc", NpgsqlDbType.Text)
                {
                    Value = model.faqdesc
                };

                param[6] = new NpgsqlParameter("@faqord", NpgsqlDbType.Text)
                {
                    Value = model.faqord
                };

                param[7] = new NpgsqlParameter("@faqques", NpgsqlDbType.Text)
                {
                    Value = model.faqques
                };

                param[8] = new NpgsqlParameter("@faqans", NpgsqlDbType.Text)
                {
                    Value = model.faqans
                };

                param[9] = new NpgsqlParameter("@faqhomecheck", NpgsqlDbType.Text)
                {
                    Value = model.faqhomecheck
                };

                param[10] = new NpgsqlParameter("@username", NpgsqlDbType.Text)
                {
                    Value = model.username
                };

                param[11] = new NpgsqlParameter("@status", NpgsqlDbType.Text)
                {
                    Value = model.status
                };

                param[12] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (Exception ex)
            {
                throw new DataException("Error in addfaq", ex);
            }
        }

        // ================= UPDATE FAQ =================
        public DataSet updatefaq(updatefaqModel model)
        {
            try
            {
                string query = @"select diafrm.update_faq(
                    @faqid,
                    @prodid,
                    @subcatid,
                    @packgid,
                    @faqcode,
                    @faqname,
                    @faqdesc,
                    @faqord,
                    @faqques,
                    @faqans,
                    @faqhomecheck,
                    @username,
                    @status,
                    @ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[14];

                param[0] = new NpgsqlParameter("@faqid", NpgsqlDbType.Integer)
                {
                    Value = model.faqid
                };

                param[1] = new NpgsqlParameter("@prodid", NpgsqlDbType.Integer)
                {
                    Value = model.prodid
                };

                param[2] = new NpgsqlParameter("@subcatid", NpgsqlDbType.Integer)
                {
                    Value = model.subcatid
                };

                param[3] = new NpgsqlParameter("@packgid", NpgsqlDbType.Integer)
                {
                    Value = model.packgid
                };

                param[4] = new NpgsqlParameter("@faqcode", NpgsqlDbType.Text)
                {
                    Value = model.faqcode
                };

                param[5] = new NpgsqlParameter("@faqname", NpgsqlDbType.Text)
                {
                    Value = model.faqname
                };

                param[6] = new NpgsqlParameter("@faqdesc", NpgsqlDbType.Text)
                {
                    Value = model.faqdesc
                };

                param[7] = new NpgsqlParameter("@faqord", NpgsqlDbType.Text)
                {
                    Value = model.faqord
                };

                param[8] = new NpgsqlParameter("@faqques", NpgsqlDbType.Text)
                {
                    Value = model.faqques
                };

                param[9] = new NpgsqlParameter("@faqans", NpgsqlDbType.Text)
                {
                    Value = model.faqans
                };

                param[10] = new NpgsqlParameter("@faqhomecheck", NpgsqlDbType.Text)
                {
                    Value = model.faqhomecheck
                };

                param[11] = new NpgsqlParameter("@username", NpgsqlDbType.Text)
                {
                    Value = model.username
                };

                param[12] = new NpgsqlParameter("@status", NpgsqlDbType.Text)
                {
                    Value = model.status
                };

                param[13] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (Exception ex)
            {
                throw new DataException("Error in updatefaq", ex);
            }
        }

        // ================= DELETE FAQ =================
        public DataSet delfaq(deletefaqModel model)
        {
            try
            {
                string query = @"select diafrm.delete_faq(@faqid,@username,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[3];

                param[0] = new NpgsqlParameter("@faqid", NpgsqlDbType.Integer)
                {
                    Value = model.faqid
                };

                param[1] = new NpgsqlParameter("@username", NpgsqlDbType.Varchar)
                {
                    Value = model.username
                };

                param[2] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (Exception ex)
            {
                throw new DataException("Error in delfaq", ex);
            }
        }
    }
}