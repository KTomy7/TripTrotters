using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using TripTrotters.Services;
using TripTrotters.Services.Abstractions;
using TripTrotters.ViewModels;

namespace TripTrotters.Controllers
{
    public class ReviewController : Controller
    {
       
        private readonly IReviewService _reviewService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReviewController( IReviewService reviewService, IHttpContextAccessor httpContextAccessor)
        {   
            _reviewService = reviewService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var reviewViewModel = new ReviewViewModel { UserId = int.Parse(curUserId) };
            return View(reviewViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Create(ReviewViewModel reviewViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(reviewViewModel);
            }

            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            reviewViewModel.UserId = int.Parse(curUserId);
            reviewViewModel.Date = DateTime.Now;

            Review review = new Review
            {
                Description = reviewViewModel.Description,
                UserId = reviewViewModel.UserId,
                ApartmentId = reviewViewModel.ApartmentId,
                Rating = reviewViewModel.Rating,
                Date = reviewViewModel.Date,
            };

            _reviewService.Add(review);

            return RedirectToAction("Detail", "Apartment", new { id = reviewViewModel.ApartmentId }); 
        }



        public async Task<IActionResult> Edit(int id)
        {
            Review review = await _reviewService.GetByIdAsync(id);
            if (review == null)
            {
                return View("Error");
            }

            ReviewViewModel reviewViewModel = new ReviewViewModel
            {
                Description = review.Description,
                Rating = review.Rating,
                Date = DateTime.Now
            };


            return View(reviewViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ReviewViewModel reviewViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit review");
                return View("Edit", reviewViewModel);

            }
            Review review = await _reviewService.GetByIdAsync(reviewViewModel.Id);
            if (review == null)
            {
                return View("Error");
            }

            review.Description = reviewViewModel.Description;
            review.Rating = reviewViewModel.Rating;
            _reviewService.Update(review);

            return RedirectToAction("Detail", "Apartment", new { id = reviewViewModel.ApartmentId });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var reviewDetails = await _reviewService.GetByIdAsync(id);
            if (reviewDetails == null) return View("Error");
            return View(reviewDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null) return View("Error");

            _reviewService.Delete(review);
            return RedirectToAction("Detail", "Apartment", new { id = review.ApartmentId });

        }


    }
}