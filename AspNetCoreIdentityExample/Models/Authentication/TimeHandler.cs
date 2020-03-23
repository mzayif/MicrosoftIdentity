using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreIdentityExample.Models.Authentication
{
    /// <summary>
    ///     Bu class ve bağlantılı classlar metotlara erişimde kendinize özgü
    ///     politikalar yapmamızı/ tanımlamamızı sağlar ve istediğimiz metotlara erişim için kullanılabilir.
    /// </summary>
    public class TimeHandler: AuthorizationHandler<TimeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TimeRequirement requirement)
        {
            if (DateTime.Now.Minute >= 20 && DateTime.Now.Minute < 25)
                context.Succeed(requirement);
            else
                context.Fail();
            return Task.CompletedTask;
        }
    }
}
