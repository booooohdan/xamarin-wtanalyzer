using System.Threading.Tasks;

namespace WTAnalyzer
{
    public interface IAppTracking
    {
        Task<bool> IsAuthorized();
        Task<bool> RequestAuthorizationAsync();
    }
}
