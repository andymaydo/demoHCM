using HCM.Resources;
using HCMDataAccess;
namespace HCM.Options
{

    public class AliasManagerLocalizer : IAliasManagerLocalizer
    {

        public string GetText(string key)
        {
            return this.ResourceManager.GetString(key);
        }

        public System.Resources.ResourceManager ResourceManager
        {
            get
            {
                return AmResource.ResourceManager;
            }
        }
    }
}
