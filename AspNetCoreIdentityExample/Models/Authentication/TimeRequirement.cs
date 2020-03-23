using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreIdentityExample.Models.Authentication
{
    public class TimeRequirement: IAuthorizationRequirement
    {
    }
}
