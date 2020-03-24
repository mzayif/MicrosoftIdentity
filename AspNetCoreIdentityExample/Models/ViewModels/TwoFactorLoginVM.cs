using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentityExample.Models.ViewModels
{
    public class TwoFactorLoginVM
    {
        [Required(ErrorMessage = "Lütfen doğrulama kodunu boş geçmeyiniz.")]
        [Display(Name = "Doğrulama Kodu")]
        public string VerifyCode { get; set; }
        public bool Recovery { get; set; }
    }
}
