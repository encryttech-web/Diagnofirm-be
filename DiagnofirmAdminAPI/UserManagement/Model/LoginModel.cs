namespace DiagnofirmAdmin.Model
{
    public class LoginModel
    {
        public string sgid { get; set; }
    }

    public class LoginValidationModel
    {
        public string SGID { get; set; }
    }

    public class LoginLDAPValidationModel
    {
        public string SGID { get; set; }
        public string LANG { get; set; }
    }

    public class LoginModelLDAP
    {
        public string SGID { get; set; }
        public string Password { get; set; }
    }


    public class LoginUserModel
    {
        public string username { get; set; }
        public string Password { get; set; }
    }

}
