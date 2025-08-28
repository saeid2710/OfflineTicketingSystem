
using System.Security.Cryptography;
using System.Text;

namespace Shared.Utils
{
	public static class PasswordHelper
	{
		public static string HashPassword(this string password)
		{
			using var sha256 = SHA256.Create();
			var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
			return Convert.ToBase64String(hashedBytes);
		}

		public static bool VerifyPassword(this string hashedPassword, string password)
		{
			return HashPassword(password) == hashedPassword;
		}
	}
}
