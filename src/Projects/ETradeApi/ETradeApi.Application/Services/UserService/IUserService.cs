﻿using Core.Security.Entities;

namespace ETradeApi.Application.Services.UserService
{
    public interface IUserService
    {
        public Task<User?> GetByEmail(string email);
        public Task<User> GetById(int id);
        public Task<User> Update(User user);
    }
}
