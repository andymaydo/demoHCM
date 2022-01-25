using HCM.Resources;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace HCM.Options
{
    public class CommonLocalizationService
    {
        
            public IStringLocalizer Str { get; set; }
            public CommonLocalizationService(IStringLocalizerFactory factory)
            {
                var assemblyName = new AssemblyName(typeof(SharedResources).GetTypeInfo().Assembly.FullName);
                Str = factory.Create(nameof(SharedResources), assemblyName.Name);
            }

            public string Get(string key)
            {
                return Str[key];
            }
        
    }
}
