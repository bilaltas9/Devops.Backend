namespace MyDotNetApi.Models
{
    public class Calculation
    {
        public int Id { get; set; }
        public double Number1 { get; set; }
        public double Number2 { get; set; }
        public double Result { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}