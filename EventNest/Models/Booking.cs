namespace EventNest.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string UserName { get; set; }
        public int TicketCount { get; set; }
        public DateTime BookingDate { get; set; }
    }
}