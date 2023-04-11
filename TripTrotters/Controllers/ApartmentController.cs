using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using TripTrotters.Services.Abstractions;

namespace TripTrotters.Controllers
{
    public class ApartmentController : Controller
    {
        
        private readonly IApartmentService _apService;

        public ApartmentController( IApartmentService apService)
        {
            _apService = apService;
        }
        public async Task<IActionResult> Index() 

        { IEnumerable<Apartment> apartments =  await _apService.GetAll();
            return View(apartments);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Apartment apartment  = await _apService.GetByIdAsync(id);
            return View(apartment);
        }
    }
}
