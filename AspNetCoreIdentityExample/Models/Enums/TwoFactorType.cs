using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentityExample.Models.Enums
{
    public enum TwoFactorType
    {
        [Display(Name = "Microsoft & Google Authenticator İle Doğrulama")]
        Authenticator,
        [Display(Name = "SMS İle Doğrulama")]
        SMS,
        [Display(Name = "E-Posta İle Doğrulama")]
        Email
    }
}
