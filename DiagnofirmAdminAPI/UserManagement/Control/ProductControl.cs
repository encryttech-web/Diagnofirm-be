using DiagnofirmAdmin.Handler;
using DiagnofirmAdmin.Model;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;

namespace DiagnofirmAdmin.Control
{
    public class ProductControl
    {
        private readonly DALHandler dalhandler;

        public ProductControl()
        {
            dalhandler = new DALHandler();
        }

        // ================= GET PRODUCT =================
        public DataSet getproduct()
        {
            try
            {
                string query = @"select diafrm.get_all_product(@ref1);";

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
                throw new DataException("Error in getproduct", ex);
            }
        }

        // ================= GET BY ID =================
        public DataSet getproductbyId(getproductModel model)
        {
            try
            {
                string query = @"select diafrm.get_product_by_id(@productid,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@productid", NpgsqlDbType.Integer)
                {
                    Value = model.productid
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
                throw new DataException("Error in getproductbyId", ex);
            }
        }

        // ================= ADD =================
        public DataSet addproduct(addproductModel model)
        {
            try
            {
                string query = @"select diafrm.add_product(
                    @categoryid,
                    @subcategoryid,
                    @packageid,
                    @userid,
                    @producthead,
                    @productcode,
                    @productname,
                    @productdesc,
                    @productord,
                    @productprice,
                    @productgrpcod,
                    @productimage,
                    @productimagename,
                    @status,
                    @username,
                    @ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[16];

                param[0] = new NpgsqlParameter("@categoryid", NpgsqlDbType.Integer)
                {
                    Value = model.categoryid
                };

                param[1] = new NpgsqlParameter("@subcategoryid", NpgsqlDbType.Array | NpgsqlDbType.Integer)
                {
                    Value = model.subcategoryid
                };

                param[2] = new NpgsqlParameter("@packageid", NpgsqlDbType.Text)
                {
                    Value = model.packageid
                };

                param[3] = new NpgsqlParameter("@userid", NpgsqlDbType.Text)
                {
                    Value = model.userid
                };

                param[4] = new NpgsqlParameter("@producthead", NpgsqlDbType.Text)
                {
                    Value = model.producthead
                };

                param[5] = new NpgsqlParameter("@productcode", NpgsqlDbType.Text)
                {
                    Value = model.productcode
                };

                param[6] = new NpgsqlParameter("@productname", NpgsqlDbType.Text)
                {
                    Value = model.productname
                };

                param[7] = new NpgsqlParameter("@productdesc", NpgsqlDbType.Text)
                {
                    Value = model.productdesc
                };

                param[8] = new NpgsqlParameter("@productord", NpgsqlDbType.Text)
                {
                    Value = model.productord
                };

                param[9] = new NpgsqlParameter("@productprice", NpgsqlDbType.Numeric)
                {
                    Value = model.productprice
                };

                param[10] = new NpgsqlParameter("@productgrpcod", NpgsqlDbType.Text)
                {
                    Value = model.productgrpcod
                };

                param[11] = new NpgsqlParameter("@productimage", NpgsqlDbType.Text)
                {
                    Value = model.productimage
                };

                param[12] = new NpgsqlParameter("@productimagename", NpgsqlDbType.Text)
                {
                    Value = model.productimagename
                };

                param[13] = new NpgsqlParameter("@status", NpgsqlDbType.Text)
                {
                    Value = model.status
                };

                param[14] = new NpgsqlParameter("@username", NpgsqlDbType.Text)
                {
                    Value = model.username
                };

                param[15] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (Exception ex)
            {
                throw new DataException("Error in addproduct", ex);
            }
        }

        // ================= UPDATE =================
        public DataSet updateproduct(updateproductModel model)
        {
            try
            {
                string query = @"select diafrm.update_product(
                    @productid,
                    @categoryid,
                    @subcategoryid,
                    @packageid,
                    @userid,
                    @producthead,
                    @productcode,
                    @productname,
                    @productdesc,
                    @productord,
                    @productprice,
                    @productgrpcod,
                    @productimage,
                    @productimagename,
                    @status,
                    @username,
                    @ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[17];

                param[0] = new NpgsqlParameter("@productid", NpgsqlDbType.Integer)
                {
                    Value = model.productid
                };

                param[1] = new NpgsqlParameter("@categoryid", NpgsqlDbType.Integer)
                {
                    Value = model.categoryid
                };

                param[2] = new NpgsqlParameter("@subcategoryid", NpgsqlDbType.Integer)
                {
                    Value = model.subcategoryid
                };

                param[3] = new NpgsqlParameter("@packageid", NpgsqlDbType.Text)
                {
                    Value = model.packageid
                };

                param[4] = new NpgsqlParameter("@userid", NpgsqlDbType.Text)
                {
                    Value = model.userid
                };

                param[5] = new NpgsqlParameter("@producthead", NpgsqlDbType.Text)
                {
                    Value = model.producthead
                };

                param[6] = new NpgsqlParameter("@productcode", NpgsqlDbType.Text)
                {
                    Value = model.productcode
                };

                param[7] = new NpgsqlParameter("@productname", NpgsqlDbType.Text)
                {
                    Value = model.productname
                };

                param[8] = new NpgsqlParameter("@productdesc", NpgsqlDbType.Text)
                {
                    Value = model.productdesc
                };

                param[9] = new NpgsqlParameter("@productord", NpgsqlDbType.Text)
                {
                    Value = model.productord
                };

                param[10] = new NpgsqlParameter("@productprice", NpgsqlDbType.Numeric)
                {
                    Value = model.productprice
                };

                param[11] = new NpgsqlParameter("@productgrpcod", NpgsqlDbType.Text)
                {
                    Value = model.productgrpcod
                };

                param[12] = new NpgsqlParameter("@productimage", NpgsqlDbType.Text)
                {
                    Value = model.productimage
                };

                param[13] = new NpgsqlParameter("@productimagename", NpgsqlDbType.Text)
                {
                    Value = model.productimagename
                };

                param[14] = new NpgsqlParameter("@status", NpgsqlDbType.Text)
                {
                    Value = model.status
                };

                param[15] = new NpgsqlParameter("@username", NpgsqlDbType.Text)
                {
                    Value = model.username
                };

                param[16] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (Exception ex)
            {
                throw new DataException("Error in updateproduct", ex);
            }
        }

        // ================= DELETE =================
        public DataSet delproduct(delproductModel model)
        {
            try
            {
                string query = @"select diafrm.delete_product(@productid,@username,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[3];

                param[0] = new NpgsqlParameter("@productid", NpgsqlDbType.Integer)
                {
                    Value = model.productid
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
                throw new DataException("Error in delproduct", ex);
            }
        }

        // ================= CATEGORY =================
        public DataSet getbycatid(getproductbycatModel model)
        {
            try
            {
                string query = @"select diafrm.get_product_by_categoryid(@categoryid,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@categoryid", NpgsqlDbType.Integer)
                {
                    Value = model.categoryid
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
                throw new DataException("Error in getbycatid", ex);
            }
        }

        // ================= CAT + SUBCAT =================
        public DataSet getbycatandsubcat(getproductbycatsubModel model)
        {
            try
            {
                string query = @"select diafrm.get_product_by_cat_subcat(@categoryid,@subcategoryid,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[3];

                param[0] = new NpgsqlParameter("@categoryid", NpgsqlDbType.Integer)
                {
                    Value = model.categoryid
                };

                param[1] = new NpgsqlParameter("@subcategoryid", NpgsqlDbType.Integer)
                {
                    Value = model.subcategoryid
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
                throw new DataException("Error in getbycatandsubcat", ex);
            }
        }

        // ================= IMAGE =================
        public DataSet getImagebyId(imageviewModel model)
        {
            try
            {
                string query = @"select public.get_Imagebyid(@Productid,@username,@ref1,@ref2);";

                NpgsqlParameter[] param = new NpgsqlParameter[4];

                param[0] = new NpgsqlParameter("@Productid", NpgsqlDbType.Text)
                {
                    Value = model.Productid
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