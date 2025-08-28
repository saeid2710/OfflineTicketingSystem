
using Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shared.Dto
{
	public class CreateUserDto
	{
		public string FullName { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string PasswordHash { get; set; }
		[Required]
		public UserRole Role { get; set; }
	}
}
