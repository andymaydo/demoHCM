using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMModels
{
    public static class AmEnums
    {
        public static Dictionary<int, string> AliasStatusDic = new Dictionary<int, string>()
        {
            {-100, "Alias_Status_SelectAll"},
            {1, "Alias_Status_Active"},
            {0, "Alias_Status_Inactive"},
            {-1, "Alias_Status_Deleted"}


        };

        public static Dictionary<int, string> AliasProtocolActionDic = new Dictionary<int, string>()
        {

            {1, "AliasProtocol_Action_Insert"},
            {2, "AliasProtocol_Action_Delete"},
            {3, "AliasProtocol_Action_Deactivation"},
            {4, "AliasProtocol_Action_Reactivation"}

        };

        public static Dictionary<int, string> AliasProtocolStatusDic = new Dictionary<int, string>()
        {

            {-1, "AliasProtocol_Status_Discarded"},
            {0, "AliasProtocol_Status_Waiting"},
            {1, "AliasProtocol_Status_Authorized"}

        };
    }
}
