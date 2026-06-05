using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using VaultSharp;
using Newtonsoft.Json;
using System.Threading.Tasks;
using VaultSharp.V1.AuthMethods.AppRole;
using VaultSharp.V1.Commons;

namespace DiagnofirmAdmin.Vault
{
    public class VaultConfigurationProvider : ConfigurationProvider
    {
        private readonly VaultOptions _config;
        private readonly IVaultClient _client;
        private readonly Action<IDictionary<string, string>, VaultResponse> _updateConfiguration;

        public VaultConfigurationProvider(VaultOptions config, Action<IDictionary<string, string>, VaultResponse> myUpdateConfiguration)
        {
            _config = config;
            _updateConfiguration = myUpdateConfiguration;

            Console.WriteLine("Service Timeout set to " + _config.Timeout + " minute(s)");

            var vaultClientSettings = new VaultClientSettings(_config.Address, new AppRoleAuthMethodInfo(_config.Role, _config.Secret))
            {
                VaultServiceTimeout = new TimeSpan(0, Convert.ToInt32(_config.Timeout), 0)
            };

            _client = new VaultClient(vaultClientSettings);
        }

        public override void Load()
        {
            LoadAsync(_updateConfiguration).Wait();
        }

        public async Task LoadAsync(Action<IDictionary<string, string>, VaultResponse> updateConfiguration)
        {
            await GetFromVault(updateConfiguration);
        }

        public async Task GetFromVault(Action<IDictionary<string, string>, VaultResponse> myUpdateAction)
        {
            try
            {
                VaultResponse vaultValues;

                Console.WriteLine("MountPath: " + _config.MountPath);

                Console.WriteLine("Config Version: " + _config.Version);
                Secret<Dictionary<string, object>> secrets = await _client.V1.Secrets.KeyValue.V1.ReadSecretAsync(_config.MountPath);
                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                if (secrets.Data != null)
                {
                    foreach (var item in secrets.Data)
                    {
                        keyValuePairs.Add(item.Key, item.Value.ToString());
                    }
                }

                vaultValues = JsonConvert.DeserializeObject<VaultResponse>(JsonConvert.SerializeObject(keyValuePairs, Newtonsoft.Json.Formatting.Indented));
                myUpdateAction(this.Data, vaultValues);
            }
            catch (Exception ex)
            {
                throw ex;
            }


            //if (secrets.Data != null)
            //{
            //    foreach (var item in secrets.Data)
            //    {   
            //        Data[item.Key] = item.Value.ToString();
            //    }
            //}

            //myUpdateAction(this.Data, vaultValues);
        }
    }

    public class VaultConfigurationSource : IConfigurationSource
    {
        private readonly VaultOptions _config;
        private readonly Action<IDictionary<string, string>, VaultResponse> _updateConfig;

        public VaultConfigurationSource(Action<VaultOptions> config, Action<IDictionary<string, string>, VaultResponse> updateConfig)
        {
            _updateConfig = updateConfig;
            _config = new VaultOptions();
            config.Invoke(_config);
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new VaultConfigurationProvider(_config, _updateConfig);
        }
    }
}
