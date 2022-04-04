using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using AccountManagement.Infrastructure.Configuration;

namespace _0_Framework.Application
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool IsAuthenticated()
        {
            var claims = _contextAccessor.HttpContext.User.Claims.ToList();
            //if (claims.Count > 0)
            //    return true;
            //return false;
            return claims.Count > 0;
        }

        public void Signin(AccountViewModel account)
        {
            var claims = new List<Claim>
            {
                new Claim("AccountId", account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Fullname),
                new Claim(ClaimTypes.Role, account.RoleId.ToString()),
                new Claim("Username", account.Username)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
            };

            _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public string? GetCurrentInfo()
        {
            if (IsAuthenticated())
            {
                return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)
                    ?.Value;
            }

            return null;
        }

        public AccountViewModel? GetAccountInfo()
        {
            var result = new AccountViewModel();
            if (!IsAuthenticated())
                return result;

            var claims=_contextAccessor.HttpContext.User.Claims.ToList();
            result.Id = long.Parse(claims.FirstOrDefault(x => x.Type == "AccountId")?.Value!);
            result.RoleId= long.Parse(claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value!);
            result.Fullname=claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value!;
            result.Username=claims.FirstOrDefault(x => x.Type == "Username")?.Value!;
            result.Role = Roles.GetBy(result.RoleId);
            return result;
        }

        public void SignOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
