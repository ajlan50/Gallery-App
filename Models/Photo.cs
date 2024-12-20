using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gallrey.Models
{
	public class Photo
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Title { get; set; } = default!;

		[Display(Name ="Image")]
		//[Required]
		public byte[]? Image { get; set; }

		//public string ImageName { get; set; }

	}
}
