using System;
using System.Linq;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.Security.Authentication;
using HMPPS.Utilities.Helpers;
using HMPPS.Utilities.Services;

namespace HMPPS.Authentication.Pipelines
{
    /// <summary>
    /// Verifies authentication tickets:
    /// If sitecore logged in and IDAM tokan missing: log out from sitecore, it will trigger a redirect to login
    /// If sitecore logged out, and IDAM token is valid: login sitecore user
    /// If sitecore logged in and IDAM token expired: refresh token
    /// If both are available:
    ///     Check identities: if they are equal: OK
    ///     Else: logout both identities , it will trigger a redirect to login
    /// </summary>
    /// If both are unavailable: anonymous user - do nothing, it will trigger a redirect to login

    public class AuthenticationChecker : AuthenticationProcessorBase
    {
        public override void Process(HttpRequestArgs args)
        {
            // Not checking IDAM authentication of content editors
            if ((new String[] { "shell", "login", "admin" }).Contains(Sitecore.Context.Site.Name)) return;

            Assert.ArgumentNotNull(args, "args");
            var sitecoreUserLoggedIn = Context.IsLoggedIn;
            var userDataService = new UserDataService(Settings.AuthenticationCheckerCookieName, Settings.AuthenticationCheckerCookieKey, Settings.JwtTokenSecurityKey);

            var userData = userDataService.GetUserDataFromCookie(args.Context);

            if (sitecoreUserLoggedIn && userData == null)
            {
                AuthenticationManager.Logout();
                return;
            }
            if (!sitecoreUserLoggedIn && userData != null)
            {
                var user = BuildVirtualUser(userData);
                AuthenticationManager.LoginVirtualUser(user);
            }
            if (sitecoreUserLoggedIn && userData != null)
            {
                if (ExpirationHelper.IsExpired(userData.ExpiresAt))
                {
                    var claims = RefreshUserData(ref userData);
                    userDataService.SaveUserDataToCookie(claims, args.Context);
                }
                else if (!Sitecore.Context.User.LocalName.Equals(userData.NameIdentifier))
                {
                    AuthenticationManager.Logout();
                    userDataService.DeleteUserDataCookie(args.Context);
                }
            }
        }
    }
}