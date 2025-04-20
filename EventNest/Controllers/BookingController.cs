using Microsoft.AspNetCore.Mvc;
using EventNest.Models;
using System;
using System.Collections.Generic;

namespace EventNest.Controllers
{
    public class BookingController : Controller
    {
        private static List<Booking> _bookings = new();

        public IActionResult Create(int eventId)
        {
            ViewBag.EventId = eventId;
            return View();
        }

        [HttpPost]
        public IActionResult Create(int eventId, string userName, int ticketCount)
        {
            var booking = new Booking
            {
                Id = _bookings.Count + 1,
                EventId = eventId,
                UserName = userName,
                TicketCount = ticketCount,
                BookingDate = DateTime.Now
            };

            _bookings.Add(booking);
            return RedirectToAction("Confirm", new { id = booking.Id });
        }

        public IActionResult Confirm(int id)
        {
            var booking = _bookings.Find(b => b.Id == id);
            var evnt = EventController.GetEvents().Find(e => e.Id == booking.EventId);

            ViewBag.Event = evnt;
            return View(booking);
        }

        public static int GetTotalTickets()
        {
            int total = 0;
            foreach (var b in _bookings)
            {
                total += b.TicketCount;
            }
            return total;
        }

        public static List<Booking> GetAllBookings() => _bookings;
    }
}