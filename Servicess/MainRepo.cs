using Gallrey.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Gallrey.Servicess.Base;
using Azure.Core;
using Gallrey.Models;
using System.Linq;
using System.Drawing;

namespace Gallrey.Repos
{
	public class MainRepo<T> : IRepo<T> where T : class
	{
		private AppDbContext _context;

		public MainRepo(AppDbContext context)
		{
			_context = context;
		}

		public void Create(T std)
		{
			_context.Set<T>().Add(std);
			_context?.SaveChanges();
		}

		public void Update(T std)
		{

			if (std != null)
			{
				_context.Set<T>().Update(std);
				_context.SaveChanges();
			}
		}

		public void Delete(T std)
		{

			_context.Set<T>().Remove(std);
			_context.SaveChanges();
		}

		public T? Get(int Id)
		{
			return _context.Set<T>().Find(Id);

		}

		public IQueryable<T> GetAll()
		{
			return _context.Set<T>().AsNoTracking().AsQueryable();
		}	 

		public IQueryable<T> Paginat(int pageSize = 3,int pageNumber = 1)
		{
			var data = _context.Set<T>().Skip(pageSize * pageNumber - pageSize).Take(pageSize).AsQueryable();
			return data;
		}

		public MemoryStream ValidateIFormImage(IFormFileCollection Images, short SizeInMB, params string[] AllowedExtintions)
		{
			var Files = Images;

			if (!Files.Any())
			{
				throw new Exception("Please Slecte Image");

			}

			var Image = Files.FirstOrDefault();
			//var AllowedExte = new List<string>() { ".JPG", ".JPEG", ".PNG" };
			var ImgExte = Path.GetExtension(Image.FileName);
			if (!AllowedExtintions.Contains(Path.GetExtension(ImgExte).ToLower()))
			{
				throw new Exception($"Onley {string.Join(" ,",AllowedExtintions)} Extentions Are Allowed!");

			}
			 int AllowedSize = 1024 * 1024 * SizeInMB; // 2MB Are Allowed
			if (Image.Length > AllowedSize)
			{
				throw new Exception($"Tow Big Image Size {SizeInMB}MB Are Allowed!");

			}

			
			using var stream = new MemoryStream();

			Image.CopyTo(stream);
			return stream;
		}

		
	}
}