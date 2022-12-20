using System;
using System.Security.Cryptography;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace Nagaira.Core.Extentions.Standard
{
    public static class StringUtility
    {
        /// <summary>
        /// Determina si una cadena de texto contiene sólo letras
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns name="bool"></returns>
        public static bool AreLetters(this string value)
        {
            return new Regex("[^a-zA-ZñÑ áéíóúÁÉÍÓÚ]").Match(value).Success;
        }

        /// <summary>
        /// Convierte una cadena de texto en su totalidad a mayúsculas
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
        /// Determina si una cadena de texto contiene el cuerpo aceptado de un e-mail
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <param name="value"></param>
        /// <returns name="bool"></returns>
        public static bool IsMail(this string value)
        {
            return new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Match(value).Success;
        }

        /// <summary>
        /// Decodifica una cadena de texto que ha sido codificada con Base64
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
                return RaiseGeneralException(ex);
            }

        }

        /// <summary>
        /// Convierte un valor strign a una cadena de texto que no contiene tildes.
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
        /// Convierte un valor tipo string a un arreglo de bytes.
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
        /// Convierte un arreglo de bytes a una cadena string en formato Hash.
        /// </summary>
        /// <typeparam name="byte[]"></typeparam>
        /// <param name="value"></param>
        /// <returns name="string"></returns>
        public static string ToHashString(this byte[] value)
        {
            return string.Join("", value.Select(x => x.ToString("x2")).ToArray());
        }

        /// <summary>
        /// Convierte un valor tipo string a un valor string en formato Hash.
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
        /// Convierte un valor tipo string a una cadena de texto compatible con comunicación HTTP cliente/servidor.
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <param name="value"></param>
        /// <returns name="StringContent"></returns>
        public static StringContent ToContent(this string value)
        {     
            return new StringContent(value, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Permite lanzar una excepción evaluando el objeto InnerException, si este es distinto de nulo devuelve InnerException.Message, en caso contrario devuelve el valor de Message
        /// </summary>
        /// <typeparam name="Exception"></typeparam>
        /// <param name="exception"></param>
        /// <returns name="string"></returns>
        public static string RaiseGeneralException(this Exception exception)
        {
            string message = exception.InnerException == null ? exception.Message : exception.InnerException.Message;
            return message;
            
        }

        /// <summary>
        /// Convierte un valor tipo string a enum.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns name="TEnum"></returns>
        public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct, IConvertible
        {
            bool isValidValue = Enum.TryParse(value, out TEnum enumValue);
            if (!isValidValue) throw new InvalidCastException($"No se pudo convertir el valor ('{value}') al tipo de enum especificado.");
        
            return enumValue;
        }
    }
}
