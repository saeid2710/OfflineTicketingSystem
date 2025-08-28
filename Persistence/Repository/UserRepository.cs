
using Contracts.UserContract;
using Entities.Users;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repository
{
	public class UserRepository:IUser
	{
		private readonly AppDbContext _db;
		public UserRepository(AppDbContext db) => _db = db;

		public async Task<User?> GetUserByIdAsync(Guid userId)
		{
			return await _db.Users.SingleOrDefaultAsync(x => x.Id == userId);
		}

		public async Task<User?> GetUserByEmailAsync(string email)
		{
			return await _db.Users.SingleOrDefaultAsync(x => x.Email==email && !x.IsDelete);
		}

		public async Task<IEnumerable<User>> GetAllUserAsync()
		{
			return await _db.Users.AsNoTracking().ToListAsync();
		}

		public async Task<Guid> AddUserAsync(User user)
		{
			user.IsDelete = false;
			user.CreatedAt=DateTime.Now;
			user.UpdateAt=DateTime.Now;
			await _db.Users.AddAsync(user);
			await _db.SaveChangesAsync();
			return user.Id;
		}

		public async Task<bool> RemoveUserAsync(User user)
		{
			user.UpdateAt=DateTime.Now;
			user.IsDelete=true;
			_db.Update(user);
			await _db.SaveChangesAsync();
			return true;
		}

		public async Task<Guid> UpdateUserAsync(User user)
		{
			user.UpdateAt = DateTime.Now;
			_db.Update(user);
			await _db.SaveChangesAsync();
			return user.Id;
		}
	}
}
