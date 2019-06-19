using ClaimBasedAuthorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaimBasedAuthorization.Models
{
    public class AudienceModel
    {
        public string Name { get; set; }
        public string ApiKey { get; set; }
        public Dictionary<PermissionType,bool> Permissions { get; set; }
    }
}
