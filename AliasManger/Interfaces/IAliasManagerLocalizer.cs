using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

namespace AliasManger.Interfaces
{
    public interface IAliasManagerLocalizer
    {
        Type ResourceSetType
        {
            get;
        }

        ResourceManager ResourceManager
        {
            get;
        }
        string GetText(string key);
        
    }

    
}
