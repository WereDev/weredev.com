using System.Threading.Tasks;
using Weredev.Domain.Models.Developer;

namespace Weredev.Domain.Interfaces
{
    public interface ICodeRepoService
    {
        Task<string> GetReadmeMarkdown(string repoKey);

        Task<ReleaseNotesDomainModel[]> GetReleaseHistory(string repoKey);
    }
}
