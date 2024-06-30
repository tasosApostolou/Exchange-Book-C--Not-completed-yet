using AutoMapper;
using AutoMapper.QueryableExtensions;
using ExchangeBook.Data;
using ExchangeBook.DTO.UserDTOs;
using ExchangeBook.models;
using ExchangeBook.Security;
using Microsoft.EntityFrameworkCore;
using System;

namespace ExchangeBook.Repositories
{
    public class UserRepositorty(BookExchangeDbContext context, IMapper mapper) : BaseRepository<User>(context), IUserRepository
    {
        private readonly IMapper _mapper = mapper;

        public async Task<User?> GetByUsernameAsync(string username)
        {
            var user = await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User?> GetUserAsync(string username, string password)
        {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return null;
            }
            if (!BCryptUtil.IsValidPassword(password, user.Password!))
            {
                return null;
            }
            return user; 
        }

        public async Task<UserPersonReadOnlyDTO?> GetUserPersonInfoAsync(string username)
        {
            var userPerson = await _context.Users
               .Where(u => u.Username == username && u.UserRole == UserRole.PERSONAL)
               .Include(u => u.Person)
               .ProjectTo<UserPersonReadOnlyDTO>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync();

            return userPerson!;
        }

        public async Task<UserStoreReadOnlyDTO?> GetUserStoreInfoAsync(string username)
        {
            var userStore = await _context.Users
               .Where(u => u.Username == username && u.UserRole == UserRole.STORE)
               .Include(u => u.Store)
               .ProjectTo<UserStoreReadOnlyDTO>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync();

            return userStore!;
        }
            public async Task<User?> UpdateUserAsync(int userId, User? user)
        {
            var existingUser= await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user.Id != userId && userId != user.Id)
            {
                return null;

            }
            if (existingUser != null)
            {
                //Μου εβγαζε ενα error οτι δεν μπορει να γινει attach με new instance, να θυμηθω να το αλλαξω!
                _context.Entry(existingUser).State = EntityState.Detached;
            }
            // Attach the new entity instance and set its state to Modified
            _context.Users.Attach(user);
            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return user;
        }
      
    }
}
