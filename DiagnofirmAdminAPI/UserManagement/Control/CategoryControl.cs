using Npgsql;
using NpgsqlTypes;
using System.Data;
using DiagnofirmAdmin.Handler;
using DiagnofirmAdmin.Model;

namespace DiagnofirmAdmin.Control
{
    public class CategoryControl
    {
        private readonly DALHandler dalhandler;

        public CategoryControl()
        {
            dalhandler = new();
        }

        // ================= GET CATEGORY =================
        public DataSet getcategory()
        {
            try
            {
                string query = @"select diafrm.get_all_category(@ref1);";

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
        public DataSet getcategorybyId(getcategoryModel model)
        {
            try
            {
                string query = @"select diafrm.get_category_by_id(@categoryid,@ref1);";

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
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= ADD CATEGORY =================
        public DataSet addcategory(addcategoryModel model)
        {
            try
            {
                string query = @"select diafrm.add_category(
                    @categorycode,
                    @categoryname,
                    @categoryorder,
                    @categorydescription,
                    @createdby,
                    @status,
                    @ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[7];

                param[0] = new NpgsqlParameter("@categorycode", NpgsqlDbType.Text)
                {
                    Value = model.categorycode
                };

                param[1] = new NpgsqlParameter("@categoryname", NpgsqlDbType.Text)
                {
                    Value = model.categoryname
                };

                param[2] = new NpgsqlParameter("@categoryorder", NpgsqlDbType.Text)
                {
                    Value = model.categoryorder
                };

                param[3] = new NpgsqlParameter("@categorydescription", NpgsqlDbType.Text)
                {
                    Value = model.categorydescription
                };

                param[4] = new NpgsqlParameter("@createdby", NpgsqlDbType.Text)
                {
                    Value = model.createdby
                };

                param[5] = new NpgsqlParameter("@status", NpgsqlDbType.Text)
                {
                    Value = model.status
                };

                param[6] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
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

        // ================= UPDATE CATEGORY =================
        public DataSet updatecategory(updatecategoryModel model)
        {
            try
            {
                string query = @"select diafrm.update_category(
                    @categoryid,
                    @categorycode,
                    @categoryname,
                    @categoryorder,
                    @categorydescription,
                    @createdby,
                    @status,
                    @ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[8];

                param[0] = new NpgsqlParameter("@categoryid", NpgsqlDbType.Integer)
                {
                    Value = model.categoryid
                };

                param[1] = new NpgsqlParameter("@categorycode", NpgsqlDbType.Text)
                {
                    Value = model.categorycode
                };

                param[2] = new NpgsqlParameter("@categoryname", NpgsqlDbType.Text)
                {
                    Value = model.categoryname
                };

                param[3] = new NpgsqlParameter("@categoryorder", NpgsqlDbType.Text)
                {
                    Value = model.categoryorder
                };

                param[4] = new NpgsqlParameter("@categorydescription", NpgsqlDbType.Text)
                {
                    Value = model.categorydescription
                };

                param[5] = new NpgsqlParameter("@createdby", NpgsqlDbType.Text)
                {
                    Value = model.createdby
                };

                param[6] = new NpgsqlParameter("@status", NpgsqlDbType.Text)
                {
                    Value = model.status
                };

                param[7] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
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

        // ================= DELETE CATEGORY =================
        public DataSet delcategory(delcategoryModel model)
        {
            try
            {
                string query = @"select diafrm.delete_category(@categoryid,@username,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[3];

                param[0] = new NpgsqlParameter("@categoryid", NpgsqlDbType.Integer)
                {
                    Value = model.categoryid
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
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= GET LAST CODE =================
        public DataSet getlastcode(getlastcodeModel model)
        {
            try
            {
                string query = @"select diafrm.get_last_code(@schema_name,@table_name,@column_name,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[4];

                param[0] = new NpgsqlParameter("@schema_name", NpgsqlDbType.Text)
                {
                    Value = model.schemaname
                };

                param[1] = new NpgsqlParameter("@table_name", NpgsqlDbType.Text)
                {
                    Value = model.tablename
                };

                param[2] = new NpgsqlParameter("@column_name", NpgsqlDbType.Text)
                {
                    Value = model.columnname
                };

                param[3] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
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