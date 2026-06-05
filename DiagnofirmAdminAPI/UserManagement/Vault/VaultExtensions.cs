using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;

namespace DiagnofirmAdmin.Vault
{
    public static class VaultExtensions
    {
        public static IConfigurationBuilder AddVault(this IConfigurationBuilder configuration, Action<VaultOptions> options, Action<IDictionary<string, string>, VaultResponse> updateConfig)
        {
            var vaultOptions = new VaultConfigurationSource(options, updateConfig);
            configuration.Add(vaultOptions);
            return configuration;
        }
    }
}
