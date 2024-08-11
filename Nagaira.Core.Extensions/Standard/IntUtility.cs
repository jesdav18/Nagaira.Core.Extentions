using Nagaira.Core.Extentions.Dictionaries;
using System;

namespace Nagaira.Core.Extentions.Standard
{
    //Funcionality worked for spanish
    public static class IntUtility
    {
        public static string ToAlphabet(this int value, string currencyId)
        {
            string currency = string.Empty;
            string totalLetters = string.Empty;

            currency = CountryCurrencyAlphabet.GetCurrencyAlphabet(currency, value > 1 ? false : true);

            if (value > 999999999) return "No se puede convertir a letras";

            if (value == 0)
                return $"Cero {currency}";

            if (value < 0)
            {
                totalLetters = $"(-) ";
                value = Math.Abs(value);
            }

            if (value == 1) return $"Un {currency} netos";

            int unityThousands = (int)Math.Floor(((decimal)value / 1000000));
            value = value - (unityThousands * 1000000);

            if (unityThousands > 0)
                totalLetters += unityThousands == 1 ? "Un millón" : $"{unityThousands.Hundreds()} millones";

            if (value == 0) goto enviar;

            int millar = (int)Math.Floor(((decimal)value / 1000));
            value = value - (millar * 1000);

            if (millar > 0)
                totalLetters += $" {millar.Hundreds()} mil";

            if (value == 0) goto enviar;

            totalLetters += $" {value.Hundreds()}";

            enviar:
            return $"{totalLetters.TrimStart()} {currency} netos".ToCapitalize();
        }
    }
}
