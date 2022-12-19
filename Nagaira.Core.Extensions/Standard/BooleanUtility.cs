namespace Diunsa.Core.Extentions.Standard
{
    public static class BooleanUtility
    {
        /// <summary>
        /// Convierte un valor boolean a 1 o 0. Esto es útil scripts de SQL Server donde se requiere mandar un booleano como 1 o 0.
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
