using MemoryPalaceAPI.Entities;

namespace MemoryPalaceAPI.Models.TwoDigitSystemModels
{
    public class TwoDigitSystemDto
    {
        public int Id { get; set; }
        public int? CreatedById { get; set; }
        public List<TwoDigitElementDto> TwoDigitElements { get; set; }
    }
}
