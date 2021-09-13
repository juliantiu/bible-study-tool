using System;
using System.Threading.Tasks;

namespace BibleStudyTool.Core.Interfaces
{
    public interface ITokenClaimsService
    {
        Task<string> GetTokenAsync(string userName);
    }
}
