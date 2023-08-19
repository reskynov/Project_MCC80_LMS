﻿using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(LmsDbContext context) : base(context)
        {
        }

        //get user by email
        public User? GetByEmail(string email)
        {
            return _context.Set<User>().SingleOrDefault(e => e.Email.Contains(email));
        }
    }
}
