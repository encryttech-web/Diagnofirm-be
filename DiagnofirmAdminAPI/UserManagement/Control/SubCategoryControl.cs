using DiagnofirmAdmin.Handler;
using DiagnofirmAdmin.Model;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;

namespace DiagnofirmAdmin.Control
{
    public class SubCategoryControl
    {
        private readonly DALHandler dalhandler;

        public SubCategoryControl()
        {
            dalhandler = new DALHandler();
        }

        // ================= GET SUBCATEGORY =================
        public DataSet getsubcategory()
        {
            try
            {
                string query = @"select diafrm.get_all_subcategory(@ref1);";

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
        public DataSet getsubcategorybyId(getsubcategoryModel model)
        {
            try
            {
                string query = @"select diafrm.get_subcategory_by_id(@subcategoryid,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@subcategoryid", NpgsqlDbType.Integer)
                {
                    Value = model.subcategoryid
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

        // ================= ADD =================
        public DataSet addsubcategory(addsubcategoryModel model)
        {
            try
            {
                string query = @"select diafrm.add_subcategory(
                    @categoryid,
                    @subcategorycode,
                    @subcategoryname,
                    @subcategorydescription,
                    @subcategoryorder,
                    @subcategoryimage,
                    @subcategoryimagename,
                    @createdby,
                    @status,
                    @ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[10];

                param[0] = new NpgsqlParameter("@categoryid", NpgsqlDbType.Integer)
                {
                    Value = model.categoryid
                };

                param[1] = new NpgsqlParameter("@subcategorycode", NpgsqlDbType.Text)
                {
                    Value = model.subcategorycode
                };

                param[2] = new NpgsqlParameter("@subcategoryname", NpgsqlDbType.Text)
                {
                    Value = model.subcategoryname
                };

                param[3] = new NpgsqlParameter("@subcategorydescription", NpgsqlDbType.Text)
                {
                    Value = model.subcategorydescription
                };

                param[4] = new NpgsqlParameter("@subcategoryorder", NpgsqlDbType.Text)
                {
                    Value = model.subcategoryorder
                };

                param[5] = new NpgsqlParameter("@subcategoryimage", NpgsqlDbType.Text)
                {
                    Value = model.subcategoryimage
                };

                param[6] = new NpgsqlParameter("@subcategoryimagename", NpgsqlDbType.Text)
                {
                    Value = model.subcategoryimagename
                };

                param[7] = new NpgsqlParameter("@createdby", NpgsqlDbType.Text)
                {
                    Value = model.createdby
                };

                param[8] = new NpgsqlParameter("@status", NpgsqlDbType.Text)
                {
                    Value = model.status
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

        // ================= UPDATE =================
        public DataSet updatesubcategory(updatesubcategoryModel model)
        {
            try
            {
                string query = @"select diafrm.update_subcategory(
                    @subcategoryid,
                    @categoryid,
                    @subcategorycode,
                    @subcategoryname,
                    @subcategorydescription,
                    @subcategoryorder,
                    @subcategoryimage,
                    @subcategoryimagename,
                    @createdby,
                    @status,
                    @ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[11];

                param[0] = new NpgsqlParameter("@subcategoryid", NpgsqlDbType.Integer)
                {
                    Value = model.subcategoryid
                };

                param[1] = new NpgsqlParameter("@categoryid", NpgsqlDbType.Integer)
                {
                    Value = model.categoryid
                };

                param[2] = new NpgsqlParameter("@subcategorycode", NpgsqlDbType.Text)
                {
                    Value = model.subcategorycode
                };

                param[3] = new NpgsqlParameter("@subcategoryname", NpgsqlDbType.Text)
                {
                    Value = model.subcategoryname
                };

                param[4] = new NpgsqlParameter("@subcategorydescription", NpgsqlDbType.Text)
                {
                    Value = model.subcategorydescription
                };

                param[5] = new NpgsqlParameter("@subcategoryorder", NpgsqlDbType.Text)
                {
                    Value = model.subcategoryorder
                };

                param[6] = new NpgsqlParameter("@subcategoryimage", NpgsqlDbType.Text)
                {
                    Value = model.subcategoryimage
                };

                param[7] = new NpgsqlParameter("@subcategoryimagename", NpgsqlDbType.Text)
                {
                    Value = model.subcategoryimagename
                };

                param[8] = new NpgsqlParameter("@createdby", NpgsqlDbType.Text)
                {
                    Value = model.createdby
                };

                param[9] = new NpgsqlParameter("@status", NpgsqlDbType.Text)
                {
                    Value = model.status
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

        // ================= DELETE =================
        public DataSet delsubcategory(delsubcategoryModel model)
        {
            try
            {
                string query = @"select diafrm.del_subcategory(@subcategoryid,@username,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[3];

                param[0] = new NpgsqlParameter("@subcategoryid", NpgsqlDbType.Integer)
                {
                    Value = model.subcategoryid
                };

                param[1] = new NpgsqlParameter("@username", NpgsqlDbType.Text)
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
                throw new DataException("", ex);
            }
        }

        // ================= GET BY CATEGORY ID =================
        public DataSet getsubcategorybycategoryid(getsubcategorybycategoryModel model)
        {
            string query = @"select diafrm.get_subcategory_by_categoryid(@categoryid,@ref1);";

            NpgsqlParameter[] param = {
                new NpgsqlParameter("@categoryid", NpgsqlDbType.Integer)
                {
                    Value = model.categoryid
                },
                new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                }
            };

            return dalhandler.getConnectionObject()
                .ExecuteMultipleSelectQuery(query, param);
        }

        public DataSet getsubcatImagebyId(subcatimageviewModel model)
        {
            try
            {
                string query = @"select public.get_subcatImagebyid(@subcatid,@username,@ref1,@ref2);";
                NpgsqlParameter[] NpgsqlParameters = new NpgsqlParameter[4];
                NpgsqlParameters[0] = new NpgsqlParameter("@subcatid", NpgsqlDbType.Text)
                {
                    Value = model.subcatid
                };
                NpgsqlParameters[1] = new NpgsqlParameter("@username", NpgsqlDbType.Text)
                {
                    Value = model.username
                };
                NpgsqlParameters[2] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };
                NpgsqlParameters[3] = new NpgsqlParameter("@ref2", NpgsqlDbType.Refcursor)
                {
                    Value = "ref2"
                };

                return dalhandler.getConnectionObject().ExecuteMultipleSelectQuery(query, NpgsqlParameters);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= SEARCH SUBCATEGORY =================
        public DataSet SearchSubcategory(string query)
        {
            try
            {
                string sql = @"select diafrm.SearchSubcategory(@p_query, @ref1);";
                NpgsqlParameter[] param = new NpgsqlParameter[2];
                param[0] = new NpgsqlParameter("@p_query", NpgsqlDbType.Varchar)
                {
                    Value = query.Trim()
                };
                param[1] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
                {
                    Value = "ref1"
                };
                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(sql, param);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }
    }
}