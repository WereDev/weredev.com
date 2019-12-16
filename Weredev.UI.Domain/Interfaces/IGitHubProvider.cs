using System.Threading.Tasks;

namespace Weredev.UI.Domain.Interfaces
{
    public interface IGitHubProvider
    {
        Task<string> GetReadmeMarkdown(string repoKey);

        Task GetReleaseInfo(string repoKey);
    }
}
