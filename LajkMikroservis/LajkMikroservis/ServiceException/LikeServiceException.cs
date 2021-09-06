using System;

namespace LajkMikroservis.ServiceException
{
    public class LikeServiceException : Exception
    {
        public int StatusCode { get; set; }
        public LikeServiceException(string message, int statusCode = 500) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
