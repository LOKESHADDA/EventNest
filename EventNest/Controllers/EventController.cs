using Microsoft.AspNetCore.Mvc;
using EventNest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
 
namespace EventNest.Controllers
{
    public class EventController : Controller
    {
        private static List<Event> _events = new()
        {
            // 🎬 Movies
            new Event { Id = 1, Title = "Inception", Category = "Movies", City = "Mumbai", Venue = "PVR Juhu", Date = DateTime.Parse("2025-05-01"), Price = 300, PosterUrl = "/img/inception.png", Description = "Sci-fi thriller by Christopher Nolan" },
            new Event { Id = 2, Title = "Dunki", Category = "Movies", City = "Pune", Venue = "INOX Phoenix", Date = DateTime.Parse("2025-05-04"), Price = 280, PosterUrl = "/img/dunki.jpg", Description = "Drama-comedy by Rajkumar Hirani" },
            new Event { Id = 3, Title = "Oppenheimer", Category = "Movies", City = "Delhi", Venue = "DT Cinemas", Date = DateTime.Parse("2025-05-06"), Price = 350, PosterUrl = "/img/oppenheimer.jpg", Description = "Historical thriller by Nolan" },
 
            // 🏏 Sports
            new Event { Id = 4, Title = "IPL Finals", Category = "Sports", City = "Ahmedabad", Venue = "Narendra Modi Stadium", Date = DateTime.Parse("2025-05-07"), Price = 2000, PosterUrl = "/img/ipl.jpg", Description = "Cricket final extravaganza" },
            new Event { Id = 5, Title = "India vs Australia", Category = "Sports", City = "Chennai", Venue = "Chepauk Stadium", Date = DateTime.Parse("2025-05-10"), Price = 1800, PosterUrl = "/img/indvsaus.jpg", Description = "T20 international showdown" },
 
            // 📺 Streams
            new Event { Id = 6, Title = "Coldplay Live", Category = "Stream", City = "Online", Venue = "YouTube", Date = DateTime.Parse("2025-05-02"), Price = 150, PosterUrl = "/img/coldplay.jpg", Description = "Live concert stream by Coldplay" },
            new Event { Id = 7, Title = "Netflix Stand-up Special", Category = "Stream", City = "Online", Venue = "Netflix", Date = DateTime.Parse("2025-05-05"), Price = 200, PosterUrl = "/img/standup.jpg", Description = "Comedy night live" },
 
            // 🎭 Plays
            new Event { Id = 8, Title = "Hamlet", Category = "Plays", City = "Kolkata", Venue = "Nandan Theatre", Date = DateTime.Parse("2025-05-09"), Price = 400, PosterUrl = "/img/hamlet.jpg", Description = "Shakespeare's timeless tragedy" },
 
            // 💪 Activities
            new Event { Id = 9, Title = "Yoga for Beginners", Category = "Activities", City = "Bangalore", Venue = "Cubbon Park", Date = DateTime.Parse("2025-05-03"), Price = 100, PosterUrl = "/img/yoga.jpg", Description = "Start your wellness journey" },
            new Event { Id = 10, Title = "Art Workshop", Category = "Activities", City = "Pune", Venue = "Art Hub", Date = DateTime.Parse("2025-05-06"), Price = 150, PosterUrl = "/img/art.jpg", Description = "Paint, sculpt, and create" },
 
            // 🎤 Other Events
            new Event { Id = 11, Title = "Open Mic Night", Category = "Event", City = "Hyderabad", Venue = "Phoenix Arena", Date = DateTime.Parse("2025-05-04"), Price = 250, PosterUrl = "/img/openmic.jpg", Description = "Talent showcase for new performers" }
        };
 
        public IActionResult Browse()
        {
            var grouped = _events
                .GroupBy(e => e.Category)
                .ToDictionary(g => g.Key, g => g.ToList());
 
            ViewBag.GroupedEvents = grouped;
            return View();
        }
 
        public IActionResult Manage()
        {
            var user = HttpContext.Session.GetString("user");
            if (user != "admin") return Unauthorized();
            return View(_events);
        }
 
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("user") != "admin") return Unauthorized();
            return View();
        }
 
        [HttpPost]
        public IActionResult Create(Event evnt)
        {
            evnt.Id = _events.Max(e => e.Id) + 1;
            _events.Add(evnt);
            return RedirectToAction("Manage");
        }
 
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("user") != "admin") return Unauthorized();
            var evnt = _events.FirstOrDefault(e => e.Id == id);
            return View(evnt);
        }
 
        [HttpPost]
        public IActionResult Edit(Event evnt)
        {
            if (HttpContext.Session.GetString("user") != "admin") return Unauthorized();
            var existing = _events.FirstOrDefault(e => e.Id == evnt.Id);
            if (existing == null) return NotFound();
 
            existing.Title = evnt.Title;
            existing.Category = evnt.Category;
            existing.City = evnt.City;
            existing.Date = evnt.Date;
            existing.Venue = evnt.Venue;
            existing.Price = evnt.Price;
            existing.PosterUrl = evnt.PosterUrl;
            existing.Description = evnt.Description;
 
            return RedirectToAction("Manage");
        }
 
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("user") != "admin") return Unauthorized();
            var evnt = _events.FirstOrDefault(e => e.Id == id);
            return View(evnt);
        }
 
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("user") != "admin") return Unauthorized();
            var evnt = _events.FirstOrDefault(e => e.Id == id);
            if (evnt != null) _events.Remove(evnt);
            return RedirectToAction("Manage");
        }
 
        public IActionResult Details(int id)
        {
            var evnt = _events.FirstOrDefault(e => e.Id == id);
            return View(evnt);
        }
 
        public static List<Event> GetEvents() => _events;
    }
}
 
 