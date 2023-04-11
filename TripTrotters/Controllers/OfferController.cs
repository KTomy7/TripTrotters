using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using TripTrotters.Services.Abstractions;

namespace TripTrotters.Controllers
{
    public class OfferController : Controller
    {

        private readonly IOfferService _offerService;

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
    }
}
