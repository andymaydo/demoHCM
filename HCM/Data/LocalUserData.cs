using Blazored.LocalStorage;
using HCMDataAccess;
using HCMDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCM.Data
{
    public class LocalUserData : ILocalUserData
    {
        private readonly ILocalStorageService _storageService;

        public LocalUserData(ILocalStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<int> GetUserID()
        {
            if (await _storageService.ContainKeyAsync("User"))
            {
                var userInfo = await _storageService.GetItemAsync<UsersModel>("User");
                return userInfo.UserID;
            }

            return -1;
        }
        public async Task<int> GetContactID()
        {
            if (await _storageService.ContainKeyAsync("User"))
            {
                var userInfo = await _storageService.GetItemAsync<UsersModel>("User");
                return userInfo.ContactID;
            }

            return -1;
        }
    }
}
