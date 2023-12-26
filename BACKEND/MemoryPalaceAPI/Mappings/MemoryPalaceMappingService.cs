using MemoryPalaceAPI.Entities;
using MemoryPalaceAPI.Models.TwoDigitSystemModels;
using MemoryPalaceAPI.Models.UserModels;

namespace MemoryPalaceAPI.Mappings
{
    public class MemoryPalaceMappingService
    {
        public TwoDigitElementDto MapToTwoDigitElementDto(TwoDigitElement twoDigitElement)
        {
            var twoDigitElementDto = new TwoDigitElementDto
            {
                Number = twoDigitElement.Number,
                Text = twoDigitElement.Text
            };

            return twoDigitElementDto;
        }

        public TwoDigitElement MapToTwoDigitElement(TwoDigitElementDto twoDigitElementDto)
        {
            var twoDigitElement = new TwoDigitElement
            {
                Number = twoDigitElementDto.Number,
                Text = twoDigitElementDto.Text
            };

            return twoDigitElement;
        }

        public TwoDigitSystemDto MapToTwoDigitSystemDto(TwoDigitSystem twoDigitSystem)
        {
            var twoDigitSystemDto = new TwoDigitSystemDto
            {
                Id = twoDigitSystem.Id,
                CreatedById = twoDigitSystem.CreatedById,
                TwoDigitElements = new List<TwoDigitElementDto>()
            };

            foreach (var twoDigitElement in twoDigitSystem.TwoDigitElements)
            {
                var twoDigitElementDto = MapToTwoDigitElementDto(twoDigitElement);
                twoDigitSystemDto.TwoDigitElements.Add(twoDigitElementDto);
            }

            return twoDigitSystemDto;
        }
        public TwoDigitSystem MapToTwoDigitSystem(CreateTwoDigitSystemDto createTwoDigitSystemDto)
        {
            var twoDigitSystem = new TwoDigitSystem
            {

                TwoDigitElements = createTwoDigitSystemDto.TwoDigitElements
                    .Select(MapToTwoDigitElement)
                    .ToList()
            };
            

            return twoDigitSystem;
        }

        public UserDto MapToUserDto(User user)
        {
            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role?.Name
            };

            return userDto;
        }
    }
}