using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCM.Resources;

namespace HCM.Options
{
    public class SyncfusionLocalizer : ISyncfusionStringLocalizer
    {

        public string GetText(string key)
        {
            return ResourceManager.GetString(key);
        }

        public System.Resources.ResourceManager ResourceManager
        {
            get
            {
                return SfResources.ResourceManager;
            }
        }
    }
}
