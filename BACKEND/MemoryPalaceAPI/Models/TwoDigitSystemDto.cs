using MemoryPalaceAPI.Entities;

namespace MemoryPalaceAPI.Models
{
    public class TwoDigitSystemDto
    {
        public int Id { get; set; }
        public List<TwoDigitElementDto> TwoDigitElements { get; set; }
    }
}
