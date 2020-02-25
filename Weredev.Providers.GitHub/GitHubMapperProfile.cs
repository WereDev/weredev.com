using AutoMapper;
using Weredev.Providers.GitHub.Models;
using Weredev.Domain.Models.Developer;

namespace Weredev.Providers.GitHub
{
    internal class GitHubMapperProfile : Profile
    {
        public GitHubMapperProfile()
        {
            CreateMap<GetReleaseNotesResponse, ReleaseNotesDomainModel>();
            CreateMap<GetReleaseNotesResponse.Asset, ReleaseNotesDomainModel.Asset>();
        }
    }
}
