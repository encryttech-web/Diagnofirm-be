using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Specialized;
using Serilog.Events;
using System.Threading;
using DiagnofirmAdmin.Vault;
using System.IO;

namespace DiagnofirmAdmin
{
    public static class Program
    {
        public static void Main(string[] args)
        {

            ThreadPool.SetMaxThreads(32_767, 2_000);
            ThreadPool.SetMinThreads(1_000, 1_000);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                AddVaultConfigForAdmin(config, args);
                AddVaultConfigForWarehouse(config, args);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(serverOptions =>
                {
                    // Set properties and call methods on options
                })
                .UseStartup<Startup>();
            }).UseSerilog();

        private static void AddVaultConfigForAdmin(IConfigurationBuilder config, string[] args)
        {
            IConfiguration globalConfig = new ConfigurationBuilder()
               .AddEnvironmentVariables(prefix: "VAULT_")
               .AddCommandLine(args)
               .Build();

            //Get it from HashiCrop vault
            if (globalConfig.GetSection("ADDRESS").Exists() && globalConfig.GetSection("SECRET_ID").Exists())
            {
                config.AddVault(options =>
                {
                    options.Address = globalConfig.GetSection("ADDRESS").Value;
                    options.MountPath = $"{globalConfig.GetSection("MOUNT_PATH").Value}/{globalConfig.GetSection("PLATFORM").Value}/{globalConfig.GetSection("PLANT_ID").Value}/{globalConfig.GetSection("MOUNTPATH_APIPREFIX").Value}";
                    options.SecretType = globalConfig.GetSection("SECRET_TYPE").Value;
                    options.Role = globalConfig.GetSection("ROLE_ID").Value;
                    options.Secret = globalConfig.GetSection("SECRET_ID").Value;
                    options.Timeout = globalConfig.GetSection("TIMEOUT").Value ?? "1";
                    options.Version = globalConfig.GetSection("VERSION").Value ?? "2";

                },
                (data, vaultValues) =>
                {
                    data.Add("ElasticConfiguration:Token", vaultValues.EToken);
                    data.Add("ElasticConfiguration:Uri", vaultValues.EURL);
                    
                    data.Add("ConnectionStrings:LoginConnection", vaultValues.LoginConnection);
                    data.Add("ConnectionStrings:J340", vaultValues.J340);
                    data.Add("ConnectionStrings:I1B1", vaultValues.I1B1);
                    data.Add("ConnectionStrings:J310", vaultValues.J310);
                    data.Add("ConnectionStrings:J320", vaultValues.J320);
                    data.Add("ConnectionStrings:J330", vaultValues.J330);
                    data.Add("ConnectionStrings:G113", vaultValues.G113);
                    data.Add("ConnectionStrings:J701", vaultValues.J701);
                    data.Add("ConnectionStrings:B101", vaultValues.B101);

                    data.Add("AllowedHosts", "*");
                    data.Add("ApplicationSettings:JWT_Secret", vaultValues.JWT_Secret);
                    data.Add("ApplicationSettings:Client_URL", vaultValues.Client_URL);
                    data.Add("ApplicationSettings:ADMIN_URL", vaultValues.ADMIN_URL);
                    data.Add("ApplicationSettings:APP_URL", vaultValues.APP_URL);
                    data.Add("ApplicationSettings:AuthMode", vaultValues.AuthMode);
                    data.Add("ApplicationSettings:Version", vaultValues.Version);
                    data.Add("ApplicationSettings:FTPAddress", vaultValues.FTPAddress);
                    data.Add("ApplicationSettings:FTPUser", vaultValues.FTPUser);
                    data.Add("ApplicationSettings:FPass", vaultValues.FPass);
                    data.Add("ApplicationSettings:CryptId", vaultValues.CryptId);
                    data.Add("ApplicationSettings:LogMethod", vaultValues.LogMethod);
                    data.Add("ApplicationSettings:Env", vaultValues.Env);
                    data.Add("ApplicationSettings:DOMAINLIST", vaultValues.DOMAINLIST);
                    data.Add("ApplicationSettings:FR_PROXY", vaultValues.FR_PROXY);
                    data.Add("ApplicationSettings:HandlingUnits_EndPoint", vaultValues.HandlingUnits_EndPoint);
                    data.Add("ApplicationSettings:InsertMaterialMovement_EndPoint", vaultValues.InsertMaterialMovement_EndPoint);
                    data.Add("ApplicationSettings:CloudClientId", vaultValues.CloudClientId);
                    data.Add("ApplicationSettings:CloudClientSecret", vaultValues.CloudClientSecret);

                    data.Add("Oidc:Authority", vaultValues.Authority);
                    data.Add("Oidc:ClientId", vaultValues.ClientId);
                    data.Add("Oidc:ClientSecret", vaultValues.ClientSecret);
                });
            }
        }

        private static void AddVaultConfigForWarehouse(IConfigurationBuilder config, string[] args)
        {
            IConfiguration globalConfig = new ConfigurationBuilder()
               .AddEnvironmentVariables(prefix: "VAULT_")
               .AddCommandLine(args)
               .Build();

            //Get it from HashiCrop vault
            if (globalConfig.GetSection("ADDRESS").Exists() && globalConfig.GetSection("SECRET_ID").Exists())
            {

                config.AddVault(options =>
                {
                    options.Address = globalConfig.GetSection("ADDRESS").Value;
                    options.MountPath = $"{globalConfig.GetSection("MOUNT_PATH").Value}/{globalConfig.GetSection("PLATFORM").Value}/{globalConfig.GetSection("PLANT_ID").Value}/{globalConfig.GetSection("MOUNTPATH_APIPREFIX").Value}/{globalConfig.GetSection("MOUNTPATH_APISUFFIX").Value}";
                    options.SecretType = globalConfig.GetSection("SECRET_TYPE").Value;
                    options.Role = globalConfig.GetSection("ROLE_ID").Value;
                    options.Secret = globalConfig.GetSection("SECRET_ID").Value;
                    options.Timeout = globalConfig.GetSection("TIMEOUT").Value ?? "1";
                    options.Version = globalConfig.GetSection("VERSION").Value ?? "2";
                },
                (data, vaultValues) =>
                {
                    data.Add("ApplicationSettings:UAT_URL", vaultValues.UAT_URL);
                    data.Add("ApplicationSettings:PathBase", vaultValues.PathBase);
                    data.Add("ApplicationSettings:EUser", vaultValues.EUser);
                    data.Add("ApplicationSettings:EPass", vaultValues.EPass);
                    data.Add("ApplicationSettings:PrintTopic", vaultValues.PrintTopic);
                    data.Add("ApplicationSettings:UAT_GYPSUM_ENDPOINT", vaultValues.UAT_GYPSUM_ENDPOINT);
                    data.Add("ApplicationSettings:UAT_GYPSUM_USERID", vaultValues.UAT_GYPSUM_USERID);
                    data.Add("ApplicationSettings:UAT_GYPSUM_PASSWORD", vaultValues.UAT_GYPSUM_PASSWORD);
                    data.Add("Producers:Host", vaultValues.Host);
                    data.Add("Producers:Topic", vaultValues.Topic);
                    data.Add("Producers:Username", vaultValues.Username);
                    data.Add("Producers:Password", vaultValues.Password);
                    data.Add("Producers:SaslMechanism", vaultValues.SaslMechanism);
                    data.Add("Producers:EnableSslCertificateVerification", vaultValues.EnableSslCertificateVerification);
                    data.Add("Producers:ProtocolType", vaultValues.ProtocolType);
                });
            }
        }
    }
}
