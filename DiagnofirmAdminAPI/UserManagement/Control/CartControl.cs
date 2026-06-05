using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;
using DiagnofirmAdmin.Handler;
using DiagnofirmAdmin.Model;

namespace DiagnofirmAdmin.Control
{
    public class CartControl
    {
        private readonly DALHandler dalhandler;

        public CartControl()
        {
            dalhandler = new();
        }

        // ================= CREATE CART =================
        public DataSet createcart(addCartModel model)
        {
            try
            {
                string query = @"select diafrm.create_cart(
                    @usr_id,
                    @prod_id,
                    @packg_id,
                    @cart_qty,
                    @prod_total,
                    @cart_total,
                    @cart_desc,
                    @is_active,
                    @username,
                    @ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[10];

                param[0] = new NpgsqlParameter("@usr_id", NpgsqlDbType.Text)
                {
                    Value = model.usr_id
                };

                param[1] = new NpgsqlParameter("@prod_id", NpgsqlDbType.Integer)
                {
                    Value = model.prod_id
                };

                param[2] = new NpgsqlParameter("@packg_id", NpgsqlDbType.Integer)
                {
                    Value = model.packg_id
                };

                param[3] = new NpgsqlParameter("@cart_qty", NpgsqlDbType.Numeric)
                {
                    Value = model.cart_qty
                };

                param[4] = new NpgsqlParameter("@prod_total", NpgsqlDbType.Numeric)
                {
                    Value = model.prod_total
                };

                param[5] = new NpgsqlParameter("@cart_total", NpgsqlDbType.Numeric)
                {
                    Value = model.cart_total
                };

                param[6] = new NpgsqlParameter("@cart_desc", NpgsqlDbType.Text)
                {
                    Value = model.cart_desc
                };

                param[7] = new NpgsqlParameter("@is_active", NpgsqlDbType.Text)
                {
                    Value = model.is_active
                };

                param[8] = new NpgsqlParameter("@username", NpgsqlDbType.Text)
                {
                    Value = model.username
                };

                param[9] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= GET ALL CART =================
        public DataSet getcart()
        {
            try
            {
                string query = @"select diafrm.get_all_cart(@ref1);";

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
                throw new DataException("", ex);
            }
        }

        // ================= GET BY ID =================
        public DataSet getcartbyid(getCartModel model)
        {
            try
            {
                string query = @"select diafrm.get_cart_by_id(@cid,@ref1);";

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
            catch (Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= UPDATE CART =================
        public DataSet updatecart(CartModel model)
        {
            try
            {
                string query = @"select diafrm.update_cart(
                    @cid,
                    @p_usr_id,
                    @p_prod_id,
                    @p_packg_id,
                    @p_cart_qty,
                    @p_prod_total,
                    @p_cart_total,
                    @p_cart_desc,
                    @p_is_active,
                    @p_username,
                    @ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[11];

                param[0] = new NpgsqlParameter("@cid", NpgsqlDbType.Integer)
                {
                    Value = model.cid
                };

                param[1] = new NpgsqlParameter("@p_usr_id", NpgsqlDbType.Text)
                {
                    Value = model.p_usr_id
                };

                param[2] = new NpgsqlParameter("@p_prod_id", NpgsqlDbType.Integer)
                {
                    Value = model.p_prod_id
                };

                param[3] = new NpgsqlParameter("@p_packg_id", NpgsqlDbType.Integer)
                {
                    Value = model.p_packg_id
                };

                param[4] = new NpgsqlParameter("@p_cart_qty", NpgsqlDbType.Numeric)
                {
                    Value = model.p_cart_qty
                };

                param[5] = new NpgsqlParameter("@p_prod_total", NpgsqlDbType.Numeric)
                {
                    Value = model.p_prod_total
                };

                param[6] = new NpgsqlParameter("@p_cart_total", NpgsqlDbType.Numeric)
                {
                    Value = model.p_cart_total
                };

                param[7] = new NpgsqlParameter("@p_cart_desc", NpgsqlDbType.Text)
                {
                    Value = model.p_cart_desc
                };

                param[8] = new NpgsqlParameter("@p_is_active", NpgsqlDbType.Text)
                {
                    Value = model.p_is_active
                };

                param[9] = new NpgsqlParameter("@p_username", NpgsqlDbType.Text)
                {
                    Value = model.p_username
                };

                param[10] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= DELETE CART =================
        public DataSet deletecart(getCartModel model)
        {
            try
            {
                string query = @"select diafrm.delete_cart(@cid,@ref1);";

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
            catch (Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= GET CART BY PRODUCT ID =================
        public DataSet getcartbyproductid(getCartbyproductModel model)
        {
            try
            {
                string query = @"select diafrm.get_cart_by_product(@prod_id,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@prod_id", NpgsqlDbType.Integer)
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

        // ================= GET CART BY PACKAGE ID =================
        public DataSet getcartbypackageid(getCartbypackageModel model)
        {
            try
            {
                string query = @"select diafrm.get_cart_by_package(@packg_id,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@packg_id", NpgsqlDbType.Integer)
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
    }
}