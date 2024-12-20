using Gallrey.Models;
using Gallrey.Servicess.Base;
using Microsoft.AspNetCore.Mvc;

namespace Gallrey.Controllers
{
	public class HomeController : Controller
	{
		private IRepo<Photo> _photos;

		public HomeController(IRepo<Photo> photos)
		{
			_photos = photos;
		}

		public IActionResult Index()
		{
			IQueryable<Photo> photos = _photos.GetAll();
			if (photos is null)
				return View();

			return View(photos);
		}
	}
}
