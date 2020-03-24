using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentityExample.Models.ViewModels
{
    public class VerifyState
    {
        public bool State { get; set; }
        public IEnumerable<string> RecoveryCode { get; set; }
    }
}
