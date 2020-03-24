using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AspNetCoreIdentityExample.Models.Authentication;
using AspNetCoreIdentityExample.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityExample.Servises
{
    public class AuthenticatorService
    {
        UserManager<AppUser> _userManager;
        UrlEncoder _urlEncoder;

        public AuthenticatorService(UserManager<AppUser> userManager, UrlEncoder urlEncoder)
        {
            _userManager = userManager;
            _urlEncoder = urlEncoder;
        }

        public async Task<string> GenerateSharedKey(AppUser user)
        {
            string sharedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(sharedKey))
            {
                IdentityResult result = await _userManager.ResetAuthenticatorKeyAsync(user);
                if (result.Succeeded)
                    sharedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }
            return sharedKey;
        }

        public async Task<string> GenerateQrCodeUri(string sharedKey, string title, AppUser user) =>
            $"otpauth://totp/{_urlEncoder.Encode(title)}:{_urlEncoder.Encode(user.Email)}?secret={sharedKey}&issuer={_urlEncoder.Encode(title)}";

        public async Task<VerifyState> Verify(AuthenticatorVM model, AppUser user)
        {
            VerifyState verifyState = new VerifyState();
            verifyState.State = await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, model.VerificationCode);
            if (verifyState.State)
            {
                user.TwoFactorEnabled = true;
                verifyState.RecoveryCode = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 5);
            }
            return verifyState;
        }
    }
}
