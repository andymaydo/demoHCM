using HCM.Options;
using System.Collections.Generic;


namespace HCM.Models
{
    public static class Const
    {
        public static List<ProfileRole>  GetProfileRoleList(CommonLocalizationService localizer)
        {
            var ProfileRoleList = new List<ProfileRole>()
            {
                new ProfileRole(){ Key = "User", Description = localizer.Str["Profile_User_Role_1"] },
                new ProfileRole(){ Key = "Moderator", Description = localizer.Str["Profile_User_Role_2"] },
                new ProfileRole(){ Key = "AliasManager", Description = localizer.Str["Profile_User_Role_3"] },
                new ProfileRole(){ Key = "GlobalAliasManager", Description = localizer.Str["Profile_User_Role_4"] }
            };
            return ProfileRoleList;
        }

        public static string GetRoleName(string roleKey, CommonLocalizationService localizer)
        {
            switch (roleKey)
            {
                case "User":
                    return localizer.Str["Profile_User_Role_1"];
                    

                case "Moderator":
                    return localizer.Str["Profile_User_Role_2"];

                case "AliasManager":
                    return localizer.Str["Profile_User_Role_3"];

                case "GlobalAliasManager":
                    return localizer.Str["Profile_User_Role_4"];


                default: 
                    return localizer.Str["Profile_User_Role_1"];

            }
        }
    }
}
