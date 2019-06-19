using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClaimBasedAuthorization.Entity;
using ClaimBasedAuthorization.Models;

namespace ClaimBasedAuthorization.Services
{
    public class AudienceService : IAudienceService
    {
        public async Task<AudienceModel> GetByCredentials(string apiKey, string password)
        {
            if (apiKey == "the-chosen-one" && password == "very-complex-password")
            {
                return new AudienceModel
                {
                    Name = "www.neo.com",
                    ApiKey = "the-chosen-one",
                    Permissions = new Dictionary<PermissionType, bool>
                    {
                        { PermissionType.ProductRead, true },
                        { PermissionType.ProductWrite, false },
                        { PermissionType.SalesOrderRead, true }
                    }
                };
            }
            else return await Task.FromResult(default(AudienceModel));
        }

        public Task<Audience> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
