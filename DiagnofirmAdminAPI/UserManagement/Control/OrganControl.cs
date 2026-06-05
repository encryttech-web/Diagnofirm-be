using Npgsql;
using NpgsqlTypes;
using System.Data;
using DiagnofirmAdmin.Handler;
using DiagnofirmAdmin.Model;

namespace DiagnofirmAdmin.Control
{
    public class OrganControl
    {
        private readonly DALHandler dalhandler;

        public OrganControl()
        {
            dalhandler = new DALHandler();
        }

        // ================= GET =================
        public DataSet getorgan(organModel model)
        {
            string query = @"select diafrm.get_organ(@ref1);";

            NpgsqlParameter[] param = new NpgsqlParameter[1];

            param[0] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
            {
                Value = "ref1"
            };

            return dalhandler.getConnectionObject()
                .ExecuteMultipleSelectQuery(query, param);
        }

        // ================= GET BY ID =================
        public DataSet getorganbyId(getorganModel model)
        {
            string query = @"select diafrm.get_organbyid(@organid,@ref1);";

            NpgsqlParameter[] param = new NpgsqlParameter[2];

            param[0] = new NpgsqlParameter("@organid", NpgsqlDbType.Text)
            {
                Value = model.organid
            };

            param[1] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
            {
                Value = "ref1"
            };

            return dalhandler.getConnectionObject()
                .ExecuteMultipleSelectQuery(query, param);
        }

        // ================= ADD =================
        public DataSet addorgan(addorganModel model)
        {
            string query = @"select diafrm.add_organ(
                @categoryid,
                @subcategoryid,
                @organcode,
                @organname,
                @organdescription,
                @organorder,
                @createdby,
                @status,
                @ref1);";

            NpgsqlParameter[] param = new NpgsqlParameter[9];

            param[0] = new NpgsqlParameter("@categoryid", NpgsqlDbType.Text) { Value = model.categoryid };
            param[1] = new NpgsqlParameter("@subcategoryid", NpgsqlDbType.Text) { Value = model.subcategoryid };
            param[2] = new NpgsqlParameter("@organcode", NpgsqlDbType.Text) { Value = model.organcode };
            param[3] = new NpgsqlParameter("@organname", NpgsqlDbType.Text) { Value = model.organname };
            param[4] = new NpgsqlParameter("@organdescription", NpgsqlDbType.Text) { Value = model.organdescription };
            param[5] = new NpgsqlParameter("@organorder", NpgsqlDbType.Text) { Value = model.organorder };
            param[6] = new NpgsqlParameter("@createdby", NpgsqlDbType.Text) { Value = model.createdby };
            param[7] = new NpgsqlParameter("@status", NpgsqlDbType.Text) { Value = model.status };
            param[8] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor) { Value = "ref1" };

            return dalhandler.getConnectionObject()
                .ExecuteMultipleSelectQuery(query, param);
        }

        // ================= UPDATE =================
        public DataSet updateorgan(updateorganModel model)
        {
            string query = @"select diafrm.update_organ(
                @organid,
                @categoryid,
                @subcategoryid,
                @organcode,
                @organname,
                @organdescription,
                @organorder,
                @createdby,
                @status,
                @ref1);";

            NpgsqlParameter[] param = new NpgsqlParameter[10];

            param[0] = new NpgsqlParameter("@organid", NpgsqlDbType.Text) { Value = model.organid };
            param[1] = new NpgsqlParameter("@categoryid", NpgsqlDbType.Text) { Value = model.categoryid };
            param[2] = new NpgsqlParameter("@subcategoryid", NpgsqlDbType.Text) { Value = model.subcategoryid };
            param[3] = new NpgsqlParameter("@organcode", NpgsqlDbType.Text) { Value = model.organcode };
            param[4] = new NpgsqlParameter("@organname", NpgsqlDbType.Text) { Value = model.organname };
            param[5] = new NpgsqlParameter("@organdescription", NpgsqlDbType.Text) { Value = model.organdescription };
            param[6] = new NpgsqlParameter("@organorder", NpgsqlDbType.Text) { Value = model.organorder };
            param[7] = new NpgsqlParameter("@createdby", NpgsqlDbType.Text) { Value = model.createdby };
            param[8] = new NpgsqlParameter("@status", NpgsqlDbType.Text) { Value = model.status };
            param[9] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor) { Value = "ref1" };

            return dalhandler.getConnectionObject()
                .ExecuteMultipleSelectQuery(query, param);
        }

        // ================= DELETE =================
        public DataSet delorgan(delorganModel model)
        {
            string query = @"select diafrm.del_organ(@organid,@username,@ref1);";

            NpgsqlParameter[] param = new NpgsqlParameter[3];

            param[0] = new NpgsqlParameter("@organid", NpgsqlDbType.Text) { Value = model.organid };
            param[1] = new NpgsqlParameter("@username", NpgsqlDbType.Text) { Value = model.username };
            param[2] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor) { Value = "ref1" };

            return dalhandler.getConnectionObject()
                .ExecuteMultipleSelectQuery(query, param);
        }

        // ================= GET BY CAT + SUBCAT =================
        public DataSet getbycategoryandsubcategory(getorganbycatModel model)
        {
            string query = @"select diafrm.get_organ_by_cat_subcat(@categoryid,@subcategoryid,@ref1);";

            NpgsqlParameter[] param = new NpgsqlParameter[3];

            param[0] = new NpgsqlParameter("@categoryid", NpgsqlDbType.Text) { Value = model.categoryid };
            param[1] = new NpgsqlParameter("@subcategoryid", NpgsqlDbType.Text) { Value = model.subcategoryid };
            param[2] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor) { Value = "ref1" };

            return dalhandler.getConnectionObject()
                .ExecuteMultipleSelectQuery(query, param);
        }
    }
}