using Microsoft.AspNetCore.Mvc;
using EventNest.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventNest.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            var user = HttpContext.Session.GetString("user");
            if (user != "admin") return Unauthorized();

            var events = EventController.GetEvents();
            var bookings = BookingController.GetAllBookings();

            // KPIs
            ViewBag.TotalEvents = events.Count;
            ViewBag.TotalTickets = bookings.Sum(b => b.TicketCount);

            // 1. Events Per City
            var eventsPerCity = events
                .GroupBy(e => e.City)
                .ToDictionary(g => g.Key, g => g.Count());
            ViewBag.EventsPerCity = eventsPerCity;

            // 2. Bookings Per Day
            var bookingsPerDay = bookings
                .GroupBy(b => b.BookingDate.Date)
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key.ToString("yyyy-MM-dd"), g => g.Count());
            ViewBag.BookingsPerDay = bookingsPerDay;

            // 3. Tickets Per Event
            var ticketsPerEvent = bookings
                .GroupBy(b => b.EventId)
                .Select(g =>
                {
                    var ev = events.FirstOrDefault(e => e.Id == g.Key);
                    return new
                    {
                        Title = ev?.Title ?? "Unknown",
                        Tickets = g.Sum(b => b.TicketCount)
                    };
                })
                .ToDictionary(x => x.Title, x => x.Tickets);
            ViewBag.TicketsPerEvent = ticketsPerEvent;

            return View();
        }
    }
}
