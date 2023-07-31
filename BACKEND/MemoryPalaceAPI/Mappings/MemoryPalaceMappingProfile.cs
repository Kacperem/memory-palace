using AutoMapper;
using MemoryPalaceAPI.Entities;
using MemoryPalaceAPI.Models;

namespace MemoryPalaceAPI.Mappings
{
    public class MemoryPalaceMappingProfile : Profile
    {
        public MemoryPalaceMappingProfile() {

            CreateMap<TwoDigitElement, TwoDigitElementDto>();
            CreateMap<TwoDigitElementDto, TwoDigitElement>();

            CreateMap<TwoDigitSystem, TwoDigitSystemDto>();

            CreateMap<CreateTwoDigitSystemDto, TwoDigitSystem>();
        }
    }
}
