using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using TripTrotters.Services.Abstractions;
using TripTrotters.ViewModels;

namespace TripTrotters.Controllers
{
    public class OfferController : Controller
    {

        private readonly IOfferService _offerService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OfferController(IOfferService offerService, IHttpContextAccessor httpContextAccessor)
        {
            _offerService = offerService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Offer> offers = await _offerService.GetAll();
            return View(offers);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Offer offer = await _offerService.GetByIdAsync(id);
            return View(offer);
        }

        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var offerViewModel = new OfferViewModel { AgentId = int.Parse(curUserId) };
            return View(offerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OfferViewModel offerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(offerViewModel);
            }

            Offer offer = new Offer
            {
                Title = offerViewModel.Title,
                Description = offerViewModel.Description,
                StartDate = offerViewModel.StartDate,
                EndDate = offerViewModel.EndDate,
                AgentId = offerViewModel.AgentId,
                ApartmentId = offerViewModel.ApartmentId,

            };

            _offerService.Add(offer);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Offer offer = await _offerService.GetByIdAsync(id);
            if(offer == null)
            {
                return View("Error");
            }
            var offerViewModel = new OfferViewModel
            {
                /*Title = offer.Title,*/
                Description = offer.Description,
                StartDate = offer.StartDate,
                EndDate = offer.EndDate,
                ApartmentId = offer.ApartmentId,

            };

            return View(offerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, OfferViewModel offerViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit offer");
                return View("Edit", offerViewModel);
            }

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

            _offerService.Update(offer);
            return RedirectToAction("Index");
        }


        [HttpGet]
         public async Task<IActionResult> Delete(int id)
        {
            var offerDetails = await _offerService.GetByIdAsync(id);
            if (offerDetails == null) return View("Error");
            return View(offerDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteOffer(int id)
        {
            var offerDetails = await _offerService.GetByIdAsync(id);
            
            if (offerDetails == null) return View("Error");

            _offerService.Delete(offerDetails);
            return RedirectToAction("Index");

        }
    }

}
