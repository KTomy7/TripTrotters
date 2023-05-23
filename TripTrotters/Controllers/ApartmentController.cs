using Microsoft.AspNetCore.Authorization;
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
        private readonly ICloudinaryImageService _cloudinaryImageService;
        private readonly IImageService _imageService;
        private readonly IReviewService _reviewService;
        

        public ApartmentController( IApartmentService apService, IAddressService addressService, IHttpContextAccessor httpContextAccessor, ICloudinaryImageService cloudinaryImageService, IImageService imageService, IReviewService reviewService )
        {
            _apartmentService = apService;
            _addressService = addressService;
            _httpContextAccessor = httpContextAccessor;
            _cloudinaryImageService = cloudinaryImageService;
            _imageService = imageService;
            _reviewService = reviewService; 

        }

        [HttpGet]
        public async Task<IActionResult> Index() 
        {
            if (!_httpContextAccessor.HttpContext.User.IsLoggedIn())
            {
                TempData["Error"] = "You must be logged in first!";
                return RedirectToAction("Login", "Account");
            }
            IEnumerable<Apartment> apartments = await _apartmentService.GetAll();
            foreach (Apartment apartment in apartments)
            {
                var reviews = await _reviewService.GetAllByApartmentId(apartment.Id);
                apartment.Reviews = reviews.ToList();
            }
            return View(apartments);
        }

        [HttpGet]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> MyApartments()
        {
            if (!_httpContextAccessor.HttpContext.User.IsLoggedIn())
            {
                TempData["Error"] = "You must be logged in first!";
                return RedirectToAction("Login", "Account");
            }
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.GetUserId());
            IEnumerable<Apartment> apartments = await _apartmentService.GetByUserIdAsync(userId);
            foreach (Apartment apartment in apartments)
            {
                var reviews = await _reviewService.GetAllByApartmentId(apartment.Id);
                apartment.Reviews = reviews.ToList();
            }
            return View(apartments);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            Apartment apartment  = await _apartmentService.GetByIdAsync(id);
            var reviews = await _reviewService.GetAllByApartmentId(id);
            apartment.Reviews = reviews.ToList();

            return View(apartment);
        }

        [HttpGet]
        [Authorize(Roles = "Owner")]
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
                var apartmentViewModel = new CreateApartmentViewModel { OwnerId = int.Parse(curUserId) };
                return View(apartmentViewModel);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Create(CreateApartmentViewModel apartmentVM)
        { 
            if(ModelState.IsValid)
            {
                Address address = new Address
                {
                    Country = apartmentVM.Country,
                    City = apartmentVM.City,
                    Street = apartmentVM.Street,
                    StreetNumber = apartmentVM.StreetNumber
                };
                _addressService.Add(address);

                Apartment apartment = new Apartment
                {
                    Title = apartmentVM.Title,
                    Description = apartmentVM.Description,
                    Price = apartmentVM.Price,
                    AddressId = address.Id,
                    Address = address,
                    OwnerId = apartmentVM.OwnerId
                };
                _apartmentService.Add(apartment);

                foreach (var item in apartmentVM.Images)
                {
                    var result = await _cloudinaryImageService.AddPhotoAsync(item);
                    Image image = new Image
                    {
                        ImageUrl = result.Url.ToString(),
                        ApartmentId = apartment.Id
                    };
                   _imageService.Add(image);
                }

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(apartmentVM);
        }

        [HttpGet]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Edit(int id)
        {
            Apartment apartment = await _apartmentService.GetByIdAsync(id);
            if (apartment == null)
            {
                return View("Error");
            }
            var apartmentVM = new EditApartmentViewModel
            {
                Title = apartment.Title,
                Description = apartment.Description,
                Price = apartment.Price,
                AddressId = apartment.AddressId,
                Country = apartment.Address.Country,
                City = apartment.Address.City,
                Street = apartment.Address.Street,
                StreetNumber = apartment.Address.StreetNumber,
                ImageUrls = new List<string>()
            };
            var images = await _imageService.GetAllImagesByApartmentId(id);
            foreach (var image in images)
            {
                apartmentVM.ImageUrls.Add(image.ImageUrl);
            }

            return View(apartmentVM);
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Edit(int id, EditApartmentViewModel apartmentVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit apartment");
                return View("Edit", apartmentVM);
            }
            Apartment apartment = await _apartmentService.GetByIdAsync(apartmentVM.Id);
            if (apartment == null)
            {
                return View("Error");
            }
            apartment.Title = apartmentVM.Title;
            apartment.Description = apartmentVM.Description;
            apartment.Price = apartmentVM.Price;
            apartment.Address.Country = apartmentVM.Country;
            apartment.Address.City = apartmentVM.City;
            apartment.Address.Street = apartmentVM.Street;
            apartment.Address.StreetNumber = apartmentVM.StreetNumber;
            _apartmentService.Update(apartment);

            if (apartmentVM.NewImages != null)
            {
                foreach (var item in apartmentVM.NewImages)
                {
                    var result = await _cloudinaryImageService.AddPhotoAsync(item);
                    Image image = new Image
                    {
                        ImageUrl = result.Url.ToString(),
                        ApartmentId = apartment.Id
                    };
                    _imageService.Add(image);
                }
            }
            if (apartmentVM.ImagesToDelete != null)
            {
                foreach (var imageUrl in apartmentVM.ImagesToDelete)
                {
                    try
                    {
                        await _cloudinaryImageService.DeletePhotoAsync(imageUrl);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Couldn't delete photo!");
                        return View("Edit", apartmentVM);
                    }
                    var image = await _imageService.GetImageByUrl(imageUrl);
                    _imageService.Delete(image);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Delete(int id)
        {
            var apartmentDetails = await _apartmentService.GetByIdAsync(id);
            if (apartmentDetails == null) {
                return View("Error");
            }
            return View(apartmentDetails); 
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            var apartmentDetails = await _apartmentService.GetByIdAsync(id);
            if (apartmentDetails == null)
            {
                return View("Error");
            }

            
            var images = await _imageService.GetAllImagesByApartmentId(id);
            if (images != null)
            {
                foreach (var image in images)
                {
                    try
                    {
                        await _cloudinaryImageService.DeletePhotoAsync(image.ImageUrl);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Couldn't delete photo!");
                        return View("Delete", apartmentDetails);
                    }
                    _imageService.Delete(image);
                }
            }

            _apartmentService.Delete(apartmentDetails);
            return RedirectToAction("Index");
        }
    } 
}
