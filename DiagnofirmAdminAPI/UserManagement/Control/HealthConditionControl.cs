using DiagnofirmAdmin.Handler;
using DiagnofirmAdmin.Model;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;

namespace DiagnofirmAdmin.Control
{
    public class HealthConditionControl
    {
        private readonly DALHandler dalhandler;

        public HealthConditionControl()
        {
            dalhandler = new DALHandler();
        }

        // ================= GET ALL =================
        public DataSet gethealthcondition(healthconditionModel model)
        {
            try
            {
                string query = @"select diafrm.get_healthcondition(@ref1);";

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
        public DataSet gethealthconditionbyId(gethealthconditionModel model)
        {
            try
            {
                string query = @"select diafrm.get_healthconditionbyid(@healthconditionid,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@healthconditionid", NpgsqlDbType.Text)
                {
                    Value = model.healthconditionid
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
        public DataSet addhealthcondition(addhealthconditionModel model)
        {
            try
            {
                string query = @"select diafrm.add_healthcondition(
                    @categoryid,
                    @subcategoryid,
                    @healthconditioncode,
                    @healthconditionname,
                    @healthconditiondescription,
                    @healthconditionorder,
                    @createdby,
                    @status,
                    @ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[9];

                param[0] = new NpgsqlParameter("@categoryid", NpgsqlDbType.Text)
                {
                    Value = model.categoryid
                };

                param[1] = new NpgsqlParameter("@subcategoryid", NpgsqlDbType.Text)
                {
                    Value = model.subcategoryid
                };

                param[2] = new NpgsqlParameter("@healthconditioncode", NpgsqlDbType.Text)
                {
                    Value = model.healthconditioncode
                };

                param[3] = new NpgsqlParameter("@healthconditionname", NpgsqlDbType.Text)
                {
                    Value = model.healthconditionname
                };

                param[4] = new NpgsqlParameter("@healthconditiondescription", NpgsqlDbType.Text)
                {
                    Value = model.healthconditiondescription
                };

                param[5] = new NpgsqlParameter("@healthconditionorder", NpgsqlDbType.Text)
                {
                    Value = model.healthconditionorder
                };

                param[6] = new NpgsqlParameter("@createdby", NpgsqlDbType.Text)
                {
                    Value = model.createdby
                };

                param[7] = new NpgsqlParameter("@status", NpgsqlDbType.Text)
                {
                    Value = model.status
                };

                param[8] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
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
        public DataSet updatehealthcondition(updatehealthconditionModel model)
        {
            try
            {
                string query = @"select diafrm.update_healthcondition(
                    @healthconditionid,
                    @categoryid,
                    @subcategoryid,
                    @healthconditioncode,
                    @healthconditionname,
                    @healthconditiondescription,
                    @healthconditionorder,
                    @createdby,
                    @status,
                    @ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[10];

                param[0] = new NpgsqlParameter("@healthconditionid", NpgsqlDbType.Text)
                {
                    Value = model.healthconditionid
                };

                param[1] = new NpgsqlParameter("@categoryid", NpgsqlDbType.Text)
                {
                    Value = model.categoryid
                };

                param[2] = new NpgsqlParameter("@subcategoryid", NpgsqlDbType.Text)
                {
                    Value = model.subcategoryid
                };

                param[3] = new NpgsqlParameter("@healthconditioncode", NpgsqlDbType.Text)
                {
                    Value = model.healthconditioncode
                };

                param[4] = new NpgsqlParameter("@healthconditionname", NpgsqlDbType.Text)
                {
                    Value = model.healthconditionname
                };

                param[5] = new NpgsqlParameter("@healthconditiondescription", NpgsqlDbType.Text)
                {
                    Value = model.healthconditiondescription
                };

                param[6] = new NpgsqlParameter("@healthconditionorder", NpgsqlDbType.Text)
                {
                    Value = model.healthconditionorder
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

        // ================= DELETE =================
        public DataSet delhealthcondition(delhealthconditionModel model)
        {
            try
            {
                string query = @"select diafrm.del_healthcondition(@healthconditionid,@username,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[3];

                param[0] = new NpgsqlParameter("@healthconditionid", NpgsqlDbType.Text)
                {
                    Value = model.healthconditionid
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

        // ================= GET BY CAT + SUBCAT =================
        public DataSet getbycategoryandsubcategory(gethcbycatModel model)
        {
            try
            {
                string query = @"select diafrm.get_healthcondition_by_cat_subcat(@categoryid,@subcategoryid,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[3];

                param[0] = new NpgsqlParameter("@categoryid", NpgsqlDbType.Text)
                {
                    Value = model.categoryid
                };

                param[1] = new NpgsqlParameter("@subcategoryid", NpgsqlDbType.Text)
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
                throw new DataException("", ex);
            }
        }
    }
}