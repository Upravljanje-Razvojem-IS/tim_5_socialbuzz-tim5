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

            string keyOnly = key[(key.IndexOf("Bearer") + 7)..];
            var storedKey1 = configuration.GetValue<string>("Authorization:Secret");
            var storedKey2 = configuration.GetValue<string>("Authorization:Secret2");
            var storedKey3 = configuration.GetValue<string>("Authorization:Secret3");
            var storedKey4 = configuration.GetValue<string>("Authorization:Secret4");
            var storedKey5 = configuration.GetValue<string>("Authorization:Secret5");

            List<String> list = new();
            list.Add(storedKey1);
            list.Add(storedKey2);
            list.Add(storedKey3);
            list.Add(storedKey4);
            list.Add(storedKey5);

            if (!list.Contains(keyOnly))
            {
                return false;
            }
            return true;
        }
    }
}
