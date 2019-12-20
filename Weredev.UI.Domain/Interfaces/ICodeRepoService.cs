using System.Threading.Tasks;

namespace Weredev.UI.Domain.Interfaces
{
    public interface ICodeRepoService
    {
        Task<string> GetReadmeMarkdown(string repoKey);
    }
}
