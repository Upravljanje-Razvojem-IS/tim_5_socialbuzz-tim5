using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Authorization
{
    public class Authorize : IAuthorization
    {
        private readonly IConfiguration configuration;

        public Authorize(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool AuthorizeUser(string key)
        {
            if (!key.StartsWith("Bearer"))
            {
                return false;
            }

            var keyOnly = key.Substring(key.IndexOf("Bearer") + 7);
            var storedKey = configuration.GetValue<string>("Authorization:SecretKey");

            if (storedKey != keyOnly)
            {
                return false;
            }
            return true;
        }
    }
}
