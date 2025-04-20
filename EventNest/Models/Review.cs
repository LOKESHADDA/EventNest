namespace EventNest.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}