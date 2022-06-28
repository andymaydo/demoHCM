using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILicLimits
    {
        Task<int> GetUserCountLimit(string applianceId);
        Task<int> GetProfileCountLimit(string applianceId);

        Task SetCurrentProfileCount(string applianceId, int currProfileCount);
        Task SetCurrentUserCount(string applianceId, int currUserCount);
    }
}
