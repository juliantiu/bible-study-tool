using System;
using System.Threading.Tasks;

namespace BibleStudyTool.Core.NonEntityInterfaces
{
    public interface ITokenClaimsService
    {
        Task<string> GetTokenAsync(string userName);
    }
}
