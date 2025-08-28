
namespace Shared.Dto
{
	public class BaseDto
	{
		public Guid Id { get; set; } 
		public DateTime CreatedAt { get; set; }
		public DateTime UpdateAt { get; set; }
		public bool IsDelete { get; set; }
	}
}
