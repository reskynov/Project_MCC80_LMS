﻿using API.Models;

namespace API.Contracts
{
    public interface IUserRepository : IGeneralRepository<User>
    {
        User? GetByEmail(string email);
        bool IsNotExist(string value);
        bool IsDataUnique(string data);
    }
}
