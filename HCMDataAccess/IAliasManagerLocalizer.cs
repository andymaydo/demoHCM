using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

namespace HCMDataAccess
{
    public interface IAliasManagerLocalizer
    {
        ResourceManager ResourceManager
        {
            get;
        }     
        string GetText(string key);
    }
    
}
