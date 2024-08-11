using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Nagaira.Core.Extentions.Responses;
using Nagaira.WebApi.Utilities.Extensions;
using System;
using System.Data.SqlClient;
using System.IO;

namespace Nagaira.Core.WebApi.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly LoggerHelper _loggerHelper;
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(LoggerHelper loggerHelper)
        {
            _loggerHelper = loggerHelper;
            _logger = _loggerHelper.CreateLogger<GlobalExceptionFilter>();
        }

        public void OnException(ExceptionContext context)
        {
            string userFriendlyMessage;
            int statusCode;


            if (context.Exception is SqlException sqlEx)
            {
                if (sqlEx.Message.Contains("Connection Timeout Expired"))
                {
                    userFriendlyMessage = "La conexión a la base de datos ha expirado. Por favor, intenta de nuevo más tarde.";
                    statusCode = StatusCodes.Status504GatewayTimeout;
                }
                else if (sqlEx.Number == -2) // Código para timeout general
                {
                    userFriendlyMessage = "Tiempo de espera agotado para la operación. Por favor, intenta de nuevo más tarde.";
                    statusCode = StatusCodes.Status504GatewayTimeout;
                }
                else if (sqlEx.Number == 547) // Violación de llave foránea
                {
                    userFriendlyMessage = "El registro que intentas guardar está relacionado con otros datos y no se puede completar la operación.";
                    statusCode = StatusCodes.Status409Conflict;
                }
                else if (sqlEx.Number == 2627 || sqlEx.Number == 2601) // Violación de llave primaria
                {
                    userFriendlyMessage = "El registro que intentas guardar ya existe. Por favor, verifica la información e intenta de nuevo.";
                    statusCode = StatusCodes.Status409Conflict;
                }
                else
                {
                    userFriendlyMessage = "Ocurrió un error al intentar acceder a la base de datos. Por favor, intenta de nuevo más tarde.";
                    statusCode = StatusCodes.Status500InternalServerError;
                }
            }
            else if (context.Exception is NullReferenceException)
            {
                userFriendlyMessage = "Ocurrió un problema interno. Por favor, intenta de nuevo más tarde.";
                statusCode = StatusCodes.Status500InternalServerError;
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                userFriendlyMessage = "No tienes permiso para realizar esta acción. Por favor, verifica tus credenciales.";
                statusCode = StatusCodes.Status401Unauthorized;
            }
            else if (context.Exception is FileNotFoundException)
            {
                userFriendlyMessage = "No se encontró el archivo solicitado. Por favor, verifica la información e intenta de nuevo.";
                statusCode = StatusCodes.Status404NotFound;
            }
            else if (context.Exception is InvalidOperationException)
            {
                userFriendlyMessage = "La operación que estás intentando realizar no es válida en este momento.";
                statusCode = StatusCodes.Status400BadRequest;
            }
            else if (context.Exception is ArgumentException)
            {
                userFriendlyMessage = "Uno de los parámetros que has ingresado no es válido. Por favor, revisa e intenta de nuevo.";
                statusCode = StatusCodes.Status400BadRequest;
            }
            else
            {
                userFriendlyMessage = "Ocurrió un evento inesperado. Por favor, intenta de nuevo más tarde, o reportalo al equipo de Soporte Aplicativo.";
                statusCode = StatusCodes.Status500InternalServerError;
            }

            _logger.LogError(context.Exception, userFriendlyMessage);

            context.HttpContext.Response.StatusCode = statusCode;
            context.Result = new JsonResult(Response<object>.Exception(userFriendlyMessage))
            {
                StatusCode = statusCode
            };
            context.ExceptionHandled = true;
        }
    }
}
