﻿using System.Security.Claims;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace GymClub.App.Services.Security
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _protectedSessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        private static readonly string Key = "UserSession";
        private static readonly string AuthType = "CustomAuthentication";
        public CustomAuthenticationStateProvider(ProtectedSessionStorage protectedSessionStorage)
        {
            _protectedSessionStorage = protectedSessionStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSession = await _protectedSessionStorage.GetAsync<UserSessionModel>(Key);

                if (userSession is { Success: true, Value: not null })
                {
                    var claimsPrincipal = CreateClaimsPrincipal(userSession.Value!);
                    return await Task.FromResult(new AuthenticationState(claimsPrincipal));
                }
            }
            catch (Exception)
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }

            return await Task.FromResult(new AuthenticationState(_anonymous));
        }

        public async Task UpdateAuthenticationState(UserSessionModel? userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession is not null)
            {
                await _protectedSessionStorage.SetAsync(Key, userSession);

                claimsPrincipal = CreateClaimsPrincipal(userSession);
            }
            else
            {
                await _protectedSessionStorage.DeleteAsync(Key);
                claimsPrincipal = _anonymous;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        private static ClaimsPrincipal CreateClaimsPrincipal(UserSessionModel userSession)
        {
            var claims = new List<Claim>
            {
                new Claim("UserName", userSession.UserName),
                new Claim("UserId", userSession.UserId),
            };

            return new ClaimsPrincipal(new ClaimsIdentity(claims, AuthType));
        }
        public async Task<UserSessionModel> GetUserData()
        {
            UserSessionModel model = new();

            var result =
                await _protectedSessionStorage.GetAsync<UserSessionModel>(Key);
            if (result.Success)
            {
                model = result.Value;
            }

            return model;
        }
    }
}

public class UserSessionModel
{
    public string? UserId { get; set; }
    public string? UserName { get; set; }
}
