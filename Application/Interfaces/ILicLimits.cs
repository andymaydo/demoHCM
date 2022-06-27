using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILicLimits
    {
        Task<int> UserCount(string applianceId);
        Task<int> ProfileCount(string applianceId);
    }
}
