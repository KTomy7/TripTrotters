using Microsoft.AspNetCore.Mvc;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TripTrotters.Controllers
{
    public class PostController : Controller
    {
        private readonly TripTrottersDbContext _context;

        public PostController(TripTrottersDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Post> posts = _context.Posts.ToList();
            return View(posts);

        }


        public IActionResult Detail(int id)
        {
            Post post = _context.Posts.FirstOrDefault(p => p.Id == id);
            return View(post);
        }
    }
}
