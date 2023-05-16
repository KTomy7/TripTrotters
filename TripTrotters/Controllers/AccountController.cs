using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using TripTrotters.Services;
using TripTrotters.Services.Abstractions;
using TripTrotters.ViewModels;

namespace TripTrotters.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ICloudinaryImageService _cloudinaryImageService;
    
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ICloudinaryImageService cloudinaryImageService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cloudinaryImageService = cloudinaryImageService;

        }
        
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
            
            if (user != null)
            {
                // User is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                // Password is incorrect
                TempData["Error"] = "Wrong credentials! Please try again.";
                return View(loginViewModel);
            }
            // User not found 
            TempData["Error"] = "Wrong credentials! Please try again.";
            return View(loginViewModel);
        }
        
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _cloudinaryImageService.AddPhotoAsync(registerViewModel.Image);


                var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
                if (user != null)
                {
                    TempData["Error"] = "This email address already exists!";
                    return View(registerViewModel);
                }

                var newUser = new User()
                {
                    UserName = registerViewModel.Username,
                    Email = registerViewModel.EmailAddress,
                    ImageUrl = result.Url.ToString()
                };

                var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);
                if (newUserResponse.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, registerViewModel.UserRole.ToString());
                }
                return RedirectToAction("Index", "Home");
            }
            else
     
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
