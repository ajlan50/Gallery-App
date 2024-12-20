using Gallrey.Models;
using Microsoft.AspNetCore.Mvc;
using Gallrey.Servicess.Base;
using System.Net.NetworkInformation;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Gallrey.Controllers;

public class PhotosController : Controller
{
	private readonly IRepo<Photo> _photos;

	public PhotosController(IRepo<Photo> photo)
	{
		_photos = photo;
	}

	public IActionResult Index(int pageSize = 3 , int pageNumber = 1)
	{
		IQueryable<Photo> photos = _photos.GetAll();
		 ViewBag.AllwoPagination = false;
		if (photos.Count() >= 10)
		{
			ViewBag.AllwoPagination = true;
			int numofpages = (int)Math.Ceiling(photos.Count() / (double)pageSize);

			if (pageNumber == 0)
				pageNumber = numofpages;

			else if (pageNumber > numofpages)
				pageNumber = 1;

			ViewBag.NumOfPages = numofpages;

			ViewBag.CurentPage = pageNumber;

			var Data = _photos.Paginat(pageSize,pageNumber);

			if (ViewBag.NumOfPages > pageNumber)
				return View(Data);

			return View(Data);

		}
		if(photos is null) 
			return View();

		return View(photos);
	}

	public IActionResult Create()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Create(Photo model)
	{
		
		try
		{
			
			using var stream = _photos.ValidateIFormImage(Request.Form.Files,2,".jpg",".png",".jpej");

			model.Image = stream.ToArray();

			if (!ModelState.IsValid)
			{
				return View(model);
			}
		
		}
		catch (Exception ex)
		{
			ModelState.AddModelError($"{ex.GetHashCode()}",$"{ex.Message}");
			return View(model);
		}

		_photos.Create(model);
		TempData["Messege"] = "Created Sucssesfuly";
		return RedirectToAction(nameof(Index));
	}

	public IActionResult Edit(int? id)
	{
		if(id == null)
			return NotFound();

		var photo = _photos.Get(id.Value);
		if (photo == null)
				return BadRequest();

		return View(photo);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Edit(Photo model)
	{
		if (!ModelState.IsValid)
		{
			return View(model);
		}

		if (!Request.Form.Files.Any())
		{
			var selectedphoto = _photos.Get(model.Id);

			// Set Title ---> Mabey there is anther best Solution
			selectedphoto.Title = model.Title;

			TempData["Messege"] = "Updated Sucssesfuly";
			_photos.Update(selectedphoto);
			return RedirectToAction(nameof(Index));
		}
		try
		{
			
				using var stream = _photos.
					ValidateIFormImage(Request.Form.Files, 2,
					".jpg", ".png", ".jpej");
				// Set Image
				model.Image = stream.ToArray();

				TempData["Messege"] = "Updated Sucssesfuly";
				_photos.Update(model);
			    return RedirectToAction(nameof(Index));


		}
		catch (Exception ex)
		{
			ModelState.AddModelError($"{ex.GetHashCode()}", $"{ex.Message}");
			return View(model);
		}

	}

	[HttpDelete]
	public IActionResult Delete(int? id)
	{
		if(id == null)
			return NotFound();

		var ph = _photos.Get(id.Value);

		if (ph == null)
			return BadRequest();

		_photos.Delete(ph);
		return Ok();
	}

}
