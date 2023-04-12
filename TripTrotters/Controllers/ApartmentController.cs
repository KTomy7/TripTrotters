using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using TripTrotters.Services.Abstractions;
using TripTrotters.ViewModels;

namespace TripTrotters.Controllers
{
    public class ApartmentController : Controller
    {
        
        private readonly IApartmentService _apartmentService;
        private readonly IAddressService _addressService;
        

        public ApartmentController( IApartmentService apService, IAddressService addressService)
        {
            _apartmentService = apService;
            _addressService = addressService;   
        }

      
        public async Task<IActionResult> Index() 

        { IEnumerable<Apartment> apartments =  await _apartmentService.GetAll();
            return View(apartments);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Apartment apartment  = await _apartmentService.GetByIdAsync(id);
            return View(apartment);
        }

        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateApartmentViewModel apartmentVM)
        { 
            if(!ModelState.IsValid)
            {
                return View(apartmentVM);
            }

            Address address = new Address
            {   
                Country = apartmentVM.Country,
                City = apartmentVM.City,
                Street = apartmentVM.Street,
                StreetNumber = apartmentVM.StreetNumber,
               
            };



            _addressService.Add(address); 


            Apartment apartment = new Apartment
            {
                Title = apartmentVM.Title,
                Description = apartmentVM.Description,
                Price = apartmentVM.Price,
                AddressId = address.Id,
                Address = address,
                
      
            };

            _apartmentService.Add(apartment);


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Apartment apartment = await _apartmentService.GetByIdAsync(id);
            if (apartment == null)
             return View("Error");
            var apartmentVM = new EditApartmentViewModel
            {
                Title = apartment.Title,
                Description = apartment.Description,
                Price = apartment.Price,
                AddressId = apartment.AddressId,
                Address = apartment.Address,
                //URL = apartment.Image;
                 Reviews = apartment.Reviews,
                 Offers = apartment.Offers,
                 Posts = apartment.Posts,

            };


            return View(apartmentVM);
        }
    }
}
