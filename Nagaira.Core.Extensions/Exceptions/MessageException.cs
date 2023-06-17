using System;

namespace Nagaira.Core.Extentions.Exceptions
{
    public static class MessageException
    {
        public static string ShowException(Exception exception)
        {
            if (exception.InnerException != null) return new string(exception.InnerException.Message);
            return new string(exception.Message);
        }
    }
}
