using System.Collections.Generic;
using System.Linq;

namespace Nagaira.Core.Extentions.Dictionaries
{
    public static class CountryCurrencyAlphabet
    {
        public static string GetCurrencyAlphabet(string currencyId, bool isSingular)
        {
            if (string.IsNullOrEmpty(currencyId)) return string.Empty;

            var configurations = GetCurrencyConfigurations();
            var currencyAlphabet = configurations.FirstOrDefault(x => x.CurrencyId == currencyId && x.IsSingular == isSingular)?.CurrencyAlphabet;

            return currencyAlphabet!;
        }

        private static List<CurrencyConfiguration> GetCurrencyConfigurations()
        {
            List<CurrencyConfiguration> currencyConfigurations = new List<CurrencyConfiguration>();

            currencyConfigurations.Add(new CurrencyConfiguration { CurrencyId = "HNL", CurrencyAlphabet = "lempiras", IsSingular = false });
            currencyConfigurations.Add(new CurrencyConfiguration { CurrencyId = "HNL", CurrencyAlphabet = "lempira", IsSingular = true });
            currencyConfigurations.Add(new CurrencyConfiguration { CurrencyId = "USD", CurrencyAlphabet = "dólares", IsSingular = false });
            currencyConfigurations.Add(new CurrencyConfiguration { CurrencyId = "USD", CurrencyAlphabet = "dólar", IsSingular = true });
            currencyConfigurations.Add(new CurrencyConfiguration { CurrencyId = "EUR", CurrencyAlphabet = "euros", IsSingular = false });
            currencyConfigurations.Add(new CurrencyConfiguration { CurrencyId = "EUR", CurrencyAlphabet = "euro", IsSingular = true });
            currencyConfigurations.Add(new CurrencyConfiguration { CurrencyId = "JPY", CurrencyAlphabet = "yenes", IsSingular = false });
            currencyConfigurations.Add(new CurrencyConfiguration { CurrencyId = "JPY", CurrencyAlphabet = "yen", IsSingular = true });
            currencyConfigurations.Add(new CurrencyConfiguration { CurrencyId = "GTQ", CurrencyAlphabet = "quetzales", IsSingular = false });
            currencyConfigurations.Add(new CurrencyConfiguration { CurrencyId = "GTQ", CurrencyAlphabet = "quetzal", IsSingular = true });


            return currencyConfigurations;
        }
    }

    public class CurrencyConfiguration
    {
        public string? CurrencyId { get; set; }
        public bool IsSingular { get; set; }
        public string? CurrencyAlphabet { get; set; }
    }
}

