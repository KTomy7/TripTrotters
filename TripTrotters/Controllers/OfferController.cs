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
        private readonly IApartmentService _apartmentService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOfferViewModel offerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(offerVM);
            }

            Offer offer = new Offer
            {
                Title = offerVM.Title,
                Description = offerVM.Description,
                StartDate = offerVM.StartDate,
                EndDate = offerVM.EndDate,
                //TREBE LUAT USERU CURENT
                AgentId = 3,
                ApartmentId = offerVM.ApartmentId,

            };

            _offerService.Add(offer);

            return RedirectToAction("Index");
        }
    }
}
