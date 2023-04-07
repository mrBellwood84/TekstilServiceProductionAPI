namespace Simulator.Models
{
    public class ProductionData
    {
        public Guid Id { get; set; }
        public Machine Machine { get; set; }
        public DateOnly Date { get; set; }
        public int CurrentValue { get; set; } = 0;
        public int YellowTarget { get; set; }
        public int GreenTarget { get; set; }
        public int MaxTarget { get; set; }
    }
}
