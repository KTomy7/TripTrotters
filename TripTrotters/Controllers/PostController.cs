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

namespace TripTrotters.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IApartmentService _apartmentService;
        private readonly ICommentService _commentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICloudinaryImageService _cloudinaryImageService;
        private readonly IImageService _imageService;
        private readonly IUserPostLikeService _userPostLikeService;

        public PostController(IPostService postService, IApartmentService apartmentService, ICommentService commentService, IHttpContextAccessor httpContextAccessor, ICloudinaryImageService cloudinaryImageService, IImageService imageService, IUserPostLikeService userPostLikeService)

        {
            _postService = postService;
            _apartmentService = apartmentService;
            _commentService = commentService;
            _httpContextAccessor = httpContextAccessor;
            _cloudinaryImageService = cloudinaryImageService;   
            _imageService = imageService; 
            _userPostLikeService = userPostLikeService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Post> posts = await _postService.GetAll();
            foreach (Post post in posts)
            {
                post.Comments =  _commentService.GetAllByPostId(post.Id).ToList();
            }

            return View(posts);

        }

        public async Task<IActionResult> Detail(int id)
        {
            Post post = await _postService.GetByIdAsync(id);
            return View(post);
        }


        public IActionResult Create()
        {
            var currentUI = _httpContextAccessor.HttpContext.User.GetUserId();
            var postViewModel = new CreatePostViewModel { UserId = int.Parse(currentUI) };
            return View(postViewModel);
        }

        [HttpPost]
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
                ApartmentId = post.ApartmentId,
            };


            return View(postViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditPostViewModel editPostViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit post");
                return View("Edit", editPostViewModel);

            }
            Post post = await _postService.GetByIdAsync(editPostViewModel.Id);
            if (post == null)
                return View("Error");
            post.Title = editPostViewModel.Title;
            post.Description = editPostViewModel.Description;

            _postService.Update(post);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLike(int id, EditPostViewModel editPostViewModel)
        {
            Post post = await _postService.GetByIdAsync(editPostViewModel.Id);
            var currentUId = int.Parse(_httpContextAccessor.HttpContext.User.GetUserId());

            bool likedByUser = _userPostLikeService.PostLikedByUser(currentUId, post.Id);

            if (likedByUser)
            {
                post.Likes--;
                var userLike = _userPostLikeService.GetByUserAndPostId(currentUId, post.Id);
                _userPostLikeService.Delete(userLike);
            }
            else
            {
                post.Likes++;
                var newLike = new UserPostLike
                {
                    UserId = currentUId,
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
            if (postDetails == null) return View("Error");
            return View(postDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var postDetails = await _postService.GetByIdAsync(id);
            if (postDetails == null) return View("Error");

            _postService.Delete(postDetails);
            return RedirectToAction("Index");

        }
    }
}
