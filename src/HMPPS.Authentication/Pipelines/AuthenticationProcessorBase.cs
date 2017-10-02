using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.Security.Accounts;
using Sitecore.Security.Authentication;
using HMPPS.Utilities.Models;


namespace HMPPS.Authentication.Pipelines
{
    public abstract class AuthenticationProcessorBase : HttpRequestProcessor
    {

        public IEnumerable<Claim> RefreshUserData(ref UserData userData)
        {
            var tokenManager = new TokenManager();
            var tokenResponse = tokenManager.RequestRefreshToken(userData.RefreshToken);
            var claimsPrincipal = tokenManager.ValidateIdentityToken(tokenResponse.IdentityToken);
            var claims = tokenManager.ExtractClaims(tokenResponse, claimsPrincipal).ToList();
            AddPrisonerDetailsToClaims(userData.NameIdentifier, ref claims);
            userData = new UserData(claims);
            return claims;
        }

        protected void AddPrisonerDetailsToClaims(string prisonerId, ref List<Claim> claims)
        {
            var nomisApiService = new NomisApiService.Services.NomisApiService();

            var establishment = nomisApiService.GetPrisonerLocationDetails(prisonerId);
            var prisonId = establishment.Code;
            claims.Add(new Claim("prison_id", prisonId));
            claims.Add(new Claim("prison_name", establishment.Desc));

            var accounts = nomisApiService.GetPrisonerAccounts(prisonId, prisonerId);
            claims.Add(new Claim("account_balance", ((decimal)(accounts.Spends + accounts.Cash)).ToString(CultureInfo.InvariantCulture.NumberFormat)));
            claims.Add(new Claim("account_balance_lastupdated", DateTime.Now.ToString(CultureInfo.InvariantCulture)));
        }

        protected User BuildVirtualUser(UserData idamData)
        {
            var domain = "extranet";
            var userId = idamData.NameIdentifier;
            var email = idamData.Email;
            var roles = idamData.Roles;

            var username = $"{domain}\\{userId}";
            var user = AuthenticationManager.BuildVirtualUser(username, true);
            user.Profile.Email = email;
            user.Profile.FullName = idamData.Name;
            AssignUserRoles(user, roles);
            return user;
        }

        private void AssignUserRoles(User user, IEnumerable<string> roles)
        {
            user.RuntimeSettings.AddedRoles.Clear();
            user.Roles.RemoveAll();

            foreach (var role in roles.Where(Role.Exists))
            {
                user.Roles.Add(Role.FromName(role));
            }
        }
    }
}