using Microsoft.AspNetCore.Mvc;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using TripTrotters.Services.Abstractions;
using TripTrotters.ViewModels;
using System.Net;
using TripTrotters.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Authorization;

namespace TripTrotters.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICloudinaryImageService _cloudinaryImageService;
        private readonly IImageService _imageService;
        private readonly IUserPostLikeService _userPostLikeService;
        private readonly IUserCommentLikeService _userCommentLikeService;


        public PostController(IPostService postService, ICommentService commentService, IHttpContextAccessor httpContextAccessor, ICloudinaryImageService cloudinaryImageService, IImageService imageService, IUserPostLikeService userPostLikeService, IUserCommentLikeService userCommentLikeService)
        {
            _postService = postService;
            _commentService = commentService;
            _httpContextAccessor = httpContextAccessor;
            _cloudinaryImageService = cloudinaryImageService;
            _imageService = imageService;
            _userPostLikeService = userPostLikeService;
            _userCommentLikeService = userCommentLikeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!_httpContextAccessor.HttpContext.User.IsLoggedIn())
            {
                TempData["Error"] = "You must be logged in first!";
                return RedirectToAction("Login", "Account");
            }
            IEnumerable<Post> posts = await _postService.GetAll();
            foreach (Post post in posts)
            {
                post.Comments = _commentService.GetAllByPostId(post.Id).ToList();
            }

            posts = posts.OrderByDescending(post => post.Date);

            return View(posts);

        }
        [HttpPost]
        public async Task<IActionResult> Index(string userName)
        {
            if (!_httpContextAccessor.HttpContext.User.IsLoggedIn())
            {
                TempData["Error"] = "You must be logged in first!";
                return RedirectToAction("Login", "Account");
            }

            IEnumerable<Post> posts;
            
            posts = await _postService.GetAllbyUser(userName);

            if(!posts.Any())
            {
                posts = await _postService.GetAll();
            }
            
            foreach (Post post in posts)
            {
                post.Comments = _commentService.GetAllByPostId(post.Id).ToList();
            }

            posts = posts.OrderByDescending(post => post.Date);

            return View(posts);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            Post post = await _postService.GetByIdAsync(id);
            return View(post);
        }

        [HttpGet]
        [Authorize(Roles = "Traveller")]
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
                var postViewModel = new CreatePostViewModel { UserId = int.Parse(curUserId) };
                return View(postViewModel);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Traveller")]
        public async Task<IActionResult> Create(CreatePostViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post
                {
                    Title = createViewModel.Title,
                    Description = createViewModel.Description,
                    ApartmentId = createViewModel.ApartmentId,
                    Budget = createViewModel.Budget,
                    Date = DateTime.Now,
                    Likes = 0,
                    UserId = createViewModel.UserId
                };
                _postService.Add(post);

                foreach (var item in createViewModel.Images)
                {
                    var result = await _cloudinaryImageService.AddPhotoAsync(item);
                    Image image = new Image
                    {
                        ImageUrl = result.Url.ToString(),
                        PostId = post.Id
                    };
                    _imageService.Add(image);
                }

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(createViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Traveller")]
        public async Task<IActionResult> Edit(int id)
        {
            Post post = await _postService.GetByIdAsync(id);
            if (post == null)
            {
                return View("Error");
            }

            var postViewModel = new EditPostViewModel
            {
                Title = post.Title,
                Description = post.Description,
                Budget = post.Budget,
                ApartmentId = post.ApartmentId,
                ImageUrls = new List<string>()
            };
            var images = await _imageService.GetAllImagesByPostId(post.Id);
            foreach (var image in images)
            {
                postViewModel.ImageUrls.Add(image.ImageUrl);
            }
            return View(postViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Traveller")]
        public async Task<IActionResult> Edit(int id, EditPostViewModel editPostViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit post!");
                return View("Edit", editPostViewModel);
            }
            Post post = await _postService.GetByIdAsync(editPostViewModel.Id);
            if (post == null)
            {
                return View("Error");
            }
            post.Title = editPostViewModel.Title;
            post.Description = editPostViewModel.Description;
            post.Budget = editPostViewModel.Budget;
            _postService.Update(post);

            if (editPostViewModel.NewImages != null)
            {
                foreach (var item in editPostViewModel.NewImages)
                {
                    var result = await _cloudinaryImageService.AddPhotoAsync(item);
                    Image image = new Image
                    {
                        ImageUrl = result.Url.ToString(),
                        PostId = post.Id
                    };
                    _imageService.Add(image);
                }
            }
            
            if (editPostViewModel.ImagesToDelete != null)
            {
                foreach (var imageUrl in editPostViewModel.ImagesToDelete)
                {
                    try
                    {
                        await _cloudinaryImageService.DeletePhotoAsync(imageUrl);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Couldn't delete photo!");
                        return View("Edit", editPostViewModel);
                    }
                    var image = await _imageService.GetImageByUrl(imageUrl);
                    _imageService.Delete(image);
                }
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> UpdateLike(int id, EditPostViewModel editPostViewModel)
        {
            Post post = await _postService.GetByIdAsync(editPostViewModel.Id);
            var curUserId = int.Parse(_httpContextAccessor.HttpContext.User.GetUserId());
            bool likedByUser = _userPostLikeService.PostLikedByUser(curUserId, post.Id);

            if (likedByUser)
            {
                post.Likes--;
                var userLike = _userPostLikeService.GetByUserAndPostId(curUserId, post.Id);
                _userPostLikeService.Delete(userLike);
            }
            else
            {
                post.Likes++;
                var newLike = new UserPostLike
                {
                    UserId = curUserId,
                    PostId = post.Id,
                };
                _userPostLikeService.Add(newLike);
            }
            _postService.Update(post);

            return RedirectToAction("Index", "Post");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var postDetails = await _postService.GetByIdAsync(id);
            if (postDetails == null)
            {
                return View("Error");
            }
            return View(postDetails);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Traveller")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var postDetails = await _postService.GetByIdAsync(id);

            if (postDetails == null)
            {
                return View("Error");
            }

            var images = await _imageService.GetAllImagesByPostId(id);
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
                        return View("Delete", postDetails);
                    }
                    _imageService.Delete(image);
                }
            }
            _postService.Delete(postDetails);
            return RedirectToAction("Index");
        }
    }
}
