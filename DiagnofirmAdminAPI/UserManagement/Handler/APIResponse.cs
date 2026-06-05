using System.Data;

namespace DiagnofirmAdmin.Handler
{
    public static class APIResponse
    {
        public static object createResponseWithSuccess(DataSet ds)
        {
            if (ds == null)
            {
                return new { status = "failed", response = "", message = "Please contact IT for UserManagement" };
            }
            else
            {
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0].ItemArray[0].ToString() == "success")
                {
                    return new { status = "success", response = ds, message = "" };
                }
                else
                {
                    return new { status = "information", response = ds, message = "Please contact IT for UserManagement" };
                }
            }
        }

        public static object createResponse(DataSet ds)
        {
            if (ds == null)
            {
                return new { status = "failed", response = "", message = "Please contact IT for UserManagement" };
            }
            else
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return new { status = "success", response = ds, message = "" };
                }
                else
                {
                    return new { status = "information", response = ds, message = "Please contact IT for UserManagement" };
                }
            }
        }

        public static object createResponsewithdata(DataSet ds)
        {
            if (ds == null)
            {
                return new { status = "failed", response = "", message = "Please contact IT for UserManagement" };
            }
            else
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "data")
                    {
                        return new { status = "success", response = ds, message = "" };
                    }
                    else
                    {
                        return new { status = "information", response = ds, message = "Please contact IT for UserManagement" };
                    }
                }
                else
                {
                    return new { status = "information", response = ds, message = "Please contact IT for UserManagement" };
                }
            }
        }

    }
}
