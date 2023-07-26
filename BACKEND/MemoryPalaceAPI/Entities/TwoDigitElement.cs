namespace MemoryPalaceAPI.Entities
{
    public class TwoDigitElement
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Text { get; set; }

        public int TwoDigitSystemId { get; set; }
        public virtual TwoDigitSystem TwoDigitSystem { get; set; }
    }
}
