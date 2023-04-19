using Microsoft.AspNetCore.Mvc;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TripTrotters.Services.Abstractions;
using TripTrotters.ViewModels;
using System.Net;
using TripTrotters.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace TripTrotters.Controllers
{
    public class PostController : Controller
    {
        private readonly TripTrottersDbContext _context;
        private readonly IPostService _postService;
        private readonly IApartmentService _apartmentService;



        public PostController(IPostService postService, IApartmentService apartmentService)
        {

            _postService = postService;
            _apartmentService = apartmentService;

        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Post> posts = await _postService.GetAll();
            return View(posts);

        }

        public async Task<IActionResult> Detail(int id)
        {
            Post post = await _postService.GetByIdAsync(id);
            return View(post);
        }


        public IActionResult Create()
        {
            return View();
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
                Description = postViewModel.Description,
                ApartmentId = postViewModel.ApartmentId,
                UserId = 1,


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
