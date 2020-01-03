using System.Threading.Tasks;
using Weredev.UI.Domain.Models.Developer;

namespace Weredev.UI.Domain.Interfaces
{
    public interface ICodeRepoService
    {
        Task<string> GetReadmeMarkdown(string repoKey);

        Task<ReleaseNotesDomainModel[]> GetReleaseHistory(string repoKey);
    }
}
