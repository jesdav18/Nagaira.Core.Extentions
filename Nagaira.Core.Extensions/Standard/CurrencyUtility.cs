using System;
using System.Collections.Generic;

namespace Nagaira.Core.Extentions.Standard
{
    public static class CurrencyUtility
    {
        //Funcionality worked for spanish
        public static string Hundreds(this int value)
        {
            string totalCharacters = string.Empty;
            List<int> withoutRename = new List<int> { 5, 7, 9 };

            int hundred = (int)Math.Floor(((decimal)value / 100));
            value = value - (hundred * 100);

            if (hundred == 1 & value == 0)
                totalCharacters = hundred.HundredsLabels();
            else if (hundred == 1 & value > 0)
                totalCharacters = $"{hundred.HundredsLabels()}to";
            else if (hundred > 0)
                totalCharacters = $"{hundred.HundredsLabels()}{(withoutRename.Contains(hundred) == true ? "" : "cientos")}";

            if (value == 0) goto send;

            int dozen = (int)Math.Floor(((decimal)value / 10));
            value = value - (dozen * 10);

            if (dozen == 0) goto unities;

            switch (dozen)
            {
                case 1:
                    totalCharacters += $"{(value == 0 ? " diez" : value >= 1 && value <= 5 ? (value + (dozen * 10)).UnitiesLabels() : "")}";
                    break;
                case 2:
                    totalCharacters += $"{(value == 0 ? " veinte" : dozen.DozensLabels())}";
                    break;
                default:
                    totalCharacters += $" {dozen.DozensLabels()}{(value == 0 ? "" : " y")}";
                    break;
            }

            unities:
            totalCharacters += $" {value.UnitiesLabels()}";

            send:
            return totalCharacters;
        }


        public static string HundredsLabels(this int value)
        {
            switch (value)
            {
                case 1:
                    return "cien";
                case 2:
                    return "dos";
                case 3:
                    return "tres";
                case 4:
                    return "cuatro";
                case 5:
                    return "quinientos";
                case 6:
                    return "seis";
                case 7:
                    return "setecientos";
                case 8:
                    return "ocho";
                case 9:
                    return "novecientos";
                default:
                    return "";
            }
        }

        public static string DozensLabels(this int value)
        {
            switch (value)
            {
                case 1:
                    return "dieci";
                case 2:
                    return "veinti";
                case 3:
                    return "treinta";
                case 4:
                    return "cuarenta";
                case 5:
                    return "cincuenta";
                case 6:
                    return "sesenta";
                case 7:
                    return "setenta";
                case 8:
                    return "ochenta";
                case 9:
                    return "noventa";
                default:
                    return "";
            }
        }

        public static string UnitiesLabels(this int value)
        {
            switch (value)
            {
                case 1:
                    return "uno";
                case 2:
                    return "dos";
                case 3:
                    return "tres";
                case 4:
                    return "cuatro";
                case 5:
                    return "cinco";
                case 6:
                    return "seis";
                case 7:
                    return "siete";
                case 8:
                    return "ocho";
                case 9:
                    return "nueve";
                case 10:
                    return "diez";
                case 11:
                    return "once";
                case 12:
                    return "doce";
                case 13:
                    return "trece";
                case 14:
                    return "catorce";
                case 15:
                    return "quince";
                default:
                    return "";
            }
        }
    }
}
