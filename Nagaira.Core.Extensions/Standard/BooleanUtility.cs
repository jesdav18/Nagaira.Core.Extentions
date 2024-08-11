namespace Nagaira.Core.Extentions.Standard
{
    public static class BooleanUtility
    {
        /// <summary>
        /// Converts a boolean value to 1 or 0. This is useful for SQL Server scripts where a boolean is required to be passed as 1 or 0.
        /// </summary>
        /// <typeparam name="bool"></typeparam>
        /// <param name="valor"></param>
        /// <returns name="int"></returns>
        public static int ToBit(this bool valor)
        {
            return valor ? 1 : 0;
        }
    }
}
