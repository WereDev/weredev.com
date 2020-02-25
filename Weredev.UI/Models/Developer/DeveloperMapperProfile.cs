using AutoMapper;
using Weredev.Domain.Models.Developer;

namespace Weredev.UI.Models.Developer
{
    public class DeveloperMapperProfile : Profile
    {
        public DeveloperMapperProfile()
        {
            CreateMap<ReleaseNotesDomainModel, ReleaseNotesResponse.Release>();
            CreateMap<ReleaseNotesDomainModel.Asset, ReleaseNotesResponse.Release.Asset>();
        }
    }
}
