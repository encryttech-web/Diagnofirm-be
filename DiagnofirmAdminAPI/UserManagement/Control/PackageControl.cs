using DiagnofirmAdmin.Handler;
using DiagnofirmAdmin.Model;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;

namespace DiagnofirmAdmin.Control
{
    public class PackageControl
    {
        private readonly DALHandler dalhandler;

        public PackageControl()
        {
            dalhandler = new DALHandler();
        }

        // ================= GET ALL =================
        public DataSet getpackage()
        {
            try
            {
                string query = @"select diafrm.get_all_package(@ref1);";

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
        public DataSet getpackagebyid(getpackagebyidModel model)
        {
            try
            {
                string query = @"select diafrm.get_package_by_id(@packageid,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@packageid", NpgsqlDbType.Integer)
                {
                    Value = model.packageid
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
        public DataSet addpackage(addpackageModel model)
        {
            try
            {
                string query = @"select diafrm.add_package(
                    @packagehead,
                    @packagecode,
                    @packagename,
                    @packagesampletype,
                    @packagegender,
                    @packageprice,
                    @packagetestparam,
                    @packageord,
                    @packagedesc,
                    @packagefacts,
                    @packageimage,
                    @packageimagename,
                    @username,
                    @status,
                    @ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[15];

                param[0] = new NpgsqlParameter("@packagehead", NpgsqlDbType.Text)
                {
                    Value = model.packagehead
                };

                param[1] = new NpgsqlParameter("@packagecode", NpgsqlDbType.Text)
                {
                    Value = model.packagecode
                };

                param[2] = new NpgsqlParameter("@packagename", NpgsqlDbType.Text)
                {
                    Value = model.packagename
                };

                param[3] = new NpgsqlParameter("@packagesampletype", NpgsqlDbType.Text)
                {
                    Value = model.packagesampletype
                };

                param[4] = new NpgsqlParameter("@packagegender", NpgsqlDbType.Text)
                {
                    Value = model.packagegender
                };

                param[5] = new NpgsqlParameter("@packageprice", NpgsqlDbType.Numeric)
                {
                    Value = model.packageprice
                };

                param[6] = new NpgsqlParameter("@packagetestparam", NpgsqlDbType.Text)
                {
                    Value = model.packagetestparam
                };

                param[7] = new NpgsqlParameter("@packageord", NpgsqlDbType.Text)
                {
                    Value = model.packageord
                };

                param[8] = new NpgsqlParameter("@packagedesc", NpgsqlDbType.Text)
                {
                    Value = model.packagedesc
                };

                param[9] = new NpgsqlParameter("@packagefacts", NpgsqlDbType.Text)
                {
                    Value = model.packagefacts
                };

                param[10] = new NpgsqlParameter("@packageimage", NpgsqlDbType.Text)
                {
                    Value = model.packageimage
                };

                param[11] = new NpgsqlParameter("@packageimagename", NpgsqlDbType.Text)
                {
                    Value = model.packageimagename
                };

                param[12] = new NpgsqlParameter("@username", NpgsqlDbType.Text)
                {
                    Value = model.username
                };

                param[13] = new NpgsqlParameter("@status", NpgsqlDbType.Text)
                {
                    Value = model.status
                };

                param[14] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
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
        public DataSet updatepackage(updatepackageModel model)
        {
            try
            {
                string query = @"select diafrm.update_package(
                    @packageid,
                    @packagehead,
                    @packagecode,
                    @packagename,
                    @packagesampletype,
                    @packagegender,
                    @packageprice,
                    @packagetestparam,
                    @packageord,
                    @packagedesc,
                    @packagefacts,
                    @packageimage,
                    @packageimagename,
                    @username,
                    @status,
                    @ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[16];

                param[0] = new NpgsqlParameter("@packageid", NpgsqlDbType.Integer)
                {
                    Value = model.packageid
                };

                param[1] = new NpgsqlParameter("@packagehead", NpgsqlDbType.Text)
                {
                    Value = model.packagehead
                };

                param[2] = new NpgsqlParameter("@packagecode", NpgsqlDbType.Text)
                {
                    Value = model.packagecode
                };

                param[3] = new NpgsqlParameter("@packagename", NpgsqlDbType.Text)
                {
                    Value = model.packagename
                };

                param[4] = new NpgsqlParameter("@packagesampletype", NpgsqlDbType.Text)
                {
                    Value = model.packagesampletype
                };

                param[5] = new NpgsqlParameter("@packagegender", NpgsqlDbType.Text)
                {
                    Value = model.packagegender
                };

                param[6] = new NpgsqlParameter("@packageprice", NpgsqlDbType.Numeric)
                {
                    Value = model.packageprice
                };

                param[7] = new NpgsqlParameter("@packagetestparam", NpgsqlDbType.Text)
                {
                    Value = model.packagetestparam
                };

                param[8] = new NpgsqlParameter("@packageord", NpgsqlDbType.Text)
                {
                    Value = model.packageord
                };

                param[9] = new NpgsqlParameter("@packagedesc", NpgsqlDbType.Text)
                {
                    Value = model.packagedesc
                };

                param[10] = new NpgsqlParameter("@packagefacts", NpgsqlDbType.Text)
                {
                    Value = model.packagefacts
                };

                param[11] = new NpgsqlParameter("@packageimage", NpgsqlDbType.Text)
                {
                    Value = model.packageimage
                };

                param[12] = new NpgsqlParameter("@packageimagename", NpgsqlDbType.Text)
                {
                    Value = model.packageimagename
                };

                param[13] = new NpgsqlParameter("@username", NpgsqlDbType.Text)
                {
                    Value = model.username
                };

                param[14] = new NpgsqlParameter("@status", NpgsqlDbType.Text)
                {
                    Value = model.status
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
                throw new DataException("", ex);
            }
        }

        // ================= DELETE =================
        public DataSet deletepackage(deletepackageModel model)
        {
            try
            {
                string query = @"select diafrm.delete_package(@packageid,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@packageid", NpgsqlDbType.Integer)
                {
                    Value = model.packageid
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

        // ================= IMAGE =================
        public DataSet getpackageImagebyId(packageimageviewModel model)
        {
            try
            {
                string query = @"select public.get_packageImagebyid(@Packageid,@username,@ref1,@ref2);";

                NpgsqlParameter[] param = new NpgsqlParameter[4];

                param[0] = new NpgsqlParameter("@Packageid", NpgsqlDbType.Text)
                {
                    Value = model.Packageid
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

        // ================= SEARCH PACKAGES =================
        public DataSet SearchPackages(string query)
        {
            try
            {
                string sql = @"select diafrm.SearchPackages(@p_query, @ref1);";
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