using Npgsql;
using NpgsqlTypes;
using System.Data;
using DiagnofirmAdmin.Handler;
using DiagnofirmAdmin.Model;

namespace DiagnofirmAdmin.Control
{
    public class TestDirectoryControl
    {
        private readonly DALHandler dalhandler;

        public TestDirectoryControl()
        {
            dalhandler = new();
        }

        // ================= GET TEST DIRECTORY =================
        public DataSet gettestdirectory()
        {
            try
            {
                string query = @"select diafrm.get_all_testdirectory(@ref1);";

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
        public DataSet gettestdirectorybyId(gettestdirectoryModel model)
        {
            try
            {
                string query = @"select diafrm.get_testdirectory_by_id(@testdirectoryid,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@testdirectoryid", NpgsqlDbType.Integer)
                {
                    Value = model.testdirectoryid
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

        // ================= ADD =================
        public DataSet addtestdirectory(addtestdirectoryModel model)
        {
            try
            {
                string query = @"select diafrm.add_testdirectory(@industryid,@testdirectoryhead,@testdirectorycode,@testdirectoryname,@specimen,@unit,@refrange,@testdescription,@testorder,@createdby,@status,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[12];

                param[0] = new NpgsqlParameter("@industryid", NpgsqlDbType.Integer) { Value = model.industryid };
                param[1] = new NpgsqlParameter("@testdirectoryhead", NpgsqlDbType.Text) { Value = model.testdirectoryhead };
                param[2] = new NpgsqlParameter("@testdirectorycode", NpgsqlDbType.Text) { Value = model.testdirectorycode };
                param[3] = new NpgsqlParameter("@testdirectoryname", NpgsqlDbType.Text) { Value = model.testdirectoryname };
                param[4] = new NpgsqlParameter("@specimen", NpgsqlDbType.Text) { Value = model.specimen };
                param[5] = new NpgsqlParameter("@unit", NpgsqlDbType.Text) { Value = model.unit };
                param[6] = new NpgsqlParameter("@refrange", NpgsqlDbType.Text) { Value = model.refrange };
                param[7] = new NpgsqlParameter("@testdescription", NpgsqlDbType.Text) { Value = model.testdescription };
                param[8] = new NpgsqlParameter("@testorder", NpgsqlDbType.Text) { Value = model.testorder };
                param[9] = new NpgsqlParameter("@createdby", NpgsqlDbType.Text) { Value = model.createdby };
                param[10] = new NpgsqlParameter("@status", NpgsqlDbType.Text) { Value = model.status };
                param[11] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor) { Value = "ref1" };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= UPDATE =================
        public DataSet updatetestdirectory(updatetestdirectoryModel model)
        {
            try
            {
                string query = @"select diafrm.update_testdirectory(@testdirectoryid,@industryid,@testdirectoryhead,@testdirectorycode,@testdirectoryname,@specimen,@unit,@refrange,@testdescription,@testorder,@createdby,@status,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[13];

                param[0] = new NpgsqlParameter("@industryid", NpgsqlDbType.Integer) { Value = model.industryid };
                param[1] = new NpgsqlParameter("@testdirectoryid", NpgsqlDbType.Integer) { Value = model.testdirectoryid };
                param[2] = new NpgsqlParameter("@testdirectoryhead", NpgsqlDbType.Text) { Value = model.testdirectoryhead };
                param[3] = new NpgsqlParameter("@testdirectorycode", NpgsqlDbType.Text) { Value = model.testdirectorycode };
                param[4] = new NpgsqlParameter("@testdirectoryname", NpgsqlDbType.Text) { Value = model.testdirectoryname };
                param[5] = new NpgsqlParameter("@specimen", NpgsqlDbType.Text) { Value = model.specimen };
                param[6] = new NpgsqlParameter("@unit", NpgsqlDbType.Text) { Value = model.unit };
                param[7] = new NpgsqlParameter("@refrange", NpgsqlDbType.Text) { Value = model.refrange };
                param[8] = new NpgsqlParameter("@testdescription", NpgsqlDbType.Text) { Value = model.testdescription };
                param[9] = new NpgsqlParameter("@testorder", NpgsqlDbType.Text) { Value = model.testorder };
                param[10] = new NpgsqlParameter("@createdby", NpgsqlDbType.Text) { Value = model.createdby };
                param[11] = new NpgsqlParameter("@status", NpgsqlDbType.Text) { Value = model.status };
                param[12] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor) { Value = "ref1" };

                return dalhandler.getConnectionObject()
                    .ExecuteMultipleSelectQuery(query, param);
            }
            catch (System.Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= DELETE =================
        public DataSet deltestdirectory(deltestdirectoryModel model)
        {
            try
            {
                string query = @"select diafrm.delete_testdirectory(@testdirectoryid,@username,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[3];

                param[0] = new NpgsqlParameter("@testdirectoryid", NpgsqlDbType.Integer)
                {
                    Value = model.testdirectoryid
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

        // ================= GET TEST DIRECTORY =================
        public DataSet gettestdirectoryIndustry()
        {
            try
            {
                string query = @"select diafrm.get_all_testdirectory_industry(@ref1);";

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
        public DataSet gettestdirectorybyIndustryId(gettestdirectoryIndustryModel model)
        {
            try
            {
                string query = @"select diafrm.get_testdirectory_by_Industryid(@testdirectoryindustryid,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@testdirectoryindustryid", NpgsqlDbType.Integer)
                {
                    Value = model.testdirectoryindustryid
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