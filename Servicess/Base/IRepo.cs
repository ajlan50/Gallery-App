using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Gallrey.Servicess.Base
{
	public interface IRepo<T>
	{
		void Create(T std);

		void Update(T stdId);

		void Delete(T std);

		T? Get(int stdId);

		IQueryable<T> GetAll();

		MemoryStream ValidateIFormImage(IFormFileCollection Images,short SizeInByte, params string[] AllowedExtintions);

		IQueryable<T> Paginat(int pageSize = 3, int pageNumber = 1);


	}
}
