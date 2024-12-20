using Gallrey.Models;
using Microsoft.EntityFrameworkCore;

namespace Gallrey.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<Photo> Photos { get; set; }
		public AppDbContext(DbContextOptions options) : base(options)
		{
		}


	}
}
