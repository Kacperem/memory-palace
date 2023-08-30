using System.ComponentModel.DataAnnotations;

namespace MemoryPalaceAPI.Models.TwoDigitSystemModels
{
    public class CreateTwoDigitSystemDto
    {
        [Required]
        [MaxLength(100)]
        [MinLength(100)]
        public List<TwoDigitElementDto> TwoDigitElements { get; set; }
    }
}
