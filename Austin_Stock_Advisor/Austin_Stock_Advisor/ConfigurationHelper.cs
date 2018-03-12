﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AustinStockAdvisor.Helper
{
    public static class ConfigurationHelper
    {
        #region GetConfiguration()
        public static IConfigurationRoot GetConfiguration(string path = "", string environmentName = null, bool addUserSecrets = false)
        {
            if (String.IsNullOrEmpty(path))
            {
                path = AppDomain.CurrentDomain.BaseDirectory;
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (!String.IsNullOrWhiteSpace(environmentName))
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
            }

            builder = builder.AddEnvironmentVariables();

            if (addUserSecrets)
            {
                //builder.AddUserSecrets(); // requires adding Microsoft.Extensions.Configuration.UserSecrets from NuGet.
            }

            return builder.Build();
        }
        #endregion
    }
}
