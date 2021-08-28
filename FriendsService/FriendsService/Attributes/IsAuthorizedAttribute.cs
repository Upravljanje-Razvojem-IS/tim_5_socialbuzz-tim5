using Microsoft.AspNetCore.Mvc.Filters;
using FriendsService.Exceptions;
using System;

namespace FriendsService.Attributes
{
    public class IsAuthorizedAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _mockToken = "KP9ndQ7LxNuAsPuhkVswcEqxVRvskLaFTHjm5jRcDrt3xVYupFzCA6A4NEYU4vm3";

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
