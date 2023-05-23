using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using TripTrotters.Services;
using TripTrotters.Services.Abstractions;
using TripTrotters.ViewModels;


namespace TripTrotters.Controllers
{
    public class OfferController : Controller
    {
        private readonly IOfferService _offerService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApartmentService _apartmentService;

        public OfferController(IOfferService offerService, IHttpContextAccessor httpContextAccessor, IApartmentService apartmentService)
        {
            _offerService = offerService;
            _httpContextAccessor = httpContextAccessor;
            _apartmentService = apartmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!_httpContextAccessor.HttpContext.User.IsLoggedIn())
            {
                TempData["Error"] = "You must be logged in first!";
                return RedirectToAction("Login", "Account");
            }
            IEnumerable<Offer> offers = await _offerService.GetAll();
            return View(offers);

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            Offer offer = await _offerService.GetByIdAsync(id);
            return View(offer);
        }

        [HttpGet]
        [Authorize(Roles = "Agent")]
        public IActionResult Create()
        {
            if (!_httpContextAccessor.HttpContext.User.IsLoggedIn())
            {
                TempData["Error"] = "You must be logged in first!";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
                var offerViewModel = new OfferViewModel { AgentId = int.Parse(curUserId) };
                return View(offerViewModel);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> Create(OfferViewModel offerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(offerViewModel);
            }
            Apartment apartment = await _apartmentService.GetByIdAsync(offerViewModel.ApartmentId);
            Offer offer = new Offer
            {
                Title = offerViewModel.Title,
                Description = offerViewModel.Description,
                StartDate = offerViewModel.StartDate,
                EndDate = offerViewModel.EndDate,
                Price = apartment.Price * (offerViewModel.EndDate - offerViewModel.StartDate).TotalDays,
                AgentId = offerViewModel.AgentId,
                ApartmentId = offerViewModel.ApartmentId,
            };
            _offerService.Add(offer);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> Edit(int id)
        {
            Offer offer = await _offerService.GetByIdAsync(id);
            if (offer == null)
            {
                return View("Error");
            }

            Apartment apartment = await _apartmentService.GetByIdAsync(offer.ApartmentId);
            var offerViewModel = new OfferViewModel
            {
                Title = offer.Title,
                Description = offer.Description,
                StartDate = offer.StartDate,
                EndDate = offer.EndDate,
                ApartmentId = offer.ApartmentId,
                Price = apartment.Price * (offer.EndDate - offer.StartDate).TotalDays,


            };

            return View(offerViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> Edit(int id, OfferViewModel offerViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit offer");
                return View("Edit", offerViewModel);
            }
            Apartment apartment = await _apartmentService.GetByIdAsync(offerViewModel.ApartmentId);
            Offer offer = await _offerService.GetByIdAsync(id);
            if (offer == null)
            {
                return View("Error");
            }

            offer.Title = offerViewModel.Title;
            offer.Description = offerViewModel.Description;
            offer.StartDate = offerViewModel.StartDate;
            offer.EndDate = offerViewModel.EndDate;
            offer.ApartmentId = offerViewModel.ApartmentId;
            offer.Price = apartment.Price * (offer.EndDate - offer.StartDate).TotalDays;

            _offerService.Update(offer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> Delete(int id)
        {
            var offerDetails = await _offerService.GetByIdAsync(id);
            if (offerDetails == null)
            {
                return View("Error");
            }
            return View(offerDetails);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> DeleteOffer(int id)
        {
            var offerDetails = await _offerService.GetByIdAsync(id);
            if (offerDetails == null)
            {
                return View("Error");
            }
            _offerService.Delete(offerDetails);
            return RedirectToAction("Index");
        }
    }
}
