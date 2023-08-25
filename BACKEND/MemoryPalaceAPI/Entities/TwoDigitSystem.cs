namespace MemoryPalaceAPI.Entities
{
    public class TwoDigitSystem
    {
        public int Id { get; set; }
        public virtual List<TwoDigitElement> TwoDigitElements { get; set; }

        public int? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
    }
}
