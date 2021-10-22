using System;
using System.Collections.Generic;
using System.Text;

namespace HCMModels
{
    public class UserRolesModel
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleNameText { get; set; }
        public string LocalizerRoleName { get; set; }
        public int RoleCount { get; set; }
    }
}
