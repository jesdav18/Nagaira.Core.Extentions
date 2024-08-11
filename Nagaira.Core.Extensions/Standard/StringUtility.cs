using Nagaira.Core.Extentions.Exceptions;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Nagaira.Core.Extentions.Standard
{
    public static class StringUtility
    {
        /// <summary>
        /// Determines if a text string contains only letters
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns name="bool"></returns>
        public static bool AreLetters(this string value)
        {
            return new Regex("[^a-zA-ZñÑ áéíóúÁÉÍÓÚ]").Match(value).Success;
        }

        /// <summary>
        /// Converts an entire text string to uppercase
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns name="string"></returns>
        public static string ToCapitalize(this string value)
        {
            string[] caracteres = value.Split(' ');

            for (int caracter = 0; caracter < caracteres.Length; caracter++)
            {
                string caracterEnMinuscula = caracteres[caracter].ToLower();
                if (char.IsLetter(caracterEnMinuscula[0])) caracteres[caracter] = $"{caracterEnMinuscula[0].ToString().ToUpper()}{caracterEnMinuscula.Substring(1, caracterEnMinuscula.Length - 1)}";
            }

            return string.Join(" ", caracteres);
        }

        /// <summary>
        /// Determines if a text string contains the accepted body of an email
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <param name="value"></param>
        /// <returns name="bool"></returns>
        public static bool IsMail(this string value)
        {
            return new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Match(value).Success;
        }

        /// <summary>
        /// Decodes a text string that has been encoded with Base64
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <param name="value"></param>
        /// <returns name="string"></returns>
        public static string Atob(this string value)
        {
            try
            {
                string dummyData = value.Trim().Replace(" ", "+");
                if (dummyData.Length % 4 > 0)
                    dummyData = dummyData.PadRight(dummyData.Length + 4 - dummyData.Length % 4, '=');
                byte[] byteArray = Convert.FromBase64String(dummyData);

                string base64Decoded = ASCIIEncoding.ASCII.GetString(byteArray);

                return base64Decoded;
            }
            catch (Exception ex)
            {
                return MessageException.ShowException(ex);
            }

        }

        /// <summary>
        /// Converts a strign value to a text string that does not contain tildes (spanish).
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns name="string"></returns>
        public static string ToNonAccentedString(this string value)
        {
            return value.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
                    .Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U");
        }

        /// <summary>
        /// Converts a strign value to a text string that does not contain tildes.
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <param name="value"></param>
        /// <returns name="byte[]"></returns>
        public static byte[] ToHash(this string value)
        {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(value));
            return hash;
        }

        /// <summary>
        /// Converts a byte array to a string in hash format.
        /// </summary>
        /// <typeparam name="byte[]"></typeparam>
        /// <param name="value"></param>
        /// <returns name="string"></returns>
        public static string ToHashString(this byte[] value)
        {
            return string.Join("", value.Select(x => x.ToString("x2")).ToArray());
        }

        /// <summary>
        /// Converts a byte array to a string in hash format.
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <param name="value"></param>
        /// <returns name="string"></returns>
        public static string ToHashString(this string value)
        {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(value));
            return ToHashString(hash);
        }

        /// <summary>
        /// Converts a string value to a text string compatible with HTTP client/server communication.
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <param name="value"></param>
        /// <returns name="StringContent"></returns>
        public static StringContent ToContent(this string value)
        {
            return new StringContent(value, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Converts a string value to an enum.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns name="TEnum"></returns>
        public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct, IConvertible
        {
            bool isValidValue = Enum.TryParse(value, out TEnum enumValue);
            if (!isValidValue) throw new InvalidCastException($"Can not convert ('{value}') to enum time specified.");

            return enumValue;
        }
    }
}
