﻿using ShopLibrary.Models;

namespace ShopLibrary.Services.Interfaces
{
    public interface IUsersService
    {
        public bool IsUserRegistered(string userName);

        public User GetUserByUserName(string userName);

        public Task<bool> AddNewUserAsync(User user);

    }
}