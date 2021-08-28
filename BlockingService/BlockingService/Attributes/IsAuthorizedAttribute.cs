using Microsoft.AspNetCore.Mvc.Filters;
using BlockingService.Exceptions;
using System;

namespace BlockingService.Attributes
{
    public class IsAuthorizedAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _mockToken = "ZFB2zkS4vDyFju3n2nPsrYA48q4aneKwJRBAnbYQAhewt72jbBvYcmuaMC8pdQWY";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string authorizationHeader = context.HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                throw new BusinessException("Not authorized", 401);
            }

            string tokenString = authorizationHeader.Substring("Bearer ".Length);

            if (tokenString != _mockToken)
            {
                throw new BusinessException("Not authorized", 401);
            }
        }
    }
}
