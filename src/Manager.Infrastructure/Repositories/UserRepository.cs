using Manager.Domain.Entity;
using Manager.Infrastructure.Context;
using Manager.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ManagerContext _context;

        public UserRepository(ManagerContext context) : base(context)
        {
            _context = context;
        }

        public virtual async Task<User> Createasync(User obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return obj;
        }
        public async Task<List<User>> SearchByEmail(string email)
        {
            var allUsers = await _context.Users.Where(x => x.Email.ToLower().Contains(email.ToLower()))
               .AsNoTracking()
               .ToListAsync();

            return allUsers;
        }

        public async Task<List<User>> SearchByName(string name)
        {
            var allUsers = await _context.Users.Where(x => x.Name.ToLower().Contains(name.ToLower()))
               .AsNoTracking()
               .ToListAsync();

            return allUsers;
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _context.Users.Where(x => x.Email.ToLower() == email.ToLower())
                .AsNoTracking()
                .ToListAsync();

            return user.FirstOrDefault();
        }

        public async Task<User> Get(Guid id)
        {
            var obj = await _context.Set<User>()
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ToListAsync();

            return obj.FirstOrDefault();
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Set<User>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task Remove(Guid id)
        {
            var obj = await Get(id);
            if (obj != null)
            {
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> Update(User obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return obj;
        }


    }
}
