using Blazored.LocalStorage;
using HCMModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HCM
{
    public class LocalAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _storageService;

        public LocalAuthenticationStateProvider(ILocalStorageService storageService)
        {
            _storageService = storageService;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (await _storageService.ContainKeyAsync("User"))
            {
                var userInfo = await _storageService.GetItemAsync<UsersModel>("User");

                var claims = new[]
                {
                    new Claim("UserID", userInfo.UserID.ToString()),
                    new Claim("ContactID", userInfo.ContactID.ToString()),
                    new Claim("FullName", string.IsNullOrEmpty(userInfo.FullName) ? "FullName":userInfo.FullName ),
                    new Claim("NickName", string.IsNullOrEmpty(userInfo.NickName) ? "NikName":userInfo.NickName),
                    new Claim(ClaimTypes.NameIdentifier, userInfo.UserID.ToString()),
                };

                var identity = new ClaimsIdentity(claims, "BearerToken");
                var user = new ClaimsPrincipal(identity);
                var state = new AuthenticationState(user);
                NotifyAuthenticationStateChanged(Task.FromResult(state));
                return state;
            }

            return new AuthenticationState(new ClaimsPrincipal());
        }

        public async Task LogoutAsync()
        {
            await _storageService.RemoveItemAsync("User");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
        }

        
    }
}
