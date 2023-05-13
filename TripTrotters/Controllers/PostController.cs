using Microsoft.AspNetCore.Mvc;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using TripTrotters.Services.Abstractions;
using TripTrotters.ViewModels;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace TripTrotters.Controllers
{
    public class PostController : Controller
    {
        private readonly TripTrottersDbContext _context;
        private readonly IPostService _postService;
        private readonly IApartmentService _apartmentService;
        private readonly ICommentService _commentService;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public PostController(IPostService postService, IApartmentService apartmentService, ICommentService commentService, IHttpContextAccessor httpContextAccessor)
        {

            _postService = postService;
            _apartmentService = apartmentService;
            _commentService = commentService;
            _httpContextAccessor = httpContextAccessor;

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
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var postViewModel = new CreatePostViewModel { UserId = int.Parse(curUserId) };
            return View(postViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel postViewModel)
        {


            if (!ModelState.IsValid)
            {
                return View(postViewModel);
            }

            Post post = new Post
            {
                Title = postViewModel.Title,
                Date = DateTime.Now,
                Likes = 0,
                //budget?
                Description = postViewModel.Description,
                ApartmentId = postViewModel.ApartmentId,
                UserId = postViewModel.UserId,
            };

            _postService.Add(post);

            return RedirectToAction("Index");
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
                UserId = 1,

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
            if(post == null)
                { return View("Error"); }
            post.Likes++;
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
