namespace Nagaira.Core.Extentions.Responses
{
    public enum TypeResponse
    {
        Ok = 1,
        Excepcion = 2,
        Advertencia = 3,
        AdvertenciaEnEntidad = 4,
        NoEncontrado = 5
    }

    public class Response
    {
        public string? Message { get; set; }
        public TypeResponse Type { get; set; }
        public static Response Ok(string message) => new Response { Type = TypeResponse.Ok, Message = message };
        public static Response Excepcion(string message) => new Response { Type = TypeResponse.Excepcion, Message = message };
        public static Response Advertencia(string message) => new Response { Type = TypeResponse.Advertencia, Message = message };
        public static Response AdvertenciaDeEntidad(string message) => new Response { Type = TypeResponse.AdvertenciaEnEntidad, Message = message };
        public static Response NoEncontrado(string message) => new Response { Type = TypeResponse.NoEncontrado, Message = message };
    }

    public class Response<TData> : Response
    {
        public TData Data { get; set; }
        public static Response<TData> Ok(string message, TData data) => new Response<TData> { Type = TypeResponse.Ok, Message = message, Data = data };
        public static Response<TData> Excepcion(string message, TData data) => new Response<TData> { Type = TypeResponse.Excepcion, Message = message, Data = data };
        public static Response<TData> Advertencia(string message, TData data) => new Response<TData> { Type = TypeResponse.Advertencia, Message = message, Data = data };
        public static Response<TData> AdvertenciaDeEntidad(string message, TData data) => new Response<TData> { Type = TypeResponse.AdvertenciaEnEntidad, Message = message, Data = data };
        public static Response<TData> NoEncontrado(string message, TData data) => new Response<TData> { Type = TypeResponse.NoEncontrado, Message = message, Data = data };
    }
}
