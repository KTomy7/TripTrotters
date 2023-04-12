using Microsoft.AspNetCore.Mvc;
using TripTrotters.DataAccess;
using TripTrotters.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TripTrotters.Services.Abstractions;

namespace TripTrotters.Controllers
{
	public class PostController : Controller
	{
		private readonly TripTrottersDbContext _context;
		private readonly IPostService _postService;

		public PostController(TripTrottersDbContext context, IPostService postService)
		{

			_postService = postService;
		}
		public async Task<IActionResult> Index()
		{
			IEnumerable<Post> posts = await _postService.GetAllbyApartment(22);
			return View(posts);

		}

		public async Task<IActionResult> Detail(int id)
		{
			Post post = await _postService.GetByIdAsync(id);
			return View(post);
		}
	}
}
