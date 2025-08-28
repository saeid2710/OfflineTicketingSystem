
namespace Entities.Base
{
	public class Entity
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime CreatedAt { get; set; }
		public DateTime UpdateAt { get; set; }
		public bool IsDelete { get; set; }
	}
}
