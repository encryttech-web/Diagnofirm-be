using Npgsql;
using NpgsqlTypes;
using System.Data;
using DiagnofirmAdmin.Handler;
using DiagnofirmAdmin.Model;

namespace DiagnofirmAdmin.Control
{
    public class CheckoutControl
    {
        private readonly DALHandler dalhandler;

        public CheckoutControl()
        {
            dalhandler = new DALHandler();
        }

        // ================= CREATE CHECKOUT =================
        public DataSet createcheckout(CheckoutModel model)
        {
            try
            {
                string query = @"select diafrm.create_checkout(
                    @p_order_id,
                    @usr_id,
                    @prod_id,
                    @packg_id,
                    @pay_id,
                    @check_qty,
                    @prod_total,
                    @check_total,
                    @check_firstname,
                    @check_lastname,
                    @check_country,
                    @check_address1,
                    @check_address2,
                    @check_city,
                    @check_state,
                    @check_zip,
                    @check_phno,
                    @check_email,
                    @check_addnote,
                    @is_active,
                    @username,
                    @ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[22];

                param[0] = new NpgsqlParameter("@p_order_id", NpgsqlDbType.Integer) { Value = model.order_id };
                param[1] = new NpgsqlParameter("@usr_id", NpgsqlDbType.Text) { Value = model.usr_id };
                param[2] = new NpgsqlParameter("@prod_id", NpgsqlDbType.Integer) { Value = model.prod_id };
                param[3] = new NpgsqlParameter("@packg_id", NpgsqlDbType.Integer) { Value = model.packg_id };
                param[4] = new NpgsqlParameter("@pay_id", NpgsqlDbType.Integer) { Value = model.pay_id };
                param[5] = new NpgsqlParameter("@check_qty", NpgsqlDbType.Numeric) { Value = model.check_qty };
                param[6] = new NpgsqlParameter("@prod_total", NpgsqlDbType.Numeric) { Value = model.prod_total };
                param[7] = new NpgsqlParameter("@check_total", NpgsqlDbType.Numeric) { Value = model.check_total };
                param[8] = new NpgsqlParameter("@check_firstname", NpgsqlDbType.Text) { Value = model.check_firstname };
                param[9] = new NpgsqlParameter("@check_lastname", NpgsqlDbType.Text) { Value = model.check_lastname };
                param[10] = new NpgsqlParameter("@check_country", NpgsqlDbType.Text) { Value = model.check_country };
                param[11] = new NpgsqlParameter("@check_address1", NpgsqlDbType.Text) { Value = model.check_address1 };
                param[12] = new NpgsqlParameter("@check_address2", NpgsqlDbType.Text) { Value = model.check_address2 };
                param[13] = new NpgsqlParameter("@check_city", NpgsqlDbType.Text) { Value = model.check_city };
                param[14] = new NpgsqlParameter("@check_state", NpgsqlDbType.Text) { Value = model.check_state };
                param[15] = new NpgsqlParameter("@check_zip", NpgsqlDbType.Text) { Value = model.check_zip };
                param[16] = new NpgsqlParameter("@check_phno", NpgsqlDbType.Text) { Value = model.check_phno };
                param[17] = new NpgsqlParameter("@check_email", NpgsqlDbType.Text) { Value = model.check_email };
                param[18] = new NpgsqlParameter("@check_addnote", NpgsqlDbType.Text) { Value = model.check_addnote };
                param[19] = new NpgsqlParameter("@is_active", NpgsqlDbType.Text) { Value = model.is_active };
                param[20] = new NpgsqlParameter("@username", NpgsqlDbType.Text) { Value = model.username };

                param[21] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= GET ALL CHECKOUT =================
        public DataSet getcheckout()
        {
            try
            {
                string query = @"select diafrm.get_all_checkout(@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[1];

                param[0] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= GET BY ID =================
        public DataSet getcheckoutbyId(getCheckoutModel model)
        {
            try
            {
                string query = @"select diafrm.get_checkout_by_id(@cid,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@cid", NpgsqlDbType.Integer)
                {
                    Value = model.cid
                };

                param[1] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= UPDATE CHECKOUT =================
        public DataSet updatecheckout(updateCheckoutModel model)
        {
            try
            {
                string query = @"select diafrm.update_checkout(
                                @cid,
                                @p_order_id,
                                @p_usr_id,
                                @p_prod_id,
                                @p_packg_id,
                                @p_pay_id,
                                @p_check_qty,
                                @p_prod_total,
                                @p_check_total,
                                @p_check_firstname,
                                @p_check_lastname,
                                @p_check_country,
                                @p_check_address1,
                                @p_check_address2,
                                @p_check_city,
                                @p_check_state,
                                @p_check_zip,
                                @p_check_phno,
                                @p_check_email,
                                @p_check_addnote,
                                @p_is_active,
                                @p_username,
                                @ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[23];

                param[0] = new NpgsqlParameter("@cid", NpgsqlDbType.Integer) { Value = model.cid };
                param[1] = new NpgsqlParameter("@p_order_id", NpgsqlDbType.Integer) { Value = model.order_id };
                param[2] = new NpgsqlParameter("@p_usr_id", NpgsqlDbType.Text) { Value = model.usr_id };
                param[3] = new NpgsqlParameter("@p_prod_id", NpgsqlDbType.Integer) { Value = model.prod_id };
                param[4] = new NpgsqlParameter("@p_packg_id", NpgsqlDbType.Integer) { Value = model.packg_id };
                param[5] = new NpgsqlParameter("@p_pay_id", NpgsqlDbType.Integer) { Value = model.pay_id };

                param[6] = new NpgsqlParameter("@p_check_qty", NpgsqlDbType.Numeric) { Value = model.check_qty };
                param[7] = new NpgsqlParameter("@p_prod_total", NpgsqlDbType.Numeric) { Value = model.prod_total };
                param[8] = new NpgsqlParameter("@p_check_total", NpgsqlDbType.Numeric) { Value = model.check_total };
                param[9] = new NpgsqlParameter("@p_check_firstname", NpgsqlDbType.Text) { Value = model.check_firstname };
                param[10] = new NpgsqlParameter("@p_check_lastname", NpgsqlDbType.Text) { Value = model.check_lastname };
                param[11] = new NpgsqlParameter("@p_check_country", NpgsqlDbType.Text) { Value = model.check_country };
                param[12] = new NpgsqlParameter("@p_check_address1", NpgsqlDbType.Text) { Value = model.check_address1 };
                param[13] = new NpgsqlParameter("@p_check_address2", NpgsqlDbType.Text) { Value = model.check_address2 };
                param[14] = new NpgsqlParameter("@p_check_city", NpgsqlDbType.Text) { Value = model.check_city };
                param[15] = new NpgsqlParameter("@p_check_state", NpgsqlDbType.Text) { Value = model.check_state };
                param[16] = new NpgsqlParameter("@p_check_zip", NpgsqlDbType.Text) { Value = model.check_zip };
                param[17] = new NpgsqlParameter("@p_check_phno", NpgsqlDbType.Text) { Value = model.check_phno };
                param[18] = new NpgsqlParameter("@p_check_email", NpgsqlDbType.Text) { Value = model.check_email };
                param[19] = new NpgsqlParameter("@p_check_addnote", NpgsqlDbType.Text) { Value = model.check_addnote };

                param[20] = new NpgsqlParameter("@p_is_active", NpgsqlDbType.Text) { Value = model.is_active };
                param[21] = new NpgsqlParameter("@p_username", NpgsqlDbType.Text) { Value = model.username };

                param[22] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= DELETE CHECKOUT =================
        public DataSet deletecheckout(getCheckoutModel model)
        {
            try
            {
                string query = @"select diafrm.delete_checkout(@cid,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@cid", NpgsqlDbType.Integer)
                {
                    Value = model.cid
                };

                param[1] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= GET BY PRODUCT ID =================
        public DataSet getcheckoutbyproduct(getCheckoutByProductModel model)
        {
            try
            {
                string query = @"select diafrm.get_checkout_by_product(@pid,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@pid", NpgsqlDbType.Integer)
                {
                    Value = model.prod_id
                };

                param[1] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= GET BY PACKAGE ID =================
        public DataSet getcheckoutbypackage(getCheckoutByPackageModel model)
        {
            try
            {
                string query = @"select diafrm.get_checkout_by_package(@pid,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@pid", NpgsqlDbType.Integer)
                {
                    Value = model.packg_id
                };

                param[1] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= GET ALL =================
        public DataSet getpayment()
        {
            try
            {
                string query = @"select diafrm.get_all_payment(@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[1];

                param[0] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= GET BY ID =================
        public DataSet getpaymentbyid(getPaymentModel model)
        {
            try
            {
                string query = @"select diafrm.get_payment_by_id(@id,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@id", NpgsqlDbType.Integer)
                {
                    Value = model.id
                };

                param[1] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= GET BY Order ID =================
        public DataSet getcheckoutbyOrderId(getCheckoutbyOrderModel model)
        {
            try
            {
                string query = @"select diafrm.get_checkout_by_order(@ordid,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@ordid", NpgsqlDbType.Integer)
                {
                    Value = model.ordid
                };

                param[1] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= GET ALL ORDER =================
        public DataSet getallorder()
        {
            try
            {
                string query = @"select diafrm.get_all_Order_detail(@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[1];

                param[0] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= GET ALL ORDER =================
        public DataSet getallcount()
        {
            try
            {
                string query = @"select diafrm.get_all_count(@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[1];

                param[0] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

    }
}