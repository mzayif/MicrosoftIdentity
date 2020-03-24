using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentityExample.Models.Authentication;
using AspNetCoreIdentityExample.Models.ViewModels;
using AspNetCoreIdentityExample.Servises;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityExample.Controllers
{
    public class TwoAuthenticationController : Controller
    {
        AuthenticatorService _authenticatorService;
        UserManager<AppUser> _userManager;

        public TwoAuthenticationController(AuthenticatorService authenticatorService, UserManager<AppUser> userManager)
        {
            _authenticatorService = authenticatorService;
            _userManager = userManager;
        }

        public async Task<IActionResult> SelectTwoFactorAuthentication()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SelectTwoFactorAuthentication(TwoFactorTypeSelectVM model)
        {
            switch (model.TwoFactorType)
            {
                case Models.Enums.TwoFactorType.Authenticator:
                    return RedirectToAction("AuthenticatorVerify");
                case Models.Enums.TwoFactorType.SMS:
                    return RedirectToAction("SMSVerify");
                case Models.Enums.TwoFactorType.Email:
                    return RedirectToAction("EmailVerify");
            }

            return View();
        }

        public async Task<IActionResult> AuthenticatorVerify()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            string sharedKey = await _authenticatorService.GenerateSharedKey(user);
            string qrcodeUri = await _authenticatorService.GenerateQrCodeUri(sharedKey, "www.gencayyildiz.com", user);

            return View(new AuthenticatorVM
            {
                SharedKey = sharedKey,
                QrCodeUri = qrcodeUri
            });
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticatorVerify(AuthenticatorVM model)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            VerifyState verifyState = await _authenticatorService.Verify(model, user);
            if (verifyState.State)
            {
                TempData["message2"] = "İki adımlı doğrulama hesaba tanımlanmıştır.";
                TempData["message3"] = verifyState.RecoveryCode;
            }
            return View(model);
        }

        public async Task<IActionResult> SMSVerify()
        {
            //İlgili makalede kodlanacaktır...
            return View();
        }

        public async Task<IActionResult> EmailVerify()
        {
            //İlgili makalede kodlanacaktır...
            return View();
        }
    }
}