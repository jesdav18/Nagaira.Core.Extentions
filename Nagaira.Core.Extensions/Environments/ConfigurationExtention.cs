using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nagaira.Core.Extentions.Environments
{
    public static class ConfigurationExtention
    {
        static Dictionary<string, string> _listaVariables = new Dictionary<string, string>();

        static ConfigurationExtention()
        {
            var variables = Environment.GetEnvironmentVariables();
            foreach (string key in variables.Keys)
            {
                if (!key.StartsWith("NA")) continue;
                _listaVariables.Add(key, variables[key].ToString());
            }
        }

        public static string ReplaceEnvironmentVariables(this string texto)
        {
            StringBuilder builder = new StringBuilder(texto);
            foreach (var item in _listaVariables)
            {
                builder.Replace("{{" + item.Key + "}}", item.Value);
            }
            return builder.ToString();
        }
        public static string GetConnectionStringFromENV(this IConfiguration configuration, string variableName)
        {
            return configuration.GetConnectionString(variableName).ReplaceEnvironmentVariables();
        }
        public static string GetConnectionStringFromENV(this ConfigurationManager configuration, string variableName)
        {
            return configuration.GetConnectionString(variableName).ReplaceEnvironmentVariables();
        }

        public static string GetFromEnvironment(this IConfiguration configuration, string variableName)
        {
            return configuration[variableName].ReplaceEnvironmentVariables();
        }

        public static string GetFromEnvironment(this ConfigurationManager configuration, string variableName)
        {
            return configuration[variableName].ReplaceEnvironmentVariables();
        }

        public static bool GetBoolFromEnvironment(this IConfiguration configuration, string variableName)
        {
            return bool.Parse(configuration.GetFromEnvironment(variableName));
        }
        public static bool GetBoolFromEnvironment(this ConfigurationManager configuration, string variableName)
        {
            return bool.Parse(configuration.GetFromEnvironment(variableName));
        }
    }
}
