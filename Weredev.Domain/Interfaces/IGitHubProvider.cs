using System.Threading.Tasks;
using Weredev.Domain.Models.Developer;

namespace Weredev.Domain.Interfaces
{
    public interface IGitHubProvider
    {
        Task<string> GetReadmeMarkdown(string repoKey);

        Task<ReleaseNotesDomainModel[]> GetReleaseInfo(string repoKey);
    }
}
