using DiagnofirmAdmin.Handler;
using DiagnofirmAdmin.Model;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;

namespace DiagnofirmAdmin.Control
{
    public class ContactControl
    {
        private readonly DALHandler dalhandler;

        public ContactControl()
        {
            dalhandler = new DALHandler();
        }

        // ================= GET ALL CONTACTS =================
        public DataSet getcontact()
        {
            try
            {
                string query = @"select diafrm.get_all_contact(@ref1);";

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
        public DataSet getcontactbyId(getcontactModel model)
        {
            try
            {
                string query = @"select diafrm.get_contact_by_id(@contactid,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@contactid", NpgsqlDbType.Integer)
                {
                    Value = model.contactid
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
        public DataSet addcontact(addcontactModel model)
        {
            try
            {
                string query = @"select diafrm.add_contact(
                    @conttype,
                    @contname,
                    @contaddress,
                    @contcity,
                    @contstate,
                    @contcountry,
                    @contphno,
                    @contaltphno,
                    @contwrkhrs1,
                    @contwrkhrs2,
                    @contwrkhrs3,
                    @contemail,
                    @contdircts,
                    @contdesc,
                    @contord,
                    @createdby,
                    @status,
                    @ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[18];

                param[0] = new NpgsqlParameter("@conttype", NpgsqlDbType.Varchar)
                {
                    Value = model.conttype
                };

                param[1] = new NpgsqlParameter("@contname", NpgsqlDbType.Varchar)
                {
                    Value = model.contname
                };

                param[2] = new NpgsqlParameter("@contaddress", NpgsqlDbType.Varchar)
                {
                    Value = model.contaddress
                };

                param[3] = new NpgsqlParameter("@contcity", NpgsqlDbType.Varchar)
                {
                    Value = model.contcity
                };

                param[4] = new NpgsqlParameter("@contstate", NpgsqlDbType.Varchar)
                {
                    Value = model.contstate
                };

                param[5] = new NpgsqlParameter("@contcountry", NpgsqlDbType.Varchar)
                {
                    Value = model.contcountry
                };

                param[6] = new NpgsqlParameter("@contphno", NpgsqlDbType.Varchar)
                {
                    Value = model.contphno
                };

                param[7] = new NpgsqlParameter("@contaltphno", NpgsqlDbType.Varchar)
                {
                    Value = model.contaltphno
                };

                param[8] = new NpgsqlParameter("@contwrkhrs1", NpgsqlDbType.Varchar)
                {
                    Value = model.contwrkhrs1
                };

                param[9] = new NpgsqlParameter("@contwrkhrs2", NpgsqlDbType.Varchar)
                {
                    Value = model.contwrkhrs2
                };

                param[10] = new NpgsqlParameter("@contwrkhrs3", NpgsqlDbType.Varchar)
                {
                    Value = model.contwrkhrs3
                };

                param[11] = new NpgsqlParameter("@contemail", NpgsqlDbType.Varchar)
                {
                    Value = model.contemail
                };

                param[12] = new NpgsqlParameter("@contdircts", NpgsqlDbType.Text)
                {
                    Value = model.contdircts
                };

                param[13] = new NpgsqlParameter("@contdesc", NpgsqlDbType.Varchar)
                {
                    Value = model.contdesc
                };

                param[14] = new NpgsqlParameter("@contord", NpgsqlDbType.Varchar)
                {
                    Value = model.contord
                };

                param[15] = new NpgsqlParameter("@createdby", NpgsqlDbType.Varchar)
                {
                    Value = model.createdby
                };

                param[16] = new NpgsqlParameter("@status", NpgsqlDbType.Varchar)
                {
                    Value = model.status
                };

                param[17] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
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
        public DataSet updatecontact(updatecontactModel model)
        {
            try
            {
                string query = @"select diafrm.update_contact(
                    @contactid,
                    @conttype,
                    @contname,
                    @contaddress,
                    @contcity,
                    @contstate,
                    @contcountry,
                    @contphno,
                    @contaltphno,
                    @contwrkhrs1,
                    @contwrkhrs2,
                    @contwrkhrs3,
                    @contemail,
                    @contdircts,
                    @contdesc,
                    @contord,
                    @createdby,
                    @status,
                    @ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[19];

                param[0] = new NpgsqlParameter("@contactid", NpgsqlDbType.Integer)
                {
                    Value = model.contactid
                };

                param[1] = new NpgsqlParameter("@conttype", NpgsqlDbType.Varchar)
                {
                    Value = model.conttype
                };

                param[2] = new NpgsqlParameter("@contname", NpgsqlDbType.Varchar)
                {
                    Value = model.contname
                };

                param[3] = new NpgsqlParameter("@contaddress", NpgsqlDbType.Varchar)
                {
                    Value = model.contaddress
                };

                param[4] = new NpgsqlParameter("@contcity", NpgsqlDbType.Varchar)
                {
                    Value = model.contcity
                };

                param[5] = new NpgsqlParameter("@contstate", NpgsqlDbType.Varchar)
                {
                    Value = model.contstate
                };

                param[6] = new NpgsqlParameter("@contcountry", NpgsqlDbType.Varchar)
                {
                    Value = model.contcountry
                };

                param[7] = new NpgsqlParameter("@contphno", NpgsqlDbType.Varchar)
                {
                    Value = model.contphno
                };

                param[8] = new NpgsqlParameter("@contaltphno", NpgsqlDbType.Varchar)
                {
                    Value = model.contaltphno
                };

                param[9] = new NpgsqlParameter("@contwrkhrs1", NpgsqlDbType.Varchar)
                {
                    Value = model.contwrkhrs1
                };

                param[10] = new NpgsqlParameter("@contwrkhrs2", NpgsqlDbType.Varchar)
                {
                    Value = model.contwrkhrs2
                };

                param[11] = new NpgsqlParameter("@contwrkhrs3", NpgsqlDbType.Varchar)
                {
                    Value = model.contwrkhrs3
                };

                param[12] = new NpgsqlParameter("@contemail", NpgsqlDbType.Varchar)
                {
                    Value = model.contemail
                };

                param[13] = new NpgsqlParameter("@contdircts", NpgsqlDbType.Text)
                {
                    Value = model.contdircts
                };

                param[14] = new NpgsqlParameter("@contdesc", NpgsqlDbType.Varchar)
                {
                    Value = model.contdesc
                };

                param[15] = new NpgsqlParameter("@contord", NpgsqlDbType.Varchar)
                {
                    Value = model.contord
                };

                param[16] = new NpgsqlParameter("@createdby", NpgsqlDbType.Varchar)
                {
                    Value = model.createdby
                };

                param[17] = new NpgsqlParameter("@status", NpgsqlDbType.Varchar)
                {
                    Value = model.status
                };

                param[18] = new NpgsqlParameter("@ref1", NpgsqlDbType.Refcursor)
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
        public DataSet delcontact(delcontactModel model)
        {
            try
            {
                string query = @"select diafrm.delete_contact(@contactid,@username,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[3];

                param[0] = new NpgsqlParameter("@contactid", NpgsqlDbType.Integer)
                {
                    Value = model.contactid
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
                throw new DataException("", ex);
            }
        }

        // ================= SEARCH CONTACT =================
        public DataSet SearchContact(string query)
        {
            try
            {
                string sql = @"select diafrm.SearchContact(@p_query, @ref1);";

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
            catch (Exception ex)
            {
                throw new DataException("", ex);
            }
        }

        // ================= GET ALL CONTACTS =================
        public DataSet getcontacttype()
        {
            try
            {
                string query = @"select diafrm.get_all_contacttype(@ref1);";

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
        public DataSet getcontacttypebyId(getcontacttypeModel model)
        {
            try
            {
                string query = @"select diafrm.get_contacttype_by_id(@ctid,@ref1);";

                NpgsqlParameter[] param = new NpgsqlParameter[2];

                param[0] = new NpgsqlParameter("@ctid", NpgsqlDbType.Integer)
                {
                    Value = model.ctid
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
    }
}