using System;
using System.Threading.Tasks;
using Weredev.UI.Domain.Interfaces;
using Weredev.UI.Domain.Models.Developer;

namespace Weredev.UI.Domain.Services
{
    public class CodeRepoService : ICodeRepoService
    {
        private readonly IGitHubProvider _gitHubProvider;

        public CodeRepoService(IGitHubProvider gitHubProvider)
        {
            _gitHubProvider = gitHubProvider ?? throw new ArgumentNullException(nameof(gitHubProvider));
        }

        public async Task<string> GetReadmeMarkdown(string repoKey)
        {
            if (string.IsNullOrWhiteSpace(repoKey))
                throw new ArgumentNullException(nameof(repoKey));

            return await _gitHubProvider.GetReadmeMarkdown(repoKey);
        }

        public async Task<ReleaseNotesDomainModel[]> GetReleaseHistory(string repoKey)
        {
            if (string.IsNullOrWhiteSpace(repoKey))
                throw new ArgumentNullException(nameof(repoKey));

            return await _gitHubProvider.GetReleaseInfo(repoKey);
        }
    }
}
