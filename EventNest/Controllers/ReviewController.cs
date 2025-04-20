using Microsoft.AspNetCore.Mvc;
using EventNest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
 
namespace EventNest.Controllers
{
    public class ReviewController : Controller
    {
        private static List<Review> _reviews = new();
 
        public IActionResult Add(int eventId)
        {
            ViewBag.EventId = eventId;
            return View();
        }
 
        [HttpPost]
        public IActionResult Add(int eventId, string userName, int rating, string comment)
        {
            _reviews.Add(new Review
            {
                Id = _reviews.Count + 1,
                EventId = eventId,
                UserName = userName,
                Rating = rating,
                Comment = comment,
                ReviewDate = DateTime.Now
            });
 
            return RedirectToAction("View", new { eventId = eventId });
        }
 
        public IActionResult View(int eventId)
        {
            var reviews = _reviews.Where(r => r.EventId == eventId).ToList();
            ViewBag.EventId = eventId;
            return View(reviews);
        }
 
        public static List<Review> GetReviews() => _reviews;
    }
}