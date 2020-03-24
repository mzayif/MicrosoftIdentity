using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentityExample.Models.ViewModels
{
    public class AuthenticatorVM
    {
        public string SharedKey { get; set; }
        public string QrCodeUri { get; set; }
        public string VerificationCode { get; set; }
    }
}
