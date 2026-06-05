namespace DiagnofirmAdmin.Contaxdb
{
    public class ApplicationSettings
    {
        public string JWT_Secret { get; set; }
        public string Client_URL { get; set; }
        public string UAT_URL { get; set; }
        public string APP_URL { get; set; }
        public string PathBase { get; set; }
        public string CloudClientId { get; set; }
        public string CloudClientSecret { get; set; }
        public string CryptId { get; set; }
        public string AuthMode { get; set; }
    }

    public class OidcSettings
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }

}
