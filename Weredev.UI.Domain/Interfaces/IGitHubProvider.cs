using System.Threading.Tasks;
using Weredev.UI.Domain.Models.Developer;

namespace Weredev.UI.Domain.Interfaces
{
    public interface IGitHubProvider
    {
        Task<string> GetReadmeMarkdown(string repoKey);

        Task<ReleaseNotesDomainModel[]> GetReleaseInfo(string repoKey);
    }
}
