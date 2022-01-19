using Blazored.LocalStorage;
using HCMModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HCM
{
    public class LocalAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _storageService;
    

        public LocalAuthenticationStateProvider(ProtectedSessionStorage storageService)
        {
            _storageService = storageService;
        }

       
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var sessionUser = await _storageService.GetAsync<UsersModel>("User");
                if (sessionUser.Success)
                {
                    var userInfo = sessionUser.Value;

                    var claims = new[]
                    {
                        new Claim("UserID", userInfo.UserID.ToString()),
                        new Claim("ContactID", userInfo.ContactID.ToString()),
                        new Claim("FullName", string.IsNullOrEmpty(userInfo.FullName) ? "FullName":userInfo.FullName ),
                        new Claim("NickName", string.IsNullOrEmpty(userInfo.NickName) ? "NikName":userInfo.NickName),
                        new Claim(ClaimTypes.NameIdentifier, userInfo.UserID.ToString()),
                    };

                    var identity = new ClaimsIdentity(claims, "BearerToken");
                    var principal = new ClaimsPrincipal(identity);
                    var state = new AuthenticationState(principal);
                    NotifyAuthenticationStateChanged(Task.FromResult(state));
                    return state;
                }

                return new AuthenticationState(new ClaimsPrincipal());
            }
            catch
            {
                return new AuthenticationState(new ClaimsPrincipal());
            }
            
        }

        public async Task LogoutAsync()
        {
            await _storageService.DeleteAsync("User");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
        }

        
    }
}
