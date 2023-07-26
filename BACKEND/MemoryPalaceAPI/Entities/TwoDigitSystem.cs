namespace MemoryPalaceAPI.Entities
{
    public class TwoDigitSystem
    {
        public int Id { get; set; }
        public virtual List<TwoDigitElement> TwoDigitElements { get; set; }
    }
}
