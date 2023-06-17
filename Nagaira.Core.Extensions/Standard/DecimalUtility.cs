namespace Nagaira.Core.Extentions.Standard
{
    public static class DecimalUtility
    {
        //Funcionality worked for spanish
        public static string ToLetters(this decimal value, string currencyId)
        {
            int intPart = (int)value;
            var totalLetters = intPart.ToAlphabet(currencyId);

            value = value - intPart;

            if (value > 0)
                totalLetters = totalLetters.Replace("netos", $"con {(int)(value * 100)}/100 centavos");

            return totalLetters.ToCapitalize();
        }
    }
}
