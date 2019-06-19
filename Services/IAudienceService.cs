using ClaimBasedAuthorization.Entity;
using ClaimBasedAuthorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaimBasedAuthorization.Services
{
    public interface IAudienceService
    {
        Task<Audience> GetById(int id);
        Task<AudienceModel> GetByCredentials(string apiKey, string password);
    }
}
