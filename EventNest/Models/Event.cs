namespace EventNest.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public string PosterUrl { get; set; }
        public string Description { get; set; }
    }
}
 