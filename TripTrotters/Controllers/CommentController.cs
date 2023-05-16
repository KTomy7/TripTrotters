using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using TripTrotters.Services;
using TripTrotters.Services.Abstractions;
using TripTrotters.ViewModels;

namespace TripTrotters.Controllers
{
    public class CommentController : Controller
    {
        private readonly TripTrottersDbContext _context;
        private readonly ICommentService _commentService;
        private readonly IUserCommentLikeService _userCommentLikeService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentController(ICommentService commentService, IHttpContextAccessor httpContextAccessor, IUserCommentLikeService userCommentLikeService) { 
            _commentService = commentService;
            _userCommentLikeService = userCommentLikeService;
            _httpContextAccessor = httpContextAccessor;
        }



        [HttpPost]
        public async Task<IActionResult> Create(CommentViewModel commentViewModel)
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            commentViewModel.UserId = int.Parse(curUserId);
            if (!ModelState.IsValid)
            {
                return View(commentViewModel);
            }

            Comment comment = new Comment
            {
                Like = 0,
                Text = commentViewModel.Text,
                Date = DateTime.Now,
                UserId = commentViewModel.UserId,
                PostId = commentViewModel.PostId,
            };
            _commentService.Add(comment);


            return RedirectToAction("Index", "Post");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CommentViewModel commentViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit comment");
                return View("Edit", commentViewModel);

            }
            Comment comment = await _commentService.GetByIdAsync(commentViewModel.Id);
            if (comment == null)
                return View("Error");

            comment.Text = commentViewModel.Text;

            _commentService.Update(comment);
            return RedirectToAction("Index", "Post");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLike(int id, EditPostViewModel editPostViewModel)
        {
            Comment comment = await _commentService.GetByIdAsync(editPostViewModel.Id);
            var currentUId = int.Parse(_httpContextAccessor.HttpContext.User.GetUserId());

            bool likedByUser = _userCommentLikeService.CommentLikedByUser(currentUId, comment.Id);

            if (likedByUser)
            {
                comment.Like--;
                var userLike = _userCommentLikeService.GetByUserAndCommentId(currentUId, comment.Id);
                _userCommentLikeService.Delete(userLike);
            }
            else
            {
                comment.Like++;
                var newLike = new UserCommentLike
                {
                    UserId = currentUId,
                    CommentId = comment.Id,
                };
                _userCommentLikeService.Add(newLike);
            }

            _commentService.Update(comment);

            return RedirectToAction("Index", "Post");
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null) return View("Error");

            _commentService.Delete(comment);
            return RedirectToAction("Index", "Post");

        }
    }
}
