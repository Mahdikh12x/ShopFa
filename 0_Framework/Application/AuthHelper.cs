﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using AccountManagement.Infrastructure.Configuration;
using Newtonsoft.Json;

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
            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public void Signin(AccountViewModel account)
        {
            var permissions = JsonConvert.SerializeObject(account.Permissions);
            var claims = new List<Claim>
            {
                new("AccountId", account.Id.ToString()),
                new(ClaimTypes.Name, account.Fullname),
                new(ClaimTypes.Role, account.RoleId.ToString()),
                new("Username", account.Username),
                new("permission",permissions)
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

        public List<int>? GetPermissions()
        {
            if (!IsAuthenticated())
                return new List<int>();
            var permissions=_contextAccessor.HttpContext.User.Claims.FirstOrDefault(x=>x.Type=="permission")?.Value;

            return JsonConvert.DeserializeObject<List<int>>(permissions);
        }

        public long GetAccountId()
        {
            if (IsAuthenticated())
            {
                var value = _contextAccessor.HttpContext.User.Claims
                    .FirstOrDefault(claim => claim.Type == "AccountId")
                    ?.Value;
                if (value != null) return long.Parse(value);
            }
            return 0;
        }

        public void SignOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
