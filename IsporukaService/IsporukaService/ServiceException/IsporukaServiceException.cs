using System;

namespace IsporukaService.ServiceException
{
    public class IsporukaServiceException : Exception
    {
        public int StatusCode { get; set; }
        public IsporukaServiceException(string message, int statusCode = 500) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
