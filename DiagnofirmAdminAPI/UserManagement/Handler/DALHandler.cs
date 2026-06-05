using Microsoft.Extensions.Configuration;

namespace DiagnofirmAdmin.Handler
{
    public class DALHandler
    {
        private readonly SG.Common.Utility.EncryptDecrypt protect;
        public DALHandler()
        {
            string code = Startup.StaticConfig["ApplicationSettings:CryptId"];
            protect = new SG.Common.Utility.EncryptDecrypt(code);
        }

        internal SG.Common.DAL.Postgresql.DBConnection getConnectionObject()
        {
            string hashedConnectionString = Startup.StaticConfig["ConnectionStrings:LoginConnection"];

            //string sConnectionString = protect.SGDecryption(hashedConnectionString);
            return new SG.Common.DAL.Postgresql.DBConnection(hashedConnectionString);
        }

    }
}
