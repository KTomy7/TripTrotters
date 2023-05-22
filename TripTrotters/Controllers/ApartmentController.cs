using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using TripTrotters.Services;
using TripTrotters.Services.Abstractions;
using TripTrotters.ViewModels;

namespace TripTrotters.Controllers
{
    public class ApartmentController : Controller
    {
        
        private readonly IApartmentService _apartmentService;
        private readonly IAddressService _addressService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;
        private readonly IReviewService _reviewService;



        public ApartmentController( IApartmentService apService, IAddressService addressService, IHttpContextAccessor httpContextAccessor, IPhotoService photoService, IReviewService reviewService)
        {
            _apartmentService = apService;
            _addressService = addressService;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
            _reviewService = reviewService;

        }

      
        public async Task<IActionResult> Index() 

        { IEnumerable<Apartment> apartments =  await _apartmentService.GetAll();
            foreach (Apartment apartment in apartments)
            {
                var reviews = await _reviewService.GetAllByApartmentId(apartment.Id);
                apartment.Reviews = reviews.ToList();
            }
            return View(apartments);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Apartment apartment  = await _apartmentService.GetByIdAsync(id);
            var reviews = await _reviewService.GetAllByApartmentId(id);
            apartment.Reviews = reviews.ToList();

            return View(apartment);
        }

        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var apartmentViewModel = new CreateApartmentViewModel { OwnerId = int.Parse(curUserId) };
            return View(apartmentViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateApartmentViewModel apartmentVM)
        { 
            if(ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(apartmentVM.Image);
               // var Images  = result.Url.ToString().Split('/');
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
                    OwnerId = apartmentVM.OwnerId,
                    Image = result.Url.ToString()
                };

                _apartmentService.Add(apartment);

               // Image image = new Image
                //{
                   // Location = result.Url.ToString(),
                   // ApartmentId = apartmentVM.Id,
               // };

            

                return RedirectToAction("Index");
                //return View(apartmentVM);
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(apartmentVM);
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
                Country = apartment.Address.Country,
                City = apartment.Address.City,
                Street = apartment.Address.Street,
                StreetNumber = apartment.Address.StreetNumber
            };


            return View(apartmentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditApartmentViewModel apartmentVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit apartment");
                return View("Edit", apartmentVM);

                
                var userApartment = await _apartmentService.GetByIdAsync(id);
               // if(userApartment != null)
                  //  try
                    {
                        //await _phtotService.DeletePhotoAsync(userApartment.Image);

                    }
                  //  catch (Exception ex)
                    {
                   //     ModelState.AddModelError("", "Could not delete photo");

                    }
                   // return View("Error");
            }
            Apartment apartment = await _apartmentService.GetByIdAsync(apartmentVM.Id);
            if (apartment == null)
                return View("Error");
            apartment.Title = apartmentVM.Title;
            apartment.Description = apartmentVM.Description;
            apartment.Price = apartmentVM.Price;
            apartment.Address.Country = apartmentVM.Country;
            apartment.Address.City = apartmentVM.City;
            apartment.Address.Street = apartmentVM.Street;
            apartment.Address.StreetNumber = apartmentVM.StreetNumber;
            _apartmentService.Update(apartment);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var apartmentDetails = await _apartmentService.GetByIdAsync(id);
            if (apartmentDetails == null) return View("Error");
            return View(apartmentDetails); 
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            var apartmentDetails = await _apartmentService.GetByIdAsync(id);
            if (apartmentDetails == null) return View("Error");

            _apartmentService.Delete(apartmentDetails);
            return RedirectToAction("Index");

        }
    } 

}
