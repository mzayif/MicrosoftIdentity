using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentityExample.Models.Enums;
using AspNetCoreIdentityExample.Models.ViewModels;

namespace AspNetCoreIdentityExample.Models.Authentication
{
    public class AppUser : IdentityUser<int>
    {
        public string Memleket { get; set; }
        public bool Cinsiyet { get; set; }
        public TwoFactorType TwoFactorType { get; set; }

        public static implicit operator UserDetailViewModel(AppUser user)
        {
            return new UserDetailViewModel
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };
        }
    }
}
