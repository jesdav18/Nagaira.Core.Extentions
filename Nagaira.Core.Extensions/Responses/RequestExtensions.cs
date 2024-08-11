namespace Nagaira.Core.Extentions.Responses
{
    public enum TypeResponse
    {
        Ok = 1,
        Exception = 2,
        Warning = 3,
        EntityWarning = 4,
        NotFound = 5
    }

    public class Response
    {
        public string? Message { get; set; }
        public TypeResponse Type { get; set; }
        public static Response Ok(string message) => new Response { Type = TypeResponse.Ok, Message = message };
        public static Response Exception(string message) => new Response { Type = TypeResponse.Exception, Message = message };
        public static Response Warning(string message) => new Response { Type = TypeResponse.Warning, Message = message };
        public static Response EntityWarning(string message) => new Response { Type = TypeResponse.EntityWarning, Message = message };
        public static Response NotFound(string message) => new Response { Type = TypeResponse.NotFound, Message = message };
    }

    public class Response<TData>
    {
        public static Response<TData> Ok(string message, TData? data = default) => new Response<TData> { Type = TypeResponse.Ok, Message = message, Data = data };
        public static Response<TData> Exception(string message, TData? data = default) => new Response<TData> { Type = TypeResponse.Exception, Message = message };
        public static Response<TData> Warning(string message, TData? data = default) => new Response<TData> { Type = TypeResponse.Warning, Message = message };
        public static Response<TData> EntityWarning(string message, TData? data = default) => new Response<TData> { Type = TypeResponse.EntityWarning, Message = message };
        public static Response<TData> NotFound(string message, TData? data = default) => new Response<TData> { Type = TypeResponse.NotFound, Message = message, Data = data };
        public TypeResponse Type { get; set; }
        public string? Message { get; set; }
        public TData? Data { get; set; }
    }




}
